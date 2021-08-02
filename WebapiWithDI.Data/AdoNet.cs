using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebapiWithDI.Abstractions;
using WebapiWithDI.Common;

namespace WebapiWithDI.Data
{
    public class AdoNet:IDal
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
             DataSet ds=  GetProductsByAdoNet();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var row = ds.Tables[0].Rows[i];
                Product product = new();
                product.Id =Convert.ToInt32( row["Id"]);
                product.Name = row["Name"].ToString();
                product.IsAvailable = Convert.ToBoolean(row["IsAvailable"]); 
                product.Cost= Convert.ToDouble(row["Cost"]);
              
                products.Add(product);


            }

            return products;
        }
        public bool Create(Product product)
        {
            string command = "insert into product(name,cost,isavailable) values('"+ product .Name+ "'," + product.Cost + ",'" + product.IsAvailable + "')";
            bool res = DML(command);
            return res;
          
        }
        public bool Update(Product product)
        {
            string command = "update product set name='" + product.Name + "', cost="+product.Cost+",IsAvailable='"+product.IsAvailable+"' where id="+product.Id;
            bool res = DML(command);
            return res;

        }
        public bool Delete(int id)
        {
            string command = "delete from product where id=" +id;
            bool res = DML(command);
            return res;

        }
        private static DataSet GetProductsByAdoNet()
        {
            SqlConnection sqlConnection = new SqlConnection("server=.;database=demodb; Integrated Security=True;");

            SqlCommand cmd = new SqlCommand("select * from product", sqlConnection);
            DataSet dataSet = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dataSet);
            return dataSet;
        }
        private static bool DML(string command)
        {
            SqlConnection sqlConnection = new SqlConnection("server=.;database=demodb; Integrated Security=True;");
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
           int i=  cmd.ExecuteNonQuery();
            sqlConnection.Close();
            if (i>0)
            {
                return true;
            }
            return false;
        }
    }
}
