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
    public class GetItemSOHLevelsController : ApiController
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
                    cmd.CommandText = "SELECT  ITEMNO, LOCATION, PICKINGSEQ, ACTIVE, DATEACTIVE, USED, LASTUSED, QTYONHAND, QTYONORDER, QTYSALORDR, QTYOFFSET, QTYSHNOCST, QTYRENOCST, QTYADNOCST, NUMNOCST, TOTALCOST, COSTOFFSET, COSTUNIT, COSTCONV, STDCOST, LASTSTDCST, LASTSTDDAT, LASTSHIPDT, DAYSTOSHIP, UNITSSHIP, SHIPMENTS, LASTRCPTDT, RECENTCOST, COST1, COST2, LASTCOST, QTYCOMMIT, LASTSERALC, LASTLOTALC, LEADTIME, QTYMINREQ FROM dbo.ICILOC WHERE(ITEMNO = '" + ItemNo + "')";
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
