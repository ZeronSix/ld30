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

        transform.position = _grid.GridToWorld(new Vector2(_grid.Width - 1, _grid.Height - 1)) +
                             new Vector3(_grid.GridUnitSize + 0.5f, _grid.GridUnitSize + 0.5f);

        Spawn();
    }

    public void Spawn()
    {
        var pos = _grid.GridToWorld(new Vector2(_grid.Width - 1, _grid.Height - 1));
        pos.z = 5;
        Instantiate(ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length - 1)], pos, Quaternion.identity);
    }

    public static AlienMothership Get()
    {
        return GameObject.FindGameObjectWithTag("AlienMothership").GetComponent<AlienMothership>();
    }
}
