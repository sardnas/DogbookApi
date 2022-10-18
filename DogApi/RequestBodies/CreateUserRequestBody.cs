using DogApi.Models;
using Sakur.WebApiUtilities.BaseClasses;

namespace DogApi.RequestBodies
{
    public class CreateUserRequestBody : RequestBody
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Breed { get; set; }
        public override bool Valid { get { return !string.IsNullOrEmpty(UserName) || !string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Breed); } }

        public User GetUser() 
        {
            return new User(UserName, Email, Password, DateTime.Now);
        }
    }
}
