using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float[] marshIndex = new float[4] { 0,0,0,0};
    public float marshGoal=0;
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
