using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;
    
    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsername(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        User toCreate = new User
        {
            Username = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            Role = dto.Role
        };
    
        User created = await userDao.Create(toCreate);
    
        return created;
    }

    public Task<IEnumerable<User>> GetAllUsers()
    {
        return userDao.GetAllUsers();
    }
}