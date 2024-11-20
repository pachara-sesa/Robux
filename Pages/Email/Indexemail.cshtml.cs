using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using System.Xml.Linq;
using static AS3_1660706126.Pages.Email.IndexEmailModel;

namespace AS3_1660706126.Pages.Email
{
    public class IndexEmailModel : PageModel
    {

        public List<Email> Emails = new List<Email>();
        public List<IUsers> IUsersl = new List<IUsers>();
        public string uid;
        public async Task OnGetAsync()
        {

            string username = User.Identity.Name;

            try
            {
                string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from AspNetUsers where UserName=@usern;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@usern", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No data found.");
                            }
                            while (reader.Read())
                            {
                                IUsers IUsersa = new IUsers();
                                IUsersa.Id = reader.GetString(0);
                                IUsersa.Name = reader.GetString(4);
                                IUsersa.MobilePhone = reader.GetString(3);
                                IUsersa.Fname = reader.GetString(1);
                                IUsersa.Lanme = reader.GetString(2);
                                IUsersa.Username = reader.GetString(4);
                                IUsersa.Email = reader.GetString(5);
                                //IUsersa.D_Id = reader.GetString(1);
                                //IUsersa.D_Name = reader.GetString(3);
                                IUsersl.Add(IUsersa);

                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }




            var p = Request.Query["P"].ToString();
            if (p == "s")
            {
                var eid = Request.Query["eid"].ToString();
                try
                {
                    string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "UPDATE email SET e_rstatus=1 WHERE e_id=@id;";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", eid);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
            }
            else if (p == "DS" || p=="DI")
            {
                var eid = Request.Query["eid"].ToString();
                try
                {
                    string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "DELETE from email WHERE e_id=@id";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", eid);
                            command.ExecuteNonQuery();
                        }
                    }
                    
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
                if (p == "DI")
                {
                    Response.Redirect("Indexemail");
                }
            }



            try
            {
                string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string Ps = Request.Query["P"];
                    string sql;
                    if (string.IsNullOrEmpty(Ps))
                    {
                        // หากไม่มีการส่งค่า P มา
                        sql = "Select * from email,AspNetUsers where UserName=@username and e_aemail=Email order by e_id desc";

                    }
                    else
                    {
                        // หากมีการส่งค่า P มา
                        sql = "Select * from email,AspNetUsers where UserName=@username and e_uemail=Email order by e_id desc";

                    }
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Email Email = new Email();
                                Email.Id = reader.GetInt32(0);
                                Email.SenderName = reader.GetString(1);
                                Email.Subject = reader.GetString(2);
                                Email.Body = reader.GetString(3);
                                Email.DateSent = reader.GetDateTime(4);
                                Email.IsRead = reader.GetInt32(7);
                                Emails.Add(Email);
                            }
                        }
                    }


                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            // จำลองข้อมูลอีเมล
            //Emails = new List<Email>
            //{
            //    new Email { Id = 1, SenderName = "John Doe", Subject = "Welcome!", DateSent = DateTime.Now, IsRead = true, Body = "เนื้อหาอีเมลต้อนรับ" },
            //    new Email { Id = 2, SenderName = "Jane Smith", Subject = "Meeting Update", DateSent = DateTime.Now.AddDays(-1), IsRead = false, Body = "อัปเดตการประชุมที่จะเกิดขึ้น" }
            //};
            

        }

        public IActionResult OnPostDeleteSelectedEmails(List<int> selectedEmails)
        {
            // ลบอีเมลที่มี ID ตรงกับรายการที่เลือก
            Emails = Emails.Where(email => !selectedEmails.Contains(email.Id)).ToList();

            return RedirectToPage();
        }

        public class Email
        {

            public int Id { get; set; }
            public string SenderName { get; set; }
            public string Subject { get; set; }
            public DateTime DateSent { get; set; }
            public int IsRead { get; set; }
            public string Body { get; set; }
        }

        public class IUsers
        {

            public string Id { get; set; }
            public string Name { get; set; }
            public string MobilePhone { get; set; }
            public string Fname { get; set; }
            public string Lanme { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string D_Id { get; set; }
            public string D_Name { get; set; }
        }
        IUsers IusersP = new IUsers();
        public void OnPost()
        {
            string username = User.Identity.Name;

            try
            {
                string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Select * from AspNetUsers where UserName=@usern";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@usern", username);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("No data found.");
                            }
                            while (reader.Read())
                            {
                                IUsers IUsersa = new IUsers();
                                IUsersa.Id = reader.GetString(0);
                                IUsersa.Name = reader.GetString(4);
                                IUsersa.MobilePhone = reader.GetString(3);
                                IUsersa.Fname = reader.GetString(1);
                                IUsersa.Lanme = reader.GetString(2);
                                IUsersa.Username = reader.GetString(4);
                                IUsersa.Email = reader.GetString(5);
                                //IUsersa.D_Id = reader.GetString(1);
                                //IUsersa.D_Name = reader.GetString(3);
                                IUsersl.Add(IUsersa);
                                IusersP = IUsersa;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            string Pages = Request.Form["Pages"];
            if (Pages == "u")
            {

                string Eid = Request.Form["Eid"];
                try
                {
                    string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "UPDATE email SET e_rstatus=1 WHERE e_id=@id;";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", Eid);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
                Response.Redirect("/Email/Indexemail");
            }
            else if (Pages == "c")
            {
                string CName = Request.Form["CName"];
                string CSubject = Request.Form["CSubject"];
                string CBody = Request.Form["CBody"];
                DateTime DateSent = DateTime.Now;

                try
                {
                    string connectionString = "Server=tcp:cfj.database.windows.net,1433;Initial Catalog=db_CFJ;Persist Security Info=False;User ID=CFJ;Password=Ad1660706159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "INSERT INTO email (e_uemail, e_aemail, e_toppic, e_text, e_datetime, e_did, e_rank,e_rstatus) VALUES (@uid, @uaid, @CSubject, @CBody, @datet, @did, @rank,0);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@uid", IusersP.Email);
                            command.Parameters.AddWithValue("@uaid", CName);
                            command.Parameters.AddWithValue("@CSubject", CSubject);
                            command.Parameters.AddWithValue("@CBody", CBody);
                            command.Parameters.AddWithValue("@datet", DateSent);
                            command.Parameters.AddWithValue("@did", 1);
                            command.Parameters.AddWithValue("@rank", 0);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
                Response.Redirect("/Email/Indexemail");
            }
        }

    }
}
