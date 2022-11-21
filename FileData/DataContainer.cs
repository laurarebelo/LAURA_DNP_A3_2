using Domain;

namespace FileData;

public class DataContainer
{
    public ICollection<Subreddit> Subreddits { get; set; }
    public ICollection<User> Users { get; set; }
}