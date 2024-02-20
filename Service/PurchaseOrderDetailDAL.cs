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
    public class PurchaseOrderDetailDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Testcase"].ConnectionString);
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public List<PurchaseOrderDetailModel> GetPODetail()
        {
            cmd = new SqlCommand("pod_select", con);
            cmd.CommandType = CommandType.StoredProcedure;

            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<PurchaseOrderDetailModel> list = new List<PurchaseOrderDetailModel>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new PurchaseOrderDetailModel
                {
                    ID = Guid.Parse(dr["ID"].ToString()),
                    PurchaseOrderID = Guid.Parse(dr["PurchaseOrderID"].ToString()),
                    ProductID = Guid.Parse(dr["ProductID"].ToString()),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])

                });
            }

            return list;
        }

        public bool InsertPO(PurchaseOrderDetailModel pod)
        {
            try
            {
                cmd = new SqlCommand("pod_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", pod.PurchaseOrderID);
                cmd.Parameters.AddWithValue("@purchasedate", pod.ProductID);
                cmd.Parameters.AddWithValue("@poID", pod.Quantity);
                cmd.Parameters.AddWithValue("@remarks", pod.UnitPrice);
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

        public bool Updatepo(PurchaseOrderDetailModel pod)
        {
            try
            {
                cmd = new SqlCommand("pod_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", pod.PurchaseOrderID);
                cmd.Parameters.AddWithValue("@purchasedate", pod.ProductID);
                cmd.Parameters.AddWithValue("@poID", pod.Quantity);
                cmd.Parameters.AddWithValue("@remarks", pod.UnitPrice);
                cmd.Parameters.AddWithValue("@id", pod.ID);
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
                cmd = new SqlCommand("pod_delete", con);
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