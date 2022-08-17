using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InstagramMVC.Services
{
    public class ProfileService : IProfileService
    {
        private readonly InstagramContext _users;
        private IMemoryCache _cache;

        public ProfileService(InstagramContext users, IMemoryCache cache)
        {
            _users = users;
            _cache = cache;
        }

        public async Task<User> GetUser(int userId)
        {
            User user;
            if (!_cache.TryGetValue(userId, out user))
            {
                user = await _users.Users
                    .Include(u => u.Subscribers)
                    .Include(u => u.Followers)
                    .ThenInclude(u => u.Publications)
                    .Include(u => u.Publications)
                    .ThenInclude(u => u.Likes)
                    .ThenInclude(u => u.User)
                    .Include(u => u.Publications)
                    .ThenInclude(u => u.Comments)
                    .FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    _cache.Set(user.Id, user,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5)));
                }
            }
            
            return user;
            
        }

        public async Task AddPublication(Publication publication, int userId)
        {
            User user = await _users.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is not null)
            {
                user.Publications.Add(publication);
                _users.Publications.Add(publication); 
               int status =  await _users.SaveChangesAsync();
               if (status > 0)
               {
                   _cache.Set(user.Id, user, new MemoryCacheEntryOptions
                   {
                       AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                   });
               }
            }
        }
        
        public async Task<List<User>> SearchUsers(string value)
        {
            List<User> values = await _users.Users.Include(u => u.Publications).ToListAsync();
            List<User> users = values.Where(u => u.NormalizedEmail == value.ToUpper()).ToList();
            if (users.Count != 0)
                return users;

            users = values.Where(u => String.Equals(u.Name, value, StringComparison.CurrentCultureIgnoreCase)).ToList();
            if (users.Count != 0)
                return users;

            users = values.Where(u => u.NormalizedUserName == value.ToUpper()).ToList();
            return users;
        }
    }
}