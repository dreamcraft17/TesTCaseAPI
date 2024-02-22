using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TestCaseWebAPI.ViewModel;
using Newtonsoft.Json;
using System.Collections;
using System.Text;
using WebApi.Service;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class MvcPurchaseOrderDetailController : Controller
    {
        // GET: MvcPurchaseOrderDetail
        // GET: PurchaseOrderDetail
        Uri basedAddress = new Uri("https://localhost:44370/api");
        HttpClient client;

        public Encoding Encoding { get; private set; }

        public MvcPurchaseOrderDetailController()
        {
            client = new HttpClient();
            client.BaseAddress = basedAddress;
        }
        public ActionResult Index()
        {
            List<PurchaseOrderDetailViewModel> modelList = new List<PurchaseOrderDetailViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/purchaseorderdetail").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<PurchaseOrderDetailViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PurchaseOrderDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/purchaseorderdetail", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        string errorMessage = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", "Failed to save data. Server error: " + errorMessage);
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "An error occurred while saving data: " + ex.Message);
                }
            }


            return View(model);
        }

        private List<PurchaseOrderViewModel> GetPurchaseOrders()
        {
            List<PurchaseOrderViewModel> purchaseOrders = new List<PurchaseOrderViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/purchase").Result;
            if (response.IsSuccessStatusCode)
            {
                string purchaseData = response.Content.ReadAsStringAsync().Result;
                purchaseOrders = JsonConvert.DeserializeObject<List<PurchaseOrderViewModel>>(purchaseData);
            }
            return purchaseOrders;
        }

        private List<ProductViewModel> GetProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                string productData = response.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(productData);
            }
            return products;
        }
        public ActionResult GetPurchaseOrder()
        {


            List<PurchaseOrderViewModel> suppliers = new List<PurchaseOrderViewModel>();


            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/purchase").Result;
            if (response.IsSuccessStatusCode)
            {
                string purchaseData = response.Content.ReadAsStringAsync().Result;
                suppliers = JsonConvert.DeserializeObject<List<PurchaseOrderViewModel>>(purchaseData);
            }
            else
            {

            }

            // Kemudian, kembalikan daftar supplier dalam format JSON
            return Json(suppliers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid id)
        {
            PurchaseOrderDetailViewModel model = new PurchaseOrderDetailViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/purchasedetail/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<PurchaseOrderDetailViewModel> models = JsonConvert.DeserializeObject<List<PurchaseOrderDetailViewModel>>(data);

                // Cari model dengan ID yang sesuai
                model = models.FirstOrDefault(m => m.ID == id);
            }

            return View("Edit", model);
        }



        [HttpPost]
        public ActionResult Edit(PurchaseOrderDetailViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/purchasedetail/" + model.ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/purchasedetail/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Jika penghapusan gagal, Anda dapat menangani situasi tersebut di sini
            // Misalnya, Anda dapat menampilkan pesan kesalahan atau melakukan tindakan lain yang sesuai.

            return RedirectToAction("Index");
        }

        public ActionResult GetPurchaseOrderIDs()
        {
            PurchaseOrderDetailDAL dal = new PurchaseOrderDetailDAL();
            List<Guid> poIDs = dal.GetPurchaseOrderIDs();
            return Json(poIDs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductIDs()
        {
            PurchaseOrderDetailDAL dal = new PurchaseOrderDetailDAL();
            List<Guid> purchaseIDs = dal.GetProductIDs();
            return Json(purchaseIDs, JsonRequestBehavior.AllowGet);
        }
    }
}