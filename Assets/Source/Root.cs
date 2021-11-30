using System;
using System.Collections.Generic;
using System.Linq;
using ManAndPig.Extentions;
using ManAndPig.Model;
using UnityEngine;

public class Root : MonoBehaviour
{
    [Tooltip("First: X\nSecond: Y")]
    [SerializeField] private CellArray[] _cellArray;
    [Header("Player")]
    [SerializeField] private PlayerPresenter _playerPresenter;
    [SerializeField] private Vector2 _playerStartPosition;
    [Header("Enemy")]
    [SerializeField] private EnemyPresenter _enemyPresenter;
    [SerializeField] private Vector2 _enemyStartPosition;
    private GameFieldPresenter _gameFieldPresenter = new GameFieldPresenter();
    private GameField _gameField;
    private Player _player;
    private Enemy _enemy;

    private void OnValidate()
    {
        _playerStartPosition.x = Mathf.Round(_playerStartPosition.x);
        _playerStartPosition.y = Mathf.Round(_playerStartPosition.y);
        _enemyStartPosition.x = Mathf.Round(_enemyStartPosition.x);
        _enemyStartPosition.y = Mathf.Round(_enemyStartPosition.y);
    }

    private void Awake()
    {
        List<List<Cell>> cells = new List<List<Cell>>(_cellArray.Length);
        List<List<CellPresenter>> cellPresenters = new List<List<CellPresenter>>(_cellArray.Length);

        for (int i = 0; i < _cellArray.Length; i++)
        {
            cellPresenters.Add(_cellArray[i].Cells.ToList());

            for (int j = 0; j < _cellArray[i].Cells.Length; j++)
            {
                if (cells.Count < i + 1)
                {
                    cells.Add(new List<Cell>());
                }

                cells[i].Add(_cellArray[i].Cells[j].Model);
            }
        }

        _gameField = new GameField(cells);
        _player = new Player(_gameField, _playerStartPosition.GetPosition());
        _enemy = new Enemy(_gameField, _enemyStartPosition.GetPosition());

        _gameFieldPresenter.Init(cellPresenters);
        _playerPresenter.Init(_player, _gameFieldPresenter);
        _enemyPresenter.Init(_enemy, _gameFieldPresenter);
    }

    private void OnEnable()
    {
        _player.Diying += _playerPresenter.Destroy;
        _enemy.Diying += _enemyPresenter.Destroy;
    }

    private void OnDisable()
    {
        _player.Diying -= _playerPresenter.Destroy;
        _enemy.Diying -= _enemyPresenter.Destroy;
    }
}

[Serializable]
public class CellArray
{
    public CellPresenter[] Cells;
}

namespace ManAndPig.Extentions
{
    public static class Extentions
    {
        public static Vector2 GetVector(this Position position)
        {
            return new Vector2(position.X, position.Y);
        }

        public static Position GetPosition(this Vector2 vector2)
        {
            return new Position(Convert.ToInt32(vector2.x), Convert.ToInt32(vector2.y));
        }
    }
}
