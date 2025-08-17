using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CCSMDataManagerLibrary
{
    public class SqlDataAccess
    { 
        public static string GetConnectionString(string connectionName, IConfiguration configuration)
        {
            return configuration.GetConnectionString(connectionName);
        }


    }
        
}
