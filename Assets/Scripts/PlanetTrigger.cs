using UnityEngine;
using System.Collections;

public class PlanetTrigger : MonoBehaviour
{
    public BattleSide ContestedBy = BattleSide.None;

    private GameController _gc;
    private GameObject _alienIcon;
    private GameObject _humanIcon;

    void Awake()
    {
        if (!enabled)
            return;

        _gc = GameController.Get();
        _alienIcon = transform.Find("aliens").gameObject;
        _humanIcon = transform.Find("humans").gameObject;
    }

    void Update()
    {
        switch (ContestedBy)
        {
            case BattleSide.Aliens:
                _alienIcon.renderer.enabled = true;
                _humanIcon.renderer.enabled = false;
                break;
            case BattleSide.Humans:
                _alienIcon.renderer.enabled = false;
                _humanIcon.renderer.enabled = true;
                break;
            case BattleSide.None:
                _alienIcon.renderer.enabled = false;
                _humanIcon.renderer.enabled = false;
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Unit")
        {
            if (other.GetComponent<Unit>().Type == UnitType.Cruiser)
            {
                ContestedBy = other.GetComponent<Unit>().BattleSide;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        ContestedBy = BattleSide.None;
    }
}