using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void PopScreenUI()
    {
        GameController.instance.ShowScoreScreen();
    }
    void NextAnim()
    {
        GetComponent<Animation>().Play("LevelIntro2");
    }
    void StartGame()
    {
        GameController.instance.UnlockGame();
    }
}
