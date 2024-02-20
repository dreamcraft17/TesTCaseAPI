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
    public class SupplierDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Testcase"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<SupplierModel> GetSupplier()
        {
            cmd = new SqlCommand("sp_select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<SupplierModel> list = new List<SupplierModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new SupplierModel
                {
                    ID = Guid.Parse(dr["ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString(),
                    City = dr["City"].ToString()
                });
            }

            return list;
        }

        public bool InsertSupplier(SupplierModel supplier)
        {
            try
            {
                cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", supplier.Code);
                cmd.Parameters.AddWithValue("@name", supplier.Name);
                cmd.Parameters.AddWithValue("@city", supplier.City);
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

        public bool UpdateSupplier(SupplierModel supplier)
        {
            try
            {
                cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", supplier.Code);
                cmd.Parameters.AddWithValue("@name", supplier.Name);
                cmd.Parameters.AddWithValue("@city", supplier.City);
                cmd.Parameters.AddWithValue("@id", supplier.ID);
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

        public int DeleteSupplier(Guid id)
        {
            try
            {
                cmd = new SqlCommand("sp_delete", con);
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