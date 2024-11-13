using System.Transactions;
using lab2.DAL;
using lab2.DAL.Settings;
using lab2.Services;
using Lab2.Services;

namespace Lab2.Services;

public class UpdateService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var mongoService = scope.ServiceProvider.GetRequiredService<IMongoService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<Lab2DbContext>();

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var students = dbContext.Students.ToList();

            foreach (var student in students)
            {
                var studentDto = new StudentMongo
                {
                    Name = student.FirstName,
                    GroupNumber = student.GroupNumber
                };

                studentDto.Id = $"{student.FirstName}-{student.GroupNumber}";

                await mongoService.CreateAsync(studentDto);
            }

            transaction.Complete();
        }
        catch (Exception ex)
        {
            transaction.Dispose(); 
            throw new Exception("Transaction failed", ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}