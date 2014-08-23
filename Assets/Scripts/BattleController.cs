using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour
{
    public float CenterSpawnOffset;
    public float VerticalSpawnDistance;

    public GameObject Unit1Prefab;
    public GameObject Unit2Prefab;
    public BattleSide AttackingSide;

    public static BattleController Global { get; private set; }

    private Unit Unit1;
    private Unit Unit2;

    void Awake()
    {
        Global = this;

        Unit1 = Unit1Prefab.GetComponent<Unit>();
        Unit2 = Unit2Prefab.GetComponent<Unit>();

        SpawnUnit(Unit1);
        SpawnUnit(Unit2);
    }

    private void SpawnUnit(Unit unit)
    {
        float spawnX = unit.BattleSide == BattleSide.Humans ? -CenterSpawnOffset : CenterSpawnOffset;
        float spawnY = -VerticalSpawnDistance*(unit.MaxShipCount - 1)/2.0f;

        for (int i = 0; i < unit.MaxShipCount; i++)
        {
            var ship = Instantiate(unit.BattleObject, new Vector3(spawnX, spawnY, 0), unit.BattleObject.transform.rotation) as GameObject;
            var battleShipComp = ship.GetComponent<BattleShip>();
            battleShipComp.Unit = unit;
            if (AttackingSide == unit.BattleSide)
            {
                battleShipComp.PlayAttackAnimation();
                battleShipComp.TriggerAttack(); // TODO: fix
            }
               
            spawnY += VerticalSpawnDistance;
        }
    }

    private static float CalculateSpecialDamageMults(Unit unit, Unit enemy)
    {
        var specialDamageMult = 1.0f;
        switch (enemy.Type) {
            case UnitType.Assault:
                specialDamageMult = unit.DamageMultiplier.DamageAgainstAssaults;
                break;
            case UnitType.Cruiser:
                specialDamageMult = unit.DamageMultiplier.DamageAgainstCruisers;
                break;
            case UnitType.Fighter:
                specialDamageMult = unit.DamageMultiplier.DamageAgainstFighters;
                break;
        }
        return specialDamageMult;
    }

    public void StartAttack(BattleSide side)
    {
        var unit = side == BattleSide.Humans ? Unit1 : Unit2;
        var enemyUnit = side == BattleSide.Humans ? Unit2 : Unit1;

        
        enemyUnit.TakeDamage(unit.BaseDamage*CalculateSpecialDamageMults(unit, enemyUnit));
        unit.TakeDamage(enemyUnit.BaseDamage * CalculateSpecialDamageMults(enemyUnit, unit) * enemyUnit.DamageMultiplier.Defense);
    }
}
