using System;
using System.Linq;
using InstagramMVC.Helpers;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using InstagramMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InstagramMVC.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly InstagramContext _context;
        private readonly IMemoryCache _cache;
     

        public PublicationService(InstagramContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public Publication GetPublication(int id)
        {
            Publication publication;
            if (!_cache.TryGetValue(id, out publication))
            {
                publication =  _context.Publications
                    .Include(u => u.User)
                    .Include(p => p.Likes)
                    .ThenInclude(u => u.User)
                    .Include(p => p.Comments)
                    .ThenInclude(p => p.User)
                    .FirstOrDefault(p => p.Id == id);
                if (publication != null)
                {
                    _cache.Set(publication.Id, publication,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(5)));
                }
            }
            
            return publication;
        }

        public void AddComment(int userId, AddCommentViewModel model)
        {
            var user = _context.Users
                .Include(u => u.Publications)
                .ThenInclude(u => u.Comments).FirstOrDefault(u => u.Id == userId);
            if(user is null)
                return;
            var publication = _context.Publications
                    .Include(u => u.User)
                    .Include(p => p.Likes)
                    .ThenInclude(u => u.User)
                    .Include(p => p.Comments)
                    .ThenInclude(p => p.User)
                    .FirstOrDefault(p => p.Id == model.Comment.PublicationId);
            if(publication is null)
                return;
            
            Comment comment = model.MapToComent(user);
            if(comment is null) return;
            
            publication.Comments.Add(comment);
            _context.Comments.Add(comment);
           int cach =  _context.SaveChanges();
           if (cach > 0)
           {
               _cache.Set(publication.Id, publication, new MemoryCacheEntryOptions
               {
                   AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
               });
           }
        }

        public User GetFollowersPublications(int userId)
        {
            var users = _context.Users
                .Include(u => u.Followers)
                .ThenInclude(u => u.Publications.OrderBy(p =>  p.DateCreated))
                .ThenInclude(u => u.Likes)
                .ThenInclude(u => u.User)
                .Include(u => u.Followers)
                .ThenInclude(u => u.Publications)
                .ThenInclude(u => u.Comments)
                .FirstOrDefault(u => u.Id == userId);
            return users;
        }

        public void DeletePublication(int publicationId)
        {
            var publication = GetPublication(publicationId);
            if (publication == null)
                return;
            _context.Publications.Remove(publication);
            _context.SaveChanges();
        }

        public void EditPublication(int publicationId, string description)
        {
            var publication = GetPublication(publicationId);
            if (publication == null)
                return;
            publication.Description = description;
            _context.Publications.Update(publication);
            _context.SaveChanges();
        }
        
    }
}