using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class Planet : MonoBehaviour {

	public GameObject captionPrefab;

	public GameObject orbit;
	public Vector3 orbitScale;
    
	public float realScale = 1f;

	private TextMesh caption;

	void Start () {
		GenerateName ();

		caption = ((GameObject)Instantiate (captionPrefab)).GetComponent<TextMesh>();
		caption.transform.parent = transform;
		caption.transform.localPosition = new Vector3 (0f, 0.6549172f, -0.5f);
		caption.transform.localScale = Vector3.one * 0.009f;
		caption.text = name;
		caption.name = "Caption";
	}
	
	void Update () {
		transform.RotateAround (transform.position, Vector3.forward, 14 * Time.deltaTime);
		transform.RotateAround (transform.parent.position, Vector3.forward, 0.3f * 25f * Time.deltaTime / (Vector3.Distance(transform.position, transform.parent.position) * 0.05f));

		caption.transform.rotation = Quaternion.identity;

		orbit.transform.RotateAround (orbit.transform.position, Vector3.forward, orbitScale.x * 3 * Time.deltaTime);
	}

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Action"))
        {
//            if (gc.SelectedUnit)
//                gc.SelectedUnit.Target = transform;
        }
    }
	
	public void GenerateName() {
		int num = Random.Range(0, 25);
		int num2 = Random.Range(0, 25);
		char let = (char)('a' + num);

		name = ((char)('a' + num)).ToString() + ((char)('a' + num2)).ToString() + "-" + Random.Range (0, 9).ToString() + Random.Range (0, 9).ToString();
		name = name.ToUpper ();
	}
}
