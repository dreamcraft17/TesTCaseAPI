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
        //PurchaseOrderDAL dal = new PurchaseOrderDAL();
        //SupplierDAL supplierDAL = new SupplierDAL();
        public class MvcPurchaseOrderController : Controller
        {
            // GET: MvcPurchaseOrder
            Uri basedAddress = new Uri("https://localhost:44370/api");
            HttpClient client;

            public Encoding Encoding { get; private set; }

            public MvcPurchaseOrderController()
            {
                client = new HttpClient();
                client.BaseAddress = basedAddress;
            }
            public ActionResult Index()
            {
                List<PurchaseOrderViewModel> modelList = new List<PurchaseOrderViewModel>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/purchaseorder").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<PurchaseOrderViewModel>>(data);
                }
                return View(modelList);
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<ActionResult> Create(PurchaseOrderViewModel model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string data = JsonConvert.SerializeObject(model);
                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(client.BaseAddress + "/purchaseorder", content);

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

            public ActionResult GetSupplierIDs()
            {
                PurchaseOrderDAL dal = new PurchaseOrderDAL();
                List<Guid> supplierIDs = dal.GetSupplierIDs();
                return Json(supplierIDs, JsonRequestBehavior.AllowGet);
            }



            public ActionResult Edit(Guid id)
            {
                PurchaseOrderViewModel model = new PurchaseOrderViewModel();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ppurchaseorder/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    List<PurchaseOrderViewModel> models = JsonConvert.DeserializeObject<List<PurchaseOrderViewModel>>(data);

                   
                    model = models.FirstOrDefault(m => m.ID == id);
                }

                return View("Edit", model);
            }



            [HttpPost]
            public ActionResult Edit(PurchaseOrderViewModel model)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/purchaseorder/" + model.ID, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View("Edit", model);
            }

            public ActionResult Delete(Guid id)
            {
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/purchaseorder/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                // Jika penghapusan gagal, Anda dapat menangani situasi tersebut di sini
                // Misalnya, Anda dapat menampilkan pesan kesalahan atau melakukan tindakan lain yang sesuai.

                return RedirectToAction("Index");
            }
        }
    }