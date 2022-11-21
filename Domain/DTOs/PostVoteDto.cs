

namespace Domain.DTOs
{
    public class PostVoteDto
    {
        public int postId { get; set; }
        public string subreddit { get; set; }

        public PostVoteDto(int postId, string subreddit)
        {
            this.postId = postId;
            this.subreddit = subreddit;
        }
    }
}