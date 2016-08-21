using UnityEngine;
using System.Collections;

public class MarshmallowRoasting : MonoBehaviour {

    public float RoastingRange;
    public float RoastingRateModifier;
    public LayerMask GroundMask;

    public int index;

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<ParticleSystem>().Pause();
    }
	
	// Update is called once per frame
	void Update () {
        // The grid surrounding the marshmallow.
        Collider[] grounds = Physics.OverlapBox(
            transform.position,
            Vector3.one * RoastingRange+Vector3.up*2,
            Quaternion.identity,
            GroundMask);
        bool isBurning = false;
        foreach(Collider col in grounds)
        {
            GameController.instance.marshIndex[index] += col.gameObject.GetComponent<GroundController>().node.flameLevel * RoastingRateModifier * Time.deltaTime;
            isBurning = col.gameObject.GetComponent<GroundController>().node.flameLevel >= 2f;
        }
        if(isBurning && gameObject.GetComponentInChildren<ParticleSystem>().isPaused)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        else if(!isBurning)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Pause();
        }
        Debug.Log(isBurning);

    }
}
