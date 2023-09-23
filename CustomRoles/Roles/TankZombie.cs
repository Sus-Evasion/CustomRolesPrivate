#nullable enable
namespace CustomRoles.Roles;

using System.Collections.Generic;
using System.ComponentModel;
using CustomRoles.Abilities;
using CustomRoles.API;

using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;

using PlayerRoles;
using PlayerStatsSystem;

[CustomRole(RoleTypeId.Scp0492)]
public class TankZombie : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 10;

    public StartTeam StartTeam { get; set; } = StartTeam.Scp | StartTeam.Revived;

    public override uint Id { get; set; } = 13;

    public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;

    public override int MaxHealth { get; set; } = 700;

    public override string Name { get; set; } = "Juggernaut Zombie";

    public override string Description { get; set; } =
        "A slightly slower zombie with double the regular health. As you take damage your AHP meter will fill. The higher it's value, the less damage you take.";

    public override string CustomInfo { get; set; } = "Juggernaut Zombie";

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
    };

    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new HumeGenerator(),
        new MoveSpeedReduction(),
    };
}
