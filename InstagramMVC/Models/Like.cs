namespace InstagramMVC.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}