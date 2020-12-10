using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Collections.Specialized;

namespace WebServiceApp.Controllers
{
    public class FinCategoriesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public DataSet Get(int id, string qrystr)
        {

            //string connection;
            //sAttr = ConfigurationManager.AppSettings.Get("ConnectionString");
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString"); //"Data Source=NUMAN-PC;Initial Catalog=Manifold;Persist Security Info=True;User ID=sa;Password=numan123";
                var ds = new DataSet();
                    using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        SqlParameter[] parameters =
                        {
                            new SqlParameter("@clid", SqlDbType.Int){Value = id},
                        //    new SqlParameter("@Pagesize", SqlDbType.BigInt){Value = pageSize},
                        //    new SqlParameter("@PageNumber", SqlDbType.BigInt){Value = page},
                        };
                        cmd.Parameters.AddRange(parameters);
                        cmd.CommandText = qrystr;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                        return ds;
                    }
                }
        }


       [HttpPost]
        // POST api/values
        public HttpResponseMessage Post(string fincode, string findescript, int coid, string dcode)
        {
           try{
               var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString"); //"Data Source=NUMAN-PC;Initial Catalog=Manifold;Persist Security Info=True;User ID=sa;Password=numan123";
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("@fincode", SqlDbType.VarChar){Value = fincode},
                        new SqlParameter("@findescript", SqlDbType.VarChar){Value = findescript},
                        new SqlParameter("@coid", SqlDbType.Int){Value = coid},
                        new SqlParameter("@dcode", SqlDbType.VarChar){Value = dcode},
                    };

                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandText = "AC_spInsertFinCategory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Request.CreateResponse(HttpStatusCode.OK);
                   }
            }
           }
           catch (Exception ex)
           {
               return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
           }
        }

       [AcceptVerbs("DELETE")]
       public HttpResponseMessage Delete(int coid)
        {
            try
            {
                var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
                var ds = new DataSet();

                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "AC_spDeleteFinCategories";
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        return Request.CreateResponse(HttpStatusCode.OK);
                   }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
