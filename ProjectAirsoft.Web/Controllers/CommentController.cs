using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAirsoft.Data.Models;
using ProjectAirsoft.Services.Data.Interfaces;
using ProjectAirsoft.ViewModels.Comment;

using static ProjectAirsoft.Common.AlertMessages.Comment;
using static ProjectAirsoft.Common.ApplicationConstants;

namespace ProjectAirsoft.Web.Controllers
{
	[Authorize]
	public class CommentController(IGameService gameService, UserManager<ApplicationUser> userManager) : Controller
	{
		[HttpPost]
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
				TempData[AlertDanger] = AddCommentFailMessage;

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
				TempData[AlertDanger] = CommentDoesNotExistMessage;

				return RedirectToAction("Index", "Game");
			}

			CommentDeleteViewModel viewModel = await gameService.GetCommentForDeleteAsync(id);

			if (viewModel == null)
			{
				TempData[AlertDanger] = CommentDoesNotExistMessage;

				return RedirectToAction("Index", "Game");
			}

			string userId = userManager.GetUserId(User)!;

			if (userId != viewModel.AuthorId)
			{
				TempData[AlertDanger] = CommentNotOwnerMessage;

				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(CommentDeleteViewModel viewModel)
		{
			CommentDeleteViewModel commentFromDeleteForm = await gameService.GetCommentForDeleteAsync(viewModel.Id);

			if (commentFromDeleteForm.AuthorId != viewModel.AuthorId)
			{
				TempData[AlertDanger] = CommentGenericErrorMessage;

				return RedirectToAction("Index", "Game");
			}

			bool commentExists = await gameService.CommentExistsAsync(viewModel.Id);

			if (!commentExists)
			{
				TempData[AlertDanger] = CommentDoesNotExistMessage;

				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			string userId = userManager.GetUserId(User)!;

			if (userId != viewModel.AuthorId)
			{
				TempData[AlertDanger] = CommentNotOwnerMessage;

				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			bool result = await gameService.DeleteCommentAsync(viewModel.Id);

			if (result == false)
			{
				TempData[AlertDanger] = CommentGenericErrorMessage;

				return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
			}

			return RedirectToAction("Details", "Game", new { id = viewModel.GameId });
		}
	}
}
