using DogApi.Helpers.ExtensionMethods;
using DogApi.Models;

namespace DogApi.Managers
{
    public class DogManager
    {
        private List<string> breeds = new List<string>();
        private List<string> personalities = new List<string>();
        private List<string> exercises = new List<string>();
        private List<string> groomings = new List<string>();
        private List<string> names = new List<string>();
        private List<string> images = new List<string>();

        private Random random = new Random();
        public DogManager() // konstruktor
        {
            breeds.Add("Svensk Fjällnos");
            breeds.Add("Fågelhund");
            breeds.Add("Sankt Vilgot");
            breeds.Add("Gammal Dansk Gethund");

            personalities.Add("Social");
            personalities.Add("Kärleksfull");
            personalities.Add("Ensamvarg");
            personalities.Add("Jakthund");
            personalities.Add("Hopplös romantiker");
            personalities.Add("Manisk");
            personalities.Add("Nyfiken");

            exercises.Add("Minst 2 h promenad per dag");
            exercises.Add("Minst 2 h promenad per dag och träning några gånger i veckan");
            exercises.Add("Mycket energi, behöver springa lös");
            exercises.Add("Soffpotatis");

            groomings.Add("Långhårig, borstas dagligen");
            groomings.Add("Nakenhund");
            groomings.Add("Borstas en gång i veckan");

            names.Add("Lillen");
            names.Add("Kajsa");
            names.Add("Bullen");
            names.Add("Herman");
            names.Add("Göran");
            names.Add("Rickard");
            names.Add("Josefine");
            names.Add("Leonardo Da Vinci");

            images.Add("bild 1");
        }

        public DogBreed GetBreed()
        {
            return new DogBreed()
            {
                Exercise = exercises.GetRandom(random),
                Grooming = groomings.GetRandom(random),
                Name = breeds.GetRandom(random),
                Temperament = personalities.GetRandom(random),
                Size = Size.Create(1, 2, 3, 4),
                Image = images.GetRandom(random)
            };
        }

        public Dog GetDog(DogBreed breed)
        {
            return new Dog()
            {
                DogBreed = breed,
                Name = names.GetRandom(random),
                Profile = Profile.CreateRandom(random)
            };
        }
    }
}
