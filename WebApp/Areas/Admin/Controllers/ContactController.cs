using DAL;
using DAL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        //private readonly ContactDatabaseContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        IUnitOfWork dbContext;      

        public ContactController(IUnitOfWork context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 2)
        {
            PagingModel<ContactModel> model = dbContext.ContactRepo.GetContacts(page, pageSize);
            return View(model); 
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactViewModel model)
        {
            ModelState.Remove("ContactId");
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Contact contact = new Contact
                {
                     Name = model.Name,                    
                    Gender = model.Gender,
                    Designation = model.Designation,
                    Country = model.Country,
                    Mobile = model.Mobile,
                    Image = uniqueFileName,
                };
                dbContext.ContactRepo.Add(contact);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ContactViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Edit(int Id)
        {
            var data = dbContext.ContactRepo.Find(Id);
            ContactViewModel model = new ContactViewModel()
            {
                ContactId = data.ContactId,
                Name = data.Name,
                Designation = data.Designation,
                Mobile = data.Mobile,
                Country = data.Country,
                Gender = data.Gender,
                ExistingImage=data.Image
            };
            return View("Create", model);
        }
        [HttpPost]
        public IActionResult Edit(int Id, ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    ContactId=model.ContactId,
                    Name = model.Name,
                    Gender = model.Gender,
                    Designation = model.Designation,
                    Country = model.Country,
                    Mobile = model.Mobile,
                    
                };
                if (model.ProfileImage != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    contact.Image = UploadedFile(model);
                }
                else
                {
                    contact.Image = model.ExistingImage;
                }
                dbContext.ContactRepo.Update(contact);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", model);             
        }

        public IActionResult Delete(int Id)
        {
            dbContext.ContactRepo.Delete(Id);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
} 
 