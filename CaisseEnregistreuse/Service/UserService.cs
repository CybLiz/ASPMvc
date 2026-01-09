using CaisseEnregistreuse.Data;
using CaisseEnregistreuse.Models;
using Microsoft.EntityFrameworkCore;

namespace CaisseEnregistreuse.Service
{
    public class UserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public User? GetUserById(int userId)
        {
            return _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(u => u.Id == userId);
        }

        public User? Authenticate(string email, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public User Register(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password,
                Cart = new Cart()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
        public void UpdateUserEmail(int userId, string newEmail)
        {
            var user = GetUserById(userId);
            if (user == null) return;
            user.Email = newEmail;
            _context.SaveChanges();
        }
    }
}