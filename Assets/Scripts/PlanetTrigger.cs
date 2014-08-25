using UnityEngine;
using System.Collections;

public class PlanetTrigger : MonoBehaviour
{
    public BattleSide ContestedBy = BattleSide.None;

    private GameController _gc;
    private GameObject _alienIcon;
    private GameObject _humanIcon;

    private Vector3 _iconDeltaPos;

    void Start()
    {
        if (!enabled)
            return;

        _gc = GameController.Get();
        _alienIcon = transform.Find("aliens").gameObject;
        _iconDeltaPos = _alienIcon.transform.localPosition;
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

        _alienIcon.transform.position = transform.position + _iconDeltaPos;
        _humanIcon.transform.position = transform.position + _iconDeltaPos;

        _alienIcon.transform.rotation = _humanIcon.transform.rotation = Quaternion.identity;
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