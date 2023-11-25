#nullable enable
using Exiled.API.Enums;

namespace CustomRoles.Roles;

using System.Collections.Generic;

using CustomRoles.API;

using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;

using PlayerRoles;

[CustomRole(RoleTypeId.NtfSergeant)]
public class RedRightHand : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 15;

    public StartTeam StartTeam { get; set; } = StartTeam.Guard;

    public override uint Id { get; set; } = 16;

    public override RoleTypeId Role { get; set; } = RoleTypeId.NtfCaptain;

    public override int MaxHealth { get; set; } = 150;

    public override string Name { get; set; } = "RedRightHand";

    public override List<string> Inventory { get; set; } = new()
    {
        $"{ItemType.KeycardMTFCaptain}",
        $"{ItemType.ArmorCombat}",
        $"{ItemType.GrenadeHE}",
        "SR-119",
        $"{ItemType.SCP500}",
        "Hat of Disguise",
        $"{ItemType.Radio}",
    };

    public override string Description { get; set; } =
        "serves as the O5 Council's special operations unit.";

    public override string CustomInfo { get; set; } = "MTF Alpha-1 (“Red Right Hand”)";

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        RoleSpawnPoints = new List<RoleSpawnPoint>
        {
            new()
            {
                Role = RoleTypeId.FacilityGuard,
                Chance = 100,
            },
        },
    };

    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
    {
        {
            AmmoType.Nato556, 120
        },
    };
}