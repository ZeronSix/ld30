using UnityEngine;
using System.Collections;

public class UnitHealthBar : MonoBehaviour
{
    public Unit Unit;
    public float Scale;
    public float UnitSize = 0.25f;

    private Transform _background;
    private Transform _foreground;

    void Awake()
    {
        _background = transform.Find("Background");
        _foreground = transform.Find("Foreground");

        _background.localScale = new Vector3(Scale, 1, 1);
        _foreground.localPosition = new Vector3(_background.localPosition.x - Scale / 2 * UnitSize, _foreground.localPosition.y, _foreground.localPosition.z);
    }

    void Update()
    {
        _foreground.localScale = new Vector3(Scale * Unit.Health/Unit.MaxHealth, 1, 1);
    }
}
