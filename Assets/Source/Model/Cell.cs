using System;

namespace ManAndPig.Model
{
    public class Cell : IReadOnlyCell
    {
        public Object CurrentObject { get; set; }
        public bool HasBomb { get; set; }

        public event Action ModelUpdated;
        public event Action ViewUpdated;

        internal void Update()
        {
            ModelUpdated?.Invoke();
        }

        internal void UpdateView()
        {
            ViewUpdated?.Invoke();
        }
    }

    public enum Object
    {
        None,
        Player,
        Enemy
    }
}