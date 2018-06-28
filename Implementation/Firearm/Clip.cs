using UnityEngine;

namespace WeaponSystem.Implementation.Firearm
{
    public class Clip : MonoBehaviour, IWeaponLogic, IWeaponValidationLogic
    {
        [SerializeField] private int _size = 30;
        [SerializeField] private int _counter = 0;
        public int Delta { get { return _size - _counter; } }

        public int Size { get { return _size; } }

        public int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }

        private void Awake()
        {
            Reload(Delta);
        }

        public void Reload(int delta)
        {
            _counter += delta;
        }

        public void Perform(params object[] data)
        {
            if (Validate())
                _counter--;
        }

        public bool Validate()
        {
            return _counter > 0;
        }
    }
}