using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Service
{
    public class ProductDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Testcase"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<ProductModel> GetProduct()
        {
            cmd = new SqlCommand("pr2_select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<ProductModel> list = new List<ProductModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new ProductModel
                {
                    ID = Guid.Parse(dr["ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                });
            }

            return list;
        }

        public bool InsertProduct(ProductModel product)
        {
            try
            {
                cmd = new SqlCommand("pr_create", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", product.Code);
                cmd.Parameters.AddWithValue("@name", product.Name);
                con.Open();

                int r = cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool UpdateProduct(ProductModel product)
        {
            try
            {
                cmd = new SqlCommand("pr_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", product.Code);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@id", product.ID);
                con.Open();

                int r = cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int DeleteProduct(Guid id)
        {
            try
            {
                cmd = new SqlCommand("pr_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();

                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}