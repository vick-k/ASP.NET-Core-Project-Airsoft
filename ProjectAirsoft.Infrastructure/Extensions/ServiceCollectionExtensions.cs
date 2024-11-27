using Microsoft.Extensions.DependencyInjection;
using ProjectAirsoft.Services.Data;
using ProjectAirsoft.Services.Data.Interfaces;

namespace ProjectAirsoft.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddCustomServices(this IServiceCollection services)
		{
			services.AddScoped<IBaseService, BaseService>();
			services.AddScoped<ICityService, CityService>();
			services.AddScoped<IGameListService, GameListService>();
			services.AddScoped<IGameService, GameService>();
			services.AddScoped<ITeamService, TeamService>();
			services.AddScoped<ITerrainService, TerrainService>();

			return services;
		}
	}
}
