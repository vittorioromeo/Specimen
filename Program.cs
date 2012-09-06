#region
using SFMLStart;
using SFMLStart.Data;

#endregion

namespace TestGenericShooter
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Settings.Framerate.Limit = 125;
            Settings.Framerate.IsLimited = true;
            Settings.Frametime.StaticValue = 1.5f;
            Settings.Frametime.IsStatic = false;

            var game = new SPGame();
            var window = new GameWindow(320, 240, 3);

            window.SetGame(game);
            window.Run();
        }
    }
}