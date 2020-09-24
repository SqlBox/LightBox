using Firedump.core.db;
using Firedump.core.sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    public partial class OptionsForm : Form
    {
        private DbConnection con;
        public OptionsForm(System.Data.Common.DbConnection con)
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.con = con;
        }


        private void OptionsForm_Load(object sender, EventArgs e)
        {
            loadGenericSettings();
            loadMySqlSettings();
            loadSqliteSettings();
            if(!DB.IsConnected(con) || !(Utils.GetDbTypeEnum(con) == sqlbox.commons.DbType.SQLITE))
            {
                groupBoxPragmaEditor.Enabled = false;
            }
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveGenericSettings();
            saveMySqlSettings();
            saveSqliteSettings();
        }

        private void loadGenericSettings()
        {
            checkBoxAutoCommit.Checked = Properties.Settings.Default.option_general_autocommit;
        }

        private void saveGenericSettings()
        {
            Properties.Settings.Default.option_general_autocommit = checkBoxAutoCommit.Checked;
            Properties.Settings.Default.Save();
        }

        private void loadMySqlSettings()
        {
            numericUpDownMySqlReadTimeout.Value = Properties.Settings.Default.option_mysql_conreadtimeout;
            numericUpDownMySqlTimeout.Value = Properties.Settings.Default.option_mysql_contimeout;
            numericUpDownMySqlKeepAlive.Value = Properties.Settings.Default.option_mysql_keepalive;
        }

        private void saveMySqlSettings()
        {
            Properties.Settings.Default.option_mysql_conreadtimeout = Decimal.ToInt32(numericUpDownMySqlReadTimeout.Value);
            Properties.Settings.Default.option_mysql_contimeout = Decimal.ToInt32(numericUpDownMySqlTimeout.Value);
            Properties.Settings.Default.option_mysql_keepalive = Decimal.ToInt32(numericUpDownMySqlKeepAlive.Value);
            Properties.Settings.Default.Save();
        }

        private void loadSqliteSettings()
        {
            checkBoxBeginTransAfterCommit.Checked   =   Properties.Settings.Default.option_sqlite_begintranscommit;
            checkBoxBeginTransAfterDbOpens.Checked = Properties.Settings.Default.option_sqlite_begintransdbopen;
            fastColoredTextBoxSqlAfterDbOpens.Text = Properties.Settings.Default.option_sqlite_sqlafteropen;
            checkBoxForeignKeys.Checked = Properties.Settings.Default.option_sqlite_foreign_keys;
            //load pragma
            if (DB.IsConnected(con) && Utils.GetDbTypeEnum(con) == sqlbox.commons.DbType.SQLITE)
            {
                var intList = DbDataHelper.getIntData(con, "PRAGMA auto_vacuum");
                if(intList.Count > 0)
                {
                    int val = intList[0];
                    //0 none,1 full, 2 INCREMENTAL
                    switch (val)
                    {
                        case 0:
                            comboBoxAutoVacuum.SelectedItem = "NONE";
                            break;
                        case 1:
                            comboBoxAutoVacuum.SelectedItem = "FULL";
                            break;
                        case 2:
                            comboBoxAutoVacuum.SelectedItem = "INCREMENTAL";
                            break;
                    }
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA automatic_index");
                if(intList.Count > 0)
                {
                    checkBoxAutoIndex.Checked = intList[0] == 0 ? false : true;
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA cell_size_check");
                if(intList.Count > 0)
                {
                    checkBoxCellSizeCheck.Checked = intList[0] == 0 ? false : true;
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA checkpoint_fullfsync");
                if(intList.Count > 0)
                {
                    checkBoxCheckFullSync.Checked = intList[0] == 0 ? false : true;
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA defer_foreign_keys");
                if(intList.Count > 0)
                {
                    checkBoxDeferForeignKeys.Checked = intList[0] == 0 ? false : true;
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA ignore_check_constraints");
                if(intList.Count > 0)
                {
                    checkBoxIgnoreCheckConstraints.Checked = intList[0] == 0 ? false : true;
                }
                var stringList = DbDataHelper.getStringData(con, "PRAGMA journal_mode");
                if(stringList.Count > 0)
                {
                    comboBoxJournalMode.SelectedItem = stringList[0].ToUpper();
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA journal_size_limit");
                if(intList.Count > 0)
                {
                    numericUpDownJournalSizeLimit.Value = intList[0];
                }
                stringList = DbDataHelper.getStringData(con, "PRAGMA locking_mode");
                if (stringList.Count > 0)
                {
                    comboBoxLockMode.SelectedItem = stringList[0].ToUpper();
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA max_page_count");
                if(intList.Count > 0)
                {
                    numericMaxPageCount.Value = intList[0];
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA page_size");
                if (intList.Count > 0)
                {
                    numericPageSize.Value = intList[0];
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA recursive_triggers");
                if (intList.Count > 0)
                {
                    checkBoxRecursiveTriggers.Checked = intList[0] == 0 ? false : true;
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA secure_delete");
                if (intList.Count > 0)
                {
                    checkBoxSecureDelete.Checked = intList[0] == 0 ? false : true;
                }
                 intList = DbDataHelper.getIntData(con, "PRAGMA synchronous");
                if (intList.Count > 0)
                {
                    int val = intList[0];
                    //0 OFF,1 NORMAL, 2 FULL, 3 EXTRA
                    switch (val)
                    {
                        case 0:
                            comboBoxSynchronous.SelectedItem = "OFF";
                            break;
                        case 1:
                            comboBoxSynchronous.SelectedItem = "NORMAL";
                            break;
                        case 2:
                            comboBoxSynchronous.SelectedItem = "FULL";
                            break;
                        case 3:
                            comboBoxSynchronous.SelectedItem = "EXTRA";
                            break;
                    }
                }
                intList = DbDataHelper.getIntData(con, "PRAGMA user_version");
                if (intList.Count > 0)
                {
                    numericUpDownUserVersion.Value = intList[0];
                }
            }
        }

        private void saveSqliteSettings()
        {
            Properties.Settings.Default.option_sqlite_begintranscommit = checkBoxBeginTransAfterCommit.Checked;
            Properties.Settings.Default.option_sqlite_begintransdbopen = checkBoxBeginTransAfterDbOpens.Checked;
            Properties.Settings.Default.option_sqlite_sqlafteropen = fastColoredTextBoxSqlAfterDbOpens.Text;

            Properties.Settings.Default.Save();
        }

        private void buttonSavePragma_Click(object sender, EventArgs e)
        {
            if (DB.IsConnected(con) && Utils.GetDbTypeEnum(con) == sqlbox.commons.DbType.SQLITE)
            {
                var dialog = new CommitRollbackForm();
                dialog.ShowDialog();
                if(dialog.action != null)
                {
                    if(dialog.action == DbData.COMMIT)
                    {
                        DB.Commit(con);
                    } else if(dialog.action == DbData.ROLLBACK)
                    {
                        DB.Rollback(con);
                    }
                    Properties.Settings.Default.option_sqlite_foreign_keys = checkBoxForeignKeys.Checked;
                    //0 none,1 full, 2 INCREMENTAL
                    if (comboBoxAutoVacuum.SelectedItem == "NONE")
                    {
                        DbDataHelper.executeNonQuery(con, "pragma auto_vacuum = 0");
                    }
                    else if (comboBoxAutoVacuum.SelectedItem == "FULL")
                    {
                        DbDataHelper.executeNonQuery(con, "pragma auto_vacuum = 1");
                    }
                    else if (comboBoxAutoVacuum.SelectedItem == "INCREMENTAL")
                    {
                        DbDataHelper.executeNonQuery(con, "pragma auto_vacuum = 2");
                    }
                    if (checkBoxRunVacuum.Checked)
                    {
                        DbDataHelper.executeNonQuery(con, "vacuum");
                    }
                }
            }
        }

    }
}
