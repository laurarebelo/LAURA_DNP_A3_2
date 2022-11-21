using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public abstract class Postable
    {
        [JsonIgnore]
        public Subreddit Subreddit { get; set; }
        public int Id { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public int NumUpvotes { get; set; }
        public int NumDownvotes { get; set; }

        protected Postable(int id, string body, User user, Subreddit subreddit)
        {
            Subreddit = subreddit;
            Id = id;
            Body = body;
            User = user;
            NumUpvotes = 0;
            NumDownvotes = 0;
        }
        
        public Postable() {}

        public void Upvote()
        {
            NumUpvotes++;
        }

        public void Downvote()
        {
            NumDownvotes++;
        }

        public void UndoUpvote()
        {
            NumUpvotes--;
        }

        public void UndoDownvote()
        {
            NumDownvotes--;
        }
    }
}