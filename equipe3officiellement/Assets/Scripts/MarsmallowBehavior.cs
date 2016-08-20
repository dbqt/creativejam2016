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
        Debug.Log("Start");
        material = gameObject.transform.FindChild("Capsule").GetComponent<MeshRenderer>().material;
        if (material == null)
        {
            Debug.Log("Heeelp!");
        }
        colorIndex = 0;
        material.SetColor("_Color", white);

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.X))
        {
            colorIndex = Mathf.Clamp(colorIndex += 2.0f, 0.0f, 100.0f);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            colorIndex = Mathf.Clamp(colorIndex -= 2.0f, 0.0f, 100.0f);
        }

        //applyColorIndex(colorIndex);
        //Debug.Log(colorIndex);
	}

    public void applyColorIndex(float newColorIndex)
    {
        Debug.Log("Apply col ind");
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

