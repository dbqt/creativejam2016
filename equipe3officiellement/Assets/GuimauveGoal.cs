using UnityEngine;
using System.Collections;

public class GuimauveGoal : MonoBehaviour {
    public Color white;
    public Color orange;
    public Color black;
    public Color currentColor;
    Material material;
    void Start()
    {
        material = gameObject.transform.FindChild("Capsule").GetComponent<MeshRenderer>().material;
        applyColorIndex(GameController.instance.marshGoal);

    }
    public void applyColorIndex(float newColorIndex)
    {
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
        currentColor = material.GetColor("_Color");
    }
}
