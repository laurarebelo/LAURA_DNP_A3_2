using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<Comment> Upvote(CommentVoteDto dto);
    Task<Comment> Downvote(CommentVoteDto dto);
}