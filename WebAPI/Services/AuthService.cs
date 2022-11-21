using System.ComponentModel.DataAnnotations;
using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogic userLogic;
    private IList<User> users;

    public AuthService(IUserLogic userLogic)
    {
        Console.WriteLine("Created AuthService");
        this.userLogic = userLogic;
        users = userLogic.GetAllUsers().Result.ToList();
    }

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }

        userLogic.CreateAsync(new UserCreationDto(user.Username, user.Password, user.Email, user.Role));
        
        return Task.CompletedTask;
    }
}