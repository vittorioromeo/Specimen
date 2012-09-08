#region
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using Specimen.Resources;
using TestGenericShooter;
using VeeEntity;
using VeeShadow;

#endregion

namespace Specimen.Components
{
    public class CShadower : Component
    {
        private readonly CBody _cBody;
        private readonly Color _color;
        private readonly SPGame _game;
        private readonly bool _isDrawn;
        private readonly List<Vertex[]> _vertexArrays;

        public CShadower(SPGame mGame, CBody mCBody, bool mIsDrawn, Color mColor, int mMultiplier = 35)
        {
            _game = mGame;
            _cBody = mCBody;
            _isDrawn = mIsDrawn;
            _color = mColor;
            _vertexArrays = new List<Vertex[]>();
            ShadowCaster = new ShadowCaster(_cBody.Position, mMultiplier);

            if (_isDrawn) _game.AddDrawAction(Draw, -10);
        }

        public ShadowCaster ShadowCaster { get; private set; }

        public override void Update(float mFrameTime)
        {
            _vertexArrays.Clear();
            var hulls = Manager.GetEntitiesByTag(Tags.Wall).Select(x => x.GetComponentUnSafe<CBody>()).Select(y => new AABBHull(y.Position, y.HalfSize));
            ShadowCaster.Position = _cBody.Position;
            ShadowCaster.CalculateShadows(hulls);

            if (!_isDrawn) return;

            foreach (var polygon in ShadowCaster.Polygons)
                _vertexArrays.Add(polygon.GetVertexArray(1f/SPUtils.Divisor, _color));
        }

        private void Draw() { foreach (var array in _vertexArrays) _game.GameWindow.RenderWindow.Draw(array, PrimitiveType.Quads); }
    }
}