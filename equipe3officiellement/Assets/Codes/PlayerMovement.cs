using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Transform Propulsor1, Propulsor2;
    public float PropulsorForce;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
        float angle1 = Propulsor1.eulerAngles.y;
        float angle2 = Propulsor2.eulerAngles.y;

        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle1), 0f, Mathf.Cos(Mathf.Deg2Rad * angle1)).normalized * PropulsorForce);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle2), 0f, Mathf.Cos(Mathf.Deg2Rad * angle2)).normalized * PropulsorForce);
    }
}
