namespace Yupi.Util.Settings
{
    using System;

    using Config.Net;

    public static class GameSettings
    {
        #region Fields

        public static readonly Setting<bool> EnableCamera = new Setting<bool>("game.feature.camera", false);

        #endregion Fields

        #region Constructors

        static GameSettings()
        {
            Cfg.Configuration.UseIniFile(Settings.GetPath("game.ini"));
        }

        #endregion Constructors
    }
}