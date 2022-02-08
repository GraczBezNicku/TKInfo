using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKInfo
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Report : ICommand
    {
        public string Command { get; } = "report";

        public string[] Aliases { get; } = {};

        public string Description { get; } = "Lets you notify online staff about a disruptive player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(!Plugin.instance.Config.canPlayersReport)
            {
                response = Plugin.instance.Config.reportingIsDisabledResponse;
                return false;
            }

            if(!(sender is PlayerCommandSender playerSender))
            {
                response = "Only players can use this command.";
                return false;
            }

            if(arguments.Count < 1)
            {
                response = Plugin.instance.Config.reportWrongUsageResponse;
                return false;
            }
    
            Player player = Player.Get((sender as PlayerCommandSender).ReferenceHub);

            string reportedPlayerName = "";
            for (int i = 0; i != arguments.Count; i++)
                reportedPlayerName += (" " + arguments.At(i));
            reportedPlayerName = reportedPlayerName.Remove(0, 1);

            Player reportedPlayer = Exiled.API.Features.Player.List.ToList().Find(x => x.Nickname == reportedPlayerName);

            if(reportedPlayer == null)
            {
                response = Plugin.instance.Config.playerNotFoundResponse;
                return false;
            }

            string modAlert = Plugin.instance.Config.alertReportedPlayer.Replace("{reporter}", player.Nickname).Replace("{reporterID}", player.RawUserId).Replace("{reported}", reportedPlayer.Nickname).Replace("{reportedID}", reportedPlayer.RawUserId);

            foreach(Player p in Exiled.API.Features.Player.List)
            {
                if(p.CheckPermission("tkinfo.modalert") && ((!p.IsAlive && Plugin.instance.Config.alertOnlyWhenDead) || !Plugin.instance.Config.alertOnlyWhenDead))
                {
                    p.ClearBroadcasts();
                    p.Broadcast(Plugin.instance.Config.alertReportDuration, modAlert);
                }
            }

            response = Plugin.instance.Config.successfulReportResponse;
            return true;
        }
    }
}
