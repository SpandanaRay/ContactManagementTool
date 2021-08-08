using DAL;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Repositories.Implementations
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactDatabaseContext context
        {
            get
            {
                return _dbContext as ContactDatabaseContext;
            }
        }
        public ContactRepository(DbContext _db) : base(_db)
        {

        }
         PagingModel<ContactModel> IContactRepository.GetContacts(int page, int pageSize)
        {
            var data = (from cont in context.Contact                       
                        orderby cont.ContactId
                        select new ContactModel
                        {
                            ContactId = cont.ContactId,
                            Name = cont.Name,
                            Designation = cont.Designation,
                            Mobile = cont.Mobile,
                            Country = cont.Country,
                            Gender = cont.Gender,
                            Image=cont.Image
                        });
            int count = data.Count();
            var dataList = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PagingModel<ContactModel> model = new PagingModel<ContactModel>();
            if (dataList.Count > 0)
            {
                model.Data = new StaticPagedList<ContactModel>(dataList, page, pageSize, count);
                model.Page = page;
                model.PageSize = pageSize;
                model.TotalPages = count;
            }
            return model;
        }
    }
}
