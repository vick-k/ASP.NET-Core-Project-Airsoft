using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Web.Models;
using System.Diagnostics;

namespace ProjectAirsoft.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Error(int? statusCode = null)
		{
			if (statusCode.HasValue)
			{
				if (statusCode == 404)
				{
					return View("Error404");
				}

				if (statusCode == 500)
				{
					return View("Error500");
				}
			}

			return View("Error500");
		}
	}
}
