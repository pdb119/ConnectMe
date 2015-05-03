using System;
using System.Collections.Generic;
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
        [WebMethod]
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void getProfile()
        {
            Profile p = new Profile();
            p.username = "pdb119";
            p.age = 22;
            p.picture = "pb.jpg";
            p.location = "Seattle, WA";
            p.games = new Game[4];
            p.games[0] = new Game();
            p.games[1] = new Game();
            p.games[2] = new Game();
            p.games[3] = new Game();
            p.games[0].name = "Halo 4";
            p.games[1].name = "Minecraft";
            p.games[2].name = "Gears of War 3";
            p.games[3].name = "Far Cry 4";
            Context.Response.Clear();
            Context.Response.ContentType = "text/json";
            Context.Response.Write(new JavaScriptSerializer().Serialize(p));
            Context.Response.End();
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true,ResponseFormat = ResponseFormat.Json)]
        public void 
        
    }
}
