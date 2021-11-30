using ManAndPig.Model;
using UnityEngine;

public class EnemyPresenter : TransformablePresenter<Enemy>
{
    [SerializeField] private Sprite _angryForwardSprite;
    [SerializeField] private Sprite _angryBackSprite;
    [SerializeField] private Sprite _angryLeftSprite;
    [SerializeField] private Sprite _angryRightSprite;
    [SerializeField] private UI _ui;

    internal bool Angry => model.Angry;

    internal override void Destroy(DieType dieType)
    {
        _ui.OpenWinPanel();
        Destroy(this.gameObject);
    }

    internal void StopAbility()
    {
        ((dynamic)model).StopAbility();
    }

    protected override void RotateTo(Position position)
    {
        Vector3 difference = gameFieldPresenter.GetCellPosition(position) - transform.position;

        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            GetComponent<SpriteRenderer>().sprite = difference.x > 0 ? (model.Angry ? _angryRightSprite : rightSprite)
                                                                        : (model.Angry ? _angryLeftSprite : leftSprite);
            return;
        }

        GetComponent<SpriteRenderer>().sprite = difference.y > 0 ? (model.Angry ? _angryForwardSprite : forwardSprite)
                                                                        : (model.Angry ? _angryBackSprite : backSprite);
    }
}
