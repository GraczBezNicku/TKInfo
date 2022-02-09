using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKInfo
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool logViaDiscordWebhook { get; set; } = false;
        public string webhookURL { get; set; } = "";
        public string staffRoleID { get; set; } = "";
        public string webhookAvatarURL { get; set; } = "https://cdn.discordapp.com/attachments/434037173281488899/940610688760545290/mrozonyhyperthink.jpg";
        public string webhookKillText { get; set; } = ":red_square: [TEAM-KILL] :red_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by their teammate {attacker} ({attackerID}) playing as {attackerRole}. <@&{roleID}>";
        public string webhookCuffKillText { get; set; } = ":orange_square: [CUFF-KILL] :orange_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by {attacker} ({attackerID}) playing as {attackerRole} whilst cuffed. <@&{roleID}>";
        public string webhookSuicideText { get; set; } = ":yellow_square: [SUICIDE] :yellow_square: {Time} {target} ({targetID}) playing as {targetRole} has commited suicide by {damageType} <@&{roleID}>";

        public bool notifyAttacker { get; set; } = true;
        public string attackerMessage { get; set; } = "<color=green>[TKINFO]</color> You have attacked your teammate <color=lime>{target}</color>!";
        public string targetMessage { get; set; } = "<color=green>[TKINFO]</color> You have been attacked by your teammate <color=maroon>{attacker} ({attackerID})</color>";
        public ushort broadcastDuration { get; set; } = 3;

        public bool logCuffedKills { get; set; } = false;
        public bool logSuicides { get; set; } = true;
        public bool alertOnlyWhenDead { get; set; } = true;
        public string alertMessageKill { get; set; } = "<color=green>[TKINFO]</color> <color=maroon>{killer} ({killerID})</color> has killed their teammate <color=lime>{target} ({targetID})</color>!";
        public string alertMessageCuffedKilll { get; set; } = "<color=green>[TKINFO]</color> <color=maroon>{killer} ({killerID})</color> has killed a cuffed player <color=lime>{target} ({targetID})</color>!";
        public string alertSuicide { get; set; } = "<color=green>[TKINFO]</color> <color=maroon>{target} ({targetID})</color> has commited suicide by <color=red>{damageType}</color>";
        public ushort alertDuration { get; set; } = 3;

        public bool canPlayersReport { get; set; } = true;
        public string playerNotFoundResponse { get; set; } = "Player doesn't exit or has left the server.";
        public string successfulReportResponse { get; set; } = "Player has been reported to online staff (if any).";
        public string reportWrongUsageResponse { get; set; } = "Wrong usage: .report Nickname";
        public string reportingIsDisabledResponse { get; set; } = "Reporting has been disabled on this server.";
        public string alertReportedPlayer { get; set; } = "<color=green>[TKINFO]</color> <color=green>{reporter} ({reporterID})</color> has reported <color=maroon>{reported} ({reportedID})</color>";
        public ushort alertReportDuration { get; set; } = 6;

        public bool reportReminder { get; set; } = true;
        public string reportReminderBroadcast { get; set; } = "<color=green>[TKINFO]</color> You can report players to online staff by using <color=lime>.report Nickname</color> in your console";
        public ushort reportReminderDuration { get; set; } = 10;
    }
}
