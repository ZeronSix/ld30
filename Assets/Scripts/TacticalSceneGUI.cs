using UnityEngine;
using System.Collections;

public class TacticalSceneGUI : MonoBehaviour
{
    public GUISkin Skin;
    public GUIText Planets;
    public GUIText Reinforcements;
    public GUIText WinText;
    public bool Enabled = false;
    public bool ReinforcementsAvailable = true;
    public GameObject[] ObjectPrefabs;

    private GameController _gc;
    private Grid _grid;

    void Awake()
    {
        _gc = GameController.Get();
        _grid = Grid.Get();
    }

    void OnGUI()
    {
        GUI.skin = Skin;

        Planets.text = " -PLANETS CONTESTED: " + GetPlanetsContested().ToString() + " of " + _gc.Planets.Length.ToString();
        Reinforcements.text = "[REINFORCEMENTS AVAILABLE: " + _gc.PlayerReinforcements.ToString() + "]";

        if (Enabled)
        {
            GUILayout.BeginArea(new Rect(Screen.width * 0.05f, Screen.height - 300, 200, 300));
            GUILayout.BeginVertical();
            if (GUILayout.Button("NEXT TURN", GUILayout.Height(50.0f)))
                {
                    if (!_gc.Busy)
                    {
                        _gc.NextTurn();
                    }
                }

                if (ReinforcementsAvailable && _grid.Cells[0, 0] == null && _gc.PlayerReinforcements > 0)
                {
                    foreach (var prefab in ObjectPrefabs)
                    {
                        if (GUILayout.Button("SPAWN " + prefab.name.ToUpper(), GUILayout.Height(50.0f)) )
                        {
                            _gc.PlayerReinforcements--;
                            _gc.PlayerUnitCount++;
                            ReinforcementsAvailable = false;
                            var pos = _grid.GridToWorld(new Vector2(0, 0));
                            pos.z = 5;
                            Instantiate(prefab, pos, Quaternion.identity);
                        }
                    }
                }
            GUILayout.EndVertical();;
            GUILayout.EndArea();
        }
    }

    private int GetPlanetsContested()
    {
        var planetsContested = 0;

        foreach (var planet in _gc.Planets)
        {
            if (planet.GetComponent<PlanetTrigger>().ContestedBy == BattleSide.Humans)
                planetsContested++;
        }

        return planetsContested;
    }

    public static TacticalSceneGUI Get()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<TacticalSceneGUI>();
    }
}
