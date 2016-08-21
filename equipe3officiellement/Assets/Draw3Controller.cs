using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draw3Controller : MonoBehaviour {


    public Image img1;
    public Image img2;
    public Image img3;
    public Text txt1;
    // Use this for initialization
    void Start () {
        GetComponent<Animation>().Play("EndScreenDraw3");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
