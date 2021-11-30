using System;
using ManAndPig.Model;
using UnityEngine;

namespace ManAndPig.Input
{
    public class EnemyPresenterInput : InputPresenter<Enemy>
    {
        [SerializeField] private TransformablePresenter<Player> _player;

        private void FixedUpdate()
        {
            if (presenter.Moved)
            {
                return;
            }

            if (_player.Position.X == presenter.Position.X || _player.Position.Y == presenter.Position.Y)
            {
                presenter.MakeAbility();
            }
            else
            {
                ((dynamic)presenter).StopAbility();
            }

            var random = new System.Random();

            Position newPosition;
            float speed = Config.EnemyStepSpeed;

            if (random.Next(0, 2) == 0)
            {
                if (random.Next(0, 2) == 0)
                {
                    newPosition = presenter.Position + new Position(0, 1);
                }
                else
                {
                    newPosition = presenter.Position + new Position(0, -1);
                }
            }
            else
            {
                if (random.Next(0, 2) == 0)
                {
                    newPosition = presenter.Position + new Position(1, 0);
                }
                else
                {
                    newPosition = presenter.Position + new Position(-1, 0);
                }
            }

            if (((dynamic)presenter).Angry)
            {
                newPosition = presenter.Position + (_player.Position - presenter.Position).Normalized;
                speed = Config.AngryEnemyStepSpeed;
            }

            MovePresenter(newPosition, speed);
        }

        private void MovePresenter(Position newPosition, float speed)
        {
            if (presenter.CanMoveTo(newPosition) == false)
            {
                return;
            }

            StartCoroutine(presenter.MoveTo(newPosition, speed));
        }
    }
}