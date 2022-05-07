using DSharp4Webhook.Core;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.Permissions.Extensions;
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
using Utf8Json;

namespace TKInfo
{
    public static class API
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static string ConvertDamageTypeToString(DamageType type)
        {
            string stringType = "This is a temporary damage type, If you're seeing this, please report it on discord: GraczBezNicku#1862";
            stringType = Plugin.instance.Config.deathReasons[(int)type + 1];
            return stringType;
        }

        public static void SendKillWebhook(Player target, Player attacker, bool cuffed, bool suicide, DamageHandler handler)
        {
            string message = "This is a temporary message. If you somehow see this while using this plugin normally. Please report it on discord: GraczBezNicku#1862";
            string damageType = ConvertDamageTypeToString(handler.Type);

            if (!cuffed && !suicide)
                message = Plugin.instance.Config.webhookKillText.Replace("{Time}", DateTime.Now.ToString()).Replace("{target}", target.Nickname).Replace("{targetID}", target.RawUserId).Replace("{targetRole}", target.Role.ToString()).Replace("{attacker}", attacker.Nickname).Replace("{attackerID}", attacker.RawUserId).Replace("{attackerRole}", attacker.Role.ToString()).Replace("{roleID}", Plugin.instance.Config.staffRoleID);
            else if (cuffed)
                message = Plugin.instance.Config.webhookCuffKillText.Replace("{Time}", DateTime.Now.ToString()).Replace("{target}", target.Nickname).Replace("{targetID}", target.RawUserId).Replace("{targetRole}", target.Role.ToString()).Replace("{attacker}", attacker.Nickname).Replace("{attackerID}", attacker.RawUserId).Replace("{attackerRole}", attacker.Role.ToString()).Replace("{roleID}", Plugin.instance.Config.staffRoleID);
            else if (suicide)
                message = Plugin.instance.Config.webhookSuicideText.Replace("{Time}", DateTime.Now.ToString()).Replace("{target}", target.Nickname).Replace("{targetID}", target.RawUserId).Replace("{targetRole}", target.Role.ToString()).Replace("{damageType}", damageType).Replace("{roleID}", Plugin.instance.Config.staffRoleID);

            var SuccessWebHook = new
            {
                username = "TKInfo",
                content = message,
                avatar_url = Plugin.instance.Config.webhookAvatarURL
            };

            StringContent content = new StringContent(Encoding.UTF8.GetString(Utf8Json.JsonSerializer.Serialize<object>(SuccessWebHook)), Encoding.UTF8, "application/json");
            _ = Send(content);

            /*
            new Thread(() =>
            {
                HttpClient httpClient = new HttpClient();

                var SuccessWebHook = new
                {
                    username = "TKInfo",
                    content = message,
                    avatar_url = Plugin.instance.Config.webhookAvatarURL
                };

                var content = new StringContent(Encoding.UTF8.GetString(Utf8Json.JsonSerializer.Serialize<object>(SuccessWebHook)), Encoding.UTF8, "application/json");
                httpClient.PostAsync(Plugin.instance.Config.webhookURL, content).Wait();
            }).Start();
            */
        }

        public static async Task Send(StringContent data)
        {
            HttpResponseMessage responseMessage = await HttpClient.PostAsync(Plugin.instance.Config.webhookURL, data);
            string responseMessageString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
            {
                Log.Error($"[{(int)responseMessage.StatusCode} - {responseMessage.StatusCode}] A non-successful status code was returned by Discord when trying to post to webhook. Response Message: {responseMessageString} .");
                return;
            }
        }

        public static void SendMessageToOnlineStaff(ushort duration, string message)
        {
            foreach (Player p in Exiled.API.Features.Player.List)
            {
                if (p.CheckPermission("tkinfo.modalert") && ((!p.IsAlive && Plugin.instance.Config.alertOnlyWhenDead) || !Plugin.instance.Config.alertOnlyWhenDead))
                {
                    p.ClearBroadcasts();
                    p.Broadcast(duration, message);
                }
            }
        }
    }
}
