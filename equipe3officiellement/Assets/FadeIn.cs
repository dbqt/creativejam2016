using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Fade());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator Fade()
    {
        for(int i=0;i<100;i++)
        {

            Color c = GetComponent<Image>().color;
            c.a += 0.01f;
            GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
