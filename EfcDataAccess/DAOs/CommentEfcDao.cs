using Application.DAOInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private RedditContext context;

    public CommentEfcDao(RedditContext context)
    {
        this.context = context;
    }
    
    
    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> newComment = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return newComment.Entity;
    }

    public Task<int> GetNextCommentId(string subreddit, int postId)
    {
        return Task.FromResult(context.Comments.OrderBy(c => c.Id).Last().Id + 1);
    }

    public Task<Comment> Get(string subreddit, int postId, int commentId)
    {
        return Task.FromResult(context.Comments.First(c => c.Id == commentId));
    }

    public Task<Comment> UpvoteComment(Comment comment)
    {
        Comment cmt = Get(comment.Subreddit.Title,comment.Post.Id,comment.Id).Result;
        cmt.Upvote(); //TODO mudar?
        context.SaveChangesAsync();
        return Task.FromResult(cmt);    }

    public Task<Comment> DownvoteComment(Comment comment)
    {
        Comment cmt = Get(comment.Subreddit.Title,comment.Post.Id,comment.Id).Result;
        cmt.Downvote(); //TODO mudar?
        context.SaveChangesAsync();
        return Task.FromResult(cmt);    }
}