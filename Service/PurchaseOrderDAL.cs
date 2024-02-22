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
    public class PurchaseOrderDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Testcase"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<PurchaseOrderModel> GetPurchaseOrder()
        {
            cmd = new SqlCommand("po_select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<PurchaseOrderModel> list = new List<PurchaseOrderModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new PurchaseOrderModel
                {
                    ID = Guid.Parse(dr["ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    PurchaseDate = Convert.ToDateTime(dr["PurchaseDate"]),
                    SupplierID = Guid.Parse(dr["SupplierID"].ToString()),
                    Remarks = dr["Remarks"].ToString()
                });
            }

            return list;
        }

        public bool InsertPO(PurchaseOrderModel po)
        {
            try
            {
                cmd = new SqlCommand("po3_create", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", po.Code);
                cmd.Parameters.AddWithValue("@purchasedate", po.PurchaseDate);
                cmd.Parameters.AddWithValue("@supplierid", po.SupplierID);
                cmd.Parameters.AddWithValue("@remarks", po.Remarks);
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

        public bool Updatepo(PurchaseOrderModel po)
        {
            try
            {
                cmd = new SqlCommand("po_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", po.Code);
                cmd.Parameters.AddWithValue("@purchasedate", po.PurchaseDate);
                cmd.Parameters.AddWithValue("@poID", po.SupplierID  );
                cmd.Parameters.AddWithValue("@remarks", po.Remarks);
                cmd.Parameters.AddWithValue("@id", po.ID);
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

        public int Deletepo(Guid id)
        {
            try
            {
                cmd = new SqlCommand("po_delete", con);
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

        public List<Guid> GetSupplierIDs()
        {
            List<Guid> supplierIDs = new List<Guid>();

            using (SqlCommand cmd = new SqlCommand("SELECT ID FROM Supplier", con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        supplierIDs.Add(reader.GetGuid(0));
                    }
                }
            }

            return supplierIDs;
        }



    }
}