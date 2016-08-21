using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraMenuMovement : MonoBehaviour {

    public Image fadePanel;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void startFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    public IEnumerator FadeOut()
    {
        for (int i = 0; i < 100; i++)
        {

            Color col = fadePanel.color;
            col.a += 0.01f;
            fadePanel.color = col;
            yield return new WaitForSeconds(0.01f);
        }
        GameController.instance.StartGame();
        
    }
    public IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; i++)
        {

            Color col = fadePanel.color;
            col.a -= 0.10f;
            fadePanel.color = col;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
