using System;
using UnityEngine;
using System.Collections;

public class AIUnitController : MonoBehaviour
{
    private Grid _grid;
    private Unit _unit;
    private GameController _gc;
    private bool _contestingPlanet;

    void Awake()
    {
        _grid = Grid.Get();
        _gc = GameController.Get();
        _unit = GetComponent<Unit>();
    }

    public void DoTurn()
    {
        Unit foundUnit = null;
        Vector3 foundPlanet = -Vector3.one;

        switch (_unit.Type)
        {
            case UnitType.Assault:
                foundUnit = _grid.FindClosestUnitOfType(_grid.WorldToGrid(transform.position), UnitType.Cruiser, BattleSide.Humans);
                break;
            case UnitType.Cruiser:
                foundPlanet = GetClosestEmptyPlanetPos();
                break;
            case UnitType.Fighter:
                foundUnit = _grid.FindClosestUnitOfType(_grid.WorldToGrid(transform.position), UnitType.Assault, BattleSide.Humans);
                break;
        }

        if (foundUnit != null)
        {
            var path = AStar.FindPath(_grid.Cells, _grid.WorldToGrid(transform.position), _grid.WorldToGrid(foundUnit.transform.position));

            if (path == null)
                return;

            while (path.Count > _unit.CellMoveCount)
                path.RemoveAt(path.Count - 1);

            _unit.MoveTo(path);
        }

        if (foundPlanet != -Vector3.one)
        {
            var path = AStar.FindPath(_grid.Cells, _grid.WorldToGrid(transform.position), _grid.WorldToGrid(foundPlanet));
            while (path.Count > _gc.SelectedUnit.CellMoveCount)
                path.RemoveAt(path.Count - 1);

            _unit.MoveTo(path);
        }
    }

    private Vector3 GetClosestEmptyPlanetPos()
    {
        var closestPos = -Vector3.one;
        var unitGridPos = _grid.WorldToGrid(transform.position);

        foreach (var planet in GameObject.FindGameObjectsWithTag("Planet")) {
            var planetComp = planet.GetComponent<PlanetTrigger>();
            var gridPos = _grid.WorldToGrid(planetComp.transform.position);

            if (planetComp.ContestedBy == BattleSide.None)
            {
                if (closestPos == -Vector3.one ||
                    (unitGridPos - gridPos).sqrMagnitude < (closestPos - gridPos).sqrMagnitude)
                {
                    closestPos = gridPos;
                }
            }
        }

        return closestPos;
    }
}
