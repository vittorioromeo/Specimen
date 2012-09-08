#region
using System;
using SFML.Graphics;
using SFML.Window;
using SFMLStart;
using SFMLStart.Data;
using Specimen.Resources;
using TestGenericShooter;
using VeeCollision;
using VeeEntity;

#endregion

namespace Specimen
{
    public class SPGame : Game
    {
        private readonly Manager _manager;
        private readonly World _world;

        public SPGame()
        {
            _manager = new Manager();
            _world = new World(Groups.GroupArray, 21, 16, 16.ToUnits());
            Factory = new Factory(this, _manager, _world);

            OnUpdate += _manager.Update;
            OnUpdate += UpdateInputs;
            OnUpdate += mFrameTime => GameWindow.RenderWindow.SetTitle(((int) GameWindow.FPS).ToString());

            OnDrawBeforeCamera += () => GameWindow.RenderWindow.Clear(new Color(125, 125, 185));

            InitializeInputs();
            NewGame();
        }

        public Factory Factory { get; private set; }
        public int NextX { get; private set; }
        public int NextY { get; private set; }
        public int NextAction { get; private set; }

        private void InitializeInputs()
        {
            Bind("quit", 0, () => Environment.Exit(0), null, new KeyCombination(Keyboard.Key.Escape));

            Bind("w", 0, () => NextY = -1, null, new KeyCombination(Keyboard.Key.W));
            Bind("s", 0, () => NextY = 1, null, new KeyCombination(Keyboard.Key.S));
            Bind("a", 0, () => NextX = -1, null, new KeyCombination(Keyboard.Key.A));
            Bind("d", 0, () => NextX = 1, null, new KeyCombination(Keyboard.Key.D));

            Bind("zoomin", 0, () => GameWindow.Camera.Zoom(1.001f), null, new KeyCombination(Keyboard.Key.N));
            Bind("zoomout", 0, () => GameWindow.Camera.Zoom(0.999f), null, new KeyCombination(Keyboard.Key.M));

            Bind("fire", 0, () => NextAction = 1, null, new KeyCombination(Mouse.Button.Left));
        }

        private void NewGame()
        {
            _manager.Clear();
            DebugLevel();
        }

        private void DebugLevel()
        {
            const int sizeX = 20;
            const int sizeY = 15;

            var map = new[]
                      {
                          "11111111111111111111",
                          "10010000444400000001",
                          "10100000000000000101",
                          "10000000000000000001",
                          "10000000000000000001",
                          "10000111111000000001",
                          "14000001000000000001",
                          "14001001000100000001",
                          "10001002000100000001",
                          "14001000000100000001",
                          "10001011110100000001",
                          "10000000000000000001",
                          "10100000400000000101",
                          "10044400440000000001",
                          "11111111111111111111"
                      };

            for (var iY = 0; iY < sizeY; iY++)
                for (var iX = 0; iX < sizeX; iX++)
                {
                    if (map.IsValue(iX, iY, 2))
                        Factory.Player(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY);

                    if (map.IsValue(iX, iY, 1))
                        Factory.Wall(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY, map.CalculateWall(iX, iY));

                    if (map.IsValue(iX, iY, 4))
                        Factory.Enemy(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY);

                    if (map.IsValue(iX, iY, 7))
                        Factory.BreakableWall(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY);

                    if (iX <= 0 || iY <= 0 || iX >= sizeX - 1 || iY >= sizeY - 1) continue;
                    if (!map.IsValue(iX, iY, 0)) continue;

                    if (map.IsValue(iX - 1, iY, 1))
                        if (map.IsValue(iX, iY - 1, 1))
                            Factory.Decoration(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY,
                                               Textures.Wall, Tilesets.Wall, "icurve");

                    if (map.IsValue(iX + 1, iY, 1))
                        if (map.IsValue(iX, iY - 1, 1))
                            Factory.Decoration(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY,
                                               Textures.Wall, Tilesets.Wall, "icurve", 90);

                    if (map.IsValue(iX + 1, iY, 1))
                        if (map.IsValue(iX, iY + 1, 1))
                            Factory.Decoration(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY,
                                               Textures.Wall, Tilesets.Wall, "icurve", 180);

                    if (map.IsValue(iX - 1, iY, 1))
                        if (map.IsValue(iX, iY + 1, 1))
                            Factory.Decoration(8.ToUnits() + 16.ToUnits()*iX, 8.ToUnits() + 16.ToUnits()*iY,
                                               Textures.Wall, Tilesets.Wall, "icurve", 270);
                }
        }

        private void UpdateInputs(float mFrameTime) { NextX = NextY = NextAction = 0; }
    }
}