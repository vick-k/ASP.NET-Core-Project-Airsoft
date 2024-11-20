using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Comment;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class CommentController(IGameService gameService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CommentFormModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			string userId = userManager.GetUserId(User)!;
			bool result = await gameService.AddCommentAsync(viewModel, userId);

			if (result == false)
			{
				// add generic error message
				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
		}
	}
}
