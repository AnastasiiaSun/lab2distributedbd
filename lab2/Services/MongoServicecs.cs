using lab2.DAL.Settings;
using lab2.DAL.Settings;
using lab2.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Lab2.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<StudentMongo> _studentsCollection;

        public MongoService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<StudentMongo>("Students");
        }

        public async Task CreateAsync(StudentMongo studentDto)
        {
            await _studentsCollection.InsertOneAsync(studentDto);
        }

        public async Task<List<StudentMongo>> GetAllAsync()
        {
            return await _studentsCollection.Find(student => true).ToListAsync();
        }

        public async Task<StudentMongo> GetByIdAsync(string id)
        {
            return await _studentsCollection.Find(student => student.Id == id).FirstOrDefaultAsync();
        }
    }
}