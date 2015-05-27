using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectMe
{
    public class NearbyUser
    {
        public int userId;
        public string userName;
        public int distance;
        public Game[] games;
    }
}