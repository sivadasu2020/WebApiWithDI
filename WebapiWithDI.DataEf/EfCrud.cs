using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebapiWithDI.Abstractions;
using WebapiWithDI.Common;

namespace WebapiWithDI.DataEf
{
  public  class EfCrud:IDal
    {
        DemoDbContext _demoDbContext;
        public EfCrud(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
      public  List<Product> GetProducts()
        {
           

          var   products = _demoDbContext.Products.ToList();


            return products;

        }
        public bool Create(Product product)
        {
            bool res = false;
            try
            {
            _demoDbContext.Products.Add(product);
           _demoDbContext.SaveChanges();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

        }
        public bool Update(Product product)
        {

            bool res = false;
            try
            {
                _demoDbContext.Products.Update(product);
                _demoDbContext.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {

                res = false;
            }
            return res;

        }
        public bool Delete(int id)
        {

            bool res = false;
            try
            {
                var product = _demoDbContext.Products.Where(x => x.Id == id).FirstOrDefault();
                _demoDbContext.Products.Remove(product);
                _demoDbContext.SaveChanges();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

        }
    }
}
