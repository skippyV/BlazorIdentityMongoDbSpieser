using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace IdentityWithSpieser.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<ObjectId>
    {
        public ApplicationUser() : base()
        {
        }
        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
    }
}
