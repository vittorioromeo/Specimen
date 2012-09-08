#region
using System;
using SFMLStart.Utilities;

#endregion

namespace Specimen
{
    public class Stat
    {
        private int _base;
        private int _bonus;

        public Stat(int mBase) { Base = mBase; }

        public Action OnChanged { get; set; }

        public int Base
        {
            get { return _base; }
            set
            {
                _base = value;
                OnChanged.SafeInvoke();
            }
        }

        public int Bonus
        {
            get { return _bonus; }
            set
            {
                _bonus = value;
                OnChanged.SafeInvoke();
            }
        }

        public int Total { get { return Base + Bonus; } }
    }
}