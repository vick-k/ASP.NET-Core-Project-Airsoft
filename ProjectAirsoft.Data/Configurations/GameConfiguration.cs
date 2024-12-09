using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectAirsoft.Data.Models;

namespace ProjectAirsoft.Data.Configurations
{
	public class GameConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(EntityTypeBuilder<Game> builder)
		{
			builder
				.Property(t => t.Fee)
				.HasColumnType("decimal(18,2)");

			builder.HasData(GenerateGames());
		}

		private IEnumerable<Game> GenerateGames()
		{
			IEnumerable<Game> games = new List<Game>()
			{
				new Game()
				{
					Id = Guid.Parse("1e83cba8-74e2-4552-9a3c-0d216016a688"),
					Name = "Sunday Game #1",
					Description = "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!",
					Date = DateTime.Parse("2024-12-01"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					TerrainId = Guid.Parse("63b8246a-aa5c-4964-95c9-10d42eb07b65"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("87536500-af29-4d83-afa8-17ba088f6be8"),
					Name = "Sunday Game #2",
					Description = "Get ready for an exhilarating day of strategy and action with a classic Capture the Flag airsoft showdown! Two teams will battle it out in a thrilling test of teamwork, tactics, and speed, all set in an immersive battlefield environment. Game Rules and Format: Teams will start at their respective bases, each guarding their own flag while strategizing to capture the opponent’s. The objective is to successfully retrieve the enemy flag from their base and return it to your own while defending your flag from capture. Players hit during gameplay must return to their base and wait for a 5-minute respawn timer before rejoining the action. Communication and coordination are essential for both offense and defense. Work together to outsmart and outmaneuver your opponents! A successful flag capture earns your team a point, and the team with the most points at the end of the game wins. Safety Guidelines: All players must wear approved protective gear, including face masks and goggles, throughout the event. Only field-approved airsoft replicas are allowed, and all weapons will be chrono-tested for safety. Honor the game and your opponents—call your hits and maintain sportsmanship at all times.",
					Date = DateTime.Parse("2024-12-13"),
					StartTime = "11:30",
					Capacity = 120,
					Fee = 0m,
					TerrainId = Guid.Parse("a8177ba1-f5af-4ec4-8631-41a1a5250460"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("4f6d481d-b666-4a2e-83f0-4aec22accdf8"),
					Name = "Battle Stations",
					ImageUrl = "https://www.surplusstore.co.uk/wp/wp-content/uploads/sites/1/2018/07/800px-US_Navy_SEAL_Reenactment_Group.jpg",
					Description = "Step into the ultimate battlefield for a day of intense competition in Battle Stations Domination! Two teams will go head-to-head, fighting to capture and hold key objectives while racking up points in this high-energy, strategy-driven airsoft game. Game Rules and Format: The terrain is equipped with 3 Battle Stations, each serving as a critical control point. Your mission: capture these stations and hold them for as long as possible. Every second a Battle Station is under your team’s control, it generates points. To capture a station, your team must secure the area and raise your team’s flag or activate the station marker. The station will remain yours until the opposing team takes it back. Eliminated players must return to their team’s base and wait for a 5-minute respawn timer before returning to the action. The team with the highest total points at the end of the game claims victory! Strategy Tips: Coordinate your team to attack, defend, and rotate positions. Balancing offense and defense is key to dominating the field. Use communication and tactics to outmaneuver your opponents and seize the advantage. Prioritize teamwork—every player’s role contributes to overall success! Safety Guidelines: All players must wear approved protective gear, including face masks and goggles. Only field-approved airsoft replicas are allowed, and all weapons must pass a chrono test before the game. Call your hits and maintain sportsmanship throughout the event.",
					Date = DateTime.Parse("2024-12-14"),
					StartTime = "09:30",
					Capacity = 200,
					Fee = 5.00m,
					TerrainId = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("17f7524e-ce89-4e78-aa9f-41f440f3466b"),
					Name = "VIP Rescue",
					ImageUrl = "https://adeptairsoft.com/wp-content/uploads/2024/02/join-airsoft-team.png",
					Description = "Gear up for an action-packed airsoft experience in VIP Rescue Mission, where strategy, precision, and teamwork are essential for success! In this high-stakes scenario, one team must escort a designated VIP to safety, while the opposing team does everything they can to capture or eliminate the target. Game Rules and Format: The VIP starts with the Defender Team at their base and must be safely escorted to a designated extraction point on the field. The Attacker Team’s mission is to intercept the VIP and prevent their extraction by capturing or eliminating them. The VIP is unarmed but equipped with a small shield for limited protection. Defenders must work as a unit to protect the VIP, while attackers use strategy and firepower to block their progress. If the VIP reaches the extraction point unharmed, the Defenders win. If the VIP is eliminated or captured, the Attackers win. Special Rules for the VIP: The VIP cannot run or hide without their team’s protection and must always remain visible during the game. If the VIP is hit, they are “down” but not eliminated and must remain stationary until they are “rescued” by a Defender within 2 minutes. If no rescue occurs, the game ends with an Attacker victory.",
					Date = DateTime.Parse("2024-12-15"),
					StartTime = "12:00",
					Capacity = 150,
					Fee = 2.00m,
					TerrainId = Guid.Parse("8b031c23-6687-4338-9845-5e9af3af951c"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("1948672c-706d-474f-b4b0-5c1f9cf6f440"),
					Name = "Night Ops",
					ImageUrl = "https://www.redwolfairsoft.com/wp/wp-content/uploads/2020/12/team-1024x784.jpg",
					Description = "Prepare for a thrilling and immersive experience under the cover of darkness with Night Ops! This after-hours airsoft game takes the action to a whole new level, where strategy and stealth are key to survival. The battlefield transforms into a realm of suspense and tactical maneuvers, pushing your skills to their limits. Game Format: Teams will engage in a series of nighttime missions, ranging from strategic objectives like capturing control points to intense combat scenarios. Visibility is limited, so players must rely on teamwork, communication, and tactical use of flashlights and night vision (if available). Silent eliminations and ambush tactics will be game-changers in this nocturnal showdown. Nighttime Rules and Safety: Light Discipline: Flashlights and other illumination tools must be used sparingly to avoid giving away positions. Glow sticks or reflective markers will identify teammates. Safety First: All players must wear standard protective gear, including goggles and face masks, with additional reflective markers to ensure visibility during gameplay. Be extra cautious when moving in the dark terrain to avoid trips or falls. Equipment Tips: Bring flashlights, headlamps, or mounted tactical lights for your replicas. Wear dark or camouflaged clothing suitable for nighttime play. Glow sticks or colored armbands will be provided for team identification.",
					Date = DateTime.Parse("2024-12-18"),
					StartTime = "20:00",
					Capacity = 60,
					Fee = 0m,
					TerrainId = Guid.Parse("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("d837bea0-00d5-4067-89c4-3b05c55dcfdf"),
					Name = "Capture the Flag",
					ImageUrl = "https://www.defconairsoft.co.uk/wp-content/uploads/2024/02/capture-the-plag.png",
					Description = "Get ready for an intense airsoft showdown in Capture the Flag, where teamwork, strategy, and precision are the keys to victory! Two rival teams will face off in a high-energy competition to seize the enemy’s flag while defending their own in a dynamic battlefield. Game Rules and Format: Each team starts at their base with their flag securely placed. The objective is simple yet challenging: infiltrate the enemy base, capture their flag, and return it to your base without being eliminated. If a player is hit, they are eliminated and must return to their base for a 5-minute respawn timer before re-entering the game. A successful flag capture earns your team a point, but the game doesn't end there—multiple captures can be achieved within the game time limit. The team with the most points at the end of the game wins. Strategy Tips: Balance offense and defense by assigning players to attack, defend, and scout. Communication is crucial—coordinate your movements and adapt to the enemy’s strategy. Be quick and precise in your movements; the faster you capture the flag, the better your chances of outscoring the opponent! Safety Guidelines: Protective gear, including goggles and face masks, is mandatory for all players. Only field-approved airsoft replicas are permitted, and all weapons must pass a chrono test before gameplay. Adhere to the honor system—call your hits and respect the game.",
					Date = DateTime.Parse("2024-12-21"),
					StartTime = "10:00",
					Capacity = 100,
					Fee = 5.00m,
					TerrainId = Guid.Parse("a8177ba1-f5af-4ec4-8631-41a1a5250460"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("a3ccaad2-3c92-4e93-9ce0-ab122ea65668"),
					Name = "Domination",
					ImageUrl = "https://i.servimg.com/u/f58/19/63/04/66/tm/dsc00025.jpg",
					Description = "Prepare for a heart-pounding airsoft experience in Domination, where tactical mastery and team coordination are the keys to victory! Two teams will battle for control of multiple zones across the battlefield, with every second of domination earning critical points. Game Rules and Format: The battlefield features 3 strategic zones that serve as control points. Each zone can be claimed by raising your team’s flag or activating the zone marker. Your objective: capture and hold as many zones as possible for as long as you can. Every second a zone is under your team’s control earns points. If a player is hit, they are eliminated and must return to their base for a 5-minute respawn timer before returning to the field. The team with the most points at the end of the game wins. Strategy Tips: Balance your forces between offense, defense, and zone rotation to maximize your team’s dominance. Communication and quick decision-making are vital—adapt to the enemy’s movements and focus on holding key zones. Remember, controlling multiple zones increases your score rate, so think strategically about where to focus your efforts. Safety Guidelines: All players must wear approved protective gear, including face masks and goggles, at all times. All airsoft replicas must pass a chrono test before the game begins. Play fair and honor the rules—call your hits and respect your fellow players.",
					Date = DateTime.Parse("2024-12-22"),
					StartTime = "11:00",
					Capacity = 200,
					Fee = 10.00m,
					TerrainId = Guid.Parse("838d3b35-4413-4721-870e-32b00bde5f8e"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("7ccfa9fc-ff63-42cf-ba59-30584cb0a206"),
					Name = "Border War",
					ImageUrl = "https://airsoft-info.at/wp-content/uploads/2023/09/borderwar.jpg",
					Description = "Join the battle in Border War, a dynamic and high-stakes airsoft scenario where two teams clash over contested territory along a shifting frontline. This intense game mode challenges players to adapt, strategize, and work as a unit to claim victory. Game Rules and Format: The battlefield is divided into several zones along a central border, representing a contested area. Teams start at their respective bases and must advance to capture and control zones along the border. To claim a zone, your team must eliminate all opponents in the area and raise your flag or activate the zone marker. The frontline shifts as zones are captured, with both teams pushing to expand their territory. Each controlled zone generates points for your team every minute. The team with the most points at the end of the game wins. Eliminated players must return to their base for a 5-minute respawn timer before rejoining the battle. Strategy Tips: Coordinate your team to maintain a strong presence at captured zones while continuing to push the enemy back. Use scouting and flanking tactics to outmaneuver your opponents and surprise them in contested areas. Communication is critical—adapt your strategy as the frontline shifts and new zones come into play.",
					Date = DateTime.Parse("2025-01-19"),
					StartTime = "14:00",
					Capacity = 500,
					Fee = 50.00m,
					TerrainId = Guid.Parse("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("efd6ab61-0ec5-498c-ba80-61bb1dfb660c"),
					Name = "Sunday Game #3",
					Description = "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!",
					Date = DateTime.Parse("2025-01-12"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					TerrainId = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("c0a3e339-d04a-41e1-8b8c-cff9b8b96149"),
					Name = "Sunday Game #4",
					Description = "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!",
					Date = DateTime.Parse("2025-01-19"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					TerrainId = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("07d6ad43-126f-4906-bed8-80ee555fbe26"),
					Name = "Sunday Game #5",
					Description = "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!",
					Date = DateTime.Parse("2025-01-26"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					TerrainId = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				},
				new Game()
				{
					Id = Guid.Parse("9bf8c44b-651c-4141-8c07-52572a01b4a8"),
					Name = "Sunday Game #6",
					Description = "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!",
					Date = DateTime.Parse("2025-02-02"),
					StartTime = "10:00",
					Capacity = 80,
					Fee = 2.00m,
					IsCanceled = true,
					TerrainId = Guid.Parse("01ac2f70-21e0-417f-b18a-41c9aa1055b6"),
					OrganizerId = Guid.Parse("0e773424-25ab-4d83-a5c9-a5f3665ef336")
				}
			};

			return games;
		}
	}
}
