#nullable enable
using Exiled.API.Enums;

namespace CustomRoles.Roles;

using System.Collections.Generic;
using CustomRoles.Abilities;
using CustomRoles.API;

using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;

using PlayerRoles;

[CustomRole(RoleTypeId.Scp0492)]

public class GuardZombie : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 5;

    public StartTeam StartTeam { get; set; } = StartTeam.Scp | StartTeam.Revived;

    /// <inheritdoc/>
    public override uint Id { get; set; } = 17;

    public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;

    public override int MaxHealth { get; set; } = 100;

    public override string Name { get; set; } = "Guard Zombie";

    public override string Description { get; set; } =
        "A regular zombie that was formally a facility guard";

    /// <inheritdoc/>
    public override string CustomInfo { get; set; }

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
    };

    public override List<string> Inventory { get; set; } = new()
    {
        $"{ItemType.GunFSP9}",
        $"{ItemType.KeycardZoneManager}",
        $"{ItemType.ArmorLight}",
    };

    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
    {
        {
            AmmoType.Nato9, 30
        },
    };

    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new GainAmmo(),
        new ForceEquip(),
    };
}