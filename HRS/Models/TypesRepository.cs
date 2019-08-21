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
    public class TypesRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Type method to Insert an object of Types type in the Database.
        /// </summary>
        /// <param name="type">Types type object</param>
        /// <returns>Unique Type ID assigned while inserting the object in database</returns>
        public int Insert(Types type)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Type_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelType", type.HotelType);
                cmd.Parameters.Add("@HotelTypeId", SqlDbType.Int);
                cmd.Parameters["@HotelTypeId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                type.HotelTypeId = Convert.ToInt32(cmd.Parameters["@HotelTypeId"].Value);
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
            return type.HotelTypeId;
        }
        /// <summary>
        /// A Type method to Read all Types type entries in the Database.
        /// </summary>
        /// <returns>A Type method to Read all Types type entries in the Database.</returns>
        public List<Types> Read()
        {
            List<Types> types = new List<Types>();
            try
            {
                SqlCommand cmd = new SqlCommand("Type_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    types.Add(new Types
                    {
                        HotelTypeId = Convert.ToInt32(dr["HotelTypeId"]),
                        HotelType = Convert.ToString(dr["HotelType"]),
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
            return types;
        }
        /// <summary>
        /// A Type method to Read a specific Types type entry in the Database.
        /// </summary>
        /// <param name="id">Type ID assigned when creating the object</param>
        /// <returns>Types type object associated with the provided ID</returns>
        public Types ReadById(int id)
        {
            Types type = new Types();
            try
            {
                SqlCommand cmd = new SqlCommand("Type_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelTypeId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    type.HotelTypeId = Convert.ToInt32(dr["HotelTypeId"]);
                    type.HotelType = Convert.ToString(dr["HotelType"]);
                    type.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    type.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //type.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    type.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
            return type;
        }
        /// <summary>
        /// A Type method to Update a specific Types type entry in the Database.
        /// </summary>
        /// <param name="type">Types type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Types type)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Type_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelType", type.HotelType);
                cmd.Parameters.AddWithValue("@HotelTypeId", type.HotelType);
                constr.Open();
                int r = cmd.ExecuteNonQuery();
                type.HotelTypeId = Convert.ToInt32(cmd.Parameters["@HotelTypeId"].Value);
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
        /// A Type method to Delete a specific Types type entry in the Database.
        /// </summary>
        /// <param name="id">Types type object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Types_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TypeId", id);
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
    }
}