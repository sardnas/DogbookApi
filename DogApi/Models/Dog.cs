namespace DogApi.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DogBreed DogBreed { get; set; }
        public Profile Profile { get; set; }
    }
}
