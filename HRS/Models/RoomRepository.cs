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
    public class RoomRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Room method to Insert an object of Room type in the Database.
        /// </summary>
        /// <param name="room">Room type object</param>
        /// <returns>Unique Room ID assigned while inserting the object in database</returns>
        public int Insert(Room room)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Room_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HotelId", room.HotelId);
                cmd.Parameters.Add("@RoomId", SqlDbType.Int);
                cmd.Parameters["@RoomId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                room.RoomId = Convert.ToInt32(cmd.Parameters["@RoomId"].Value);
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
            return room.RoomId;
        }
        /// <summary>
        /// A Room method to Read all Room type entries in the Database.
        /// </summary>
        /// <returns>List of all the Rooms in the database</returns>
        public List<Room> Read()
        {
            List<Room> rooms = new List<Room>();
            try
            {
                SqlCommand cmd = new SqlCommand("Room_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    rooms.Add(new Room
                    {
                        RoomId = Convert.ToInt32(dr["RoomId"]),
                        HotelId = Convert.ToInt32(dr["HotelId"]),
                        IsAvailable = Convert.ToBoolean(dr["IsAvailable"]),
                        Booked = Convert.ToBoolean(dr["Booked"]),
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
            return rooms;
        }
        /// <summary>
        /// A Room method to Read a specific Room type entry in the Database.
        /// </summary>
        /// <param name="id">Room ID assigned when creating the object</param>
        /// <returns>Room type object associated with the provided ID</returns>
        public Room ReadById(int id)
        {
            Room room = new Room();
            try
            {
                SqlCommand cmd = new SqlCommand("Room_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoomId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    room.RoomId = Convert.ToInt32(dr["RoomId"]);
                    room.HotelId = Convert.ToInt32(dr["HotelId"]);
                    room.IsAvailable = Convert.ToBoolean(dr["IsAvailable"]);
                    room.Booked = Convert.ToBoolean(dr["Booked"]);
                    room.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    room.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //room.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    room.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
            return room;
        }
        /// <summary>
        /// A Room method to Update a specific Room type entry in the Database.
        /// </summary>
        /// <param name="room">Room type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Room room)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Room_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoomId", room.RoomId);
                cmd.Parameters.AddWithValue("@HotelId", room.HotelId);
                cmd.Parameters.AddWithValue("@IsAvailable", room.IsAvailable);
                cmd.Parameters.AddWithValue("@Booked", room.Booked);
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
        /// A Room method to Delete a specific Room type entry in the Database.
        /// </summary>
        /// <param name="id">Room type object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Room_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoomId", id);
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