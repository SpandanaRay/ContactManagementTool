using DAL;
using System.Collections.Generic;
using WebApp.Controllers;
using Moq;
using Repositories; 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;

namespace WebAppUnitTest
{
    [TestClass]
    public class ProductControllerUnitTest
    {
        //A: Arrangement
        Product p1;
        Product p2;
        Product p3;
        Category c1;
        Category c2;

        List<Product> productList;
        List<Category> categoryList;
        ProductController ctrl;
        Mock<IUnitOfWork> uow;
        public ProductControllerUnitTest()
        {
            p1 = new Product { ProductId = 1, Name = "MVC Book", Description = "MVC Book", CategoryId = 1 };
            p2 = new Product { ProductId = 2, Name = "Azure Book", Description = "Azure Book", CategoryId = 1 };
            p3 = new Product { ProductId = 3, Name = "Angular Training", Description = "Angular Book", CategoryId = 2 };

            c1 = new Category { CategoryId = 1, Name = "Books" };
            c2 = new Category { CategoryId = 2, Name = "Courses" };
            productList = new List<Product>();
            productList.Add(p1);
            productList.Add(p2);
            categoryList = new List<Category>();
            categoryList.Add(c1);
            categoryList.Add(c2);

            uow = new Mock<IUnitOfWork>();
            ctrl = new ProductController(uow.Object);
        }
        [TestMethod]
        public void TestList()
        {
            //setup
            uow.Setup(u => u.ProductRepo.GetAll()).Returns(productList);

            //action
            var result = ctrl.List() as ViewResult;

            var model = result.Model as List<Product>;
            //assert
            CollectionAssert.Contains(model, p1);
            CollectionAssert.Contains(model, p2);

            CollectionAssert.DoesNotContain(model, p3);
        }
        [TestMethod]
        public void TestCreatePost()
        {
            //setup
            uow.Setup(u => u.ProductRepo.Add(p3)).Callback((Product p) =>
            {
                productList.Add(p);
            });

            //action
            var result = ctrl.Create(p3) as RedirectToActionResult;

            //assert
            CollectionAssert.Contains(productList, p3);
            Assert.AreEqual("Index", result.ActionName);
        }
        [TestMethod]
        public void TestCreatePostValidationFailed()
        {
            p3.Name = null;

            //setup
            uow.Setup(u => u.CategoryRepo.GetAll()).Returns(categoryList);

            //action
            var result = ctrl.Create(p3) as ViewResult;

            //assert
            Assert.AreEqual(null, result.ViewName);
        }



    }

}
