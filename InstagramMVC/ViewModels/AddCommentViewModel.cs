using InstagramMVC.Models;

namespace InstagramMVC.ViewModels
{
    public class AddCommentViewModel
    {
        public Publication Publication { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}