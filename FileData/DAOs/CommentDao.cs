using Application.DAOInterfaces;
using Domain;

namespace FileData.DAOs;

public class CommentDao : ICommentDao
{
    private readonly FileContext context;

    public CommentDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        Subreddit subreddit =
            context.Subreddits.FirstOrDefault(
                s => s.Title.Equals(comment.Subreddit.Title, StringComparison.OrdinalIgnoreCase))!;
        Post post = subreddit.Posts.FirstOrDefault(p => p.Id == comment.Post.Id)!;
        
        post.AddComment(comment);
        context.SaveChanges();
        return Task.FromResult(comment);
    }

    public Task<int> GetNextCommentId(string subreddit, int postId)
    {
        Subreddit? desiredSubreddit =
            context.Subreddits.FirstOrDefault(s => s.Title.Equals(subreddit, StringComparison.OrdinalIgnoreCase));
        if (desiredSubreddit == null)
        {
            throw new Exception("There is no such subreddit");
        }

        Post? desiredPost = desiredSubreddit.Posts.FirstOrDefault(
            p => p.Id == postId);
        if (desiredPost == null)
        {
            throw new Exception("There is no such post in this subreddit");
        }

        return Task.FromResult(desiredPost.Comments.Count);
    }

    public Task<Comment> Get(string subreddit, int postId, int commentId)
    {
        Subreddit? existingSub =
            context.Subreddits.FirstOrDefault(s => s.Title.Equals(subreddit, StringComparison.OrdinalIgnoreCase));
        if (existingSub == null)
        {
            throw new Exception("This subreddit does not exist");
        }

        Post? existingPost =
            existingSub.Posts.FirstOrDefault(p => p.Id == postId);

        if (existingPost == null)
        {
            throw new Exception("There is not a post with that id.");
        }

        Comment? existingComment = existingPost.Comments.FirstOrDefault(c => c.Id == commentId);
        if (existingComment == null)
        {
            throw new Exception("No such comment");
        }

        return Task.FromResult(existingComment);
    }

    public Task<Comment> UpvoteComment(Comment comment)
    {
        comment.NumUpvotes++;
        context.SaveChanges();
        return Task.FromResult(comment);
    }

    public Task<Comment> DownvoteComment(Comment comment)
    {
        comment.NumDownvotes++;
        context.SaveChanges();
        return Task.FromResult(comment);
    }
}