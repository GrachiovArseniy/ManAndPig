using UnityEngine;
using UnityEngine.EventSystems;
using ManAndPig.Model;

namespace ManAndPig.Input
{
    public class PlayerPresenterInput : InputPresenter<Player>, IBeginDragHandler, IDragHandler
    {
        private float _clickTime;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (presenter.Moved)
            {
                return;
            }

            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {
                Position newPosition = presenter.Position + (eventData.delta.x > 0 ? new Position(1, 0) : new Position(-1, 0));

                if (presenter.CanMoveTo(newPosition) == false)
                {
                    return;
                }

                StartCoroutine(presenter.MoveTo(newPosition, Config.PlayerStepSpeed));

                return;
            }

            Position newPosition1 = presenter.Position + (eventData.delta.y > 0 ? new Position(0, 1) : new Position(0, -1));

            if (presenter.CanMoveTo(newPosition1) == false)
            {
                return;
            }

            StartCoroutine(presenter.MoveTo(newPosition1, Config.PlayerStepSpeed));
        }

        public void OnDrag(PointerEventData eventData) { }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (Time.realtimeSinceStartup - _clickTime < 0.3f)
                {
                    presenter.MakeAbility();
                }

                _clickTime = Time.realtimeSinceStartup;
            }
        }
    }
}