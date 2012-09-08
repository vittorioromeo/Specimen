#region
using VeeEntity;

#endregion

namespace Specimen.Components
{
    public class CChild : Component
    {
        private readonly CBody _cBody;
        private readonly Entity _parent;
        private readonly CBody _parentBody;

        public CChild(Entity mParent, CBody mCBody)
        {
            _parent = mParent;
            _cBody = mCBody;
            _parentBody = _parent.GetComponentUnSafe<CBody>();

            _parent.OnDestroy += () => Entity.Destroy();
        }

        public override void Update(float mFrameTime) { _cBody.Position = _parentBody.Position; }
    }
}