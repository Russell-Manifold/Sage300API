using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace WebServiceApp.Controllers
{
    public class GetCustomerController : ApiController
    {
        public DataSet Get(string CustID)
        {
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            var ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select TOP (1)  IDCUST, IDGRP, IDNATACCT, SWHOLD, DATESTART, CODEDAB, CODEDABRTG, DATEDAB, NAMECUST, TEXTSTRE1, TEXTSTRE2, TEXTSTRE3, TEXTSTRE4, NAMECITY, CODESTTE,  CODEPSTL, CODECTRY, NAMECTAC, TEXTPHON1, TEXTPHON2, CODETERR, PRICLIST, CUSTTYPE, EMAIL1, EMAIL2, WEBSITE, BILLMETHOD, PAYMCODE, FOB, DELMETHOD, PRIMSHIPTO, CTACPHONE, CTACFAX, [VALUES], LOCATION, CATEGORY FROM dbo.ARCUS WHERE IDCUST = '" + CustID + "'";
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    return ds;
                }
            }
        }
     }
}
