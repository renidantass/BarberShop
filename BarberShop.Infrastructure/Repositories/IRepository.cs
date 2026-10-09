namespace BarberShop.Infrastructure.Repositories;

public interface IRepository<T>
{
    public void Add(T entity);
    public void Delete(T entity);
    public void Update(T entity);
}