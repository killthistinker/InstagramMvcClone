using System;
using System.Linq;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InstagramMVC.Services
{
    public class LikeService : ILikeService
    {
        private readonly InstagramContext _context;
        private readonly IMemoryCache _cache;

        public LikeService(InstagramContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public Publication GetPublication(int publicationId)
        {
            Publication publication;
            if (!_cache.TryGetValue(publicationId, out publication))
            {
                publication = _context.Publications
                    .Include(u => u.User)
                    .Include(u => u.Comments)
                    .ThenInclude(u => u.User)
                    .Include(u => u.Likes)
                    .ThenInclude(u => u.User)
                    .FirstOrDefault(u => u.Id == publicationId);
                if (publication != null)
                {
                    _cache.Set(publication.Id, publication,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return publication;
        }

        public void Like(int publicationId, int authenticatedId)
        {
            var authenticatedUser = _context.Users.FirstOrDefault(u => u.Id == authenticatedId);
            if (authenticatedUser is null)
                return;
            var publication = _context.Publications
                .Include(p => p.Likes)
                .ThenInclude(u => u.User)
                .Include(u => u.User)
                .Include(u => u.Comments)
                .ThenInclude(u => u.User)
                .FirstOrDefault(p => p.Id == publicationId);
            if(publication is null)
                return;
            
            Like liker = new Like
            {
                User = authenticatedUser,
                UserId = authenticatedUser.Id
            };
            if (publication.Likes.Any(p => p.UserId == liker.UserId))
            {
                var oldLike = publication.Likes.FirstOrDefault(p => p.UserId == liker.UserId);
                publication.Likes.Remove(oldLike);
            }
            else
            {
                _context.Likes.Add(liker);
                publication.Likes.Add(liker);
            }
            
           int cache = _context.SaveChanges();
           if (cache > 0)
           {
               _cache.Set(publication.Id, publication, new MemoryCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
               });
           }
        }
    }
}