using System;

namespace ManAndPig.Model
{
    public class Enemy : Transformable
    {
        public Enemy(GameField gameField, Position startPosition) : base(gameField, startPosition) { }

        public bool Angry { get; private set; } = false;

        public override void MakeAbility()
        {
            Angry = true;
        }

        public void StopAbility()
        {
            Angry = false;
        }

        protected override void OnMoved(Position oldPosition, Position newPosition)
        {
            gameField.SetObject(newPosition, Object.Enemy);
        }
        protected override void CellUpdated()
        {
            if (currentCell.HasBomb)
            {
                gameField.DefuseBomb(Position);
                Destroy(new BombKill());
            }
        }

        public override bool CanMoveTo(Position newPosition)
        {
            return (newPosition - Position).Magnitude == 1 && gameField.HasCell(newPosition);
        }
    }
}