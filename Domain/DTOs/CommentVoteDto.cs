namespace Domain.DTOs;

public class CommentVoteDto
{
    public int CommentId { get; }
    public int PostId { get; }
    public string Subreddit { get; }

    public CommentVoteDto(int commentId, int postId, string subreddit)
    {
        CommentId = commentId;
        PostId = postId;
        Subreddit = subreddit;
    }
}