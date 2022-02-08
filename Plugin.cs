using DSharp4Webhook.Core;
using Exiled.API.Features;
using System;

namespace TKInfo
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin instance;
        private EventsHandler eventsHandler;

        public override string Name => "TKInfo";
        public override string Author => "GBN";
        public override Version Version => new Version(2, 0, 0);
        public override Version RequiredExiledVersion => new Version(4, 2, 0);

        public override void OnEnabled()
        {
            instance = this;
            eventsHandler = new EventsHandler();

            Exiled.Events.Handlers.Player.Hurting += eventsHandler.OnHurt;
            Exiled.Events.Handlers.Player.Dying += eventsHandler.OnDying;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= eventsHandler.OnHurt;
            Exiled.Events.Handlers.Player.Dying -= eventsHandler.OnDying;

            eventsHandler = null;
            instance = null;

            base.OnDisabled();
        }
    }
}