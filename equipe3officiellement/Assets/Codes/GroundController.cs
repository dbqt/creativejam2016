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
        GetComponentInChildren<ParticleSystem>().startSize = node.flameLevel / 10.0f;
        transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().startSize = node.flameLevel / 10f /1.5f;
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white,Color.red,node.flameLevel/10f);
    }
}
