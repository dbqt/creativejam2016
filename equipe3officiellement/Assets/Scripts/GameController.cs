﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {





    /*
    //USED FOR MARSHMALLOW GENERATION

    
    
    public float minDesiredMarshmallow;
    public float maxDesiredMarshmallow;

    
    private bool isCreated = false;
    */

    public enum E_GAME_STATE { START_MENU,SHOWING_MARSH,SHOWING_MAP,PLAYING,SCORE_SCREEN,ENDSCREEN}
    
    //MARSHMALLOW - public
    public GameObject marshmallow;
    public Transform[] spawnMashLocations;
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
    public GameObject winningText1;
    public GameObject winningText2;
    public GameObject winnerPicture;
    public EllipsoidParticleEmitter ep;
    public FireManager fm;
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
            for(int i =0;i<4;i++)
            {
                (Instantiate(marshmallow, spawnMashLocations[i].position,Quaternion.identity) as GameObject).GetComponent<MarshmallowRoasting>().index = i;
                
            }
            
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
                if(timer <1.5f)
                {
                    ep.emit = true;
                }
                if (timer < 0)
                {
                    timerStarted = false;
                
                    StopGame();
                }
                break;
            case E_GAME_STATE.ENDSCREEN:
                if (Input.anyKeyDown)
                {
                    backToMenu();
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
        

        fm.StopFire();
        
        StartCoroutine(ExtinguishSmoke());
    }
    public void ShowScoreScreen()
    {
        endScreen.gameObject.SetActive(true);
        
    }
    private IEnumerator ExtinguishSmoke()
    {
        yield return new WaitForSeconds(0.5f);
        ep.emit = false;
        Camera.main.GetComponent<Animation>().Play("EndScreenCamera");
    }
    public void ShowWinner()
    {
        stateGame = E_GAME_STATE.SCORE_SCREEN;
        ActiveWinnerScreen();
    }
    public void ActiveWinnerScreen()
    {
        stateGame = E_GAME_STATE.ENDSCREEN;
        winnerScreen.SetActive(true);
        winningText1.SetActive(true);
        winningText2.SetActive(true);
        winnerPicture.SetActive(true);
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
