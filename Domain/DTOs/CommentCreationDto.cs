


namespace Domain.DTOs
{
    public class CommentCreationDto
    {
        public int PostId{ get; set; }
        public string Body{ get; set; }
        public string Username{ get; set; }
        public string Subreddit{ get; set; }

        public CommentCreationDto(int postId, string body, string username, string subreddit)
        {
            PostId = postId;
            Body = body;
            Username = username;
            Subreddit = subreddit;
        }
    }
}