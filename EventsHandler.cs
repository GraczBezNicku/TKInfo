using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
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

            if ((ev.Attacker == null || ev.Target == null || (ev.Attacker == ev.Target)))
                return;

            if (Plugin.instance.Config.whitelistedRoles.Contains(ev.Attacker.Role) && Plugin.instance.Config.whitelistedRoles.Contains(ev.Target.Role))
                return;

            if (ev.Attacker.Role.Side != ev.Target.Role.Side)
                return;

            string attackerMessage = Plugin.instance.Config.attackerMessage.Replace("{target}", ev.Target.Nickname);
            string targetMessage;

            if(ev.Target.DoNotTrack)
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
                ev.Target.ClearBroadcasts();
                ev.Target.Broadcast(Plugin.instance.Config.broadcastDuration, targetMessage);
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if ((ev.Killer == null || ev.Killer == ev.Target) && Plugin.instance.Config.logSuicides && !Plugin.instance.Config.deathsNotToCountAsSuicide.Contains(ev.Handler.Type.ToString()))
            {
                string damageType = API.ConvertDamageTypeToString(ev.Handler.Type);

                string suicideAlert = Plugin.instance.Config.alertSuicide.Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId).Replace("{damageType}", damageType);
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, suicideAlert);

                if (Plugin.instance.Config.logViaDiscordWebhook)
                    API.QueueMessage(ev.Target, null, false, true, ev.Handler);

                return;
            }
            else if (ev.Killer == null)
                return;

            if (ev.Target.IsCuffed && Plugin.instance.Config.logCuffedKills)
            {
                string cuffedAlert = Plugin.instance.Config.alertMessageCuffedKilll.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, cuffedAlert);

                if(Plugin.instance.Config.logViaDiscordWebhook)
                    API.QueueMessage(ev.Target, ev.Killer, true, false, ev.Handler);

                return;
            }

            if (Plugin.instance.Config.whitelistedRoles.Contains(ev.Killer.Role) && Plugin.instance.Config.whitelistedRoles.Contains(ev.Target.Role))
                return;

            if (ev.Killer.Role.Side != ev.Target.Role.Side)
                return;
            
            string teamkillAlert = Plugin.instance.Config.alertMessageKill.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
            API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, teamkillAlert);

            if(Plugin.instance.Config.logViaDiscordWebhook)
                API.QueueMessage(ev.Target, ev.Killer, false, false, ev.Handler);
        }
    }
}
