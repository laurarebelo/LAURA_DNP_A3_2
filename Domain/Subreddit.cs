using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Subreddit
    {
        [Key]
        public string Title { get; set; }
        public List<Post> Posts { get; set; }

        public Subreddit(string title)
        {
            Title = title;
            Posts = new List<Post>();
        }
        
        public Subreddit() {}

        public void AddPost(Post post)
        {
            Posts.Add(post);
        }

        public void RemovePost(Post post)
        {
            Posts.Remove(post);
        }

    }
    
    
}