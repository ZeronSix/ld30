using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour
{
    public Sprite BasicState;
    public Sprite PathState;

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void DrawAsTile()
    {
        _renderer.sprite = BasicState;
    }

    public void DrawAsPath()
    {
        _renderer.sprite = PathState;
    }
}