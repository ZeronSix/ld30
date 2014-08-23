using System;
using UnityEngine;
using System.Collections;

public class BattleShip : MonoBehaviour
{
    public float DefenceTimer = 1.0f;
    public Unit  Unit;

    private Vector3 _startPosition;

    void Awake()
    {
        _startPosition = transform.position;
    }

    void LateUpdate()
    {
        _startPosition.x = transform.position.x;
        transform.position = _startPosition;
    }

    public void PlayAttackAnimation()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }

    public void TriggerAttack()
    {
        BattleController.Global.StartAttack(Unit.BattleSide);
    }
}