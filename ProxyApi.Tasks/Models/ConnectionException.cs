﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyApi.Tasks.Models
{
    public class ConnectionException : Exception
    {



        private string uri;
        public ConnectionException(string uri)
        {
            this.uri = uri;
        }

        public override string Message
        {
            get
            {
                return "ProxyApi: Could not connect to remote server - " + uri;
            }
        }
    }

    public class ConfigFileNotFoundException : Exception
    {
        public override string StackTrace
        {
            get
            {
                return String.Empty;
            }
        }

       

        public override string Message
        {
            get
            {
                return "ProxyApi: Configuration file not found. Please make sure 'ProxyApiConfig.json' exists within your project root.";
            }
        }
    }
}
