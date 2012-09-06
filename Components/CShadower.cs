#region
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using TestGenericShooter.Resources;
using VeeEntitySystem2012;
using VeeShadow;

#endregion

namespace TestGenericShooter.Components
{
    public class CShadower : Component
    {
        private readonly CBody _cBody;
        private readonly Color _color;
        private readonly SPGame _game;
        private readonly List<Vertex[]> _vertexArrays;

        public CShadower(SPGame mGame, CBody mCBody, bool mDraw, Color mColor, int mMultiplier = 35)
        {
            _game = mGame;
            _cBody = mCBody;
            _color = mColor;
            _vertexArrays = new List<Vertex[]>();
            ShadowCaster = new ShadowCaster(_cBody.Position, mMultiplier);

            if (mDraw) _game.AddDrawAction(Draw, -10);
        }

        public ShadowCaster ShadowCaster { get; private set; }

        public override void Update(float mFrameTime)
        {
            _vertexArrays.Clear();
            var hulls = Manager.GetEntitiesByTag(Tags.Wall).Select(x => x.GetComponentUnSafe<CBody>()).Select(y => new AABBHull(y.Position, y.HalfSize));
            ShadowCaster.Position = _cBody.Position;
            ShadowCaster.CalculateShadows(hulls);

            foreach (var polygon in ShadowCaster.Polygons)
            {
                var vertexArray = polygon.GetVertexArray(1f/SPUtils.Divisor, _color);
                _vertexArrays.Add(vertexArray);
            }
        }

        private void Draw() { foreach (var array in _vertexArrays) _game.GameWindow.RenderWindow.Draw(array, PrimitiveType.Quads); }
    }
}