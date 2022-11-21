using Domain;
using Domain.DTOs;

namespace Application.DAOInterfaces;

public interface IPostDao
{
    public Task<Post> Create(Post post);
    public Task<int> GetNextPostId(String subreddit);
    public Task<Post> GetByIdAndSubreddit(string subreddit, int id);
    public Task<Post> UpvotePost(Post post);
    public Task<Post> DownvotePost(Post post);
    public Task<List<PostBrowseDto>> GetAllPostTitles(string subreddit);
    public Task<IEnumerable<Post>> Get(PostSearchParameters searchParams);
}