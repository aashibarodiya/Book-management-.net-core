using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public partial class SimpleUserService : IUserService
    {
        IRepository<User, string> _userRepository;
        IRepository<Role, string> _roleRepository;
        IBookService _bookService;
        private IAuthorService _authorService;

        public SimpleUserService(IRepository<User,string> userRepository, 
                                IRepository<Role,string> roleRepository,
                                IBookService bookService,
                                IAuthorService authorService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _bookService = bookService;
            _authorService = authorService;
        }

        public async Task<UserInfo> AssignRoleToUser(string email, string roleName)
        {
            var user = await _userRepository.GetById(email);
            var role = await _roleRepository.GetById(roleName);

            user.Roles.Add(role);
            await _userRepository.Save();
            return CreateUserInfo(user);

        }

        private static UserInfo CreateUserInfo(User user)
        {
            return new UserInfo()
            {
                Email = user.Email,
                Name = user.Name,
                Roles = user.Roles.Select(r=> new Role() { Name=r.Name}).ToList(),
                FavoriteAuthors = user.FavoriteAuthors,
                FavoritieBooks = user.FavoritieBooks,
                ProfilePicture = user.ProfilePicture,
            };
        }

      
        public async Task<List<UserInfo>> GetAllUsers()
        {
            return (from user in (await _userRepository.GetAll())
                    select CreateUserInfo(user))
                   .ToList();
        }

        public async Task<UserInfo> GetUserByEmail(string email)
        {
            return CreateUserInfo(await _userRepository.GetById(email));
        }

        public async Task<bool> IsUserInRole(string email, string role)
        {
            var user= await _userRepository.GetById(email);

            return user.Roles.FirstOrDefault(r => r.Name == role) != null;
        }

        public async Task<UserInfo> Login(string email, string password)
        {
            try
            {
                var user=await _userRepository.GetById(email);
                if (user.Email == email && user.Password == password)
                    return CreateUserInfo( user);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                // we will handle the exception below
            }
            throw new InvalidCredentialsException("Invalid Credentials");
        }

        public async Task<UserInfo> Register(User user)
        {
            List<Role> roles= new List<Role>();
            foreach(var role in user.Roles)
            {
                var existingRole =await  _roleRepository.GetById(role.Name);
                roles.Add(existingRole);
            }
            user.Roles = roles;
            await _userRepository.Add(user);
            await _userRepository.Save();

            return CreateUserInfo(user);
        }

       
    }
}
