﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zwitscher.Models
{
    public class Comment
    {
        public string commentId { get; set; }
        public string user_username { get; set; }
        public string user_profilePicture { get; set; }
        public string createdDate { get; set; }
        public string commentText { get; set; }
        public bool loggedInUserIsCreator { get; set; }

        public List<Comment> Comments { get; set; }
        public bool hasComments { get; set; }
    }
}
