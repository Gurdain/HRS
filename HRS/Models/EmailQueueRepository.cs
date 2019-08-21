using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using Utility;

namespace HRS.Models
{
    public class EmailQueueRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A EmailQueue method to Insert an object of EmailQueue type in the Database.
        /// </summary>
        /// <param name="book">Email type object</param>
        /// <returns>Unique Email ID assigned while inserting the object in database</returns>
        public int Insert(EmailQueue email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Email_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ToAddress", email.ToAddress);
                cmd.Parameters.AddWithValue("@FromAddress", email.FromAddress);
                cmd.Parameters.AddWithValue("@Subject", email.Subject);
                cmd.Parameters.AddWithValue("@Body", email.Body);
                cmd.Parameters.Add("@EmailId", SqlDbType.Int);
                cmd.Parameters["@EmailId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                email.EmailId = Convert.ToInt32(cmd.Parameters["@EmailId"].Value);
                constr.Close();
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return email.EmailId;
        }
        /// <summary>
        /// A EmailQueue method to Read all EmailQueue type entries in the Database.
        /// </summary>
        /// <returns>List of all the Emails in the database</returns>
        public List<EmailQueue> Read()
        {
            List<EmailQueue> emails = new List<EmailQueue>();
            try
            {
                SqlCommand cmd = new SqlCommand("Email_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    emails.Add(new EmailQueue
                    {
                        EmailId = Convert.ToInt32(dr["EmailId"]),
                        ToAddress = Convert.ToString(dr["ToAddress"]),
                        FromAddress = Convert.ToString(dr["FromAddress"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        Body = Convert.ToString(dr["Body"]),
                        Tries = Convert.ToInt32(dr["Tries"]),
                        CreatedOn = Convert.ToDateTime(dr["CreatedOn"]),
                        ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]),
                        //DeletedOn = Convert.ToDateTime(dr["DeletedOn"]),
                        IsDeleted = Convert.ToBoolean(dr["IsDeleted"])
                    });
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return emails;
        }
        /// <summary>
        /// A EmailQueue method to Read a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="id">Email ID assigned when creating the object</param>
        /// <returns>EmailQueue type object associated with the provided ID</returns>
        public EmailQueue ReadById(int id)
        {
            EmailQueue email = new EmailQueue();
            try
            {
                SqlCommand cmd = new SqlCommand("Email_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    email.EmailId = Convert.ToInt32(dr["EmailId"]);
                    email.ToAddress = Convert.ToString(dr["ToAddress"]);
                    email.FromAddress = Convert.ToString(dr["FromAddress"]);
                    email.Subject = Convert.ToString(dr["Subject"]);
                    email.Body = Convert.ToString(dr["Body"]);
                    email.Tries = Convert.ToInt32(dr["Tries"]);
                    email.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    email.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //book.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    email.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return email;
        }
        /// <summary>
        /// A EmailQueue method to Update a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="book">Email type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(EmailQueue email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Email_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", email.EmailId);
                cmd.Parameters.AddWithValue("@ToAddress", email.ToAddress);
                cmd.Parameters.AddWithValue("@FromAddress", email.FromAddress);
                cmd.Parameters.AddWithValue("@Subject", email.Subject);
                cmd.Parameters.AddWithValue("@Body", email.Body);
                cmd.Parameters.AddWithValue("@Tries", email.Tries);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                constr.Close();
                if (r == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {

                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return false;
        }
        /// <summary>
        /// A EmailQueue method to Delete a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="id">Email ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Email_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", id);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                constr.Close();
                if (r == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return false;
        }
        /// <summary>
        /// A EmailQueue method to Undelete a specific EmailQueue type entry in the Database.
        /// </summary>
        /// <param name="id">Email ID assigned when creating the object</param>
        /// <returns>True if the Recovery was successful and False if it was not</returns>
        public bool Undelete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Email_Undelete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", id);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                constr.Close();
                if (r == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MethodBase method = MethodBase.GetCurrentMethod();
                Exceptions exception = new Exceptions
                {
                    ExceptionNumber = ex.HResult.ToString(),
                    ExceptionMessage = ex.Message,
                    ExceptionMethod = method.Name,
                    ExceptionUrl = HttpContext.Current.Request.Url.AbsoluteUri
                };
                int r = exceptionrepo.Exception_Create(exception);
                if (r == 0)
                {
                    exceptionrepo.Exception_InsertInLogFile(exception);
                }
                if (constr.State != ConnectionState.Open)
                {
                    constr.Close();
                    constr.Open();
                }
            }
            return false;
        }
    }
}