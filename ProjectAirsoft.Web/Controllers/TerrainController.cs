using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Terrain;

namespace ProjectAirsoft.Web.Controllers
{
	[AllowAnonymous]
	public class TerrainController(ITerrainService terrainService) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			IEnumerable<TerrainIndexViewModel> allTerrains = await terrainService.GetAllTerrainsForIndexAsync();

			return View(allTerrains);
		}
	}
}
