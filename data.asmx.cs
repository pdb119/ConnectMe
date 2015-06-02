using System;
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
        //private string profileId = "1";
        private Profile p;
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void putProfile(string username, string age)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getProfile(int ajaxid)
        {
            getOtherProfile(int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value), ajaxid);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getOtherProfile(int profileId, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            SqlCommand getProfile = new SqlCommand("SELECT * FROM Profile WHERE profileId=@profileid", conn);
            conn.Open();
            getProfile.Parameters.Add(new SqlParameter("profileid", profileId));
            SqlDataReader profileReturn = getProfile.ExecuteReader();
            p = new Profile();
            p.username = "nodb";
            while (profileReturn.Read())
            {
                p.username = (string)profileReturn["username"];

            }
            p.profileId = profileId;
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
            Context.Response.Write(ajaxid);
            Context.Response.Write(new JavaScriptSerializer().Serialize(p));
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getFriends(int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            SqlCommand getProfile = new SqlCommand("SELECT * FROM Profile JOIN Friendship ON Profile.profileId=Friendship.user2 WHERE Friendship.user1=@profileid", conn);
            conn.Open();
            getProfile.Parameters.Add(new SqlParameter("profileid", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            SqlDataReader profileReturn = getProfile.ExecuteReader();
            List<Profile> friendsList = new List<Profile>();
            while (profileReturn.Read())
            {
                Profile temp = new Profile();
                temp.username = (string)profileReturn["username"];
                temp.profileId = (int)profileReturn["profileId"];
                temp.age = 1;
                temp.picture = "fdsa";
                temp.location = "fdsa";
                friendsList.Add(temp);
            }
            profileReturn.Close();
            foreach (Profile prof in friendsList)
            {
                SqlCommand getGames = new SqlCommand("SELECT * FROM ProfileGame JOIN Game ON ProfileGame.gameId=Game.gameId WHERE ProfileGame.profileId=@profileid", conn);
                getGames.Parameters.Add(new SqlParameter("profileid", prof.profileId));
                SqlDataReader gamesReturn = getGames.ExecuteReader();
                List<Game> gamesList = new List<Game>();
                while (gamesReturn.Read())
                {
                    Game g = new Game();
                    g.name = (string)gamesReturn["gameName"];
                    gamesList.Add(g);
                }
                gamesReturn.Close();
                prof.games = gamesList.ToArray();
            }
            conn.Close();
            Profile[] friends = friendsList.ToArray();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"friends\":" + new JavaScriptSerializer().Serialize(friends) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void gameSearch(string searchTerm, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getGames = new SqlCommand("SELECT * FROM Game WHERE gameName LIKE '%'+@gameName+'%'", conn);
            getGames.Parameters.Add(new SqlParameter("gameName", searchTerm.ToLower()));
            SqlDataReader gamesReturn = getGames.ExecuteReader();
            List<Game> gamesList = new List<Game>();
            while (gamesReturn.Read())
            {
                Game g = new Game();
                g.name = (string)gamesReturn["gameName"];
                g.gameId = (int)gamesReturn["gameId"];
                gamesList.Add(g);
            }
            gamesReturn.Close();
            conn.Close();
            Game[] games = gamesList.ToArray();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"games\":" + new JavaScriptSerializer().Serialize(games) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void checkLogin(int ajaxid)
        {
            int loggedin = 0;
            if (HttpContext.Current.Request.Cookies["loginId"] != null)
            {
                loggedin = 1;
            }
            //HttpCookie newLogin = new HttpCookie("loginId");
            //newLogin.Value = 1.ToString();
            Context.Response.Clear();
            //Context.Response.Cookies.Add(newLogin);
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"loggedIn\":" + loggedin + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getNearby(double latitude, double longitude, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand updateLocation = new SqlCommand("UPDATE Profile SET lat=@Lat,long=@long WHERE profileId=@profileId;", conn);
            updateLocation.Parameters.Add(new SqlParameter("lat", latitude));
            updateLocation.Parameters.Add(new SqlParameter("long", longitude));
            updateLocation.Parameters.Add(new SqlParameter("profileId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            updateLocation.ExecuteNonQuery();
            SqlCommand getUsers = new SqlCommand("SELECT * FROM Profile WHERE profileId<>@profileId;", conn);
            getUsers.Parameters.Add(new SqlParameter("profileId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            SqlDataReader usersReturn = getUsers.ExecuteReader();
            List<Profile> usersList = new List<Profile>();
            while (usersReturn.Read())
            {
                Profile p = new Profile();
                p.profileId = (int)usersReturn["profileId"];
                p.username = (string)usersReturn["username"];
                p.latitude = (double)usersReturn["lat"];
                p.longitude = (double)usersReturn["long"];
                usersList.Add(p);
            }
            usersReturn.Close();
            List<NearbyUser> nearbyList = new List<NearbyUser>();
            foreach (Profile prof in usersList)
            {
                double a = Math.Pow(latitude - prof.latitude, 2.0);
                double b = Math.Pow(longitude - prof.longitude, 2.0);
                double arcDistance = Math.Sqrt(a + b);
                double secondsArcDistance;
                try
                {
                    secondsArcDistance = arcDistance * 60 * 60;
                }
                catch (Exception e)
                {
                    secondsArcDistance = -1;
                }
                if (secondsArcDistance >= 0 && prof.latitude>0)
                {
                    //don't include > 100mi
                    if (secondsArcDistance <= 5280)
                    {
                        //convert seconds to feet
                        prof.distance = secondsArcDistance * 100;
                        NearbyUser nb = new NearbyUser();
                        nb.userId = prof.profileId;
                        nb.userName = prof.username;
                        nb.distance = (int)prof.distance;
                        SqlCommand getGames = new SqlCommand("SELECT * FROM ProfileGame JOIN Game ON ProfileGame.gameId=Game.gameId WHERE ProfileGame.profileId=@profileid", conn);
                        getGames.Parameters.Add(new SqlParameter("profileid", prof.profileId));
                        SqlDataReader gamesReturn = getGames.ExecuteReader();
                        List<Game> gamesList = new List<Game>();
                        while (gamesReturn.Read())
                        {
                            Game g = new Game();
                            g.name = (string)gamesReturn["gameName"];
                            gamesList.Add(g);
                        }
                        while (gamesList.Count < 3)
                        {
                            Game g = new Game();
                            g.name = "(User Has no games)";
                            gamesList.Add(g);
                        }
                        gamesReturn.Close();
                        nb.games = gamesList.ToArray();
                        nearbyList.Add(nb);
                    }
                }
            }
            conn.Close();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"nearbyUsers\":" + new JavaScriptSerializer().Serialize(nearbyList.ToArray()) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void addFriend(int profileId)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getConversations(int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getGames = new SqlCommand("SELECT * FROM Conversation JOIN ConversationProfile ON Conversation.conversationId=ConversationProfile.conversationId WHERE ConversationProfile.profileId=@profileId", conn);
            getGames.Parameters.Add(new SqlParameter("profileId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            SqlDataReader convReturn = getGames.ExecuteReader();
            List<Conversation> convList = new List<Conversation>();
            while (convReturn.Read())
            {
                Conversation c = new Conversation(true);
                c.id = (int)convReturn["conversationId"];
                c.name = (string)convReturn["name"];
                convList.Add(c);
            }
            convReturn.Close();
            foreach (Conversation c in convList)
            {
                SqlCommand profiles = new SqlCommand("SELECT * FROM ConversationProfile WHERE ConversationProfile.conversationId=@profileId", conn);
                profiles.Parameters.Add(new SqlParameter("profileId", c.id));
                SqlDataReader profret = profiles.ExecuteReader();
                List<Profile> profList = new List<Profile>();
                while (profret.Read())
                {
                    Profile p = new Profile();
                    p.profileId = (int)profret["profileId"];
                    profList.Add(p);
                }
                c.members = profList.ToArray();
                profret.Close();
            }
            conn.Close();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            //string url = Context.Request.Url.PathAndQuery;
            //string url2url.IndexOf("?ajaxindex=")
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"conversations\":" + new JavaScriptSerializer().Serialize(convList.ToArray()) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getConversation(int conversationId, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getProfiles = new SqlCommand("SELECT * FROM ConversationProfile JOIN Profile ON ConversationProfile.profileId=Profile.profileId WHERE conversationId=@convId", conn);
            getProfiles.Parameters.Add(new SqlParameter("convId", conversationId));
            SqlDataReader profilesReturn = getProfiles.ExecuteReader();
            List<Profile> profileList = new List<Profile>();
            while (profilesReturn.Read())
            {
                Profile c = new Profile();
                c.profileId = (int)profilesReturn["profileId"];
                c.username = (string)profilesReturn["username"];
                profileList.Add(c);
            }
            profilesReturn.Close();
            SqlCommand getMessages = new SqlCommand("SELECT * FROM ConversationMessage JOIN Profile ON ConversationMessage.fromProfile=Profile.profileId WHERE conversationId=@cId", conn);
            getMessages.Parameters.Add(new SqlParameter("cId", conversationId));
            SqlDataReader messagesReturn = getMessages.ExecuteReader();
            List<Message> messageList = new List<Message>();
            while (messagesReturn.Read())
            {
                Message c = new Message();
                c.id = (int)messagesReturn["messageId"];
                c.from = new Profile();
                c.from.profileId = (int)messagesReturn["fromProfile"];
                c.from.username = (string)messagesReturn["username"];
                c.message = (string)messagesReturn["message"];
                messageList.Add(c);
            }
            profilesReturn.Close();
            conn.Close();
            Conversation conv = new Conversation(false);
            conv.id = conversationId;
            conv.members = profileList.ToArray();
            conv.messages = messageList.ToArray();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"conversation\":" + new JavaScriptSerializer().Serialize(conv) + "}");
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void addGame(int gameId, int ajaxid)
        {
            string gameName = "(User Has no games)";
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getGames = new SqlCommand("SELECT * FROM Game WHERE gameId=@gameId", conn);
            getGames.Parameters.Add(new SqlParameter("gameId", gameId));
            SqlDataReader gamesReturn = getGames.ExecuteReader();
            List<Game> gamesList = new List<Game>();
            while (gamesReturn.Read())
            {
                Game g = new Game();
                g.name = (string)gamesReturn["gameName"];
                g.gameId = (int)gamesReturn["gameId"];
                gamesList.Add(g);
            }
            gamesReturn.Close();
            if (gamesList.Count > 0)
            {
                getGames = new SqlCommand("INSERT INTO ProfileGame (profileId,gameId) VALUES(@profileId,@gameId);", conn);
                getGames.Parameters.Add(new SqlParameter("profileId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
                getGames.Parameters.Add(new SqlParameter("gameId", gameId));
                getGames.ExecuteNonQuery();
                Game game = gamesList[0];
                gameName = game.name;
            }
            conn.Close();

            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"game\":{\"gameId\":" + gameId + ",\"name\":\"" + gameName + "\"}}");
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void sendMessage(int conversationId, string message, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand sendMessage = new SqlCommand("INSERT INTO ConversationMessage (conversationId,fromProfile,message) VALUES(@profileId,@gameId,@message);", conn);
            sendMessage.Parameters.Add(new SqlParameter("profileId", conversationId));
            sendMessage.Parameters.Add(new SqlParameter("gameId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            sendMessage.Parameters.Add(new SqlParameter("message", message));
            sendMessage.ExecuteNonQuery();
            conn.Close();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"sendStatus\":1}");
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void newConversation(int userId, int ajaxid)
        {
            SqlConnection conn = new SqlConnection("Data Source=sqvuyen40w.database.windows.net;Initial Catalog=connectme;Integrated Security=False;User ID=connectme;Password=AmericanHorses!;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            conn.Open();
            SqlCommand getName = new SqlCommand("SELECT username FROM Profile WHERE profileId=@gameId", conn);
            getName.Parameters.Add(new SqlParameter("gameId", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            SqlDataReader gamesReturn = getName.ExecuteReader();
            string myName = "";
            while (gamesReturn.Read())
            {
                myName = (string)gamesReturn["username"];
            }
            gamesReturn.Close();
            getName = new SqlCommand("SELECT username FROM Profile WHERE profileId=@gameId", conn);
            getName.Parameters.Add(new SqlParameter("gameId", userId));
            gamesReturn = getName.ExecuteReader();
            string otherName = "";
            while (gamesReturn.Read())
            {
                otherName = (string)gamesReturn["username"];
            }
            gamesReturn.Close();
            SqlCommand sendMessage = new SqlCommand("INSERT INTO Conversation (name) VALUES(@name);SELECT @@IDENTITY AS 'conversationId';", conn);
            sendMessage.Parameters.Add(new SqlParameter("name", myName + " and " + otherName));
            int convId = System.Convert.ToInt32(sendMessage.ExecuteScalar());
            sendMessage = new SqlCommand("INSERT INTO ConversationProfile (conversationId,profileId) VALUES(@conv,@prof);", conn);
            sendMessage.Parameters.Add(new SqlParameter("conv", convId));
            sendMessage.Parameters.Add(new SqlParameter("prof", int.Parse(HttpContext.Current.Request.Cookies["loginId"].Value)));
            sendMessage.ExecuteNonQuery();
            sendMessage = new SqlCommand("INSERT INTO ConversationProfile (conversationId,profileId) VALUES(@conv,@prof);", conn);
            sendMessage.Parameters.Add(new SqlParameter("conv", convId));
            sendMessage.Parameters.Add(new SqlParameter("prof", userId));
            sendMessage.ExecuteNonQuery();
            conn.Close();
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"conversationId\":" + convId + "}");
            Context.Response.End();
        }
    }
}
