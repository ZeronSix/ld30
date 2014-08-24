using UnityEngine;
using System.Collections;

public class MouseOverHighlighter : MonoBehaviour 
{
    void OnMouseOver()
    {
        Color oldColor = renderer.materials[1].GetColor("_OutlineColor");
        renderer.materials[1].SetColor("_OutlineColor", new Color(oldColor.r,
                  oldColor.g,
                  oldColor.b, 1.0f));
    }

    void OnMouseExit()
    {
        Color oldColor = renderer.materials[1].GetColor("_OutlineColor");
        renderer.materials[1].SetColor("_OutlineColor", new Color(oldColor.r,
                                                                   oldColor.g,
                                                                   oldColor.b, 0.0f));
    }
}
