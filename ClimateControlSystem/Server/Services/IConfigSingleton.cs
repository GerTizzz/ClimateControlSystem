﻿using ClimateControlSystem.Server.Resources.Common;

namespace ClimateControlSystem.Server.Services
{
    public interface IConfigSingleton
    {
        public Config Config { get; }

        Task UpdateConfig(Config config);

        Task TrySetInitialConfig(Config config);
    }
}