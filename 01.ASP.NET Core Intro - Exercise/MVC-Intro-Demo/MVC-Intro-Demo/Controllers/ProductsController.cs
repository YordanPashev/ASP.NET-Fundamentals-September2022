using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVC_Intro_Demo.ViewModels;
using System.Text;
using System.Text.Json;

namespace MVC_Intro_Demo.Controllers
{
    public class ProductsController : Controller
    {

        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 7.00
            },

            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 5.50
            },

            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 1.50
            }
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            return this.View(products);
        }


        public IActionResult AllAsJson()
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            return Json(products, jsonOptions);
        }


        public IActionResult AllAsText()
        {
            string textResult = GetAllProductInfoAsString();

            return Content(textResult.ToString());
        }

		public IActionResult AllAsTextFile()
        {
            string textResult = GetAllProductInfoAsString();

            Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment;filename=All-Products-Info.txt");

            return File(Encoding.UTF8.GetBytes(textResult), "text/plain");
        }

        public IActionResult ById(int id)
        {
            ProductViewModel? product = this.products.FirstOrDefault(p => p.Id == id);

			if (product == null)
			{
                return BadRequest();
			}

            return this.View(product);
        }

        private string GetAllProductInfoAsString()
        {
            StringBuilder textResult = new StringBuilder();

            foreach (var product in products)
            {
                textResult.Append($"product {product.Id}: {product.Name} - {product.Price} lv")
                          .Append(Environment.NewLine);
            }

            return textResult.ToString().TrimEnd();
        }
    }
}
