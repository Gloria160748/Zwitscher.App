using System;
using System.Collections.Generic;
using System.Text;

namespace Zwitscher.Models
{
    public class User
    {
        public string userID { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string username { get; set; }
        public DateTime birthday { get; set; }
        public string biography { get; set; }
        public string gender { get; set; }
        public int followedCount { get; set; }
        public int followerCount { get; set; }
        public string pbFileName { get; set; }
    }

    public class LoginUser
    {
        public string Username { get; set; }
        public string ProfilePicture { get; set; }
        public string RoleName { get; set; }
        public bool Success { get; set; }
    }
}
