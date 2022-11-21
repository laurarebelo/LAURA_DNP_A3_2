using Domain;

namespace Application.DAOInterfaces;

public interface ICommentDao
{
    public Task<Comment> CreateAsync(Comment comment);
    public Task<int> GetNextCommentId(string subreddit, int postId);
    public Task<Comment> Get(string subreddit, int postId, int commentId);
    public Task<Comment> UpvoteComment(Comment comment);
    public Task<Comment> DownvoteComment(Comment comment);
}