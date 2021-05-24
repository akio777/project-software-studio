using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LabReservation.Data;
using LabReservation.Models;
using LabReservation.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace LabReservation.Controllers{
    public class LabPartnerController : Controller{
        private readonly LabReservationContext _context;

				// GET: LabPartner
        public async Task<IActionResult> Index(){
          return View("Index", "Home");
        }

				public async Task<IActionResult> Lab(int id) {
					var json = await GetExternalLabs();
					
					try
					{
							var externalLabList = JsonConvert.DeserializeObject<List<PartnerLab>>(json);
							// Console.WriteLine(JsonConvert.SerializeObject(externalLabList.ToArray()[id-1], Formatting.Indented));
							return View("Index",externalLabList.ToArray()[id-1]);
					}
					catch (Exception exception)
					{
							// Console.WriteLine(exception);
							return View("Index", "NoPermission");
					}
				}

				private async Task<string> GetExternalLabs()
        {
            string baseUrl = "https://software-studio-ce57.herokuapp.com/api/ExternalAPI";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();
                            if (data == null) return "[]";
                            return data;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // Console.WriteLine(exception);
                return "[]";
            }
        }
    }
}