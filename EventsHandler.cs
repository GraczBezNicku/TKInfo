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
            if (ev.Killer == null || ev.Target == null)
                return;

            if (ev.Target.IsCuffed && Plugin.instance.Config.logCuffedKills)
            {
                string cuffedAlert = Plugin.instance.Config.alertMessageCuffedKilll.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
                foreach(Player p in Exiled.API.Features.Player.List)
                {
                    if (p.CheckPermission("tkinfo.modalert") && ((!p.IsAlive && Plugin.instance.Config.alertOnlyWhenDead) || !Plugin.instance.Config.alertOnlyWhenDead))
                    {
                        p.ClearBroadcasts();
                        p.Broadcast(Plugin.instance.Config.alertDuration, cuffedAlert);
                    }
                }
                API.SendKillWebhook(ev.Target, ev.Killer, true);
                return;
            }

            API.CustomTeam targetCustomTeam = API.SetCustomTeam(ev.Target.Team);
            API.CustomTeam killerCustomTeam = API.SetCustomTeam(ev.Killer.Team);

            if (targetCustomTeam != killerCustomTeam)
                return;
            
            string teamkillAlert = Plugin.instance.Config.alertMessageKill.Replace("{killer}", ev.Killer.Nickname).Replace("{killerID}", ev.Killer.RawUserId).Replace("{target}", ev.Target.Nickname).Replace("{targetID}", ev.Target.RawUserId);
            foreach (Player p in Exiled.API.Features.Player.List)
            {
                if (p.CheckPermission("tkinfo.modalert") && ((!p.IsAlive && Plugin.instance.Config.alertOnlyWhenDead) || !Plugin.instance.Config.alertOnlyWhenDead))
                {
                    p.ClearBroadcasts();
                    p.Broadcast(Plugin.instance.Config.alertDuration, teamkillAlert);
                }
            }
            API.SendKillWebhook(ev.Target, ev.Killer, false);
        }
    }
}
