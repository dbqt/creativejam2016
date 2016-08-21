using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Draw2Controller : MonoBehaviour {
   public Image img1;
    public Image img2;
    public Text txt1;
    public Text txt2;

	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play("FinishScreenDraw2");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
