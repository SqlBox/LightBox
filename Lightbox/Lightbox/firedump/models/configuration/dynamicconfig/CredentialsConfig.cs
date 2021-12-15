﻿using System;
using System.IO;
using Newtonsoft.Json;
using Microsoft.CSharp.RuntimeBinder;

namespace Firedump.models.configuration.dynamicconfig
{
    public class CredentialsConfig
    {
        //<!configuration fields section>
        /// <summary>
        /// mysql server ip or domain name
        /// </summary>
        public string host { set; get; }
        /// <summary>
        /// mysql port number
        /// </summary>
        public int port { set; get; } = 3306;
        /// <summary>
        /// user's username
        /// </summary>
        public string username { set; get; }
        /// <summary>
        /// user's password (leave null or empty to attemp a connection without password)
        /// </summary>
        public string password { set; get; }
        //</configuration fields section>

        public CredentialsConfig() { }

       
    }
}
