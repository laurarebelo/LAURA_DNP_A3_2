using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private IUserDao userDao;
    private IPostDao postDao;
    private ISubredditDao subredditDao;

    public PostLogic(IUserDao userDao, IPostDao postDao, ISubredditDao subredditDao)
    {
        this.userDao = userDao;
        this.postDao = postDao;
        this.subredditDao = subredditDao;
    }

    public Task<Post> GetById(int postId)
    {
        return postDao.GetByIdAndSubreddit("lol", postId);
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? userPoster = await userDao.GetByUsername(dto.Username);
        if (userPoster == null)
        {
            throw new Exception("That user dont exist");
        }

        Subreddit? existingSubreddit = await subredditDao.GetByTitle(dto.Subreddit);
        if (existingSubreddit == null)
        {
            throw new Exception("No such subreddit");
        }

        // int id = postDao.GetNextPostId(dto.Subreddit).Result;
        int id = 0;
        Post newPost = new Post(id, dto.Title, dto.Body, userPoster, existingSubreddit);
        Post created = await postDao.Create(newPost);
        return created;
    }

    public async Task<Post> UpvotePost(PostVoteDto dto)
    {
        Post? whichPost = await postDao.GetByIdAndSubreddit(dto.subreddit, dto.postId);
        if (whichPost == null)
        {
            throw new("There is no such post");
        }

        return await postDao.UpvotePost(whichPost);
    }

    public async Task<Post> DownvotePost(PostVoteDto dto)
    {
        Post? whichPost = await postDao.GetByIdAndSubreddit(dto.subreddit, dto.postId);
        if (whichPost == null)
        {
            throw new("There is no such post");
        }

        return await postDao.DownvotePost(whichPost);
    }

    public async Task<List<PostBrowseDto>> GetAllPostTitles(string subreddit)
    {
        return await postDao.GetAllPostTitles(subreddit);
    }

    public Task<IEnumerable<Post>> Get(PostSearchParameters searchParams)
    {
        return postDao.Get(searchParams);
    }
}