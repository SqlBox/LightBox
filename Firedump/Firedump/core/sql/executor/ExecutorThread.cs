using Firedump.core.db;
using Firedump.core.models.dbinfo;
using Firedump.core.models.events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Firedump.core.sql.executor
{
    public class ExecutorThread : BaseThread
    {
        public bool _Alive = true;
        private DbCommand Command;
        private DbDataReader reader;
        public bool ContinueExecutingNextOnFail;

        public ExecutorThread() : base()
        {
        }

        protected override void run()
        {
            _Alive = true;
            List<string> statements = base.Statements();
            ExecutionQueryEvent eventResult = null;
            for (int i = 0; i < statements.Count; i++)
            {
                bool is_last = false;
                CurrentQuery = statements[i];
                try
                {
                    if (!_Alive)
                    {
                        FireEvent(new ExecutionQueryEvent(Status.CANCELED));
                        break;
                    }
                    //Execute with adapter if is the last statement to fill the dataTable with data
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (Command = new DbCommandFactory(Con(), statements[i]).Create())
                    {
                        if (!_Alive)
                        {
                            break;
                        }
                        using (reader = Command.ExecuteReader())
                        {
                            if (!_Alive)
                            {
                                break;
                            }
                            is_last = statements.Count - 1 == i;
                            bool is_select = Utils.IsShowDataTypeOfCommand(statements[i]);
                            var resultData = new DataTable();
                            if (is_last)
                            {
                                using (DataSet ds = new DataSet() { EnforceConstraints = false, CaseSensitive = false })
                                {
                                    if (is_select && this.QueryParams.Offset > 0)
                                    {
                                        //skip, apply offset
                                        int count = 0;
                                        while (count++ != this.QueryParams.Offset && reader.Read());
                                    }
                                    ds.Tables.Add(resultData);
                                    if(is_select && this.QueryParams.Limit > 0)
                                    {
                                        var schema = reader.GetSchemaTable();
                                        var listCols = new List<DataColumn>();
                                        if(schema != null)
                                        {
                                            foreach(DataRow row in schema.Rows)
                                            {
                                                listCols.Add(AddTableColumn(resultData, (Type)(row["DataType"]), System.Convert.ToString(row["ColumnName"])));
                                            }
                                        }
                                        int count = 0;
                                        while(count++ != this.QueryParams.Limit && reader.Read())
                                        {
                                            var row = resultData.NewRow();
                                            for(int x = 0; x < listCols.Count; x++)
                                            {
                                                row[((DataColumn)listCols[x])] = reader[x];
                                            }
                                            resultData.Rows.Add(row);
                                        }
                                    } else
                                    {
                                        resultData.Load(reader, LoadOption.OverwriteChanges);
                                    }
                                    ds.Tables.Remove(resultData);
                                }
                            }
                            stopWatch.Stop();
                            eventResult = new ExecutionQueryEvent(is_last ? Status.FINISHED : Status.RUNNING) 
                                { query = statements[i], duration = stopWatch.Elapsed, recordsAffected = reader.RecordsAffected, data = resultData };
                            Command?.Cancel();
                        }
                    }
                    FireEvent(eventResult);
                }
                catch (DbException ex)
                {
                    if(this.ContinueExecutingNextOnFail)
                    {
                        ///log
#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                        //status change in future depending on user selection continue or not after error
                        FireEvent(new ExecutionQueryEvent(is_last ? Status.FINISHED : Status.RUNNING) { Ex = ex, query = statements[i] });
                    } else
                    {
                        FireEvent(new ExecutionQueryEvent(Status.ERROR) { Ex = ex, query = statements[i] });
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    if(this.ContinueExecutingNextOnFail)
                    {
                        //better format/handle the sql errors/exceptions
#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                        FireEvent(new ExecutionQueryEvent(is_last ? Status.FINISHED : Status.RUNNING) { Ex = ex, query = statements[i] });
                    } else
                    {
                        FireEvent(new ExecutionQueryEvent(Status.ERROR) { Ex = ex, query = statements[i] });
                    }
                }
                catch(Exception ex)
                {
                    if(this.ContinueExecutingNextOnFail)
                    {

#if DEBUG
                        Console.WriteLine(ex.Message);
#endif
                        FireEvent(new ExecutionQueryEvent(is_last ? Status.FINISHED : Status.RUNNING) { Ex = ex, query = statements[i] });
                    } else
                    {
                        FireEvent(new ExecutionQueryEvent(Status.ERROR) { Ex = ex, query = statements[i] });
                    }
                }
            }
        }


        private void FireEvent(ExecutionQueryEvent e)
        {
            if (this._Alive)
            {
                e.QueryParams = this.QueryParams;
                e.TAG = this.QueryParams.Hash;
                OnStatementExecuted(this, e);
            }
        }


        protected override void cancel()
        {
            this._Alive = false;
            try
            {
                Command?.Cancel();
            }
            catch (Exception ex) { /*log*/}
            finally 
            {
                OnStatementExecuted(this, new ExecutionQueryEvent(Status.CANCELED) { QueryParams = QueryParams, TAG = this.QueryParams.Hash, query = CurrentQuery });
            }
        }


        protected override void _abort()
        {
            this._Alive = false;
            try
            {
                Command?.Cancel();
            }
            catch (Exception ex) 
            {
#if DEBUG
                Console.WriteLine(ex);
#endif         
            }
        }

        private DataColumn AddTableColumn(DataTable table,Type type,String columnName)
        {
            try
            {
                var col = new DataColumn(columnName, type);
                table.Columns.Add(col);
                return col;
            }
            catch (System.Data.DuplicateNameException)
            {
                return AddTableColumn(table,type,columnName+"1");
            }
            return null;
        }

    }
}
