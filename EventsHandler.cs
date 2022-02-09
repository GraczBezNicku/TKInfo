using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using Exiled.Permissions.Extensions;
using PlayerStatsSystem;

namespace TKInfo
{
    public class EventsHandler
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.instance.Config.reportReminder)
                ev.Player.Broadcast(Plugin.instance.Config.reportReminderDuration, Plugin.instance.Config.reportReminderBroadcast);
        }

        public void OnHurt(HurtingEventArgs ev)
        {
            if ((ev.Attacker == null || ev.Target == null || (ev.Attacker == ev.Target)))
                return;

            API.CustomTeam targetCustomTeam = API.SetCustomTeam(ev.Target.Team);
            API.CustomTeam attackerCustomTeam = API.SetCustomTeam(ev.Attacker.Team);

            if (targetCustomTeam != attackerCustomTeam)
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

            ev.Target.ClearBroadcasts();
            ev.Target.Broadcast(Plugin.instance.Config.broadcastDuration, targetMessage);
        }

        public void OnDying(DyingEventArgs ev)
        {
            if ((ev.Killer == null || ev.Killer == ev.Target) && Plugin.instance.Config.logSuicides && ev.Handler.Type != Exiled.API.Enums.DamageType.PocketDimension)
            {
                string suicideAlert = Plugin.instance.Config.alertSuicide.Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId).Replace("{damageType}", ev.Handler.Type.ToString());
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, suicideAlert);

                if (Plugin.instance.Config.logViaDiscordWebhook)
                    API.SendKillWebhook(ev.Target, null, false, true, ev.Handler);

                return;
            }
            else if (ev.Killer == null)
                return;

            if (ev.Target.IsCuffed && Plugin.instance.Config.logCuffedKills)
            {
                string cuffedAlert = Plugin.instance.Config.alertMessageCuffedKilll.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
                API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, cuffedAlert);

                if(Plugin.instance.Config.logViaDiscordWebhook)
                    API.SendKillWebhook(ev.Target, ev.Killer, true, false, ev.Handler);

                return;
            }

            API.CustomTeam targetCustomTeam = API.SetCustomTeam(ev.Target.Team);
            API.CustomTeam killerCustomTeam = API.SetCustomTeam(ev.Killer.Team);

            if (targetCustomTeam != killerCustomTeam)
                return;
            
            string teamkillAlert = Plugin.instance.Config.alertMessageKill.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
            API.SendMessageToOnlineStaff(Plugin.instance.Config.alertDuration, teamkillAlert);

            if(Plugin.instance.Config.logViaDiscordWebhook)
                API.SendKillWebhook(ev.Target, ev.Killer, false, false, ev.Handler);
        }
    }
}
