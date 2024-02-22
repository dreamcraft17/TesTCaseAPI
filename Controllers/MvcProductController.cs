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

namespace WebApi.Controllers
{
    public class MvcProductController : Controller
    {
        // GET: MvcProduct
        Uri basedAddress = new Uri("https://localhost:44370/api");
        HttpClient client;

        public Encoding Encoding { get; private set; }

        public MvcProductController()
        {
            client = new HttpClient();
            client.BaseAddress = basedAddress;
        }
        public ActionResult Index()
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(modelList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/product", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/product/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                List<ProductViewModel> models = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

                // Cari model dengan ID yang sesuai
                model = models.FirstOrDefault(m => m.ID == id);
            }

            return View("Edit", model);
        }



        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/product/" + model.ID, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/product/" + id).Result;
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