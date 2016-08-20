using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    public GameObject Player1, Player2, Player3, Player4;

	// Use this for initialization
	void Start () {
        string[] o = Input.GetJoystickNames();
        foreach (string name in o)
        {

            Debug.Log(name);
        }
    }
	
	// Update is called once per frame
	void Update () {
        ProcessPlayer1();
        ProcessPlayer2();
    }

    private void ProcessPlayer1()
    {
        if (Player1 == null) return;

        // Get input values.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float horizontalb = Input.GetAxis("Horizontalb");
        float verticalb = Input.GetAxis("Verticalb");

        // Calculate directions & angles.
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        //Debug.Log("left: " + direction);
        Vector2 directionb = new Vector2(horizontalb, verticalb).normalized;
        //Debug.Log("right: " + directionb);
        float angle = Mathf.Atan2(-direction.x, -direction.y);
        float angleb = Mathf.Atan2(-directionb.x, -directionb.y);

        // Apply rotation to propulsors.
        if (direction.magnitude != 0f)
        {
            Transform prop1 = Player1.transform.FindChild("Propulsor1");
            float oldAngle = prop1.eulerAngles.y;
            prop1.eulerAngles = new Vector3(0f, Mathf.LerpAngle(oldAngle, Mathf.Rad2Deg * angle, 0.5f), 0f);
        }

        if (directionb.magnitude != 0f)
        {
            Transform prop2 = Player1.transform.FindChild("Propulsor2");
            float oldAngle = prop2.eulerAngles.y;
            prop2.eulerAngles = new Vector3(0f, Mathf.LerpAngle(oldAngle, Mathf.Rad2Deg * angleb, 0.5f), 0f);
        }
    }

    private void ProcessPlayer2()
    {
        if (Player2 == null) return;
        
        // Get input values.
        float horizontal = Input.GetAxis("Horizontal2");
        float vertical = Input.GetAxis("Vertical2");
        float horizontalb = Input.GetAxis("Horizontalb2");
        float verticalb = Input.GetAxis("Verticalb2");
        
        // Calculate directions & angles.
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        //Debug.Log("left: " + direction);
        Vector2 directionb = new Vector2(horizontalb, verticalb).normalized;
        //Debug.Log("right: " + directionb);
        float angle = Mathf.Atan2(-direction.x, -direction.y);
        float angleb = Mathf.Atan2(-directionb.x, -directionb.y);

        // Apply rotation to propulsors.  
        if (direction.magnitude != 0f)
        {
            Transform prop1 = Player2.transform.FindChild("Propulsor1");
            float oldAngle = prop1.eulerAngles.y;
            prop1.eulerAngles = new Vector3(0f, Mathf.LerpAngle(oldAngle, Mathf.Rad2Deg * angle, 0.5f), 0f);
        }

        if (directionb.magnitude != 0f)
        {
            Transform prop2 = Player2.transform.FindChild("Propulsor2");
            float oldAngle = prop2.eulerAngles.y;
            prop2.eulerAngles = new Vector3(0f, Mathf.LerpAngle(oldAngle, Mathf.Rad2Deg * angleb, 0.5f), 0f);
        }   
    }
}
