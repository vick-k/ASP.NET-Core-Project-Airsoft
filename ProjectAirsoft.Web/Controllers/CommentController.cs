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

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool commentExists = await gameService.CommentExistsAsync(id);

			if (!commentExists)
			{
				// add error message
				return RedirectToAction("Details", "Game");
			}

			CommentDeleteViewModel viewModel = await gameService.GetCommentForDeleteAsync(id);
			string userId = userManager.GetUserId(User)!;

			if (viewModel == null || userId != viewModel.AuthorId)
			{
				// add error message
				return RedirectToAction("Details", "Game");
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(CommentDeleteViewModel viewModel)
		{
			bool commentExists = await gameService.CommentExistsAsync(viewModel.Id);

			if (!commentExists)
			{
				// add error message
				return RedirectToAction("Details", "Game");
			}

			string userId = userManager.GetUserId(User)!;

			if (userId != viewModel.AuthorId)
			{
				// add error message
				return RedirectToAction("Details", "Game");
			}

			bool result = await gameService.DeleteCommentAsync(viewModel.Id);

			if (result == false)
			{
				// add error message
				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
		}
	}
}
