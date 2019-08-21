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
    public class UsersRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A User method to Insert an object of User type in the Database.
        /// </summary>
        /// <param name="user">Parameter of User type</param>
        /// <returns>Unique user ID assigned while inserting the object in database</returns>
        public int Insert(Users user)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                SqlCommand cmd = new SqlCommand("User_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@UserGuid", guid);
                cmd.Parameters.Add("@UserId", SqlDbType.Int);
                cmd.Parameters["@UserId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                user.UserId = Convert.ToInt32(cmd.Parameters["@UserId"].Value);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
            return user.UserId;
        }
        /// <summary>
        /// A User method to Read all the entries of User type in the Database.
        /// </summary>
        /// <returns>List of all the Users in the database</returns>
        public List<Users> Read()
        {
            List<Users> users = new List<Users>();
            try
            {
                SqlCommand cmd = new SqlCommand("User_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new Users
                    {
                        UserId = Convert.ToInt32(dr["UserId"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        RoleId = Convert.ToInt32(dr["RoleId"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        UserGuid = Convert.ToString(dr["UserGuid"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
            return users;
        }
        /// <summary>
        /// A User method to Read a specific entry of User type in the Database. 
        /// </summary>
        /// <param name="id">User ID assigned when creating the object</param>
        /// <returns>User type object associated with the provided ID</returns>
        public Users ReadById(int id)
        {
            Users user = new Users();
            try
            {
                SqlCommand cmd = new SqlCommand("User_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.UserName = Convert.ToString(dr["UserName"]);
                    user.Email = Convert.ToString(dr["Email"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.RoleId = Convert.ToInt32(dr["RoleId"]);
                    user.Phone = Convert.ToString(dr["Phone"]);
                    user.UserGuid = Convert.ToString(dr["UserGuid"]);
                    user.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    user.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    user.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //user.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    user.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
            return user;
        }
        /// <summary>
        /// A User method to Update a specific entry of User type in the Database.
        /// </summary>
        /// <param name="user">User type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Users user)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("User_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
        /// A User method to Delete a specific entry of User type in the Database.
        /// </summary>
        /// <param name="id">User ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("User_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", id);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
        /// A User method to Activate a specific entry of User type in the Database.
        /// </summary>
        /// <param name="id">The Guid of the user that was send via mail</param>
        /// <returns>True if the Activation was successful and False if it was not</returns>
        public bool ActivateAccount(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("User_ActivateAccount", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserGuid", id);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
        /// A User method to Login a User whose Account is Activated.
        /// </summary>
        /// <param name="email">The E-mail used to Register</param>
        /// <param name="password">The password used in the Registration</param>
        /// <returns>User type object that has the same E-mail and Password</returns>
        public Users Login(string email, string password)
        {
            Users user = new Users();
            try
            {
                SqlCommand cmd = new SqlCommand("User_Login", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.UserName = Convert.ToString(dr["UserName"]);
                    user.Email = Convert.ToString(dr["Email"]);
                    user.Password = Convert.ToString(dr["Password"]);
                    user.RoleId = Convert.ToInt32(dr["RoleId"]);
                    user.Phone = Convert.ToString(dr["Phone"]);
                    user.UserGuid = Convert.ToString(dr["UserGuid"]);
                    user.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    user.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    user.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //user.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    user.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
                    ExceptionUrl = request.Url.AbsoluteUri
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
            return user;
        }
    }
}