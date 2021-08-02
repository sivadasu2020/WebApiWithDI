using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebapiWithDI.Abstractions;
using WebapiWithDI.Common;
using WebapiWithDI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebapiWithDI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IDal _dal;
        public ProductsController(IDal dal)
        {
            _dal = dal;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public List<ProductModel> Get()
        {
            return GetProductsByAdo();
        }

        private  List<ProductModel> GetProductsByAdo()
        {
            List<ProductModel> productModels = new List<ProductModel>();
          

            for (int i = 0; i < _dal.GetProducts().Count(); i++)
            {
                var dbProduct = _dal.GetProducts()[i];
                ProductModel productModel = new ProductModel();
                productModel.Id = dbProduct.Id;
                productModel.Name = dbProduct.Name;
                productModel.Cost = dbProduct.Cost;
                productModel.IsAvailable = dbProduct.IsAvailable;
                productModels.Add(productModel);
            }
            return productModels;
        }

        private static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            for (int i = 1; i <= 10; i++)
            {
                Product product = new();
                product.Id = i;
                product.Name = "Product" + i;
                product.IsAvailable = true;
                Random random = new();
                product.Cost = random.Next(100, 1000);
                products.Add(product);
            }
            return products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            ProductModel product = new ProductModel();

            product= GetProductsByAdo().Where(x => x.Id == id).FirstOrDefault();

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
           return  _dal.Create(product);
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
            return _dal.Update(product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
          
   
            return _dal.Delete(id);
        }
    }
}
