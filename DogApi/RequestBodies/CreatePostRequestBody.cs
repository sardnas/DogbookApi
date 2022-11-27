using Sakur.WebApiUtilities.BaseClasses;

namespace DogApi.RequestBodies
{
    public class CreatePostRequestBody : RequestBody
    {
        public string Message { get; set; }  
        public override bool Valid { get { return !string.IsNullOrEmpty(Message); } }
    }
}
