using DAL;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        ContactDatabaseContext _db;
        public UnitOfWork(ContactDatabaseContext db)
        {
            _db = db;
        }

        private IContactRepository _ContactRepo;
        public IContactRepository ContactRepo
        {
            get
            {
                if (_ContactRepo == null)
                    _ContactRepo = new ContactRepository(_db);
                return _ContactRepo;
            }
        }
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }


}
