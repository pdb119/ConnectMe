using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectMe
{
    public class Profile
    {
        public int profileId;
        public string username;
        public int age;
        public string picture;
        public double latitude;
        public string location;
        public double longitude;
        public double distance;
        public Game[] games;
    }
}