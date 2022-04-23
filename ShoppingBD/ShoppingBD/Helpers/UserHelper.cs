using Microsoft.AspNetCore.Identity;
using ShoppingBD.Data.Entities;
using ShoppingBD.Data;
using Microsoft.EntityFrameworkCore;
using ShoppingBD.Models;

namespace ShoppingBD.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _UserManager;
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly SignInManager<User> _SignInManager;



        public UserHelper(DataContext context, UserManager<User> UserManager,
            RoleManager<IdentityRole> RoleManager, SignInManager<User> SignInManager)
        {
            _context = context;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _SignInManager = SignInManager;
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _UserManager.CreateAsync(user, password);
        }

        public async Task<User> AddUserAsync(AddUserViewModel model)
        {
            User user = new User
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageId = model.ImageId,
                PhoneNumber = model.PhoneNumber,
                City = await _context.Cities.FindAsync(model.CityId),
                UserName = model.Username,
                UserType = model.UserType
            };

            IdentityResult result = await _UserManager.CreateAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }
            User newUser = await GetUserAsync(model.Username);
            await AddUserToRoleAsync(newUser, user.UserType.ToString());
            return newUser;

        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _UserManager.AddToRoleAsync(user, roleName);
        }


        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _RoleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _RoleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }


        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
                .Include(u => u.City)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _UserManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _SignInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _SignInManager.SignOutAsync();
        }

    }
}
