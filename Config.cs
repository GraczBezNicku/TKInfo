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
        public int discordWebhookCooldown { get; set; } = 10;
        [Description("Whenever TKInfo recieves a rate-limit response code, webhooks will be sent with this delay (in seconds) to avoid restrictions.")]
        public int forcedCooldownTimer { get; set; } = 600;
        public string webhookURL { get; set; } = "";
        public string staffRoleID { get; set; } = "";
        public string webhookAvatarURL { get; set; } = "https://cdn.discordapp.com/attachments/434037173281488899/940610688760545290/mrozonyhyperthink.jpg";
        public string webhookKillText { get; set; } = ":red_square: [TEAM-KILL] :red_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by their teammate {attacker} ({attackerID}) playing as {attackerRole} with {damageType}. <@&{roleID}>";
        public string webhookCuffKillText { get; set; } = ":orange_square: [CUFF-KILL] :orange_square: {Time} {target} ({targetID}) playing as {targetRole} was killed by {attacker} ({attackerID}) playing as {attackerRole} whilst cuffed with {damageType}. <@&{roleID}>";
        public string webhookSuicideText { get; set; } = ":yellow_square: [SUICIDE] :yellow_square: {Time} {target} ({targetID}) playing as {targetRole} has commited suicide by {damageType} <@&{roleID}>";
        public string webhookHurtText { get; set; } = ":brown_square: [HURT] :brown_square: {Time} {target} ({targetID}) playing as {targetRole} was hurt by their teammate {attacker} ({attackerID}) playing as {attackerRole} with {damageType} <@&{roleID}>";
        public List<RoleType> whitelistedRoles { get; set; } = new List<RoleType>()
        {
            RoleType.ClassD
        };
        public List<string> deathReasons { get; set; } = new List<string>()
        {
            "Unknown", //note that not all of them will actually occur, I just need them all to convert from DamageType later...
            "Falldown",
            "Warhead",
            "Decontamination",
            "Asphyxiation",
            "Posion",
            "Bleeding",
            "Firearm",
            "MicroHid",
            "Tesla",
            "Scp",
            "Explosion",
            "Scp018",
            "Scp207",
            "Recontainment",
            "Crushed",
            "FemurBreaker",
            "PocketDimension",
            "FriendlyFireDetector",
            "SeveredHands",
            "Custom",
            "Scp049",
            "Scp0492",
            "Scp096",
            "Scp173",
            "Scp106",
            "Scp939",
            "Crossvec",
            "Logicer",
            "Revolver",
            "Shotgun",
            "AK",
            "Com15",
            "Com18",
            "Fsp9",
            "E11Sr",
            "Hypothermia",
            "ParticleDisruptor"
        };
        public List<string> deathsNotToCountAsSuicide { get; set; } = new List<string>()
        {
            "Warhead",
            "Decontamination",
            "Asphyxiation",
            "Poison",
            "Recontainment",
            "FemurBreaker",
            "PocketDimension",
            "Hypothermia",
        };

        public bool notifyAttacker { get; set; } = true;
        public bool notifyTarget { get; set; } = true;
        public string attackerMessage { get; set; } = "<color=green>[TKINFO]</color> You have attacked your teammate <color=lime>{target}</color>!";
        public string targetMessage { get; set; } = "<color=green>[TKINFO]</color> You have been attacked by your teammate <color=maroon>{attacker} ({attackerID})</color>";
        public ushort broadcastDuration { get; set; } = 3;

        public bool logHurting { get; set; } = true;
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
