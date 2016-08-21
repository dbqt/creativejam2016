using UnityEngine;
using System.Collections;

public class MarsmallowBehavior : MonoBehaviour {

	public float colorIndex;
	private Material material;
    public Color white;
    public Color orange;
    public Color black;

	// Use this for initialization
	void Awake () {
       
        material = gameObject.transform.FindChild("Capsule").GetComponent<MeshRenderer>().material;
        if (material == null)
        {
            Debug.Log("Marshmallow Material not found!");        
        }
        colorIndex = 0;
        material.SetColor("_Color", white);

    }

    // Update is called once per frame
    void Update() {

        applyColorIndex(GameController.instance.marshIndex[GetComponent<MarshmallowRoasting>().index]);

	}

    public void applyColorIndex(float newColorIndex)
    {
        //Debug.Log("Apply col ind");
        if (material != null)
        {
            if (newColorIndex > 0 && newColorIndex <= 75)
            {
                material.SetColor("_Color", Color.Lerp(white, orange, newColorIndex / 75));

            }
            else if (newColorIndex > 75 && newColorIndex < 100)
            {
                material.SetColor("_Color", Color.Lerp(orange, black, (newColorIndex - 75) / 25));
            }
        }
        else
        {
            Debug.Log("Error! Material does not exist!");
        }
    }

}

