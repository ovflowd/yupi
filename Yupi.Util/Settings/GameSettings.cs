namespace Yupi.Util.Settings
{
    public static class GameSettings
    {
        public static readonly Setting<bool> EnableCamera = new Setting<bool>("game.feature.camera", false);

        static GameSettings()
        {
            Cfg.Configuration.UseIniFile(Settings.GetPath("game.ini"));
        }
    }
}