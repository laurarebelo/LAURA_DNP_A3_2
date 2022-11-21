using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Comment : Postable
    {
        [JsonIgnore]
        public Post Post { get; set; }
        public Comment(int id, Post post, string body, User user, Subreddit subreddit) : base(id, body, user, subreddit)
        {
            Post = post;
        }
        
        public Comment() {}
    }
}