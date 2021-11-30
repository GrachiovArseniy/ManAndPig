using UnityEngine;
using ManAndPig.Model;

[RequireComponent(typeof(SpriteRenderer))]
public class CellPresenter : MonoBehaviour
{
    private readonly Cell _model = new Cell();

    internal Cell Model => _model;

    [SerializeField] private Sprite _bomb;
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _model.ViewUpdated += OnUpdated;
    }

    private void OnDisable()
    {
        _model.ViewUpdated -= OnUpdated;
    }

    private void OnUpdated()
    {
        _spriteRenderer.sprite = _model.HasBomb ? _bomb : null;
    }
}