using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        // https://localhost:5001/HelloWorld
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/
        // https://localhost:5001/HelloWorld/Welcome/3?name=Sergei
        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        }
    }
}
