namespace DogApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
        public string Message  { get; set; }

    }
}
