using ShortProject.Exceptions;
using ShortProject.Models;

namespace ShortProject.Services
{
    public class UserService
    {
        public User Login(string email, string password)
        {
            User user = DB.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
            if (user == null)
            {
                throw new NotFoundException("User not found. Please check your credentials.");
            }
            return user;
        }

        public void Register(string fullname, string email, string password)
        {
            
            if (DB.Users.Any(u => u.Email == email))
            {
                throw new InvalidOperationException("Email address is already registered.");
            }

            if (!email.Contains("@"))
            {
                throw new ArgumentException("Invalid email address format.");
            }
           

            int newUserId = DB.Users.Length > 0 ? DB.Users.Max(user => user.Id) + 1 : 1;

            
            User newUser = new User
            {
                Id = newUserId,
                Fullname = fullname,
                Email = email,
                Password = password
            };

            
            Array.Resize(ref DB.Users, DB.Users.Length + 1);
            DB.Users[DB.Users.Length - 1] = newUser;
        }

        public void AddUser(User user)
        {
            Array.Resize(ref DB.Users, DB.Users.Length + 1);
            DB.Users[DB.Users.Length - 1] = user;
        }

        
    }
}
