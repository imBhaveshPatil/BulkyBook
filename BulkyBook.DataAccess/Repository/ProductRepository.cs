using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var prodObj = _db.Products.FirstOrDefault(p => p.Id == obj.Id);

            if (prodObj != null) 
            { 
                prodObj.Title = obj.Title;
                prodObj.Description = obj.Description;
                prodObj.CategoryId = obj.CategoryId;
                prodObj.Price = obj.Price;
                prodObj.ListPrice = obj.ListPrice;
                prodObj.Author = obj.Author;
                prodObj.ISBN = obj.ISBN;
                prodObj.CoverTypeId = obj.CoverTypeId;

                if (prodObj.ImgUrl != null)
                {
                    prodObj.ImgUrl = obj.ImgUrl;
                }
            }
            
        }
    }
}
