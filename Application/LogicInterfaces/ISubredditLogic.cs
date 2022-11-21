using Domain;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ISubredditLogic
{ 
    Task<Subreddit> CreateAsync(SubredditCreationDto dto);
    Task<Subreddit?> GetByTitle(String title);
    Task<List<string>> GetAllTitles();
    Task<IEnumerable<Subreddit>> GetAsync(SingleSearchParameterDto searchParameter);
}