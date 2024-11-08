using Microsoft.AspNetCore.Identity;

namespace ProjectAirsoft.Data.Models
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		public ApplicationUser()
		{
			Id = Guid.NewGuid();
		}

		public virtual ICollection<UserGame> UsersGames { get; set; } = new HashSet<UserGame>();
	}
}
