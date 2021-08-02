using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebapiWithDI.Common;
using WebapiWithDI.Data;
using WebapiWithDI.DataEf;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebapiWithDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsEFController : ControllerBase
    {
        EfCrud _efCrud;
        public ProductsEFController(EfCrud efCrud)
        {
            _efCrud = efCrud;

        }
        // GET: api/<ProductsController>
        [HttpGet]
        public List<ProductModel> Get()
        {
            return GetProductsByEf();
        }

        private  List<ProductModel> GetProductsByEf()
        {
            List<ProductModel> productModels = new List<ProductModel>();
            var products = _efCrud.GetProducts();

            for (int i = 0; i < products.Count(); i++)
            {
                var dbProduct = products[i];
                ProductModel productModel = new ProductModel();
                productModel.Id = dbProduct.Id;
                productModel.Name = dbProduct.Name;
                productModel.Cost = dbProduct.Cost;
                productModel.IsAvailable = dbProduct.IsAvailable;
                productModels.Add(productModel);
            }
            return productModels;
        }

   

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            ProductModel product = new ProductModel();

            product= GetProductsByEf().Where(x => x.Id == id).FirstOrDefault();

            return product;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public bool Post([FromBody] ProductModel productModel)
        {
           
            Product product = new Product();
            product.Name = productModel.Name;
            product.Cost = productModel.Cost;
            product.IsAvailable = productModel.IsAvailable;
           return  _efCrud.Create(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut()]
        public bool Put([FromBody]ProductModel productModel)
        {
           
            Product product = new Product();
            product.Name = productModel.Name;
            product.Cost = productModel.Cost;
            product.IsAvailable = productModel.IsAvailable;
            product.Id = productModel.Id;
            return _efCrud.Update(product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
          
            return _efCrud.Delete(id);
        }
    }
}
