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
    public class GetItemPriceByPriceListController : ApiController
    {
        public DataSet Get(string PriceList, string ItemNo)
        {
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            var ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT CURRENCY, ITEMNO, PRICELIST, [DESC], PRICEDECS, MARKUPCOST, MARKUPUNIT, MARKUPCONV, PRICETYPE, PRICEFMT, PRCNTLVL1, PRCNTLVL2, PRCNTLVL3, PRCNTLVL4, PRCNTLVL5, PRICEBASE, PRICEQTY1, PRICEQTY2, PRICEQTY3, PRICEQTY4, PRICEQTY5, MARKUP, LASTMKPDT, PREVMKPCST, LASTEXCHDT, PREVEXCHRT, ROUNDMETHD, ROUNDAMT, AMOUNTLVL1, AMOUNTLVL2, AMOUNTLVL3, AMOUNTLVL4, AMOUNTLVL5, PRICEBY, MRKUPWUNIT, PRICEWGHT1, PRICEWGHT2, PRICEWGHT3, PRICEWGHT4, PRICEWGHT5, CPRICETYPE, CCHECK, CCHECKBASE, CBASE, DEFBUNIT, DEFBWUNIT, DEFSUNIT, DEFSWUNIT, BPRICETYPE, BDEFUSING, BLOCATION, BBASE, BPERCENT, BAMOUNT, BRATETYPE, BRATEDATE, BEXCHRATE, BRATEOP, BRATEOVRRD, SPRICETYPE, SDEFUSING, SLOCATION, SBASE, SPERCENT, SAMOUNT, SRATETYPE, SRATEDATE,  SEXCHRATE, SRATEOP, SRATEOVRRD, PRICESTART, PRICEEND FROM dbo.ICPRIC WHERE(PRICELIST ='" + PriceList + "') AND (ITEMNO = '" + ItemNo + "')";
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
