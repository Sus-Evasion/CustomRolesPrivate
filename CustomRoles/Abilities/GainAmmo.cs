#nullable enable
namespace CustomRoles.Abilities;

using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using CustomPlayerEffects;
using MEC;

[CustomAbility]
public class GainAmmo : ActiveAbility
{
    private readonly List<CoroutineHandle> coroutines = new ();

    public override string Name { get; set; } = "Gain Ammo";

    public override string Description { get; set; } = "Your inventory will be refilled with 90 9x19mm ammo";

    public override float Duration { get; set; } = 0.1f;

    public override float Cooldown { get; set; } = 120f;

    protected override void AbilityUsed(Player player)
    {
        try
        {
            Log.Debug($"{Name} used by player");
            player.Ammo[ItemType.Ammo9x19] = 90;
        }
        catch (Exception e)
        {
            Log.Error($"{e}\n{e.StackTrace}");
        }
    }

    protected override void AbilityEnded(Player player)
    {
        Log.Debug($"{Name} ended, successfully added 90 9x19mm to player's inventory.");
    }
}