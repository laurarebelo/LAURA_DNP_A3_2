using Application.DAOInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private RedditContext context;

    public UserEfcDao(RedditContext context)
    {
        this.context = context;
    }
    public async Task<User?> GetByUsername(string username)
    {
        User? existing = await context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()))!;
        return existing;    }

    public async Task<User> Create(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return context.Users;
    }
}