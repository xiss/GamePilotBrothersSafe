using System;
using System.Collections.Generic;
using System.Linq;

namespace GamePilotBrothersSafe
{
    internal class Game
    {
        private readonly List<Switcher> _field;
        private readonly int _sizeOfField;
        public event Action IsVictory;

        public Game(int sizeOfField)
        {
            _field = new List<Switcher>();
            _sizeOfField = sizeOfField;
        }


        public Switcher this[int hPosition, int vPosition]
        {
            get
            {
                return _field
                    .First(s => s.HorizontalPosition == hPosition && s.VerticalPosition == vPosition);
            }
        }

        public void Update(int hPosition, int vPosition)
        {
            foreach (var item in _field.Where(sw => sw.HorizontalPosition == hPosition || sw.VerticalPosition == vPosition))
            {
                item.IsChecked = !item.IsChecked;
            }

            CheckVictoryStatus();
        }

        private void CheckVictoryStatus()
        {
            int checkedSwitches = _field.Cast<Switcher>().Count(f => f.IsChecked == true);
            if (checkedSwitches == 0 || checkedSwitches == _sizeOfField * _sizeOfField)
                IsVictory.Invoke();
        }

        public void PrepareGame()
        {
            // Create field
            for (int vPosition = 0; vPosition < _sizeOfField; vPosition++)
            {
                for (int hPosition = 0; hPosition < _sizeOfField; hPosition++)
                {
                    _field.Add(new Switcher(vPosition, hPosition));
                }
            }

            // Shuffle field
            Random random = new Random();
            for (int i = 0; i < _field.Count; i++)
            {
                Update(random.Next(0, _sizeOfField), random.Next(0, _sizeOfField));
            }
        }
    }
}
