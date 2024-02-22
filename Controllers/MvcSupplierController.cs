using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using TestCaseWebAPI.Models;
using TestCaseWebAPI.ViewModel;
using Newtonsoft.Json;
using System.Collections;
using System.Text;


namespace WebApi.Controllers
{
    public class MvcSupplierController : Controller
    {
        // GET: MvcSupplier
        Uri basedAddress = new Uri("https://localhost:44370/api");
        HttpClient client;

        public Encoding Encoding { get; private set; }

        public MvcSupplierController()
        {
            client = new HttpClient();
            client.BaseAddress = basedAddress;
        }
        public ActionResult Index()
        {
            List<SupplierViewModel> modelList = new List<SupplierViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/supplier").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<SupplierViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/supplier", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            SupplierViewModel model = new SupplierViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/supplier/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<SupplierViewModel> models = JsonConvert.DeserializeObject<List<SupplierViewModel>>(data);

                // Cari model dengan ID yang sesuai
                model = models.FirstOrDefault(m => m.ID == id);
            }

            return View("Edit", model);
        }



        [HttpPost]
        public ActionResult Edit(SupplierViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/supplier/" + model.ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/supplier/" + id).Result;
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