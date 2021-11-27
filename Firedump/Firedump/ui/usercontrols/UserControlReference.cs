using Firedump.core.db;
using Firedump.models.events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.usercontrols
{
    public abstract class UserControlReference : UserControl
    {
        private IConnectionServerRef parent;

        // this default constructor exists only for the visual studio incapability 
        // and bugs causing missing and error forms render in editor
        public UserControlReference() : base()
        {
        }


        internal void InitComponent(IConnectionServerRef parentRef)
        {
            this.parent = parentRef;
            this.Init();
        }

        internal virtual void Init() { }


        internal void changeDatabase(string database)
        {
            if (DB.IsConnected(parent.GetConnection()))
            {
                try
                {
                    parent.GetConnection().ChangeDatabase(database);
                    this.OnConnectionChanged(this, new ConChangedEventArgs(parent.GetConnection()));
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        internal DbConnection GetSqlConnection()
        {
            return parent.GetConnection();
        }

        internal sqlservers GetServer()
        {
            return parent.GetServer();
        }

        public MainHome GetMainHome()
        {
            return (MainHome)parent;
        }

        //Event handlers
        public event EventHandler Disconnected;
        internal void OnDisconnected(object t, EventArgs e)
        {
            Disconnected?.Invoke(t, e);
        }
        internal virtual void onConnected()
        {
        }

        internal virtual void onDisconnect()
        {
        }

        public event EventHandler Reconnect;
        internal void OnReconnect(object t, EventArgs e)
        {
            Reconnect?.Invoke(t, e);
        }

        public event EventHandler<ConChangedEventArgs> ConnectionChanged;
        internal void OnConnectionChanged(object t, ConChangedEventArgs e)
        {
            ConnectionChanged?.Invoke(t, e);
        }


    }
}
