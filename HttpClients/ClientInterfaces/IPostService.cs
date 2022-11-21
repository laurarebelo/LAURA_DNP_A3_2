using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<IEnumerable<Post>> Get(string subreddit, int? postId);
    Task<Post> Create(PostCreationDto creationDto);
    Task<Post> Upvote(PostSearchParameters post);
    Task<Post> Downvote(PostSearchParameters post);
    Task<Comment> Comment(CommentCreationDto dto);
}