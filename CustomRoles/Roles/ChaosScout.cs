#nullable enable
using Exiled.API.Features;

namespace CustomRoles.Roles;

using System.Collections.Generic;
using CustomRoles.Abilities;
using CustomRoles.API;

using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using PlayerRoles;

[CustomRole(RoleTypeId.ChaosRifleman)]
public class ChaosScout : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 100;

    public StartTeam StartTeam { get; set; } = StartTeam.Chaos;

    public override uint Id { get; set; } = 14;

    public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRifleman;

    public override int MaxHealth { get; set; } = 85;

    public override string Name { get; set; } = "Chaos Scout";

    public override string Description { get; set; } =
        "A Chaos Scout which runs faster & has ability to run even faster";

    public override string CustomInfo { get; set; } = "Chaos Scout";

    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
        RoleSpawnPoints = new List<RoleSpawnPoint>
        {
            new()
            {
                Role = RoleTypeId.ChaosRifleman,
                Chance = 100,
            },
        },
    };

    public override List<string> Inventory { get; set; } = new()
    {
        $"{ItemType.GunA7}",
        $"{ItemType.ArmorCombat}",
        $"{ItemType.Medkit}",
        $"{ItemType.GrenadeFlash}",
        $"{ItemType.KeycardChaosInsurgency}",
        $"{ItemType.SCP500}",
    };

    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
    {
        {
            AmmoType.Nato762, 90
        },
    };

    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new Sprint(),
        new MoveSpeedBoost(),
    };

    protected override void SubscribeEvents()
    {
        Log.Debug($"{nameof(SubscribeEvents)}: Loading medic events..");
        Player.PickingUpItem += OnPickingUpItem;
        base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
        Log.Debug($"{nameof(UnsubscribeEvents)}: Unloading medic events..");
        Player.PickingUpItem -= OnPickingUpItem;
        base.UnsubscribeEvents();
    }

    private void OnUsingMedicalItem(UsingItemEventArgs ev)
    {
        if (Check(ev.Player) && ev.Item.Type == ItemType.SCP268)
            ev.IsAllowed = false;
    }

    private void OnPickingUpItem(PickingUpItemEventArgs ev)
    {
        if (Check(ev.Player) && ev.Pickup.Type == ItemType.SCP268)
            ev.IsAllowed = false;
    }

    private void OnDroppingItem(DroppingItemEventArgs ev)
    {
        if (Check(ev.Player) && ev.Item.Type == ItemType.SCP268)
            ev.IsAllowed = false;
    }
}
