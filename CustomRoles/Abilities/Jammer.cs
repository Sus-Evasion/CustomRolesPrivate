#nullable enable
namespace CustomRoles.Abilities;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;

[CustomAbility]
public class Jammer : ActiveAbility
{
    public override string Name { get; set; } = "Jammer";

    public override string Description { get; set; } = "Throws SCP 2176 at targeted location";

    public override float Duration { get; set; } = 0.1f;

    public override float Cooldown { get; set; } = 180f;

    protected override void AbilityUsed(Player player)
    {
        player.ThrowGrenade(ProjectileType.Scp2176);
    }

    protected override void AbilityEnded(Player player)
    {
        player.Health = player.Health - 10;
    }
}