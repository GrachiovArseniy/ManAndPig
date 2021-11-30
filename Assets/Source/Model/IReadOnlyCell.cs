using System;

namespace ManAndPig.Model
{
    public interface IReadOnlyCell
    {
        public Object CurrentObject { get; }
        public bool HasBomb { get; }

        public event Action ModelUpdated;

        internal void Update() { }
    }
}