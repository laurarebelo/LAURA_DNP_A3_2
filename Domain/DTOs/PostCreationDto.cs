using System;

namespace Domain.DTOs
{
    public class PostCreationDto
    {
        public String Title { get; set; }
        public String Body { get; set; }
        public String Username { get; set; }
        public String Subreddit { get; set; }
    
        public PostCreationDto(string title, string body, string username, string subreddit)
        {
            Title = title;
            Body = body;
            Username = username;
            Subreddit = subreddit;
        }
    }
}