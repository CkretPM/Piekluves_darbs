using MongoDB.Bson;
using MongoDB.Driver;

namespace Piekļuves_darbs_2
{
    public class DatabaseManager
    {
        private IMongoCollection<User> _userCollection;

        public DatabaseManager(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _userCollection = database.GetCollection<User>(collectionName);
        }

        public void AddPoints(string userId, int points)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<User>.Update.Inc(u => u.Points, points);
            _userCollection.UpdateOne(filter, update);
        }

        public int GetPoints(string userId)
        {
            var user = _userCollection.Find(u => u.UserId == userId).FirstOrDefault();
            return user != null ? user.Points : 0;
        }

        public bool SpendPoints(string userId, int pointsToSpend)
        {
            var user = _userCollection.FindOneAndUpdate(
                Builders<User>.Filter.And(
                    Builders<User>.Filter.Eq(u => u.UserId, userId),
                    Builders<User>.Filter.Gte(u => u.Points, pointsToSpend)
                ),
                Builders<User>.Update.Inc(u => u.Points, -pointsToSpend)
            );

            return user != null;
        }
    }
}

