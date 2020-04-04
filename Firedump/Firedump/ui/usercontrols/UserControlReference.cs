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
    public class UserControlReference : UserControl
    {
        private IConnectionServerRef parent;

        // this default constructor exists only for the visual studio incapability 
        // and bugs causing missing and error forms render in editor
        public UserControlReference() : base()
        {
        }

        public UserControlReference(IConnectionServerRef parentRef) : this()
        {
            this.parent = parentRef;
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
                parent.GetConnection().ChangeDatabase(database);
                this.OnConnectionChanged(this, new ConChangedEventArgs(parent.GetConnection()));
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

        [Obsolete]
        internal virtual void dataReceived(ITriplet<UserControlReference, UserControlReference, object> triplet)
        {
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
        internal virtual void onConnected() {
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


        public event EventHandler<object> Send;
        internal void OnSend(object sender,object e)
        {
            Send?.Invoke(sender, e);
        }

    }
}
