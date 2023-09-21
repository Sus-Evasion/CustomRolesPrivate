#nullable enable

namespace CustomRoles.Abilities;

using UnityEngine;

using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;

/// <inheritdoc />
[CustomAbility]
public class ForceEquip : PassiveAbility
{
    public override string Name { get; set; } = "Force Equip";

    public override string Description { get; set; } =
        "Allows zombie to equip gun";

    protected override void AbilityAdded(Player player)
    {
        Timing.CallDelayed(2.5f, () =>
        {
            Firearm gun = (Firearm)Item.Create(ItemType.GunFSP9);
            player.CurrentItem = gun;
            Debug.Log("Player given weapon");
        });
    }

    protected override void AbilityRemoved(Player player)
    {
        var gun = (Firearm)Item.Create(ItemType.GunFSP9);
        player.RemoveItem(gun);
        Debug.Log("Player weapon removed");
    }
}