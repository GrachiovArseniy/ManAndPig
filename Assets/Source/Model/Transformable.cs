using System;
using System.Collections;
using System.Threading.Tasks;

namespace ManAndPig.Model
{
    public abstract class Transformable
    {
        public Transformable(GameField gameField, Position startPosition)
        {
            this.gameField = gameField;
            Position = startPosition;
        }

        public event Action<DieType> Diying;

        public Position Position { get; protected set; }
        protected GameField gameField;
        protected IReadOnlyCell currentCell;

        public void MoveTo(Position newPosition) 
        {
            if (gameField.HasCell(newPosition) == false)
            {
                throw new InvalidOperationException();
            }

            if (CanMoveTo(newPosition) == false)
            {
                throw new InvalidOperationException();
            }

            OnMovedBase(Position, newPosition);
        }

        public virtual void MakeAbility() { }

        protected virtual void OnMoved(Position oldPosition, Position newPosition) { }

        protected virtual void CellUpdated() { }

        private void OnMovedBase(Position oldPosition, Position newPosition)
        {
            gameField.GetCell(oldPosition).ModelUpdated -= CellUpdated;
            currentCell = gameField.GetCell(newPosition);
            currentCell.ModelUpdated += CellUpdated;

            gameField.RemoveObject(oldPosition);

            OnMoved(oldPosition, newPosition);

            Position = newPosition;

            ((dynamic)currentCell).Update();
        }

        public virtual bool CanMoveTo(Position newPosition)
        {
            return false;
        }

        internal void Destroy(DieType dieType)
        {
            Diying?.Invoke(dieType);
        }
    }
}
