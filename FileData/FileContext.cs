using System.Text.Json;
using Domain;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? DataContainer;

    public ICollection<Subreddit> Subreddits
    {
        get
        {
            LoadData();
            return DataContainer!.Subreddits;
        }
    }
    
    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return DataContainer!.Users;
        }
    }
   

    private void LoadData()
    {
        if (DataContainer != null) return;

        if (!File.Exists(FilePath))
        {
            DataContainer = new()
            {
                Subreddits = new List<Subreddit>(),
                Users = new List<User>()
            };
            return;
        }

        string content = File.ReadAllText(FilePath);
        DataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(DataContainer);
        File.WriteAllText(FilePath, serialized);
        DataContainer = null;
    }
}