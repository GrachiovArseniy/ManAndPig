using System;

namespace ManAndPig.Model
{
    public class Player : Transformable
    {
        public Player(GameField gameField, Position startPosition) : base(gameField, startPosition) { }

        public override void MakeAbility()
        {
            gameField.SetBomb(Position);
        }
        protected override void OnMoved(Position oldPosition, Position newPosition)
        {
            gameField.SetObject(newPosition, Object.Player);
        }

        protected override void CellUpdated()
        {
            if (currentCell.CurrentObject == Object.Enemy)
            {
                Destroy(new EnemyKill());
            }
            else if (currentCell.HasBomb)
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
