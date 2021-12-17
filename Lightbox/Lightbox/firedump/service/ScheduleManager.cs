using Firedump.models.configuration.dynamicconfig;
using Firedump.models.dump;
using Firedump.models.location;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firedump.models.configuration.jsonconfig;

namespace Firedump.service
{
    public class ScheduleManager
    {

        private MySqlDumpAdapter mysqldumpAdapter;
        private LocationAdapterManager locationAdapterManager;
        private DumpResultSet result;
        private Lightbox.LightboxdbDataSet.schedulesRow schedulesRow;
        private Lightbox.LightboxdbDataSet.sql_serversRow server;


        public ScheduleManager()
        {
        }

        internal void StopCurrentJob()
        {
            //cancel mysql dump process
            //cancel location/upload process
        }

        internal void setSchedule(Lightbox.LightboxdbDataSet.schedulesRow schedulesRow)
        {
            this.schedulesRow = schedulesRow;
        }

       
        internal void Start()
        {
            List<string> tables =  sqlbox.commons.StringUtils.extractTableListFromString(schedulesRow.tables);
            string database = schedulesRow.database;
            Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter serveradapter = new Lightbox.LightboxdbDataSetTableAdapters.sql_serversTableAdapter();
            Lightbox.LightboxdbDataSet.sql_serversDataTable servertable = new Lightbox.LightboxdbDataSet.sql_serversDataTable();
            serveradapter.FillById(servertable, schedulesRow.server_id);

           
            if (servertable?.Count > 0)
            {
                //File.AppendAllText(@"servicelog.txt", "COUNT:"+servertable.Count+",");
                server = servertable[0];
            }                
            else
            {
                //File.AppendAllText(@"servicelog.txt", "COUNT:" + "EMPTY" + ",");
                return;
            }

            DumpCredentialsConfig dumpConfig = new DumpCredentialsConfig();
            dumpConfig.database = database;
            dumpConfig.username = server.username;
            dumpConfig.password = server.password;
            dumpConfig.host = server.host;
            dumpConfig.port = (int)server.port;
            if (tables.Count > 0)
                dumpConfig.excludeTables = tables.ToArray();

            mysqldumpAdapter = new MySqlDumpAdapter();
            mysqldumpAdapter.Cancelled += OnCancelled;
            mysqldumpAdapter.Completed += OnCompleted;
            mysqldumpAdapter.CompressProgress += oncompressprogress;
            mysqldumpAdapter.CompressStart += oncompstart;
            mysqldumpAdapter.Error += onerror;
            mysqldumpAdapter.InitDumpTables += oninitdumptables;
            mysqldumpAdapter.Progress += onprogress;
            mysqldumpAdapter.TableRowCount += ontablerowcount;
            mysqldumpAdapter.TableStartDump += ontablestartdump;
            
            //File.AppendAllText(@"servicelog.txt", "STARTDUMP");
            mysqldumpAdapter.startDump(dumpConfig);  
        }

        private void ontablestartdump(string table)
        {
        }

        private void ontablerowcount(int rowcount)
        {
        }

        private void onprogress(string progress)
        {
        }

        private void oninitdumptables(List<string> tables)
        {
        }

        private void onerror(int error)
        {
        }

        private void oncompstart()
        {
        }

        private void oncompressprogress(int progress)
        {
        }

        private void OnCompleted(DumpResultSet resultSet)
        {
            if (resultSet != null)
            {
                if(resultSet.wasSuccessful)
                {
                    List<int> locations = new List<int>();
                    //get schedule_save_location data table by schedule ID
                    Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter savelocAdapter = new Lightbox.LightboxdbDataSetTableAdapters.schedule_save_locationsTableAdapter();
                    Lightbox.LightboxdbDataSet.schedule_save_locationsDataTable saveloctable = new Lightbox.LightboxdbDataSet.schedule_save_locationsDataTable();
                    savelocAdapter.FillByScheduleId(saveloctable,schedulesRow.id);
                    
                    if(saveloctable.Count > 0)
                    {
                        //File.AppendAllText(@"servicelog.txt", "saveloctable.Count > 0");
                        //now get backuplocations by backuplocationID
                        try {
                            Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter backupAdapter = new Lightbox.LightboxdbDataSetTableAdapters.backup_locationsTableAdapter();
                            Lightbox.LightboxdbDataSet.backup_locationsDataTable backuptable = new Lightbox.LightboxdbDataSet.backup_locationsDataTable();
                            for (int i = 0; i < saveloctable.Count; i++)
                            {
                                Lightbox.LightboxdbDataSet.backup_locationsDataTable temp = backupAdapter.GetDataByID(saveloctable[i].backup_location_id);
                                locations.Add((int)temp[0].id);
                                //File.AppendAllText(@"servicelog.txt", "Addbackup_locationsRow " + temp[0].id + temp[0].name);
                            }
                            
                            locationAdapterManager = new LocationAdapterManager(locations, resultSet.fileAbsPath, null);
                            locationAdapterManager.SaveInit += onSaveInitHandler;
                            locationAdapterManager.InnerSaveInit += onInnerSaveInitHandler;
                            locationAdapterManager.LocationProgress += onLocationProgressHandler;
                            locationAdapterManager.SaveProgress += setSaveProgressHandler;
                            locationAdapterManager.SaveComplete += onSaveCompleteHandler;
                            locationAdapterManager.SaveError += onSaveErrorHandler;
                            locationAdapterManager.setProgress();

                            //File.AppendAllText(@"servicelog.txt", "locationAdapterManager.startSave");
                            locationAdapterManager.startSave();
                        }catch(Exception ex)
                        {
                            //File.AppendAllText(@"servicelog.txt", "Exception "+ex.ToString());
                        }
                    }
                    
                }
            }

        }



        private void onSaveErrorHandler(string message)
        {
            
        }

        private void onSaveCompleteHandler(List<LocationResultSet> results)
        {
            File.AppendAllText(@"servicelog.txt", "onSaveCompleteHandler");
        }

        private void setSaveProgressHandler(int progress, int speed)
        {
           
        }

        private void onLocationProgressHandler(int progress, int speed)
        {
            
        }

        private void onInnerSaveInitHandler(string location_name, int location_type)
        {
           
        }

        private void onSaveInitHandler(int maxprogress)
        {
            File.AppendAllText(@"servicelog.txt", "onSaveInitHandler");
        }

        private void OnCancelled()
        {

        }

        
    }
}
