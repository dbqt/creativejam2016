using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {
    public Node node;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        node.CalculateFlameRate(Time.deltaTime);
        GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white,Color.red,node.flameLevel/10f);
    }
}
