using Application.DAOInterfaces;
using Domain;

namespace FileData.DAOs;

public class UserDao : IUserDao
{
    private readonly FileContext context;

    public UserDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User?> GetByUsername(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
    

    public Task<User> Create(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        return Task.FromResult<IEnumerable<User>>(context.Users);
    }
}