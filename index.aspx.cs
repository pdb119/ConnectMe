using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConnectMe
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mainForm.DefaultButton = "submitBtn";
            //HttpCookie loginId = Request.Cookies["loginId"];
            if (Session["loginId"] != null)
            {
                //do we need to check login?
                //eh, we don't need security for the prototype
                Response.Redirect("radar.html");
            }
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {

            string email = emailBox.Text;
            string pass = passwordBox.Text;
                        SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getName = new SqlCommand("SELECT password FROM Profile WHERE email=@username", conn);
            getName.Parameters.Add(new SqlParameter("username", email));
            string sqlPass = (string)getName.ExecuteScalar();
            if(pass == sqlPass){
                //login sucessful
                statusLabel.Text = "Login worked";
                SqlCommand getId = new SqlCommand("SELECT profileId FROM Profile WHERE email=@username", conn);
            getId.Parameters.Add(new SqlParameter("username", email));
            int profId = (int)getId.ExecuteScalar();
            //Session["loginId"] = profId;
            HttpCookie newLogin = new HttpCookie("loginId");
            newLogin.Value = profId.ToString();
            Response.Cookies.Add(newLogin);
            Response.Redirect("radar.html");
            } else {
                statusLabel.Text = "Login Wrong";
            }
            conn.Close();

        }

        protected void signupBtn_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getUsers = new SqlCommand("SELECT * FROM Profile WHERE email=@email OR username=@username;", conn);
            getUsers.Parameters.Add(new SqlParameter("email", emailBox.Text));
            getUsers.Parameters.Add(new SqlParameter("username", usernameBox.Text));
            SqlDataReader usersReturn = getUsers.ExecuteReader();
            List<Profile> usersList = new List<Profile>();
            bool found = false;
            while (usersReturn.Read())
            {
                found = true;
            }
            usersReturn.Close();
            if (found)
            {
                statusLabel.Text = "Account already exists";
            }
            else
            {
                SqlCommand newUser = new SqlCommand("INSERT INTO Profile (username,password,online,email) VALUES(@username,@password,0,@email);", conn);                
                newUser.Parameters.Add(new SqlParameter("username", usernameBox.Text));
                newUser.Parameters.Add(new SqlParameter("password", passwordBox.Text));
                newUser.Parameters.Add(new SqlParameter("email", emailBox.Text));
                newUser.ExecuteNonQuery();
                conn.Close();
                statusLabel.Text = "Account created";
            }
        }

        protected void sampleUserBtn_Click(object sender, EventArgs e)
        {
            string email = "sampleuser@sample.com";
            string pass = "samplepassword";
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getName = new SqlCommand("SELECT password FROM Profile WHERE email=@username", conn);
            getName.Parameters.Add(new SqlParameter("username", email));
            string sqlPass = (string)getName.ExecuteScalar();
            if (pass == sqlPass)
            {
                //login sucessful
                statusLabel.Text = "Login worked";
                SqlCommand getId = new SqlCommand("SELECT profileId FROM Profile WHERE email=@username", conn);
                getId.Parameters.Add(new SqlParameter("username", email));
                int profId = (int)getId.ExecuteScalar();
                //Session["loginId"] = profId;
                HttpCookie newLogin = new HttpCookie("loginId");
                newLogin.Value = profId.ToString();
                Response.Cookies.Add(newLogin);
                Response.Redirect("radar.html");
            }
            else
            {
                statusLabel.Text = "Login Wrong";
            }
            conn.Close();
        }
    }
}