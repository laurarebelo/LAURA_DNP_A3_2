using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
  public Task<Post> GetById(int postId);
  public Task<Post> CreateAsync(PostCreationDto dto);
  public Task<Post> UpvotePost(PostVoteDto dto);
  public Task<Post> DownvotePost(PostVoteDto dto);
  public Task<List<PostBrowseDto>> GetAllPostTitles(string subreddit);
  public Task<IEnumerable<Post>> Get(PostSearchParameters searchParams);
}