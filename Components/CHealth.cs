#region
using VeeEntitySystem2012;

#endregion

namespace TestGenericShooter.Components
{
    public class CHealth : Component
    {
        private readonly CStats _cStats;
        private int _health;
        private int _maxHealth;

        public CHealth(int mHealth) { _maxHealth = _health = mHealth; }

        public CHealth(CStats mCStats)
        {
            _cStats = mCStats;
            RecalculateMaxHealth();
            _health = _maxHealth;

            _cStats.Strength.OnChanged += RecalculateMaxHealth;
            _cStats.Endurance.OnChanged += RecalculateMaxHealth;
        }

        public int Health
        {
            get { return _health; }

            set
            {
                _health = value <= _maxHealth ? value : _maxHealth;
                if (_health <= 0) Entity.Destroy();
            }
        }

        private void RecalculateMaxHealth() { _maxHealth = _cStats.Endurance.Total*10 + _cStats.Strength.Total*5; }
    }
}