using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Unit SelectedUnit;
    public float UnitMoveSpeed;
    public float AttackDuration;
    public bool Busy = false;

    private Grid _grid;
	
	void Start ()
	{
	    _grid = Grid.Get();
	}

    public static GameController Get()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
}
