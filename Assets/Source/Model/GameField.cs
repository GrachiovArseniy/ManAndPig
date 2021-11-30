using System;
using System.Collections.Generic;

namespace ManAndPig.Model
{
    public class GameField
    {
        public GameField(List<List<Cell>> cells)
        {
            _cells = cells;
        }

        public IReadOnlyList<IReadOnlyList<IReadOnlyCell>> Cells => _cells;

        private readonly List<List<Cell>> _cells; // First: position X; Second: position Y;

        internal IReadOnlyCell GetCell(Position position)
        {
            return GetNormalCell(position);
        }

        internal bool HasCell(Position position)
        {
            if (position.X < 0 || position.Y < 0 || position.X >= Cells.Count)
            {
                return false;
            }

            return position.Y < Cells[position.X].Count;
        }

        internal void SetBomb(Position position)
        {
            GetNormalCell(position).HasBomb = true;
            GetNormalCell(position).UpdateView();
        }

        internal void DefuseBomb(Position position)
        {
            if (GetNormalCell(position).HasBomb == false)
            {
                throw new InvalidOperationException();
            }

            GetNormalCell(position).HasBomb = false;
            GetNormalCell(position).UpdateView();
        }

        internal void SetObject(Position position, Object newObject)
        {
            GetNormalCell(position).CurrentObject = newObject;
        }

        internal void RemoveObject(Position position)
        {
            GetNormalCell(position).CurrentObject = Object.None;
        }

        private Cell GetNormalCell(Position position)
        {
            return _cells[position.X][position.Y];
        }
    }
}