using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public Unit SelectedUnit;
    public float UnitMoveSpeed;
    public float AttackDuration;
    public bool Busy = false;

    public GameObject[] Planets;
    public ScreenFader Fader;
    public int PlayerReinforcements = 5;
    public float PlayerUnitCount = 0;
    public int TurnCount = 0;
    public int ReinforceIncrementTurns = 5;

    private Grid _grid;
    private AlienMothership _aiSpawner;

    private void Awake()
    {
        _grid = Grid.Get();
        _aiSpawner = AlienMothership.Get();
        StartMission();

        var units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var unit in units)
        {
            if (unit.GetComponent<Unit>().BattleSide == BattleSide.Humans)
            {
                PlayerUnitCount++;
            }
        }

        foreach (var planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            planet.GetComponent<PlanetTrigger>().enabled = true;
        }
    }

    void Update () 
    {

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

			Planets[0].transform.parent.parent.GetComponent<Sun>().enabled = true;
			Planets[0].transform.parent.parent.GetComponent<Sun>().connected = true;
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

        if (TurnCount != 0 && TurnCount % ReinforceIncrementTurns == 0)
        {
            int planetsContested = 0;

            foreach (var planet in Planets)
            {
                if (planet.GetComponent<PlanetTrigger>().ContestedBy == BattleSide.Humans)
                    planetsContested++;
            }

            PlayerReinforcements += planetsContested;
        }

        if (TurnCount != 0 && TurnCount % _aiSpawner.SpawnTurnCount == 0 && _grid.Cells[_grid.Width - 1, _grid.Height - 1] == null) {
            _aiSpawner.Spawn();
        }
    }

    public void StartMission()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
    }
}
