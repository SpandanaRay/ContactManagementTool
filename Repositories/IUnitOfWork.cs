using Repositories.Interfaces;

namespace Repositories
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepo { get; }
        int SaveChanges();
    }


}
