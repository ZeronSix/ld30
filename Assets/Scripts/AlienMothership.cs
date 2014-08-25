using UnityEngine;
using System.Collections;

public class AlienMothership : MonoBehaviour
{
    public int SpawnTurnCount = 5;
    public GameObject[] ObjectPrefabs;

    private GameController _gc;
    private Grid _grid;

    void Awake()
    {
        _gc = GameController.Get();
        _grid = Grid.Get();
    }

    void Update()
    {
        if (_gc.TurnCount%SpawnTurnCount == 0 && _grid.Cells[_grid.Width - 1, _grid.Height - 1] == null)
        {
            var pos = _grid.GridToWorld(new Vector2(_grid.Width - 1, _grid.Height - 1));
            pos.z = 5;
            Instantiate(ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length - 1)], pos, Quaternion.identity);
        }
    }
}
