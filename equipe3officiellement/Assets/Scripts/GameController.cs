using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {





    /*
    //USED FOR MARSHMALLOW GENERATION

    
    
    public float minDesiredMarshmallow;
    public float maxDesiredMarshmallow;

    
    private bool isCreated = false;
    */

    public enum E_GAME_STATE { START_MENU,SHOWING_MARSH,SHOWING_MAP,PLAYING,ENDSCREEN}
    
    //MARSHMALLOW - public
    public GameObject marshmallow;
    public Transform spawnLocation;

    //MARSHMALLOW - private
    private GameObject newMarshmallow;

    //WATER LEVEL - public
    public Transform waterLevel;
    public float totalTimeToFill;
    public float targetFullWaterLevel;
    
    //WATER LEVEL - private
    private bool timerStarted = false;
    private float timer;
    private float initialWaterLevel;
    private float x;
    private float z;
    [Tooltip("Minimum for the cooking goal")]
    public float minDesiredMarshmallow;
    [Tooltip("Maximum for the cooking goal")]
    public float maxDesiredMarshmallow;
    public float[] marshIndex = new float[4] { 0, 0, 0, 0 };
    public float marshGoal = 0;
    public static GameController instance;
    public E_GAME_STATE stateGame=E_GAME_STATE.START_MENU;
    public EndScreenManager endScreen;
    private bool[] playersConfirmed = new bool[4];
    public GameObject winnerScreen;

    void Awake()
    {
        instance = this;    
    }
    // Use this for initialization
    void Start () {
        if(stateGame==E_GAME_STATE.SHOWING_MAP)
        {
            stateGame = E_GAME_STATE.SHOWING_MAP;
            initialWaterLevel = waterLevel.position.y;
            x = waterLevel.position.x;
            z = waterLevel.position.z;
            marshGoal = Random.Range(minDesiredMarshmallow, maxDesiredMarshmallow);
            UnlockGame();
        }


    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    // Update is called once per frame
    void Update()
    {
        switch (stateGame)
        {

            case E_GAME_STATE.PLAYING:               
                if (timerStarted)
                {

                    timer -= Time.deltaTime;
                    waterLevel.position = new Vector3(
                    x,
                    Mathf.Clamp(Mathf.Lerp(targetFullWaterLevel, initialWaterLevel, (timer / totalTimeToFill)), initialWaterLevel, targetFullWaterLevel),
                    z);
                }
                if (timer < 0)
                {
                    timerStarted = false;
                    Debug.Log("Time's Up!");
                    StopGame();
                }
                break;

    }
       
        /*
        //USED FOR MARSHMALLOW GENERATION
        if (isCreated == false && Input.GetKeyDown(KeyCode.Space))
        {
            isCreated = true;
            newMarshmallow = Instantiate(marshmallow, spawnLocation.position, spawnLocation.rotation) as GameObject;
            newMarshmallow.GetComponent<MarsmallowBehavior>().applyColorIndex(Random.Range(minDesiredMarshmallow, maxDesiredMarshmallow));
        }
        */
    

    }
    void StopGame()
    {
        endScreen.gameObject.SetActive( true);
    }
    public void ShowWinner()
    {
        winnerScreen.SetActive(true);
        if (Input.anyKeyDown)
        {
            backToMenu();
        }
    }
    private void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
    void UnlockGame()
    {
        stateGame = E_GAME_STATE.PLAYING;
        timerStarted = true;
        timer = totalTimeToFill;
    }
}
