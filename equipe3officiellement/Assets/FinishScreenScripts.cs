﻿using UnityEngine;
using System.Collections;

public class FinishScreenScripts : MonoBehaviour {
    public GameObject image;
	// Use this for initialization
	void Start () {
        GetComponent<Animation>().Play("FinishScreen");
	}
	void PlayImageAnim()
    {
        image.GetComponent<Animation>().Play("FinishScreenImage");
    }
	// Update is called once per frame
	void Update () {
	
	}
}