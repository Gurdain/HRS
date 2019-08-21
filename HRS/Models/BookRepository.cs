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
    public class BookRepository
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        ExceptionRepository exceptionrepo = new ExceptionRepository();
        HttpRequest request = HttpContext.Current.Request;
        /// <summary>
        /// A Book method to Insert an object of Book type in the Database.
        /// </summary>
        /// <param name="book">Book type object</param>
        /// <returns>Unique Booking ID assigned while inserting the object in database</returns>
        public int Insert(Book book)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Book_Insert", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", book.UserId);
                cmd.Parameters.AddWithValue("@HotelId", book.HotelId);
                cmd.Parameters.AddWithValue("@RoomId", book.RoomId);
                cmd.Parameters.Add("@BookId", SqlDbType.Int);
                cmd.Parameters["@BookId"].Direction = ParameterDirection.Output;
                constr.Open();
                cmd.ExecuteNonQuery();
                book.BookId = Convert.ToInt32(cmd.Parameters["@BookId"].Value);
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
            return book.BookId;
        }
        /// <summary>
        /// A Book method to Read all Book type entries in the Database.
        /// </summary>
        /// <returns>List of all the Bookings in the database</returns>
        public List<Book> Read()
        {
            List<Book> books = new List<Book>();
            try
            {
                SqlCommand cmd = new SqlCommand("Book_Read", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    books.Add(new Book
                    {
                        BookId = Convert.ToInt32(dr["BookId"]),
                        UserId = Convert.ToInt32(dr["UserId"]),
                        HotelId = Convert.ToInt32(dr["HotelId"]),
                        RoomId = Convert.ToInt32(dr["RoomId"]),
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
            return books;
        }
        /// <summary>
        /// A Book method to Read a specific Book type entry in the Database.
        /// </summary>
        /// <param name="id">Booking ID assigned when creating the object</param>
        /// <returns>Book type object associated with the provided ID</returns>
        public Book ReadById(int id)
        {
            Book book = new Book();
            try
            {
                SqlCommand cmd = new SqlCommand("Book_ReadById", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                constr.Open();
                da.Fill(dt);
                constr.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    book.BookId = Convert.ToInt32(dr["BookId"]);
                    book.UserId = Convert.ToInt32(dr["UserId"]);
                    book.HotelId = Convert.ToInt32(dr["HotelId"]);
                    book.RoomId = Convert.ToInt32(dr["RoomId"]);
                    book.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    book.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    //book.DeletedOn = Convert.ToDateTime(dr["DeletedOn"]);
                    book.IsDeleted = Convert.ToBoolean(dr["IsDeleted"]);
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
            return book;
        }
        /// <summary>
        /// A Book method to Update a specific Book type entry in the Database.
        /// </summary>
        /// <param name="book">Book type object</param>
        /// <returns>True if the Updation was successful and False if it was not</returns>
        public bool Update(Book book)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Book_Update", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", book.BookId);
                cmd.Parameters.AddWithValue("@UserId", book.UserId);
                cmd.Parameters.AddWithValue("@HotelId", book.HotelId);
                cmd.Parameters.AddWithValue("@RoomId", book.RoomId);
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
        /// A Book method to Delete a specific Book type entry in the Database.
        /// </summary>
        /// <param name="id">Book ID assigned when creating the object</param>
        /// <returns>True if the Deletion was successful and False if it was not</returns>
        public bool Delete(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Book_Delete", constr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", id);
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