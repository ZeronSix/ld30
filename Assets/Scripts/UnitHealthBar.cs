using UnityEngine;
using System.Collections;

public class UnitHealthBar : MonoBehaviour
{
    public Unit Unit;
    public float Scale;
    public float UnitSize = 0.25f;

    private Transform _background;
    private Transform _foreground;
    private Vector3 _deltaPos;

    void Awake()
    {
        _background = transform.Find("Background");
        _foreground = transform.Find("Foreground");
        _deltaPos = transform.position - Unit.transform.position;

        _background.localScale = new Vector3(Scale, 1, 1);
        _foreground.localPosition = new Vector3(_background.localPosition.x - Scale / 2 * UnitSize, _foreground.localPosition.y, _foreground.localPosition.z);
    }

    void Update()
    {
        transform.position = Unit.transform.position + _deltaPos;
        _foreground.localScale = new Vector3(Scale * Unit.Health/Unit.MaxHealth, 1, 1);
    }
}
