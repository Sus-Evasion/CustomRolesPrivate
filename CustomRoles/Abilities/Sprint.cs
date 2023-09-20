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
public class Sprint : ActiveAbility
{
    private readonly List<CoroutineHandle> coroutines = new ();

    public override string Name { get; set; } = "Sprint";

    public override string Description { get; set; } = "You will gain unlimited sprint and additional movement boost that lasts 10 seconds";

    public override float Duration { get; set; } = 10f;

    public override float Cooldown { get; set; } = 90f;

    protected override void AbilityUsed(Player player)
    {
        try
        {
            Log.Debug($"{Name} enabled for {Duration}");
            player.EnableEffect(EffectType.Invigorated, 0);
            byte curIntensity = player.GetEffectIntensity<MovementBoost>();
            byte newIntensity = curIntensity += 15;
            player.ChangeEffectIntensity<MovementBoost>(newIntensity, 0);
            player.GetEffect(EffectType.MovementBoost);
        }
        catch (Exception e)
        {
            Log.Error($"{e}\n{e.StackTrace}");
        }
    }

    protected override void AbilityEnded(Player player)
    {
        Log.Debug($"{Name} ended.");
        player.DisableEffect(EffectType.Invigorated);
        player.DisableEffect(EffectType.MovementBoost);
        player.ChangeEffectIntensity<MovementBoost>(intensity: 10, 0);
        player.GetEffect(EffectType.MovementBoost);
    }
}