namespace DogApi.Models
{
    public class Size
    {
        public int Height { get; set; }
        public int Weight { get; set; }

        public static Size Create(int minHeight, int maxHeight, int minWeight, int maxWeight)
        {
            return new Size()
            {
                Height = minHeight,
                Weight = minWeight    
            };
        }
    }

}
