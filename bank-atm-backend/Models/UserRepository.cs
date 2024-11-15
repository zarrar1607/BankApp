using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;


namespace BankAtmBackend.Models
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        // Constructor to initialize the UserRepository
        public UserRepository(IOptions<MongoDbSettings> mongoDbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<User>(mongoDbSettings.Value.UserCollectionName);
        }

        // Method to get a user by username
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _usersCollection.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        // Method to get all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }
    }

    // Class representing the User data model
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Map `_id` from MongoDB to `Id` in your C# code

        [BsonElement("username")]
        public string? Username { get; set; }  // Ensure this matches MongoDB field

        [BsonElement("password")]
        public string? Password { get; set; }  // Ensure this matches MongoDB field

        [BsonElement("email")]
        public string? Email { get; set; }  // Ensure this matches MongoDB field
    }
}
