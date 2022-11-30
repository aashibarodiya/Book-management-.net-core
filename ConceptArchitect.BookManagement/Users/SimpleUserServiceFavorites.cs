using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    partial class SimpleUserService
    {
        public async Task<FavoriteBook> AddFavoriteBook(FavoriteBook favBook)
        {
            var user = await GetUserByEmail(favBook.UserEmail);
            user.FavoritieBooks.Add(favBook);
            await _userRepository.Save();
            return favBook;
        }

        public async Task RemoveFavoriteBook(string userEmail, int favId)
        {
            var user= await GetUserByEmail(userEmail);
            var fav = user.FavoritieBooks.FirstOrDefault(b => b.Id == favId);
            if (fav != null)
                user.FavoritieBooks.Remove(fav);



        }

        public async Task UpdateFavoriteBook(FavoriteBook book)
        {
            var user = await GetUserByEmail(book.UserEmail);
            var existing = user.FavoritieBooks.FirstOrDefault(b => b.Id == book.Id);
            if (existing != null)
            {
                existing.ReadingStatus = book.ReadingStatus;
                existing.Notes = book.Notes;
                await _userRepository.Save();
            }
            
        }

        public async Task<List<FavoriteBook>> GetAllFavoriteBooks(string userId)
        {
            var user = await GetUserByEmail(userId);
            return user.FavoritieBooks.ToList();
        }
    }
}
