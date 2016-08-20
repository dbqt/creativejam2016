using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Transform Propulsor1, Propulsor2;
    public float FirePartial;
    public float PropulsorRange;
    public float PropulsorForce;
    public float FireExtinguishingPower;
    public LayerMask GroundMask;

    private Vector3 projectedPropulsor1;
    private Vector3 projectedPropulsor2;

    // Use this for initialization
    void Start () {
        projectedPropulsor1 = Vector3.zero;
        projectedPropulsor2 = Vector3.zero;
    }
	
	void FixedUpdate () {
        float angle1 = Propulsor1.eulerAngles.y;
        float angle2 = Propulsor2.eulerAngles.y;

        projectedPropulsor1 = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle1), 0f, Mathf.Cos(Mathf.Deg2Rad * angle1));
        projectedPropulsor2 = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle2), 0f, Mathf.Cos(Mathf.Deg2Rad * angle2));

        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle1), 0f, Mathf.Cos(Mathf.Deg2Rad * angle1)).normalized * PropulsorForce);
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle2), 0f, Mathf.Cos(Mathf.Deg2Rad * angle2)).normalized * PropulsorForce);
    }

    void Update ()
    {
        Quaternion proAngle = Quaternion.identity;

        proAngle.eulerAngles = new Vector3(0f, -Propulsor1.eulerAngles.y, 0f);
        
        Collider[] grounds1 = Physics.OverlapBox(
            transform.position - projectedPropulsor1*PropulsorRange/2,
            new Vector3(0f, 3f, PropulsorRange/4),
            proAngle,
            GroundMask);

        proAngle.eulerAngles = new Vector3(0f, -Propulsor2.eulerAngles.y, 0f);

        Collider[] grounds2 = Physics.OverlapBox(
            transform.position - projectedPropulsor2* PropulsorRange/2 ,
            new Vector3(0f, 3f, PropulsorRange/4),
            proAngle,
            GroundMask);

        foreach (Collider o in grounds1)
        {
            o.gameObject.GetComponent<GroundController>().node.flameRate -= FireExtinguishingPower* PropulsorRange/Vector3.Distance(o.transform.position,transform.position)*Time.deltaTime;
        }
        foreach (Collider o in grounds2)
        {
            o.gameObject.GetComponent<GroundController>().node.flameRate -= FireExtinguishingPower * PropulsorRange / Vector3.Distance(o.transform.position, transform.position) * Time.deltaTime;
        }
    }
}
