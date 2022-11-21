using Application.DAOInterfaces;
using Domain;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private RedditContext context;

    public PostEfcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<Post> Create(Post post)
    {
        EntityEntry<Post> newPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public Task<int> GetNextPostId(string subreddit)
    {
        return Task.FromResult(context.Posts.OrderBy(p => p.Id).Last().Id + 1);
    }

    public async Task<Post> GetByIdAndSubreddit(string subreddit, int id)
    {
        Post? post = await context.Posts.FindAsync(id);
        if (post == null)
        {
            throw new Exception("No such post.");
        }

        return post;
    }

    public async Task<Post> UpvotePost(Post post)
    {
        Post? p = await context.Posts.FindAsync(post.Id);
        p.Upvote();
        await context.SaveChangesAsync();
        return p;
    }

    public async Task<Post> DownvotePost(Post post)
    {
        Post? p = await context.Posts.FindAsync(post.Id);
        p.Downvote();
        await context.SaveChangesAsync();
        return p;
    }

    public async Task<List<PostBrowseDto>> GetAllPostTitles(string subreddit)
    {
        List<PostBrowseDto> list = new List<PostBrowseDto>();

        var posts = context.Posts.Where(p => p.Subreddit.Title.ToLower().Equals(subreddit));

        foreach (var p in posts)
        {
            list.Add(new PostBrowseDto(p.Title, p.Id));
        }

        return list;
    }

    public Task<IEnumerable<Post>> Get(PostSearchParameters searchParams)
    {
        IEnumerable<Post> posts = context.Posts.Where(
            p => p.Subreddit.Title.ToLower().Equals(searchParams.subreddit.ToLower())).Include(p => p.Comments).Include(p => p.User);

        if (searchParams.postId != null)
        {
            posts = posts.Where(p => p.Id == searchParams.postId);
        }
        return Task.FromResult(posts);    }
}