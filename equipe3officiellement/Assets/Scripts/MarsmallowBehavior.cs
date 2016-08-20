using UnityEngine;
using System.Collections;

public class MarsmallowBehavior : MonoBehaviour {

	public float colorIndex;
	private Material material;
    public Color white;
    public Color orange;
    public Color black;

	// Use this for initialization
	void Start () {
        material = gameObject.transform.FindChild("Capsule").GetComponent<MeshRenderer>().material;
        colorIndex = 0;
        material.SetColor("_Color", white);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            colorIndex = Mathf.Clamp(colorIndex += 2.0f, 0.0f, 100.0f);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            colorIndex = Mathf.Clamp(colorIndex -= 2.0f, 0.0f, 100.0f);
        }

        if (colorIndex >0  && colorIndex <=75)
        {
            material.SetColor("_Color",Color.Lerp(white, orange, colorIndex / 75));
            
        }
        else if (colorIndex > 75 && colorIndex < 100)
        {
            material.SetColor("_Color", Color.Lerp(orange, black, (colorIndex-75) / 25));
        }


        Debug.Log(colorIndex);
	}
}
