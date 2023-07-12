using System;
using System.Collections.Generic;
using System.Text;

namespace Zwitscher.Models
{
    public class Post
    {
        public string postID { get; set; }
        public string user_username { get; set; }
        public string user_profilePicture { get; set; }
        public DateTime createdDate { get; set; }
        public int rating { get; set; }
        public int commentCount { get; set; }
        public bool currentUserVoted { get; set; }
        public string userVoteIsUpvote { get; set; }
        public List<string> mediaList { get; set; }
        public string postText { get; set; }
        public string retweetsPost { get; set; }

        public bool isOwnPost { get; set; }
        public bool mediaIncluded { get; set; }
        public bool videoIncluded { get; set; }
        public List<string> videoList { get; set; }
        public bool isRetweet { get; set; }
        public Post RetweetedPost { get; set; }
    }

    public class NewPost
    {
        public string Id { get; set; } = "";
        public string TextContent { get; set; }
        public bool IsPublic { get; set; }
        public string UserId { get; set; } = "";
        public string retweetsID { get; set; } = "";
    }
}
