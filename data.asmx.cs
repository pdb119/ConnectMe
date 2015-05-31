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
        private string profileId = "1";
        private Profile p;
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void putProfile(string username, string age)
        {

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void getProfile(int ajaxid)
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
            p.profileId = int.Parse(profileId);
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
            getProfile.Parameters.Add(new SqlParameter("profileid", profileId));
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
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void gameSearch(string searchTerm,int ajaxid)
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
            Context.Response.Write("{\"games\":"+new JavaScriptSerializer().Serialize(games)+"}");
            Context.Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void getNearby(int ajaxid)
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
            Context.Response.Write(ajaxid);
            Context.Response.Write("{\"nearbyUsers\":" + new JavaScriptSerializer().Serialize(p) + "}");
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
            getGames.Parameters.Add(new SqlParameter("profileId", profileId));
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
            string gameName = "no game";
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
                getGames.Parameters.Add(new SqlParameter("profileId", profileId));
                getGames.Parameters.Add(new SqlParameter("gameId", gameId));
                getGames.ExecuteNonQuery();
                Game game = gamesList[0];
                gameName = game.name;
            }
            conn.Close();
            
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write("{\"game\":{\"gameId\":"+gameId+",\"name\":\""+ gameName + "\"}}");
            Context.Response.End();
        }
    }
}
