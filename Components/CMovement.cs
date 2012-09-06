#region
using SFMLStart.Utilities;
using SFMLStart.Vectors;
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter.Components
{
    public class CMovement : Component
    {
        private readonly CBody _cBody;

        public CMovement(CBody mCBody) { _cBody = mCBody; }

        public float Angle { get; set; }
        public float Speed { get; set; }
        public float Acceleration { get; set; }

        public void Stop()
        {
            Speed = 0;
            Acceleration = 0;
        }

        public override void Update(float mFrameTime)
        {
            var angleVector = Utils.Math.Angles.ToVectorDegrees(Angle);
            _cBody.Velocity = new SSVector2I(angleVector*Speed);
            Speed += Acceleration;
        }
    }
}