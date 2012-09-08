#region
using SFMLStart;
using SFMLStart.Data;

#endregion

namespace Specimen
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Settings.Framerate.IsLimited = false;
            Settings.Frametime.IsStatic = false;
            Settings.Framerate.Limit = 125;
            Settings.Frametime.StaticValue = 1.5f;

            var game = new SPGame();
            var window = new GameWindow(320, 240, 3);

            window.SetGame(game);
            window.Run();
        }
    }
}