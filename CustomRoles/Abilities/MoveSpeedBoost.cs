#nullable enable
namespace CustomRoles.Abilities;

using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;

[CustomAbility]
public class MoveSpeedBoost : PassiveAbility
{
    public override string Name { get; set; } = "Increased movement speed.";

    public override string Description { get; set; } = "Increases the player's movement speed.";

    protected override void AbilityAdded(Player player)
    {
        player.ChangeEffectIntensity(EffectType.MovementBoost, 10, 0);
        player.GetEffect(EffectType.MovementBoost);
    }

    protected override void AbilityRemoved(Player player)
    {
        player.DisableEffect(EffectType.MovementBoost);
    }
}