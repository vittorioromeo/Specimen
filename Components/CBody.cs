#region
using System;
using SFMLStart.Vectors;
using VeeCollision;
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter.Components
{
    public class CBody : Component
    {
        public CBody(Body mBody) { Body = mBody; }

        public Body Body { get; set; }

        #region Shortcut Properties
        public World World { get { return Body.World; } }
        public SSVector2I Position { get { return Body.Position; } set { Body.Position = value; } }
        public SSVector2I Velocity { get { return Body.Velocity; } set { Body.Velocity = value; } }
        public SSVector2I HalfSize { get { return Body.HalfSize; } set { Body.HalfSize = value; } }
        public Action<CollisionInfo> OnCollision { get { return Body.OnCollision; } set { Body.OnCollision = value; } }
        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
        public int Left { get { return Position.X - HalfSize.X; } }
        public int Right { get { return Position.X + HalfSize.X; } }
        public int Top { get { return Position.Y - HalfSize.Y; } }
        public int Bottom { get { return Position.Y + HalfSize.Y; } }
        #endregion

        public override void Added()
        {
            World.AddBody(Body);
            Body.UserData = Entity;
        }
        public override void Removed() { World.RemoveBody(Body); }
        public override void Update(float mFrameTime) { Body.Update(mFrameTime); }
    }
}