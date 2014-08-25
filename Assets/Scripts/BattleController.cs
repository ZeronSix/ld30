using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : MonoBehaviour
{
    public float CenterSpawnOffset;
    public float VerticalSpawnDistance;
    public float TimeBeforeExit = 2.0f;

    public GameObject Unit1Prefab;
    public GameObject Unit2Prefab;
    public BattleSide AttackingSide;

    public static BattleController Global { get; private set; }

    private Unit Unit1;
    private Unit Unit2;
    private readonly List<GameObject> ships = new List<GameObject>(); 

    void Awake()
    {
        Global = this;

        Unit1 = Unit1Prefab.GetComponent<Unit>();
        Unit2 = Unit2Prefab.GetComponent<Unit>();

        StartCoroutine(ExitCoroutine());
    }


    private void Exit()
    {
        foreach (var ship in ships)
        {
            Destroy(ship);
        }
        Destroy(gameObject);
    }

    private IEnumerator ExitCoroutine()
    {
        yield return new WaitForSeconds(TimeBeforeExit);
        Exit();
    }
}
