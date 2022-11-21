using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    public Task<Comment> CreateAsync(CommentCreationDto dto);
    public Task<Comment> UpvoteComment(CommentVoteDto dto);
    public Task<Comment> DownvoteComment(CommentVoteDto dto);
}