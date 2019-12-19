using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _Context;
        public DatingRepository(DataContext context)
        {
            this._Context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _Context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photo = await _Context.Photos.Where(p => p.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);

            return photo;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _Context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _Context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _Context.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _Context.SaveChangesAsync() > 0;
        }
    }
}