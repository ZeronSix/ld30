using UnityEngine;
using System.Collections;

public class HintGUI : MonoBehaviour {

	public GUIStyle guiStyle;
	
	void OnGUI() {
		GUI.Button (new Rect (12,10,180,100), "CLICK ON STAR TO ZOOM IN", guiStyle);
		GUI.Button (new Rect (12,30,180,100), "ESCAPE TO ZOOM OUT", guiStyle);
		GUI.Button (new Rect (12,50,180,100), "HOLD MIDDLE MOUSE BUTTON TO SCROLL", guiStyle);
	}
}
