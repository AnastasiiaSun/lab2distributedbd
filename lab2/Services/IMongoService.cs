using lab2.Services;

namespace Lab2.Services
{
    public interface IMongoService
    {
        Task CreateAsync(StudentMongo studentDto);
        Task<List<StudentMongo>> GetAllAsync();
        Task<StudentMongo> GetByIdAsync(string id);
    }
}