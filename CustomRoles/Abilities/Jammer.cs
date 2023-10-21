#nullable enable

namespace CustomRoles.Abilities;

using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Roles;
using MEC;

[CustomAbility]
public class Jammer : ActiveAbility
{
    public override string Name { get; set; } = "Jammer";

    public override string Description { get; set; } = "Jams SCP 079 in same room for 10 seconds | Hacks and Jam doors power in current room for 10 seconds if SCP 079 is not present";

    public override float Duration { get; set; } = 10f;

    public float LockdownDuration { get; set; } = 10f;

    public override float Cooldown { get; set; } = 120f;

    /// <inheritdoc/>
    protected override void AbilityUsed(Player player)
    {
        Room room = player.CurrentRoom;

        room.TurnOffLights(LockdownDuration);

        foreach (Player p in Player.List)
        {
            if (p.Role.Is(out Scp079Role scp079))
            {
                if (scp079.Camera != null && scp079.Camera.Room == room)
                    scp079.LoseSignal(LockdownDuration);
                p.ShowHint($"<color=red>you have been jammed by <b>{player}</b></color>", 9);
                player.ShowHint("SCP 079 has been jammed for 10 seconds", 5f);
            }
            else
            {
                foreach (Door door in room.Doors)
                {
                    if (door == null ||
                        (door.Type == DoorType.Scp079First && door.Type == DoorType.Scp079Second) ||
                        (door.DoorLockType > 0) ||
                        door.Type.IsElevator())
                        continue;

                    Log.Debug("Opening a door!");

                    door.IsOpen = true;
                    door.ChangeLock(DoorLockType.NoPower);

                    Timing.CallDelayed(LockdownDuration, () => { door.Unlock(); });
                }
            }
        }
    }

    /// <inheritdoc/>
    protected override void AbilityEnded(Player player)
    {
        player.Health = player.Health - 10;
    }
}
