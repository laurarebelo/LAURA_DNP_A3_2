namespace Domain.DTOs;

public class PostSearchParameters
{
    public string subreddit { get; }
    public int? postId { get; }

    public PostSearchParameters(string subreddit, int? postId)
    {
        this.subreddit = subreddit;
        this.postId = postId;
    }
}