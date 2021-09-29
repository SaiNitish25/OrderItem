using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrderItem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderItemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult insert([FromBody] int menuId)
        {
            Cart cart = new Cart();
            cart.Id = 1;
            cart.UserId = 1;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44387/api/");
                    var responseTask = client.GetAsync("MenuItem/" + menuId);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var results = result.Content.ReadAsStringAsync().Result;
                        JObject json = JObject.Parse(results);
                        cart.menuItemId = (int)json.GetValue("id");
                        cart.menuItemName = (string)json.GetValue("name");

                        return Ok(cart);
                    }

                }
            }
            catch (Exception e)
            {
                
            }
            return Ok();
        }


    }
}
