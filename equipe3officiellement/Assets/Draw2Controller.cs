using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draw2Controller : MonoBehaviour {
   public Image img1;
    public Image img2;
    public Text txt1;


	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play("EndScreenDraw2");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
