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
public class ChaosJammer : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 100;

    public StartTeam StartTeam { get; set; } = StartTeam.Chaos;

    public override uint Id { get; set; } = 16;

    public override RoleTypeId Role { get; set; } = RoleTypeId.ChaosRifleman;

    public override int MaxHealth { get; set; } = 100;

    public override string Name { get; set; } = "Chaos Jammer";

    public override string Description { get; set; } =
        "A Chaos Rifleman that has ability to throw SCP 2194";

    public override string CustomInfo { get; set; } = "Chaos Jammer";

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
        $"{ItemType.GunAK}",
        $"{ItemType.ArmorCombat}",
        $"{ItemType.Medkit}",
        $"{ItemType.Painkillers}",
        $"{ItemType.KeycardChaosInsurgency}",
    };

    public override Dictionary<AmmoType, ushort> Ammo { get; set; } = new()
    {
        {
            AmmoType.Nato762, 90
        },
    };

    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new Jammer(),
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
