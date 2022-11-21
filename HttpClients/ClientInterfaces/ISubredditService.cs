using Domain;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ISubredditService
{
    Task<Subreddit> Create(SubredditCreationDto dto);
    Task<IEnumerable<String>> GetSubreddits();
}