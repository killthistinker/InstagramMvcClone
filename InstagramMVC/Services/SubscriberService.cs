using System.Linq;
using InstagramMVC.Models;
using InstagramMVC.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InstagramMVC.Services
{
    public class SubscriberService : ISubscribeService
    {
        private readonly InstagramContext _context;

        public SubscriberService(InstagramContext context)
        {
            _context = context;
        }

        public void Subscribe(int subscriberId, int userId)
        { 
            var subscriber = _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Subscribers)
                .FirstOrDefault(u => u.Id == subscriberId);
            if (subscriber is null) return;
            
            var user = _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Subscribers)
                .FirstOrDefault(u => u.Id == userId); 
            if (user is null) return;

            if (user.Subscribers.Any(u => u.UserName == subscriber.UserName))
            {
                Unsubscribe(subscriber, user);
                return;
            }
           
            if (user.Id == subscriberId) return;
            subscriber.Followers.Add(user);
            user.Subscribers.Add(subscriber);
            _context.SaveChanges();
        }

        
        private void Unsubscribe(User subscriber, User user)
        {
            subscriber.Followers.Remove(user);
            user.Subscribers.Remove(subscriber);
            _context.SaveChanges();
        }
    }
}