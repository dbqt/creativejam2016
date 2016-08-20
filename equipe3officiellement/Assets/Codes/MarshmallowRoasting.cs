using UnityEngine;
using System.Collections;

public class MarshmallowRoasting : MonoBehaviour {

    public float RoastingRange;
    public float RoastingRateModifier;
    public LayerMask GroundMask;    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // The grid surrounding the marshmallow.
        Collider[] grounds = Physics.OverlapBox(
            transform.position,
            Vector3.one * RoastingRange,
            Quaternion.identity,
            GroundMask);

        foreach(Collider col in grounds)
        {
            //update color of marshmallow
            //RoastingLevel += col.gameObject.GetComponent<GroundController>().node.flameLevel * RoastingRateModifier * Time.deltaTime;
        }

    }
}
