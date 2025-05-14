using System.Text;
using System.Text.Json.Serialization;
using CRUDAppUsingASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Build.ObjectModelRemoting;
using Newtonsoft.Json;

namespace CRUDAppUsingASPCoreWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7285/api/StudentAPI/"; // ye base url hai root url hai
        private HttpClient client = new HttpClient(); // HttpClient ki class but zyada imprtant hai
                                                      // iske bina hum kabhi bhi apni api ke saath kaam anhi karsakte.{webAPI se related important class hai}
        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Student>>(result);
                if (data != null)
                {
                    students = data;
                }
            }
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Addedd ..";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url+std.id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student Updated ..";
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
