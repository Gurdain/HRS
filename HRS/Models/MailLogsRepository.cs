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
    public class MailLogsRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        //HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A MailLogs method to Insert an object of MailLogs type in the Database.
        /// </summary>
        /// <param name="book">Mail type object</param>
        /// <returns>Unique Mail ID assigned while inserting the object in database</returns>
        public int Insert(MailLogs email)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Mail_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ToAddress", email.ToAddress);
                cmd.Parameters.AddWithValue("@FromAddress", email.FromAdress);
                cmd.Parameters.AddWithValue("@Subject", email.Subject);
                cmd.Parameters.AddWithValue("@Body", email.Body);
                cmd.Parameters.AddWithValue("@EmailStatus", email.EmailStatus);
                cmd.Parameters.Add("@MailId", SqlDbType.Int);
                cmd.Parameters["@MailId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                email.MailId = Convert.ToInt32(cmd.Parameters["@MailId"].Value);
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
            return email.MailId;
        }
        /// <summary>
        /// A MailLogs method to Read all MailLogs type entries in the Database.
        /// </summary>
        /// <returns>List of all the Mails in the database</returns>
        public List<MailLogs> Read()
        {
            List<MailLogs> mails = new List<MailLogs>();
            try
            {
                SqlCommand cmd = new SqlCommand("Mail_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    mails.Add(new MailLogs
                    {
                        MailId = Convert.ToInt32(dr["MailId"]),
                        ToAddress = Convert.ToString(dr["ToAddress"]),
                        FromAdress = Convert.ToString(dr["FromAdress"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        Body = Convert.ToString(dr["Body"]),
                        EmailStatus = Convert.ToBoolean(dr["Status"]),
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
            return mails;
        }
        /// <summary>
        /// A MailLogs method to Read a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="id">Mail ID assigned when creating the object</param>
        /// <returns>MailLogs type object associated with the provided ID</returns>
        public MailLogs ReadById(int id)
        {
            MailLogs mail = new MailLogs();
            try
            {
                SqlCommand cmd = new SqlCommand("Mail_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MailId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    mail.MailId = Convert.ToInt32(dr["MailId"]);
                    mail.ToAddress = Convert.ToString(dr["ToAddress"]);
                    mail.FromAdress = Convert.ToString(dr["FromAdress"]);
                    mail.Subject = Convert.ToString(dr["Subject"]);
                    mail.Body = Convert.ToString(dr["Body"]);
                    mail.EmailStatus = Convert.ToBoolean(dr["Status"]);
                    mail.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    mail.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //book.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    mail.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
            return mail;
        }
        /// <summary>
        /// A MailLogs method to Update a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="book">Mail type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(MailLogs mail)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Mail_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MailId", mail.MailId);
                cmd.Parameters.AddWithValue("@ToAddress", mail.ToAddress);
                cmd.Parameters.AddWithValue("@ToAddress", mail.FromAdress);
                cmd.Parameters.AddWithValue("@Subject", mail.Subject);
                cmd.Parameters.AddWithValue("@Body", mail.Body);
                cmd.Parameters.AddWithValue("@Tries", mail.EmailStatus);
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
        /// A MailLogs method to Delete a specific MailLogs type entry in the Database.
        /// </summary>
        /// <param name="id">Mail ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Mail_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MailId", id);
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