using System;
using InstagramMVC.Models;
using InstagramMVC.ViewModels;

namespace InstagramMVC.Helpers
{
    public static class CommentMapper
    {
        public static Comment MapToComent(this AddCommentViewModel self, User user)
        {
            Comment comment = new Comment
            {
                User = user,
                UserId = user.Id,
                DateCreated = DateTime.Now,
                UserComment = self.Comment.UserComment,
            };
            return comment;
        }
    }
}