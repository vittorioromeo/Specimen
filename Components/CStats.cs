#region
using VeeEntity;

#endregion

namespace Specimen.Components
{
    public class CStats : Component
    {
        public CStats(int mStrength, int mEndurance, int mAglity, int mSpeed)
        {
            Strength = new Stat(mStrength);
            Endurance = new Stat(mEndurance);
            Agility = new Stat(mAglity);
            Speed = new Stat(mSpeed);
        }

        public Stat Strength { get; set; }
        public Stat Endurance { get; set; }
        public Stat Agility { get; set; }
        public Stat Speed { get; set; }
    }
}