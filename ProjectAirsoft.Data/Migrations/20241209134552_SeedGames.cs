using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectAirsoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a10e574-819b-440e-8bac-c19f74b5d1d6", "AQAAAAIAAYagAAAAEHxmIFLB5l6cCv7n8YL7XdbFenMXRz3NVJOgXqhEgWnKRA41Sj0wvl77blXmEnpkpQ==", "e31d4129-ac8a-41f5-ba9c-0c215a2809bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccc2090d-2bcb-4c1a-b400-97d2e8bd84c0", "AQAAAAIAAYagAAAAEGN2R3JJWAqlMussiQCPhzn6eIqGw0uEdJDLwt3n/KQWcE186LBrIedMQFZR3Zl7BQ==", "63a4ec85-5b44-496a-8e68-76f281065d49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43ecbc7c-bf63-43e0-95ac-d4ac58049738", "AQAAAAIAAYagAAAAECqE9lfs7UmPH9XH1qughf9MWlj1zgVL3CGPY7HHYlZfDrVqCsEIJY0MMCPRqIi+VA==", "c7633da9-e5a8-45e1-b1b7-800956ffb8e2" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Capacity", "Date", "Description", "Fee", "ImageUrl", "IsCanceled", "IsDeleted", "Name", "OrganizerId", "StartTime", "TerrainId" },
                values: new object[,]
                {
                    { new Guid("07d6ad43-126f-4906-bed8-80ee555fbe26"), 80, new DateTime(2025, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!", 2.00m, null, false, false, "Sunday Game #5", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6") },
                    { new Guid("17f7524e-ce89-4e78-aa9f-41f440f3466b"), 150, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gear up for an action-packed airsoft experience in VIP Rescue Mission, where strategy, precision, and teamwork are essential for success! In this high-stakes scenario, one team must escort a designated VIP to safety, while the opposing team does everything they can to capture or eliminate the target. Game Rules and Format: The VIP starts with the Defender Team at their base and must be safely escorted to a designated extraction point on the field. The Attacker Team’s mission is to intercept the VIP and prevent their extraction by capturing or eliminating them. The VIP is unarmed but equipped with a small shield for limited protection. Defenders must work as a unit to protect the VIP, while attackers use strategy and firepower to block their progress. If the VIP reaches the extraction point unharmed, the Defenders win. If the VIP is eliminated or captured, the Attackers win. Special Rules for the VIP: The VIP cannot run or hide without their team’s protection and must always remain visible during the game. If the VIP is hit, they are “down” but not eliminated and must remain stationary until they are “rescued” by a Defender within 2 minutes. If no rescue occurs, the game ends with an Attacker victory.", 2.00m, "https://adeptairsoft.com/wp-content/uploads/2024/02/join-airsoft-team.png", false, false, "VIP Rescue", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "12:00", new Guid("8b031c23-6687-4338-9845-5e9af3af951c") },
                    { new Guid("1948672c-706d-474f-b4b0-5c1f9cf6f440"), 60, new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for a thrilling and immersive experience under the cover of darkness with Night Ops! This after-hours airsoft game takes the action to a whole new level, where strategy and stealth are key to survival. The battlefield transforms into a realm of suspense and tactical maneuvers, pushing your skills to their limits. Game Format: Teams will engage in a series of nighttime missions, ranging from strategic objectives like capturing control points to intense combat scenarios. Visibility is limited, so players must rely on teamwork, communication, and tactical use of flashlights and night vision (if available). Silent eliminations and ambush tactics will be game-changers in this nocturnal showdown. Nighttime Rules and Safety: Light Discipline: Flashlights and other illumination tools must be used sparingly to avoid giving away positions. Glow sticks or reflective markers will identify teammates. Safety First: All players must wear standard protective gear, including goggles and face masks, with additional reflective markers to ensure visibility during gameplay. Be extra cautious when moving in the dark terrain to avoid trips or falls. Equipment Tips: Bring flashlights, headlamps, or mounted tactical lights for your replicas. Wear dark or camouflaged clothing suitable for nighttime play. Glow sticks or colored armbands will be provided for team identification.", 0m, "https://www.redwolfairsoft.com/wp/wp-content/uploads/2020/12/team-1024x784.jpg", false, false, "Night Ops", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "20:00", new Guid("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a") },
                    { new Guid("1e83cba8-74e2-4552-9a3c-0d216016a688"), 80, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!", 2.00m, null, false, false, "Sunday Game #1", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("63b8246a-aa5c-4964-95c9-10d42eb07b65") },
                    { new Guid("4f6d481d-b666-4a2e-83f0-4aec22accdf8"), 200, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Step into the ultimate battlefield for a day of intense competition in Battle Stations Domination! Two teams will go head-to-head, fighting to capture and hold key objectives while racking up points in this high-energy, strategy-driven airsoft game. Game Rules and Format: The terrain is equipped with 3 Battle Stations, each serving as a critical control point. Your mission: capture these stations and hold them for as long as possible. Every second a Battle Station is under your team’s control, it generates points. To capture a station, your team must secure the area and raise your team’s flag or activate the station marker. The station will remain yours until the opposing team takes it back. Eliminated players must return to their team’s base and wait for a 5-minute respawn timer before returning to the action. The team with the highest total points at the end of the game claims victory! Strategy Tips: Coordinate your team to attack, defend, and rotate positions. Balancing offense and defense is key to dominating the field. Use communication and tactics to outmaneuver your opponents and seize the advantage. Prioritize teamwork—every player’s role contributes to overall success! Safety Guidelines: All players must wear approved protective gear, including face masks and goggles. Only field-approved airsoft replicas are allowed, and all weapons must pass a chrono test before the game. Call your hits and maintain sportsmanship throughout the event.", 5.00m, "https://www.surplusstore.co.uk/wp/wp-content/uploads/sites/1/2018/07/800px-US_Navy_SEAL_Reenactment_Group.jpg", false, false, "Battle Stations", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "09:30", new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6") },
                    { new Guid("7ccfa9fc-ff63-42cf-ba59-30584cb0a206"), 500, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Join the battle in Border War, a dynamic and high-stakes airsoft scenario where two teams clash over contested territory along a shifting frontline. This intense game mode challenges players to adapt, strategize, and work as a unit to claim victory. Game Rules and Format: The battlefield is divided into several zones along a central border, representing a contested area. Teams start at their respective bases and must advance to capture and control zones along the border. To claim a zone, your team must eliminate all opponents in the area and raise your flag or activate the zone marker. The frontline shifts as zones are captured, with both teams pushing to expand their territory. Each controlled zone generates points for your team every minute. The team with the most points at the end of the game wins. Eliminated players must return to their base for a 5-minute respawn timer before rejoining the battle. Strategy Tips: Coordinate your team to maintain a strong presence at captured zones while continuing to push the enemy back. Use scouting and flanking tactics to outmaneuver your opponents and surprise them in contested areas. Communication is critical—adapt your strategy as the frontline shifts and new zones come into play.", 50.00m, "https://airsoft-info.at/wp-content/uploads/2023/09/borderwar.jpg", false, false, "Border War", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "14:00", new Guid("c48effd1-f1a7-4c8b-a60a-7cd7c626d49a") },
                    { new Guid("87536500-af29-4d83-afa8-17ba088f6be8"), 120, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Get ready for an exhilarating day of strategy and action with a classic Capture the Flag airsoft showdown! Two teams will battle it out in a thrilling test of teamwork, tactics, and speed, all set in an immersive battlefield environment. Game Rules and Format: Teams will start at their respective bases, each guarding their own flag while strategizing to capture the opponent’s. The objective is to successfully retrieve the enemy flag from their base and return it to your own while defending your flag from capture. Players hit during gameplay must return to their base and wait for a 5-minute respawn timer before rejoining the action. Communication and coordination are essential for both offense and defense. Work together to outsmart and outmaneuver your opponents! A successful flag capture earns your team a point, and the team with the most points at the end of the game wins. Safety Guidelines: All players must wear approved protective gear, including face masks and goggles, throughout the event. Only field-approved airsoft replicas are allowed, and all weapons will be chrono-tested for safety. Honor the game and your opponents—call your hits and maintain sportsmanship at all times.", 0m, null, false, false, "Sunday Game #2", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "11:30", new Guid("a8177ba1-f5af-4ec4-8631-41a1a5250460") },
                    { new Guid("9bf8c44b-651c-4141-8c07-52572a01b4a8"), 80, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!", 2.00m, null, true, false, "Sunday Game #6", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6") },
                    { new Guid("a3ccaad2-3c92-4e93-9ce0-ab122ea65668"), 200, new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for a heart-pounding airsoft experience in Domination, where tactical mastery and team coordination are the keys to victory! Two teams will battle for control of multiple zones across the battlefield, with every second of domination earning critical points. Game Rules and Format: The battlefield features 3 strategic zones that serve as control points. Each zone can be claimed by raising your team’s flag or activating the zone marker. Your objective: capture and hold as many zones as possible for as long as you can. Every second a zone is under your team’s control earns points. If a player is hit, they are eliminated and must return to their base for a 5-minute respawn timer before returning to the field. The team with the most points at the end of the game wins. Strategy Tips: Balance your forces between offense, defense, and zone rotation to maximize your team’s dominance. Communication and quick decision-making are vital—adapt to the enemy’s movements and focus on holding key zones. Remember, controlling multiple zones increases your score rate, so think strategically about where to focus your efforts. Safety Guidelines: All players must wear approved protective gear, including face masks and goggles, at all times. All airsoft replicas must pass a chrono test before the game begins. Play fair and honor the rules—call your hits and respect your fellow players.", 10.00m, "https://i.servimg.com/u/f58/19/63/04/66/tm/dsc00025.jpg", false, false, "Domination", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "11:00", new Guid("838d3b35-4413-4721-870e-32b00bde5f8e") },
                    { new Guid("c0a3e339-d04a-41e1-8b8c-cff9b8b96149"), 80, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!", 2.00m, null, false, false, "Sunday Game #4", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6") },
                    { new Guid("d837bea0-00d5-4067-89c4-3b05c55dcfdf"), 100, new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Get ready for an intense airsoft showdown in Capture the Flag, where teamwork, strategy, and precision are the keys to victory! Two rival teams will face off in a high-energy competition to seize the enemy’s flag while defending their own in a dynamic battlefield. Game Rules and Format: Each team starts at their base with their flag securely placed. The objective is simple yet challenging: infiltrate the enemy base, capture their flag, and return it to your base without being eliminated. If a player is hit, they are eliminated and must return to their base for a 5-minute respawn timer before re-entering the game. A successful flag capture earns your team a point, but the game doesn't end there—multiple captures can be achieved within the game time limit. The team with the most points at the end of the game wins. Strategy Tips: Balance offense and defense by assigning players to attack, defend, and scout. Communication is crucial—coordinate your movements and adapt to the enemy’s strategy. Be quick and precise in your movements; the faster you capture the flag, the better your chances of outscoring the opponent! Safety Guidelines: Protective gear, including goggles and face masks, is mandatory for all players. Only field-approved airsoft replicas are permitted, and all weapons must pass a chrono test before gameplay. Adhere to the honor system—call your hits and respect the game.", 5.00m, "https://www.defconairsoft.co.uk/wp-content/uploads/2024/02/capture-the-plag.png", false, false, "Capture the Flag", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("a8177ba1-f5af-4ec4-8631-41a1a5250460") },
                    { new Guid("efd6ab61-0ec5-498c-ba80-61bb1dfb660c"), 80, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prepare for an adrenaline-fueled day of tactical gameplay as two teams clash in an epic Team Deathmatch! This fast-paced, high-stakes airsoft event is designed to test your strategy, teamwork, and shooting skills in an exciting outdoor arena. Game Rules and Format: Players will be divided into two teams. Each team starts from its designated base. The objective is simple: eliminate as many opponents as possible while minimizing your team’s losses. The team with the most eliminations by the end of the game wins! When a player is hit, they are considered \"eliminated\" and must immediately return to their team’s base. Eliminated players have a 10-minute respawn timer. Once the timer is up, they may re-enter the battlefield, ready to rejoin the action. Communication, coordination, and tactics are key to gaining the upper hand and dominating the opposition. Safety Guidelines: All players must wear proper protective gear, including face masks and goggles, at all times. Only approved airsoft replicas are permitted on the field, and all weapons must be chrono-tested before the event. Maintain sportsmanship and adhere to the honor system—call your hits!", 2.00m, null, false, false, "Sunday Game #3", new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"), "10:00", new Guid("01ac2f70-21e0-417f-b18a-41c9aa1055b6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("07d6ad43-126f-4906-bed8-80ee555fbe26"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("17f7524e-ce89-4e78-aa9f-41f440f3466b"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("1948672c-706d-474f-b4b0-5c1f9cf6f440"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("1e83cba8-74e2-4552-9a3c-0d216016a688"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4f6d481d-b666-4a2e-83f0-4aec22accdf8"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("7ccfa9fc-ff63-42cf-ba59-30584cb0a206"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("87536500-af29-4d83-afa8-17ba088f6be8"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("9bf8c44b-651c-4141-8c07-52572a01b4a8"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("a3ccaad2-3c92-4e93-9ce0-ab122ea65668"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("c0a3e339-d04a-41e1-8b8c-cff9b8b96149"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("d837bea0-00d5-4067-89c4-3b05c55dcfdf"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("efd6ab61-0ec5-498c-ba80-61bb1dfb660c"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0e773424-25ab-4d83-a5c9-a5f3665ef336"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fc17956-b5b2-490f-bd14-b31de1289650", "AQAAAAIAAYagAAAAEA8N2sKcVQUCBr+bFvwv5HKHfb5ZCk7JhYbm6nta02rxk6iE997Ugbh3GYX755HdKA==", "20dfb824-3278-495e-a401-43fb8bdf61f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("184bf0c1-f0c6-41ba-94f4-fc8efcb6d0f9"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2828fdd-980f-41d0-bac2-2f8a30538ce6", "AQAAAAIAAYagAAAAEBZnpUEZ4czhZ1l08HcDlzfyoYPQFrFzOkkRNH8VGfko1FCMv8+/fowLEPEXir9Gvg==", "094377f0-813c-49b8-9dff-159f05206053" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2c05c9e-643e-4fd0-9ab4-b01fa657c6b2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4204148f-0754-4012-ab45-ab17bd7403aa", "AQAAAAIAAYagAAAAEI8GMjBqjbJLjQa4k7pcPhYC80H1i19qsg4SFgSkPITHQyEDa2ftdQyJjuuY6vgjaQ==", "fb7bce0c-e5a7-4ecb-8e4e-800d542498ca" });
        }
    }
}
