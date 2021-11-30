using System.Collections;
using ManAndPig.Model;
using UnityEngine;

public abstract class TransformablePresenter<T> : MonoBehaviour
{
    internal Position Position => ((dynamic)model).Position;

    internal bool Moved { get; private set; } = false;

    [SerializeField] protected Sprite forwardSprite;
    [SerializeField] protected Sprite backSprite;
    [SerializeField] protected Sprite leftSprite;
    [SerializeField] protected Sprite rightSprite;

    protected GameFieldPresenter gameFieldPresenter { get; private set; }

    protected T model { get; private set; }

    protected float speed { get; private set; }

    internal void Init(T model, GameFieldPresenter gameFieldPresenter)
    {
        this.model = model;
        this.gameFieldPresenter = gameFieldPresenter;
    }

    internal virtual IEnumerator MoveTo(Position position, float speed)
    {
        Moved = true;

        RotateTo(position);

        while ((gameFieldPresenter.GetCellPosition(position) - transform.position).magnitude >= 0.1f)
        {
            transform.position += (gameFieldPresenter.GetCellPosition(position) - transform.position).normalized * speed / 60;
            yield return new WaitForSeconds(1 / 60);
        }

        ((dynamic)model).MoveTo(position);

        Moved = false;
    }

    internal void MakeAbility()
    {
        ((dynamic)model).MakeAbility();
    }

    internal bool CanMoveTo(Position position)
    {
        return ((dynamic)model).CanMoveTo(position);
    }

    internal virtual void Destroy(DieType dieType)
    {
        Destroy(this.gameObject);
    }

    protected virtual void RotateTo(Position position)
    {
        Vector3 difference = gameFieldPresenter.GetCellPosition(position) - transform.position;

        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            GetComponent<SpriteRenderer>().sprite = difference.x > 0 ? rightSprite : leftSprite;
            return;
        }

        GetComponent<SpriteRenderer>().sprite = difference.y > 0 ? forwardSprite : backSprite;
    }
}
