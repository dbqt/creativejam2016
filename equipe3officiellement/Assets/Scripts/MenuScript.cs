using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public void OnStartGame()
    {
        Debug.Log("You clicked on Start Menu");

        // Load Scene for StartGame
        UnityEngine.SceneManagement.SceneManager.LoadScene("t_movement");
    }

    public void OnExitGame()
    {
        Debug.Log("You clicked Exit Game!");

        //Application.Quit();
    }
}
