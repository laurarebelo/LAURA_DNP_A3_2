using Application.DAOInterfaces;
using Domain;
using Domain.DTOs;

namespace FileData.DAOs;

public class PostDao : IPostDao
{
    private readonly FileContext context;

    public PostDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> Create(Post post)
    {
        Subreddit? belongsTo = context.Subreddits.FirstOrDefault(subreddit =>
            subreddit.Title.Equals(post.Subreddit.Title, StringComparison.OrdinalIgnoreCase));
        if (belongsTo == null)
        {
            throw new Exception("This subreddit does not exist.");
        }

        belongsTo.AddPost(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<int> GetNextPostId(string subreddit)
    {
        Subreddit? belongsTo = context.Subreddits.FirstOrDefault(s =>
            s.Title.Equals(subreddit, StringComparison.OrdinalIgnoreCase));
        if (belongsTo == null)
        {
            throw new Exception("That subreddit does not exist.");
        }

        return Task.FromResult(belongsTo.Posts.Count);
    }

    public Task<Post> GetByIdAndSubreddit(string subreddit, int id)
    {
        Subreddit? existingSub =
            context.Subreddits.FirstOrDefault(s => s.Title.Equals(subreddit, StringComparison.OrdinalIgnoreCase));
        if (existingSub == null)
        {
            throw new Exception("This subreddit does not exist");
        }

        Post? existingPost =
            existingSub.Posts.FirstOrDefault(p => p.Id == id);

        if (existingPost == null)
        {
            throw new Exception("There is not a post with that id.");
        }

        return Task.FromResult(existingPost);
    }

    public Task<Post> UpvotePost(Post post)
    {
        post.Upvote();
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<Post> DownvotePost(Post post)
    {
        post.Downvote();
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<List<PostBrowseDto>> GetAllPostTitles(string subreddit)
    {
        List<PostBrowseDto> allPostTitles = new List<PostBrowseDto>();
        Subreddit? subredditObj =
            context.Subreddits.FirstOrDefault(s => s.Title.Equals(subreddit, StringComparison.OrdinalIgnoreCase));
        if (subredditObj == null)
        {
            throw new Exception("There is no such subreddit.");
        }

        foreach (var post in subredditObj.Posts)
        {
            PostBrowseDto item = new PostBrowseDto(post.Title, post.Id);
            allPostTitles.Add(item);
        }

        return Task.FromResult(allPostTitles);
    }

    public Task<Post> GetPostById(string subreddit, int postId)
    {
        return GetByIdAndSubreddit(subreddit, postId);
    }

    public Task<IEnumerable<Post>> Get(PostSearchParameters searchParams)
    {
        Subreddit? subredditInQuestion = context.Subreddits.FirstOrDefault(s =>
            s.Title.Equals(searchParams.subreddit, StringComparison.OrdinalIgnoreCase));

        if (subredditInQuestion == null)
        {
            throw new Exception("There is no such subreddit.");
        }

        IEnumerable<Post> posts = subredditInQuestion.Posts;

        if (searchParams.postId != null)
        {
            posts = posts.Where(p => p.Id == searchParams.postId);
        }

        return Task.FromResult(posts);
    }
}