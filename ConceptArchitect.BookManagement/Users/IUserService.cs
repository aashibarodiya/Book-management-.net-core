using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public interface IUserService
    {
        Task<UserInfo> Register(User user);

        Task<UserInfo> Login(string email, string password);

        Task<UserInfo> AssignRoleToUser(string email, string role);

        Task<bool> IsUserInRole(string email, string role);

        Task<List<UserInfo>> GetAllUsers();
        Task<UserInfo> GetUserByEmail(string email);
        Task<Role> CreateRole(Role role);
        Task<IEnumerable<string>> GetAllRoles();

        Task<FavoriteBook> AddFavoriteBook(FavoriteBook book);
        Task RemoveFavoriteBook(string userEmail, int id);
        Task UpdateFavoriteBook(FavoriteBook book);
        Task<List<FavoriteBook>> GetAllFavoriteBooks(string userId);
    }
}
