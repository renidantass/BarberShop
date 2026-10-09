namespace BarberShop.Infrastructure.Repositories;

public interface IUnitOfWork
{
    public Task SaveChangesAsync();
    public void SaveChanges();
}