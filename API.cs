using DSharp4Webhook.Core;
using Exiled.API.Features;
using MEC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TKInfo
{
    public static class API
    {
        public enum CustomTeam
        {
            MTF,
            SCP,
            CHI,
        };

        public static CustomTeam SetCustomTeam(Team originalTeam)
        {
            CustomTeam team;
            
            switch(originalTeam)
            {
                case Team.SCP: team = CustomTeam.SCP; break;
                case Team.MTF: team = CustomTeam.MTF; break;
                case Team.CHI: team = CustomTeam.CHI; break;
                case Team.CDP: team = CustomTeam.CHI; break;
                case Team.RSC: team = CustomTeam.MTF; break;
                default: team = CustomTeam.SCP; break;
            }

            return team;
        }


        public static void SendKillWebhook(Player target, Player attacker, bool cuffed)
        {
            string message;

            if(!cuffed)
                message = Plugin.instance.Config.webhookKillText.Replace("{Time}", DateTime.Now.ToString()).Replace("{target}", target.Nickname).Replace("{targetID}", target.RawUserId).Replace("{targetRole}", target.Role.ToString()).Replace("{attacker}", attacker.Nickname).Replace("{attackerID}", attacker.RawUserId).Replace("{attackerRole}", attacker.Role.ToString()).Replace("{roleID}", Plugin.instance.Config.staffRoleID);
            else
                message = Plugin.instance.Config.webhookCuffKillText.Replace("{Time}", DateTime.Now.ToString()).Replace("{target}", target.Nickname).Replace("{targetID}", target.RawUserId).Replace("{targetRole}", target.Role.ToString()).Replace("{attacker}", attacker.Nickname).Replace("{attackerID}", attacker.RawUserId).Replace("{attackerRole}", attacker.Role.ToString()).Replace("{roleID}", Plugin.instance.Config.staffRoleID);

            new Thread(() =>
            {
                HttpClient httpClient = new HttpClient();

                var SuccessWebHook = new
                {
                    username = "TKInfo",
                    content = message,
                    avatar_url = Plugin.instance.Config.webhookAvatarURL
                };

                var content = new StringContent(JsonConvert.SerializeObject(SuccessWebHook), Encoding.UTF8, "application/json");
                httpClient.PostAsync(Plugin.instance.Config.webhookURL, content).Wait();
            }).Start();
        }
    }
}
