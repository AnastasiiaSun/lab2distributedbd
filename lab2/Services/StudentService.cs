using lab2.DAL.Settings;
using lab2.DAL;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace lab2.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<StudentMongo> _studentsCollection;

        public StudentService(IOptions<MongoDBSettings> mongoDBSettings, IMongoClient mongoClient)
        {
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentsCollection = mongoDatabase.GetCollection<StudentMongo>("Students");
        }

        public async Task<List<StudentMongo>> GetAsync() =>
            await _studentsCollection.Find(s => true).ToListAsync();

        public async Task<StudentMongo> GetByIdAsync(string id) =>
            await _studentsCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(StudentMongo student) =>
            await _studentsCollection.InsertOneAsync(student);

        public async Task UpdateAsync(string id, StudentMongo updatedStudent) =>
            await _studentsCollection.ReplaceOneAsync(s => s.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentsCollection.DeleteOneAsync(s => s.Id == id);
    }
}
