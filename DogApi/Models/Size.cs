namespace DogApi.Models
{
    public class Size
    {
        public int Height { get; set; }
        public int Weight { get; set; }

        public static Size CreateRandom(Random random)
        {
            return new Size()
            {
                Height = random.Next(103)+10,
                Weight = random.Next(50)+1      
            };
        }
    }

}
