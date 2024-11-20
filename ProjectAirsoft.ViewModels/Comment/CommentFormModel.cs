using System.ComponentModel.DataAnnotations;

using static ProjectAirsoft.Common.EntityValidationConstants.Comment;
using static ProjectAirsoft.Common.ValidationMessages.Comment;

namespace ProjectAirsoft.ViewModels.Comment
{
	public class CommentFormModel
	{
		[Required(ErrorMessage = RequiredContentMessage)]
		[MinLength(ContentMinLength, ErrorMessage = ContentMinLengthMessage)]
		[MaxLength(ContentMaxLength, ErrorMessage = ContentMaxLengthMessage)]
		public string Content { get; set; } = null!;

		public string GameId { get; set; } = null!;
	}
}
