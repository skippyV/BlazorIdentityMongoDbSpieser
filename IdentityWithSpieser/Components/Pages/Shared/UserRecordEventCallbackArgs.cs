namespace IdentityWithSpieser.Components.Pages.Shared
{
    public class UserRecordEventCallbackArgs
    {
        public MongoDB.Bson.ObjectId UserId {  get; set; }
        public bool IsSelected { get; set; }
    }
}
