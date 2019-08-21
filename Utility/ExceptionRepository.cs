using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web;

namespace Utility
{
    public class ExceptionRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Exceptions method to Insert an object of Exceptions type in the Database.
        /// </summary>
        /// <param name="e">Exceptions type object</param>
        /// <returns>1 if the insertion is successful and 0 if failed</returns>
        public int Exception_Create(Exceptions e)
        {
            int r = 0;
            try
            {
                
                SqlCommand cmd = new SqlCommand("Exception_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExceptionNumber", e.ExceptionNumber);
                cmd.Parameters.AddWithValue("@ExceptionMessage", e.ExceptionMessage);
                cmd.Parameters.AddWithValue("@ExceptionMethod", e.ExceptionMethod);
                cmd.Parameters.AddWithValue("@ExceptionUrl", e.ExceptionUrl);
                constr.Open();
                r = cmd.ExecuteNonQuery();
                constr.Close();
            }
            catch(Exception ex)
            {
                if (r == 0)
                {
                    MethodBase method = MethodBase.GetCurrentMethod();
                    Exceptions exception = new Exceptions
                    {
                        ExceptionNumber = ex.HResult.ToString(),
                        ExceptionMessage = ex.Message,
                        ExceptionMethod = HttpContext.Current.Request.Url.AbsoluteUri
                    };
                    Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return r;
        }
        /// <summary>
        /// A Exceptions method to Read all Exceptions type entries in the Database.
        /// </summary>
        /// <returns>List of all Exceptions in the database</returns>
        public List<Exceptions> Exception_Read()
        {
            List<Exceptions> exceptions = new List<Exceptions>();
            try
            {
                SqlCommand cmd = new SqlCommand("Exception_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    exceptions.Add(new Exceptions
                    {
                        ExceptionId = Convert.ToInt32((object)dr[(string)"ExceptionId"]),
                        ExceptionNumber = Convert.ToString((object)dr[(string)"ExceptionNumber"]),
                        ExceptionMessage = Convert.ToString((object)dr[(string)"ExceptionMessage"]),
                        ExceptionUrl = Convert.ToString((object)dr[(string)"ExceptionUrl"]),
                        ExceptionMethod = Convert.ToString((object)dr[(string)"ExceptionMethod"])
                    }
                        );
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = HttpContext.Current.Request.Url.AbsoluteUri
                };
                Exception_InsertInLogFile(exception);
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return exceptions;
        }
        /// <summary>
        /// A Exceptions method to Read a specific Exceptions type entry in the Database.
        /// </summary>
        /// <param name="id">Exceptions ID assigned when creating the object</param>
        /// <returns>Exceptions type object associated with the provided ID</returns>
        public Exceptions Exception_ReadById(int id)
        {
            Exceptions e = new Exceptions();
            int r = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("Exception_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExceptionId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                r = cmd.ExecuteNonQuery();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    e.ExceptionId = id;
                    e.ExceptionNumber = Convert.ToString(dr["ExceptionNumber"]);
                    e.ExceptionMessage = Convert.ToString(dr["ExceptionMessage"]);
                    e.ExceptionUrl = Convert.ToString(dr["ExceptionUrl"]);
                    e.ExceptionMethod = Convert.ToString(dr["ExceptionMethod"]);
                }
            }
            catch (Exception ex)
            {
                if(r == 0)
                {
                    MethodBase method = MethodBase.GetCurrentMethod();
                    Exceptions exception = new Exceptions
                    {
                        ExceptionNumber = ex.HResult.ToString(),
                        ExceptionMessage = ex.Message,
                        ExceptionMethod = HttpContext.Current.Request.Url.AbsoluteUri
                    };
                    Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return e;
        }
        /// <summary>
        /// A Exceptions method to Insert all exceptions caught while performing Exceptions Operations in the Database.
        /// </summary>
        /// <param name="e">Exceptions type object</param>
        public void Exception_InsertInLogFile(Exceptions e)
        {
            var filePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/"));
            filePath = filePath + "\\LogFile.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine();
                writer.Write("Date = " + DateTime.Now.Date);
                writer.WriteLine();
                writer.Write("Error occured at method = " + e.ExceptionMethod);
                writer.WriteLine();
                writer.Write("Error code for the Exception is = " + e.ExceptionNumber);
                writer.WriteLine();
                writer.Write("Message for the exception = " + e.ExceptionMessage);
                writer.WriteLine();
            }
        }
    }
}