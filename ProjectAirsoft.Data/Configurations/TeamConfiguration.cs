using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class TeamConfiguration : IEntityTypeConfiguration<Team>
	{
		public void Configure(EntityTypeBuilder<Team> builder)
		{
			builder.HasData(GenerateTeams());
		}

		private IEnumerable<Team> GenerateTeams()
		{
			IEnumerable<Team> teams = new List<Team>()
			{
				new Team()
				{
					Id = Guid.Parse("6ec23209-e40e-49a2-8ea4-052e83a2fe4d"),
					Name = "Drazki Spec. Ops. Group",
					LogoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhpEkZQsmay7ehKLLMajwMXwhdo_hGLtBR6A&s",
					CityId = 3,
					LeaderId = Guid.Parse("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9")
				},
				new Team()
				{
					Id = Guid.Parse("c379084f-85f5-43c9-a448-17c5514059d6"),
					Name = "OPFOR TCD",
					LogoUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDoQ68zwAWyAjM0wwucWB7FbRO_l0EUmMwLg&s",
					CityId = 10,
					LeaderId = Guid.Parse("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2")
				}
			};

			return teams;
		}
	}
}
