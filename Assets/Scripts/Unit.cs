using System;
using UnityEngine;
using System.Collections.Generic;

public enum UnitType
{
    Fighter,
    Assault,
    Cruiser
}

[System.Serializable]
public class DamageMultiplier
{
    public float Defense;
    public float DamageAgainstCruisers;
    public float DamageAgainstFighters;
    public float DamageAgainstAssaults;
}

public class Unit : MonoBehaviour
{
    public BattleSide BattleSide;
    public float      Health;
    public float      MaxHealth;
    public int        MaxShipCount;
    public float      MaxMoveDistance;
    public GameObject BattleObject;
    public UnitType   Type;
    public float      BaseDamage;
    public float      OrbitDistance;

    public DamageMultiplier DamageMultiplier;
    public Transform Target;
    public int CellMoveCount;

    private GameController _gc;
    private Grid _grid;
    private GameObject _bBox;
    private List<Vector2> pathNodes = null;

    void Awake()
    {
        _gc = GameController.Get();
        _grid = Grid.Get();
        _bBox = transform.FindChild("BoundingBox").gameObject;
    }

    void Update()
    {
        var gridPos = _grid.WorldToGrid(transform.position);

        _grid.Cells[(int) gridPos.x, (int) gridPos.y] = null;

        if (pathNodes != null)
        {
            if (pathNodes.Count == 0)
            {
                pathNodes = null;
                return;
            }

            transform.position = Vector3.Lerp(transform.position, pathNodes[0], _gc.UnitMoveSpeed*Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(pathNodes[0].y - transform.position.y, pathNodes[0].x - transform.position.x) * Mathf.Rad2Deg - 90));


            if ((transform.position - new Vector3(pathNodes[0].x, pathNodes[0].y)).magnitude < 0.01f)
            {
                //transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

                pathNodes.RemoveAt(0);
            }
        }

        gridPos = _grid.WorldToGrid(transform.position);
        _grid.Cells[(int)gridPos.x, (int)gridPos.y] = this;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
    }

    public void MoveTo(List<Vector2> positions)
    {
        pathNodes = positions;
    }

    public void EnableBoundingBox()
    {
        _bBox.SetActive(true);
    }
}