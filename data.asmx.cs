﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace ConnectMe
{
    /// <summary>
    /// Summary description for data
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class data : System.Web.Services.WebService
    {
        //will be cookie based eventually
        private string profileId = "1";
        private Profile p;
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void putProfile(string username, string age)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void getProfile()
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");            
            SqlCommand getProfile = new SqlCommand("SELECT * FROM Profile WHERE profileId=@profileid",conn);
            conn.Open();
            getProfile.Parameters.Add(new SqlParameter("profileid", profileId));
            SqlDataReader profileReturn = getProfile.ExecuteReader();
            p = new Profile();
            p.username = "nodb";
            while (profileReturn.Read())
            {
                p.username = (string)profileReturn["username"];

            }
            profileReturn.Close();
            SqlCommand getGames = new SqlCommand("SELECT * FROM ProfileGame JOIN Game ON ProfileGame.gameId=Game.gameId WHERE ProfileGame.profileId=@profileid", conn);
            getGames.Parameters.Add(new SqlParameter("profileid", profileId));
            SqlDataReader gamesReturn = getGames.ExecuteReader();
            List<Game> gamesList = new List<Game>();
            while (gamesReturn.Read())
            {
                Game g = new Game();
                g.name = (string)gamesReturn["gameName"];
                gamesList.Add(g);
            }
            gamesReturn.Close();
            conn.Close();
            p.age = 22;
            p.picture = "pb.jpg";
            p.location = "Seattle, WA";
            p.games = gamesList.ToArray();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(new JavaScriptSerializer().Serialize(p));
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getFriends()
        {
            Profile[] p = new Profile[2];
            p[0] = new Profile();
            p[1] = new Profile();
            p[0].username = "ICantCSS";
            p[0].age = 20;
            p[0].picture = "pb.jpg";
            p[0].location = "Seattle, WA";
            p[0].games = new Game[3];
            p[0].games[0] = new Game();
            p[0].games[1] = new Game();
            p[0].games[2] = new Game();           
            p[0].games[0].name = "GTA5";
            p[0].games[1].name = "Call of Duty";
            p[0].games[2].name = "Second Life";
            p[1].username = "IPlayGamz";
            p[1].age = 68;
            p[1].picture = "pb.jpg";
            p[1].location = "Seattle, WA";
            p[1].games = new Game[3];
            p[1].games[0] = new Game();
            p[1].games[1] = new Game();
            p[1].games[2] = new Game();
            p[1].games[0].name = "Forza";
            p[1].games[1].name = "World In Conflict";
            p[1].games[2].name = "Cryostasis";
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write("{\"friends\":" + new JavaScriptSerializer().Serialize(p) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void gameSearch(string searchTerm)
        {
            string[] p = new string[2];
            p[0] = "halo 4";
            p[1] = "halo 3";
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write("{\"games\":"+new JavaScriptSerializer().Serialize(p)+"}");
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getNearby()
        {
            NearbyUser[] p = new NearbyUser[3];
            p[0] = new NearbyUser();
            p[1] = new NearbyUser();
            p[2] = new NearbyUser();
            p[0].userId = 1;
            p[0].userName = "Jason Aldean";
            p[0].distance = 45;
            p[0].games = new Game[3];
            p[0].games[0] = new Game();
            p[0].games[1] = new Game();
            p[0].games[2] = new Game();
            p[0].games[0].name = "Forza";
            p[0].games[1].name = "World In Conflict";
            p[0].games[2].name = "Cryostasis";
            p[1].userId = 2;
            p[1].userName = "Lee Brice";
            p[1].distance = 11;
            p[1].games = new Game[3];
            p[1].games[0] = new Game();
            p[1].games[1] = new Game();
            p[1].games[2] = new Game();
            p[1].games[0].name = "Forza";
            p[1].games[1].name = "World In Conflict";
            p[1].games[2].name = "Cryostasis";
            p[2].userId = 3;
            p[2].userName = "Rocket Raccoon";
            p[2].distance = 104;
            p[2].games = new Game[3];
            p[2].games[0] = new Game();
            p[2].games[1] = new Game();
            p[2].games[2] = new Game();
            p[2].games[0].name = "Forza";
            p[2].games[1].name = "World In Conflict";
            p[2].games[2].name = "Cryostasis";
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write("{\"nearbyUsers\":" + new JavaScriptSerializer().Serialize(p) + "}");
            Context.Response.End();
        }
        
    }
}
