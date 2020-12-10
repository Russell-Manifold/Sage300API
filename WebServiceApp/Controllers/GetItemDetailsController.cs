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
    public class GetItemDetailsController : ApiController
    {
        public DataSet Get(string ItemNo)
        {
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            var ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TOP (1) OP (1) ITEMNO, [DESC], DATELASTMN, INACTIVE, ITEMBRKID, FMTITEMNO, CATEGORY, CNTLACCT, STOCKITEM, STOCKUNIT, DEFPRICLST, UNITWGT, PICKINGSEQ, SERIALNO, COMMODIM, DATEINACTV, SEGMENT1,                          SEGMENT2, SEGMENT3, SEGMENT4, SEGMENT5, SEGMENT6, SEGMENT7, SEGMENT8, SEGMENT9, SEGMENT10, COMMENT1, COMMENT2, COMMENT3, COMMENT4, ALLOWONWEB, KITTING, [VALUES], DEFKITNO,  SELLABLE, WEIGHTUNIT, SERIALMASK, SDIFQTYOK, SVALUES, LOTITEM, LOTMASK, DEFBOMNO, SEASONAL FROM dbo.ICITEM WHERE ITEMNO = '" + ItemNo + "'";
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
