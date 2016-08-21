
using UnityEngine;
using System.Collections;

public class BillBoardMenu : MonoBehaviour
{
    public TextMesh start;
    public TextMesh quit;
    public Animation buttonStart;
    public Animation buttonQuit;
    private int selected = 0;
    private float lockTime = 0.01f;
    // Use this for initialization
    void Start()
    {
        selectItem();
    }

    // Update is called once per frame
    void Update()
    {
        float axs = Input.GetAxis("Vertical");
        if (axs != 0 && Time.time >= lockTime)
        {
            lockTime = Time.time + 0.5f;
            if (axs > 0)
            {
                selected += 1;
                selected %= 2;
            }
            else if (axs < 0)
            {
                selected -= 1;
                selected = Mathf.Abs(selected);
            }
            selectItem();
        }
        if (Input.GetButtonDown("Submit"))
        {
            if (selected == 0)
            {
                GetComponent<Animation>().Play("ButtonClickStart");
                Camera.main.GetComponent<Animation>().Play("MainCameraMenuGame");
            }
            else
            {
                GetComponent<Animation>().Play("ButtonClickQuit");
                StartCoroutine(QuitEnu());
            }

        }
    }
    IEnumerator QuitEnu()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
    void selectItem()
    {
        if (selected == 0)
        {
            start.color = Color.white;
            quit.color = new Color(140 / 255f, 140 / 255f, 140 / 255f);
        }
        else
        {
            quit.color = Color.white;
            start.color = new Color(140 / 255f, 140 / 255f, 140 / 255f);
        }
    }
}
