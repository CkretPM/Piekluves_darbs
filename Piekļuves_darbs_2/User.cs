using MongoDB.Bson;

namespace Piekļuves_darbs_2
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public int Points { get; set; }
    }
}