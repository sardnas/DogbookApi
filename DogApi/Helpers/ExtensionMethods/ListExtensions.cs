namespace DogApi.Helpers.ExtensionMethods
{
    public static class ListExtensions
    {
        public static string GetRandom(this List<string> list, Random random)
        {
            return list[random.Next(list.Count - 1)];
        }
    }
}
