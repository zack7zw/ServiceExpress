﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace ServiceExpress.DAL
{
    public class DalDbConn
    {
 
        public SqlConnection GetConnection()
        {
            SqlConnection dbConn;
            String connString = ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
        
            dbConn = new SqlConnection(connString);

            return dbConn;
        }
    }

}
