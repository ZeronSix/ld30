using System;
using UnityEngine;
using System.Collections;

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
    public float Speed;

    private GameController _gc;
    private Quaternion _startRotation;

    void Awake()
    {
        _gc = GameController.Get();
        _startRotation = transform.rotation;
    }

    void Update()
    {
        transform.parent = null;

        if (Target)
        {
            if ((Target.position - transform.position).magnitude > OrbitDistance && transform.parent == null)
            {
                transform.position = Vector3.Lerp(transform.position, Target.position, Speed*Time.deltaTime);


                var angle = Mathf.Atan2(-transform.position.y + Target.position.y, -transform.position.x + Target.position.x) *
                            Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(_startRotation.eulerAngles.x, _startRotation.eulerAngles.y, 360 - angle - 90);
            }
            else
            {
                transform.parent = Target;
            }  
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
    }

    void OnMouseDown()
    {
        _gc.SelectedUnit = this;
    }
}