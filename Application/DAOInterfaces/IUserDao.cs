using Domain;

namespace Application.DAOInterfaces
{
    public interface IUserDao
    {
        public Task<User?> GetByUsername(String username);
        public Task<User> Create(User user);
        public Task<IEnumerable<User>> GetAllUsers();
    }
}