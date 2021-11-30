using System.Collections.Generic;
using ManAndPig.Model;
using UnityEngine;

public class GameFieldPresenter
{
    private List<List<CellPresenter>> _cells;

    internal void Init(List<List<CellPresenter>> cells)
    {
        _cells = cells;
    }

    internal Vector3 GetCellPosition(Position position)
    {
        return _cells[position.X][position.Y].transform.position;
    }
}