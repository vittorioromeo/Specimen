#region
using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;
using SFMLStart.Utilities;
using SFMLStart.Vectors;
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter.Components
{
    public class CLineOfSight : Component
    {
        private readonly CBody _cBody;
        private readonly CShadower _cShadower;
        private readonly SPGame _game;

        public CLineOfSight(SPGame mGame, CBody mCBody, CShadower mCShadower)
        {
            _game = mGame;
            _cBody = mCBody;
            _cShadower = mCShadower;
            Targets = new HashSet<Tuple<Entity, SSVector2I>>();
        }

        public float Angle { get; set; }
        public int Amplitude { get; set; }
        public string TargetTag { get; set; }
        public HashSet<Tuple<Entity, SSVector2I>> Targets { get; set; }

        public override void Added() { _game.AddDrawAction(Draw, -9); }

        public override void Removed() { _game.RemoveDrawAction(Draw); }

        public override void Update(float mFrameTime)
        {
            Targets.Clear();

            var thisPoint = new SSVector2F(_cBody.Position);

            foreach (var target in Manager.GetEntitiesByTag(TargetTag))
            {
                var body = target.GetComponentUnSafe<CBody>();

                SSVector2I[] targetPoints =
                    {
                        new SSVector2I(body.X, body.Y),
                        new SSVector2I(body.Left, body.Top),
                        new SSVector2I(body.Left, body.Bottom),
                        new SSVector2I(body.Right, body.Top),
                        new SSVector2I(body.Right, body.Bottom)
                    };

                foreach (var targetPoint in targetPoints)
                {
                    var angleVector = Utils.Math.Angles.ToVectorDegrees(180 + Angle);
                    var spanVector = thisPoint - targetPoint;

                    var checkAngle = Math.Abs(angleVector.GetAngleBetween(spanVector));

                    if (double.IsNaN(checkAngle) || checkAngle > Amplitude) continue;

                    var polygons = _cShadower.ShadowCaster.Polygons;
                    if (polygons.Any(x => x.IsIntersecting(targetPoint, 100))) continue;

                    Targets.Add(new Tuple<Entity, SSVector2I>(target, targetPoint));
                    break;
                }
            }
        }

        public void Draw()
        {
            var p0 = new SSVector2F(_cBody.X.ToPixels(), _cBody.Y.ToPixels());
            var p1 = p0 + Utils.Math.Angles.ToVectorDegrees(Angle - Amplitude)*66;
            var p2 = p0 + Utils.Math.Angles.ToVectorDegrees(Angle + Amplitude)*66;

            var v0 = new Vertex(new Vector2f(p0.X, p0.Y)) {Color = new Color(255, 0, 0, 125)};
            var v1 = new Vertex(new Vector2f(p1.X, p1.Y)) {Color = new Color(255, 0, 0, 1)};
            var v2 = new Vertex(new Vector2f(p2.X, p2.Y)) {Color = new Color(255, 0, 0, 1)};

            _game.GameWindow.RenderWindow.Draw(new[] {v0, v1, v2}, PrimitiveType.Triangles);
        }
    }
}