using UnityEditor;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum GameState {
		GALACTIC_VIEW, SYSTEM_VIEW
	}

	public GameState gameState = GameState.GALACTIC_VIEW;

	public Sun selectedSystem;

    public Unit SelectedUnit;
    public float UnitMoveSpeed;
    public float AttackDuration;
    public bool Busy = false;

    public GameObject[] Planets;
    public ScreenFader Fader;
    public int PlayerReinforcements = 5;
    public float PlayerUnitCount = 0;
    public int TurnCount = 0;

    private Grid _grid;
	
	void Awake()
	{
	    _grid = Grid.Get();
        StartMission();

	    var units = GameObject.FindGameObjectsWithTag("Unit");
	    foreach (var unit in units)
	    {
	        if (unit.GetComponent<Unit>().BattleSide == BattleSide.Humans)
	        {
	            PlayerUnitCount++;
	        }
	    }
	}

	public void SelectSystem(Sun sun) {
		gameState = GameState.SYSTEM_VIEW;
		selectedSystem = sun;
	}

	public void UnselectSystem() {
		gameState = GameState.GALACTIC_VIEW;
		selectedSystem = null;
	}

	void Update () 
    {
		if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameController.GameState.SYSTEM_VIEW) UnselectSystem();

	    var win = true;
	    foreach (var planet in Planets)
	    {
	        if (planet.GetComponent<PlanetTrigger>().ContestedBy != BattleSide.Humans)
	            win = false;
	    }

	    if (win)
	    {
	        // TODO: win
            Fader.EndScene("HUMAN VICTORY!");
	    }

	    if (PlayerReinforcements == 0 && PlayerUnitCount == 0)
	    {
	        // TODO: fail
            Fader.EndScene("ALIENS VICTORY!");
	    }
	}

    public static GameController Get()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void NextTurn()
    {
        TurnCount++;

        // TODO: AI Turn
        Busy = false;

        TacticalSceneGUI.Get().ReinforcementsAvailable = true;

        foreach (var unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            unit.GetComponent<Unit>().PrepareForNextTurn();

            var ai = unit.GetComponent<AIUnitController>();
            if (ai != null)
            {
                ai.DoTurn();
            }
        }
    }

    public void StartMission()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }
}
