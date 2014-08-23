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

    public DamageMultiplier DamageMultiplier;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
    }
}