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

        public async Task UpdateUserAsync(User user)
        {
            await _usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        }

    }

    // Class representing the User data model
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        // Add AccountBalance field
        [BsonElement("accountBalance")]
        public double AccountBalance { get; set; } = 0.0;

        // Add RecentTransactions field
        [BsonElement("recentTransactions")]
        public List<Transaction> RecentTransactions { get; set; } = new List<Transaction>();
    }

    public class Transaction
    {
        [BsonElement("date")]
        public string Date { get; set; } = string.Empty;

        [BsonElement("amount")]
        public double Amount { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
    }
}
