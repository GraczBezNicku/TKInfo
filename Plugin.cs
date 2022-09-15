using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;

namespace TKInfo
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin instance;
        private EventsHandler eventsHandler;

        public override string Name => "TKInfo";
        public override string Author => "GBN";
        public override Version Version => new Version(2, 4, 0);
        public override Version RequiredExiledVersion => new Version(5, 2, 1);

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public override void OnEnabled()
        {
            instance = this;
            eventsHandler = new EventsHandler();

            Exiled.Events.Handlers.Player.Hurting += eventsHandler.OnHurt;
            Exiled.Events.Handlers.Player.Dying += eventsHandler.OnDying;
            Exiled.Events.Handlers.Player.Verified += eventsHandler.OnVerified;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= eventsHandler.OnHurt;
            Exiled.Events.Handlers.Player.Dying -= eventsHandler.OnDying;
            Exiled.Events.Handlers.Player.Verified -= eventsHandler.OnVerified;

            coroutines.Clear();

            eventsHandler = null;
            instance = null;

            base.OnDisabled();
        }
    }
}