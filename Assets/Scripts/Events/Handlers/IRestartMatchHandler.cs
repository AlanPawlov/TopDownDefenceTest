﻿namespace Events.Handlers
{
    public interface IRestartMatchHandler : IGlobalSubscriber
    {
        void HandleRestart();
    }
}