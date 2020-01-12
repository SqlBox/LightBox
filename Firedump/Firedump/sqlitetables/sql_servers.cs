﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Firedump
{
    using Firedump.models;
    using System;
    using System.Collections.Generic;

    public partial class sqlservers
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sqlservers()
        {
            this.schedules = new HashSet<schedules>();
        }

        public sqlservers(string h, int p, string u, string passwd,int db_type) : this()
        {
            this.host = h;
            this.port = p;
            this.username = u;
            this.password = passwd;
            this.db_type = db_type;
        }

        // remove constroctur after refactor
        public sqlservers(string h, int p, string u, string passwd) : this(h,p,u,passwd,0)
        {
        }

        public sqlservers(CredentialsConfig creds) : this(creds.host, creds.port, creds.username, creds.password)
        {
        }

        public long id { get; set; }
        public string name { get; set; }
        public long port { get; set; }
        public string host { get; set; }

        public string username { get; set; }
        public string password { get; set; }

        public int db_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<schedules> schedules { get; set; }

    }
}
