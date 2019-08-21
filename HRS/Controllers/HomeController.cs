using HRS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace HRS.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        UsersRepository usersrepo = new UsersRepository();
        HotelsRepository hotelsrepo = new HotelsRepository();
        RoleRepository rolerepo = new RoleRepository();
        TypesRepository typesrepo = new TypesRepository();
        RoomRepository roomrepo = new RoomRepository();
        BookRepository bookrepo = new BookRepository();
        EmailQueueRepository emailrepo = new EmailQueueRepository();
        MailLogsRepository mailrepo = new MailLogsRepository();
        ExceptionRepository exceptionrepo = new ExceptionRepository();
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // Method to Create/Insert a user in the database.
        public ActionResult Users_Insert()
        {
            List<Role> data = rolerepo.Read();
            ViewBag.Data = data;
            return View("Users_Insert");
        }
        // Method to Retreive all the Users in the database.
        public ActionResult Users_Read()
        {
            UserViewModel data = new UserViewModel();
            data.roles = rolerepo.Read();
            data.users = usersrepo.Read();
            return View("Users_Read", data);
        }
        // Method to Retreive a specific User.
        public ActionResult Users_ReadById(int id)
        {
            Users data = usersrepo.ReadById(id);
            return View("Users_ReadById", data);
        }
        // Method to Update a User.
        public ActionResult Users_Update(int id)
        {
            Users data = usersrepo.ReadById(id);
            ViewBag.Data = rolerepo.Read();
            return View("Users_Update", data);
        }
        // Method to Activate a User's Account.
        public ActionResult Users_ActivateAccount(string id)
        {
            bool success = usersrepo.ActivateAccount(id);
            if (success)
            {
                return View("Users_ActivateAccount");
            }
            return View();
        }
        // Method to Login a User or Admin.
        public ActionResult Users_Login()
        {
            return View("Users_Login");
        }
        // Method to Logout a User or Admin.
        public ActionResult Users_Logout()
        {
            Session.Clear();
            return RedirectToAction("Users_Login");
        }
        // Method to return Admin's and Manager's Dashboard.
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
        // Method to Delete/Remove a user from the database.
        public ActionResult Users_Delete(int id)
        {
            Users data = usersrepo.ReadById(id);
            return View("Users_Delete", data);
        }
        // Method to Insert/Create a user in the database.
        [HttpPost]
        public ActionResult Users_Insert(Users user)
        {
            List<Users> users = usersrepo.Read();
            List<Role> data = rolerepo.Read();
            for (int i = 0; i < users.Count; i++)
            {
                if (!users[i].IsDeleted)
                {
                    if (user.Email == users[i].Email)
                    {
                        ViewBag.Message = "Another account with the same E-mail already exists.";
                        ViewBag.Data = data;
                        return View("Users_Insert");
                    }
                    else if (user.Phone == users[i].Phone)
                    {
                        ViewBag.Message = "Another account with the same Phone Number already exists.";
                        ViewBag.Data = data;
                        return View("Users_Insert");
                    }
                }
            }
            int success = usersrepo.Insert(user);
            if(success >= 1)
            {
                AccountActivate(user);
                ViewBag.Message = "Please complete your registration by confirming your E-mail account.";
                return View("Users_Login");
            }
            ViewBag.Message = "An error occurred while making your account";
            ViewBag.Data = data;
            return View("Users_Insert");
        }
        // Method to Update/Edit a user in the database.
        [HttpPost]
        public ActionResult Users_Update(Users user)
        {
            bool success = usersrepo.Update(user);
            UserViewModel data = new UserViewModel();
            data.roles = rolerepo.Read();
            data.users = usersrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Users_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Users_Read", data);
        }
        // Method to Delete/Remove a user from the database.
        [HttpPost]
        public ActionResult Users_Delete(Users user)
        {
            UserViewModel data = new UserViewModel();
            bool success = usersrepo.Delete(user.UserId);
            data.roles = rolerepo.Read();
            data.users = usersrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Users_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Users_Read", data);
        }
        //////// Method to Send a mail to a user.
        //////[HttpPost]
        //////public void Users_SendMail(int id)
        //////{
        //////    try
        //////    {
        //////        Users user = usersrepo.ReadById(id);
        //////        MailMessage mm = new MailMessage();
        //////        mm.From = new MailAddress("gurdain.singh@clanstech.com");
        //////        mm.To.Add(user.Email);
        //////        mm.Subject = "Registration Confirmation";
        //////        mm.IsBodyHtml = true;
        //////        mm.Body = string.Format("Dear " + user.UserName +
        //////            ", <BR><BR>Please Click on the following link to Register your account<BR>                 <a href=http://"
        //////            + Request.Url.Host + ":"
        //////            + Request.Url.Port
        //////            + Url.Action("Users_ActivateAccount", "Home", new { id = user.UserGuid })
        //////            + ">Click Here</a><BR><BR>Sincerely,<BR>Clanstech Team.");
        //////        SmtpClient smtp = new SmtpClient();
        //////        smtp.Host = "mail.clanstech.com";
        //////        smtp.Port = 25;
        //////        NetworkCredential nc = new NetworkCredential("gurdain.singh@clanstech.com", "gurdain_27");
        //////        smtp.EnableSsl = false;
        //////        smtp.UseDefaultCredentials = false;
        //////        smtp.Credentials = nc;
        //////        smtp.Send(mm);
        //////        ///////////////
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        MethodBase method = MethodBase.GetCurrentMethod();
        //////        Exceptions exception = new Exceptions
        //////        {

        //////            ExceptionNumber = ex.HResult.ToString(),
        //////            ExceptionMessage = ex.Message,
        //////            ExceptionMethod = method.Name,
        //////            ExceptionUrl = Request.Url.AbsoluteUri
        //////        };
        //////        int r = exceptionrepo.Exception_Create(exception);
        //////        if (r == 0)
        //////        {
        //////            exceptionrepo.Exception_InsertInLogFile(exception);
        //////        }
        //////        if (constr.State != ConnectionState.Open)
        //////        {
        //////            constr.Close();
        //////            constr.Open();
        //////        }
        //////    }
        //////    //UserViewModel data = new UserViewModel();
        //////    //data.roles = rolerepo.Read();
        //////    //data.users = usersrepo.Read();
        //////    return /*View("Users_Read", data)*/;
        //////}
        // Method to Login a User or Admin.
        [HttpPost]
        public ActionResult Users_Login(string email, string password)
        {
            Users user = usersrepo.Login(email, password);
            if (!user.IsActive)
            {
                ViewBag.Message = "This Account is not Verified. Please go to your registered E-mail Account in order to Verify.";
                return View("Users_Login");
            }
            Session["roleId"] = user.RoleId;
            Session["userName"] = user.UserName;
            Session["userId"] = user.UserId;
            if (user.RoleId == 1)
            {
                return View("Dashboard");
            }
            else if (user.RoleId == 2)
            {
                HotelViewModel visitorViewModel = new HotelViewModel();
                visitorViewModel.hotels = hotelsrepo.Read();
                visitorViewModel.rooms = roomrepo.Read();
                visitorViewModel.types = typesrepo.Read();
                visitorViewModel.users = usersrepo.Read();
                return View("Visitor_Dashboard", visitorViewModel);
            }
            else if(user.RoleId == 0)
            {
                return View("Dashboard");
            }
            ViewBag.Message = "Incorrect E-mail or Password";
            return View("Users_Login");
        }
        // Method to Insert/Create a hotel in the database.
        public ActionResult Hotels_Insert()
        {
            ViewBag.Types = typesrepo.Read();
            ViewBag.Users = usersrepo.Read();
            return View("Hotels_Insert");
        }
        // Method to Retreive all the hotels in the database.
        public ActionResult Hotels_Read()
        {
            HotelViewModel data = new HotelViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            data.types = typesrepo.Read();
            data.users = usersrepo.Read();
            return View("Hotels_Read", data);
        }
        // Method to Retreive a specific hotel.
        public ActionResult Hotels_ReadById(int id)
        {
            Hotels data = hotelsrepo.ReadById(id);
            return View("Hotels_ReadById", data);
        }
        // Method to Update a hotel.
        public ActionResult Hotels_Update(int id)
        {
            Hotels data = hotelsrepo.ReadById(id);
            ViewBag.Types = typesrepo.Read();
            ViewBag.Users = usersrepo.Read();
            return View("Hotels_Update", data);
        }
        // Method to Delete/Remove a hotel from the database.
        public ActionResult Hotels_Delete(int id)
        {
            Hotels data = hotelsrepo.ReadById(id);
            return View("Hotels_Delete", data);
        }
        // Method to Insert/Create a hotel in the database.
        [HttpPost]
        public ActionResult Hotels_Insert(Hotels hotel)
        {
            hotel.UserId = Convert.ToInt32(Session["userId"]);
            int success = hotelsrepo.Insert(hotel);
            if (success >= 1)
            {
                Room data = new Room();
                data.HotelId = hotel.HotelId;
                for(int i = 0; i < hotel.Rooms; i++)
                {
                    success = roomrepo.Insert(data);
                }
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a hotel in the database.
        [HttpPost]
        public ActionResult Hotels_Update(Hotels hotel)
        {
            bool success = hotelsrepo.Update(hotel);
            HotelViewModel data = new HotelViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            data.types = typesrepo.Read();
            data.users = usersrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Hotels_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Hotels_Read", data);
        }
        // Method to Delete/Remove a hotel from the database.
        [HttpPost]
        public ActionResult Hotels_Delete(Hotels hotel)
        {
            HotelViewModel data = new HotelViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            data.types = typesrepo.Read();
            data.users = usersrepo.Read();
            bool success = hotelsrepo.Delete(hotel.HotelId);
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Hotels_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Hotels_Read", data);
        }
        // Method to Insert/Create a role in the database.
        public ActionResult Role_Insert()
        {
            return View("Role_Insert");
        }
        // Method to Retreive all the roles from the database.
        public ActionResult Role_Read()
        {
            List<Role> data = rolerepo.Read();
            return View("Role_Read", data);
        }
        // Method to Retreive a specific role from the database.
        public ActionResult Role_ReadById(int id)
        {
            Role data = rolerepo.ReadById(id);
            return View("Role_ReadById", data);
        }
        // Method to Update a role.
        public ActionResult Role_Update(int id)
        {
            Role role = rolerepo.ReadById(id);
            return View("Role_Update");
        }
        // Method to Delete/Remove a role from the database.
        public ActionResult Role_Delete(int id)
        {
            Role data = rolerepo.ReadById(id);
            return View("Role_Delete", data);
        }
        // Method to Insert/Create a role in the database.
        [HttpPost]
        public ActionResult Role_Insert(Role role)
        {
            int success = rolerepo.Insert(role);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a role in the database.
        [HttpPost]
        public ActionResult Role_Update(Role role)
        {
            bool success = rolerepo.Update(role);
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Role_Read");
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Role_Read");
        }
        // Method to Delete/Remove a role from the database.
        [HttpPost]
        public ActionResult Role_Delete(Role role)
        {
            bool success = rolerepo.Delete(role.RoleId);
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Role_Read");
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Role_Read");
        }
        // Method to Insert/Create a hotel type in the database.
        public ActionResult Types_Insert()
        {
            return View("Types_Insert");
        }
        // Method to Retreive all the types of hotels from the database.
        public ActionResult Types_Read()
        {
            List<Types> data = typesrepo.Read();
            return View("Types_Read", data);
        }
        // Method to Retreive a specific type of hotel.
        public ActionResult Types_ReadById(int id)
        {
            Types data = typesrepo.ReadById(id);
            return View("Types_ReadById", data);
        }
        // Method to Update a hotel type.
        public ActionResult Types_Update(int id)
        {
            Types data = typesrepo.ReadById(id);
            return View("Types_Update", data);
        }
        // Method to Delete/Remove a type of hotel from the database.
        public ActionResult Types_Delete(int id)
        {
            Types data = typesrepo.ReadById(id);
                return View("Types_Delete", data);
        }
        // Method to Insert/Create a type of hotel in the database.
        [HttpPost]
        public ActionResult Types_Insert(Types type)
        {
            int success = typesrepo.Insert(type);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a specific type of hotel in the database.
        [HttpPost]
        public ActionResult Types_Update(Types type)
        {
            bool success = typesrepo.Update(type);
            List<Types> data = typesrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Types_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Types_Read", data);
        }
        // Method to Delete/Remove a type of hotel from the database.
        [HttpPost]
        public ActionResult Types_Delete(Types type)
        {
            bool success = typesrepo.Delete(type.HotelTypeId);
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Types_Read");
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Types_Read");
        }
        // Method to Insert/Create a room of hotel in the database.
        public ActionResult Room_Insert()
        {
            return View("Room_Insert");
        }
        // Method to Retreive all the rooms from the database.
        public ActionResult Room_Read()
        {
            RoomViewModel data = new RoomViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            return View("Room_Read", data);
        }
        // Method to Retreive a specific room from the database.
        public ActionResult Room_ReadById(int id)
        {
            Room data = roomrepo.ReadById(id);
            return View("Room_ReadById", data);
        }
        // Method to Update a hotel room.
        public ActionResult Room_Update(int id)
        {
            Room data = roomrepo.ReadById(id);
            return View("Room_Update", data);
        }
        // Method to Delete/Remove a room from the database.
        public ActionResult Room_Delete(int id)
        {
            Room data = roomrepo.ReadById(id);
            return View("Room_Delete", data);
        }
        // Method to Insert/Create a room in the database.
        [HttpPost]
        public ActionResult Room_Insert(Room room)
        {
            int success = roomrepo.Insert(room);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a specific room in the database.
        [HttpPost]
        public ActionResult Room_Update(Room room)
        {
            bool success = roomrepo.Update(room);
            RoomViewModel data = new RoomViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Room_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Room_Read", data);
        }
        // Method to Delete/Remove a specific room from the database.
        [HttpPost]
        public ActionResult Room_Delete(Room room)
        {
            bool success = roomrepo.Delete(room.RoomId);
            RoomViewModel data = new RoomViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Deleted";
                return View("Room_Read", data);
            }
            ViewBag.Message = "Encountered an Error while deleting";
            return View("Room_Read", data);
        }
        // Method to Insert/Create a booking in the database.
        public ActionResult Book_Insert()
        {
            return View("Book_Insert");
        }
        // Method to Retreive all the bookings from the database.
        public ActionResult Book_Read()
        {
            BookingViewModel data = new BookingViewModel();
            data.books = bookrepo.Read();
            data.hotels = hotelsrepo.Read();
            return View("Book_Read", data);
        }
        // Method to Retreive a specific booking from the database.
        public ActionResult Book_ReadById(int id)
        {
            Book data = bookrepo.ReadById(id);
            return View("Book_ReadById", data);
        }
        // Method to Update a hotel room booking.
        public ActionResult Book_Update(int id)
        {
            Book data = bookrepo.ReadById(id);
            return View("Book_Update", data);
        }
        // Method to Delete/Remove a specific booking from the database.
        public ActionResult Book_Delete(int id)
        {
            Book data = bookrepo.ReadById(id);
                return View("Book_Delete", data);
        }
        // Method to Insert/Create a booking in the database.
        [HttpPost]
        public ActionResult Book_Insert(Book book)
        {
            int success = bookrepo.Insert(book);
            if (success >= 1)
            {
                ViewBag.Message = "Entry Created Successfully";
                return View("Dashboard");
            }
            ViewBag.Message = "An error occurred while making the Entry";
            return View("Dashboard");
        }
        // Method to Update/Edit a booking in the database.
        [HttpPost]
        public ActionResult Book_Update(Book book)
        {
            bool success = bookrepo.Update(book);
            BookingViewModel data = new BookingViewModel();
            data.books = bookrepo.Read();
            data.hotels = hotelsrepo.Read();
            if (success)
            {
                ViewBag.Message = "Successfully Updated";
                return View("Book_Read", data);
            }
            ViewBag.Message = "Encountered an Error while updating";
            return View("Book_Read", data);
        }
        // Method to Delete/Remove a booking from the database.
        [HttpPost]
        public ActionResult Book_Delete(Book book)
        {
            bool success = bookrepo.Delete(book.BookId);
            BookingViewModel data = new BookingViewModel();
            data.books = bookrepo.Read();
            data.hotels = hotelsrepo.Read();
            if (success)
            {
                Room room = roomrepo.ReadById(book.RoomId);
                room.IsAvailable = true;
                room.Booked = false;
                roomrepo.Update(room);
                ViewBag.Message = "Successfully Deleted";
                return View("Book_Read", data);
            }
            ViewBag.Data = "Encountered an Error while deleting";
            return View("Book_Read", data);
        }
        // Method to book a room.
        public ActionResult Room_Book()
        {
            BookRoomViewModel bookRoomViewModel = new BookRoomViewModel();
            bookRoomViewModel.hotels = hotelsrepo.Read();
            bookRoomViewModel.rooms = roomrepo.Read();
            return View("Room_Book", bookRoomViewModel);
        }
        // Method to book a room.
        [HttpPost]
        public ActionResult Room_Book(int roomId)
        {
            Room room = roomrepo.ReadById(roomId);
            room.IsAvailable = false;
            room.Booked = true;
            bool booked = roomrepo.Update(room);
            if (booked)
            {
                Book book = new Book();
                book.UserId = Convert.ToInt32(Session["userId"]);
                book.HotelId = room.HotelId;
                book.RoomId = roomId;
                bookrepo.Insert(book);
                return View("Book_Room");
            }
            ViewBag.Message = "Unable to book the hotel room";
            return View("Visitor_Dashboard");
        }
        // Method to Retreive the Visitor's Dashboard.
        public ActionResult Visitor_Dashboard()
        {
            HotelViewModel data = new HotelViewModel();
            data.hotels = hotelsrepo.Read();
            data.rooms = roomrepo.Read();
            data.types = typesrepo.Read();
            data.users = usersrepo.Read();
            return View("Visitor_Dashboard", data);
        }
        // Method to prepare a E-mail to Activate a User's Account.
        public void AccountActivate(Users user)
        {
            EmailQueue email = new EmailQueue();
            email.ToAddress = user.Email;
            email.FromAddress = "gurdain.singh@clanstech.com";
            email.Subject = "Registration Confirmation";
            email.Body = string.Format("Dear " + user.UserName +
                    ", <BR><BR>Please Click on the following link to Register your account<BR>                 <a href=http://"
                    + Request.Url.Host + ":"
                    + Request.Url.Port
                    + Url.Action("Users_ActivateAccount", "Home", new { id = user.UserGuid })
                    + ">Click Here</a><BR><BR>Sincerely,<BR>Clanstech Team.");
            emailrepo.Insert(email);
            return;
        }
        // Method to Send a E-mail from the queue of unsend E-mails in the database.
        public static void SendEmailFromQueue()
        {
            SqlConnection constr = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            EmailQueueRepository emailrepo = new EmailQueueRepository();
            MailLogsRepository mailrepo = new MailLogsRepository();
            Emails email = new Emails();
            List<EmailQueue> emails = emailrepo.Read();
            ExceptionRepository exceptionrepo = new ExceptionRepository();
            try
            {
                for(int i = 0; i < emails.Count; i++)
                {
                    if (!emails[i].IsDeleted && emails[i].Tries < 6)
                    {
                        emailrepo.Delete(emails[i].EmailId);
                        while(emails[i].Tries < 6)
                        {
                            if (email.Send_Mail(emails[i].ToAddress, emails[i].FromAddress, emails[i].Subject, emails[i].Body))
                            {
                                MailLogs mail = new MailLogs()
                                {
                                    ToAddress = emails[i].ToAddress,
                                    FromAdress = emails[i].FromAddress,
                                    Subject = emails[i].Subject,
                                    Body = emails[i].Body,
                                    EmailStatus = true
                                };
                                mailrepo.Insert(mail);
                                break;
                            }
                            emails[i].Tries++;
                            emailrepo.Undelete(emails[i].EmailId);
                        }
                        emailrepo.Update(emails[i]);
                        break;
                    }
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
                    ExceptionUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri
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
            return;
        }

    }
}