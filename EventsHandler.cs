using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using Exiled.Events.EventArgs.Player;
using Exiled.Permissions.Extensions;
using MEC;
using PlayerStatsSystem;

namespace TKInfo
{
    public class EventsHandler
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.instance.Config.reportReminder)
                ev.Player.Broadcast(Plugin.instance.Config.reportReminderDuration, Plugin.instance.Config.reportReminderBroadcast);

            if (Plugin.instance.coroutines.Count == 0)
                Plugin.instance.coroutines.Add(Timing.RunCoroutine(API.SendAfterCooldown()));
        }

        public void OnHurt(HurtingEventArgs ev)
        {
            if (!Server.FriendlyFire)
                return;

            if (Plugin.instance.Config.disableAfterRoundEnds && Round.IsEnded)
                return;

            if ((ev.Attacker == null || ev.Player == null || (ev.Attacker == ev.Player)))
                return;

            if (Plugin.instance.Config.whitelistedRoles.Contains(ev.Attacker.Role) && Plugin.instance.Config.whitelistedRoles.Contains(ev.Player.Role))
                return;

            if (ev.Attacker.Role.Side != ev.Player.Role.Side)
                return;

            string attackerMessage = Plugin.instance.Config.attackerMessage.Replace("{target}", ev.Player.Nickname);
            string targetMessage;

            if(ev.Player.DoNotTrack)
                targetMessage = Plugin.instance.Config.targetMessage.Replace("{attacker}", ev.Attacker.Nickname).Replace("{attackerID}", "DoNotTrack");
            else
                targetMessage = Plugin.instance.Config.targetMessage.Replace("{attacker}", ev.Attacker.Nickname).Replace("{attackerID}", ev.Attacker.RawUserId);

            if (Plugin.instance.Config.notifyAttacker)
            {
                ev.Attacker.ClearBroadcasts();
                ev.Attacker.Broadcast(Plugin.instance.Config.broadcastDuration, attackerMessage);
            }

            if(Plugin.instance.Config.notifyTarget)
            {
                ev.Player.ClearBroadcasts();
                ev.Player.Broadcast(Plugin.instance.Config.broadcastDuration, targetMessage);
            }

            if (Plugin.instance.Config.logViaDiscordWebhook && Plugin.instance.Config.logHurting)
                API.QueueMessage(ev.Player, ev.Attacker, API.MessageType.Hurting, ev.DamageHandler);
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (Plugin.instance.Config.disableAfterRoundEnds && Round.IsEnded)
                return;

            if ((ev.Attacker == null || ev.Attacker == ev.Player) && Plugin.instance.Config.logSuicides && !Plugin.instance.Config.deathsNotToCountAsSuicide.Contains(ev.DamageHandler.Type.ToString()))
            {
                string damageType = API.ConvertDamageTypeToString(ev.DamageHandler.Type);

                string suicideAlert = Plugin.instance.Config.alertSuicide.Replace("{target}", ev.Player.Nickname).Replace("{targetID}", ev.Player.RawUserId).Replace("{damageType}", damageType);
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, suicideAlert);

                if (Plugin.instance.Config.logViaDiscordWebhook)
                    API.QueueMessage(ev.Player, null, API.MessageType.Suicide, ev.DamageHandler);

                return;
            }
            else if (ev.Attacker == null)
                return;

            if (ev.Player.IsCuffed && Plugin.instance.Config.logCuffedKills)
            {
                string cuffedAlert = Plugin.instance.Config.alertMessageCuffedKilll.Replace("{killer}", ev.Attacker.Nickname).Replace("{killerID}", ev.Attacker.RawUserId).Replace("{target}", ev.Player.Nickname).Replace("{targetID}", ev.Player.RawUserId);
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, cuffedAlert);

                if(Plugin.instance.Config.logViaDiscordWebhook)
                    API.QueueMessage(ev.Player, ev.Attacker, API.MessageType.Cuffed, ev.DamageHandler);

                return;
            }

            if (Plugin.instance.Config.whitelistedRoles.Contains(ev.Attacker.Role) && Plugin.instance.Config.whitelistedRoles.Contains(ev.Player.Role))
                return;

            if ((ev.Attacker.Role.Side != ev.Player.Role.Side) || ev.DamageHandler.Type == DamageType.Recontainment)
                return;
            
            string teamkillAlert = Plugin.instance.Config.alertMessageKill.Replace("{killer}", ev.Attacker.Nickname).Replace("{killerID}", ev.Attacker.RawUserId).Replace("{target}", ev.Player.Nickname).Replace("{targetID}", ev.Player.RawUserId);
            API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, teamkillAlert);

            if(Plugin.instance.Config.logViaDiscordWebhook)
                API.QueueMessage(ev.Player, ev.Attacker, API.MessageType.Normal, ev.DamageHandler);
        }
    }
}
