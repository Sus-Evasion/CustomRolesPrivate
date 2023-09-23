#nullable enable
namespace CustomRoles.Abilities;

using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using MEC;

[CustomAbility]
public class HumeGenerator : ActiveAbility
{
    private readonly List<CoroutineHandle> coroutines = new ();

    public override string Name { get; set; } = "Hume Generator";

    public override string Description { get; set; } =
        "Activates a short-term spray of bio-chemicals which harden itself and will protect allies for a short duration.";

    public override float Duration { get; set; } = 15f;

    public override float Cooldown { get; set; } = 180f;

    [Description("The amount of AHP given per second")]
    public ushort ProtectionAmount { get; set; } = 3;

    protected override void AbilityUsed(Player player)
    {
        GiveHumeShield(player);
        ActivateMist(player);
    }

    protected override void UnsubscribeEvents()
    {
        foreach (CoroutineHandle handle in coroutines)
            Timing.KillCoroutines(handle);
        base.UnsubscribeEvents();
    }

    private void ActivateMist(Player ply)
    {
        foreach (Player player in Player.List)
        {
            if (player.Role.Side == ply.Role.Side && player != ply)
                coroutines.Add(Timing.RunCoroutine(DoMist(ply, player)));
        }
    }

    private void GiveHumeShield(Player ply)
    {
        ply.HumeShield = +500;
    }

    private IEnumerator<float> DoMist(Player activator, Player player)
    {
        for (int i = 0; i < Duration; i++)
        {
            if (player.HumeShield + ProtectionAmount >= player.HumeShield ||
                (player.Position - activator.Position).sqrMagnitude > 144f)
                continue;

            player.HumeShield += ProtectionAmount;

            yield return Timing.WaitForSeconds(0.75f);
        }

        if ((activator.Position - player.Position).sqrMagnitude < 144f)
            player.Health = player.Health + 50;
    }
}
