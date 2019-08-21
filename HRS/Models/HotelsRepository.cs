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
    public class HotelsRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Hotel method to Insert an object of Hotels type in the Database.
        /// </summary>
        /// <param name="hotel">Hotel type object</param>
        /// <returns>Unique Hotel ID assigned while inserting the object in database</returns>
        public int Insert(Hotels hotel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotels_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelName", hotel.HotelName);
                cmd.Parameters.AddWithValue("@Address", hotel.Address);
                cmd.Parameters.AddWithValue("@City", hotel.City);
                cmd.Parameters.AddWithValue("@Locality", hotel.Locality);
                cmd.Parameters.AddWithValue("@Description", hotel.Description);
                cmd.Parameters.AddWithValue("@HotelTypeId", hotel.HotelTypeId);
                cmd.Parameters.AddWithValue("@UserId", hotel.UserId);
                cmd.Parameters.AddWithValue("@Rooms", hotel.Rooms);
                cmd.Parameters.Add("@HotelId", SqlDbType.Int);
                cmd.Parameters["@HotelId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                hotel.HotelId = Convert.ToInt32(cmd.Parameters["@HotelId"].Value);
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
            return hotel.HotelId;
        }
        /// <summary>
        /// A Hotel method to Read all Hotels type entries in the Database.
        /// </summary>
        /// <returns>List of all the Hotels in the database</returns>
        public List<Hotels> Read()
        {
            List<Hotels> hotels = new List<Hotels>();
            try
            {
                SqlCommand cmd = new SqlCommand("Hotels_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    hotels.Add(new Hotels
                    {
                        HotelId = Convert.ToInt32(dr["HotelId"]),
                        HotelName = Convert.ToString(dr["HotelName"]),
                        Address = Convert.ToString(dr["Address"]),
                        City = Convert.ToString(dr["City"]),
                        Locality = Convert.ToString(dr["Locality"]),
                        Description = Convert.ToString(dr["Description"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        Rooms = Convert.ToInt32(dr["Rooms"]),
                        HotelTypeId = Convert.ToInt32(dr["HotelTypeId"]),
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
            return hotels;
        }
        /// <summary>
        /// A Hotel method to Read a specific Hotels type entry in the Database.
        /// </summary>
        /// <param name="id">Hotel ID assigned when creating the object</param>
        /// <returns>Hotels type object associated with the provided ID</returns>
        public Hotels ReadById(int id)
        {
            Hotels hotel = new Hotels();
            try
            {
                SqlCommand cmd = new SqlCommand("Hotels_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    hotel.HotelId = Convert.ToInt32(dr["HotelId"]);
                    hotel.HotelName = Convert.ToString(dr["HotelName"]);
                    hotel.Address = Convert.ToString(dr["Address"]);
                    hotel.City = Convert.ToString(dr["City"]);
                    hotel.Locality = Convert.ToString(dr["Locality"]);
                    hotel.Description = Convert.ToString(dr["Description"]);
                    hotel.UserId = Convert.ToInt32(dr["UserId"]);
                    hotel.Rooms = Convert.ToInt32(dr["Rooms"]);
                    hotel.HotelTypeId = Convert.ToInt32(dr["HotelTypeId"]);
                    hotel.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    hotel.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //hotel.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    hotel.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
            return hotel;
        }
        /// <summary>
        /// A Hotel method to Update a specific Hotels type entry in the Database.
        /// </summary>
        /// <param name="hotel">Hotels type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Hotels hotel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotels_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelId", hotel.HotelId);
                cmd.Parameters.AddWithValue("@HotelName", hotel.HotelName);
                cmd.Parameters.AddWithValue("@Address", hotel.Address);
                cmd.Parameters.AddWithValue("@City", hotel.City);
                cmd.Parameters.AddWithValue("@Locality", hotel.Locality);
                cmd.Parameters.AddWithValue("@Description", hotel.Description);
                cmd.Parameters.AddWithValue("@HotelTypeId", hotel.HotelTypeId);
                cmd.Parameters.AddWithValue("@UserId", hotel.UserId);
                cmd.Parameters.AddWithValue("@Rooms", hotel.Rooms);
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
        /// A Hotel method to Delete a specific Hotels type entry in the Database.
        /// </summary>
        /// <param name="id">Hotels type object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Hotels_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelId", id);
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