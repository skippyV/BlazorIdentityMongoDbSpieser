using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace IdentityWithSpieser.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<ObjectId>
    {
        public ApplicationRole() : base()
        {
        }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
