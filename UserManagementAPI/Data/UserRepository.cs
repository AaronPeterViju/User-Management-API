using UserManagementAPI.Models;

namespace UserManagementAPI.Data
{
    public class UserRepository
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Role = "Admin", CreatedAt = DateTime.UtcNow },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Role = "User", CreatedAt = DateTime.UtcNow }
        };

        private static int _nextId = 3;

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User AddUser(User user)
        {
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;
            _users.Add(user);
            return user;
        }

        public User? UpdateUser(int id, User updatedUser)
        {
            var existingUser = GetUserById(id);
            if (existingUser == null)
                return null;

            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Role = updatedUser.Role;
            return existingUser;
        }

        public bool DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user == null)
                return false;

            _users.Remove(user);
            return true;
        }
    }
}
