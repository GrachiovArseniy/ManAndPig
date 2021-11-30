using UnityEngine;
using ManAndPig.Model;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerPresenter : TransformablePresenter<Player>
{
    [SerializeField] private UI _ui;

    internal override void Destroy(DieType dieType)
    {
        _ui.OpenLosePanel(dieType);
        Destroy(this.gameObject);
    }
}
