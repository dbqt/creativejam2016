using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {



    public Sprite[] sprites;
    public GameObject finishScreen;
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
    public GameObject[] marshmallows = new GameObject[4];
    public FireManager fm;
    private AudioSource musicSource;
    

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
                marshmallows[i] = Instantiate(marshmallow, spawnMashLocations[i].position, Quaternion.identity) as GameObject;
                marshmallows[i].GetComponent<MarshmallowRoasting>().index = i;
                
            }
            musicSource = GetComponent<AudioSource>();
            musicSource.Play();
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
    public void ShowWinner(List<int> winners)
    {
        stateGame = E_GAME_STATE.SCORE_SCREEN;
        ActiveWinnerScreen(winners);
    }
    public void ActiveWinnerScreen(List<int> winners)
    {
        if (winners.Count == 2)
        {
            endScreen.draw2.txt1.text += " Players " + (winners[0]+1).ToString() + " AND " + (winners[1]+1) + " ! ";
            endScreen.draw2.gameObject.SetActive(true);
            endScreen.draw2.img1.GetComponent<Image>().sprite = sprites[winners[0]];
            endScreen.draw2.img2.GetComponent<Image>().sprite = sprites[winners[1]];
        }
        else if(winners.Count == 3)
        {
            endScreen.draw.txt1.text += " Players " + winners[0].ToString()+" , "+( winners[1]+1) + " AND " + (winners[2]+1) + " ! ";
            endScreen.draw.gameObject.SetActive(true);
            endScreen.draw.img1.GetComponent<Image>().sprite = sprites[winners[0]];
            endScreen.draw.img2.GetComponent<Image>().sprite = sprites[winners[1]];
            endScreen.draw.img3.GetComponent<Image>().sprite = sprites[winners[2]];
        }
        else if(winners.Count>3)
        {
            endScreen.draw3.gameObject.SetActive(true);
        }
        else
        {
            finishScreen.GetComponent<Animation>().Play("FinishScreen");
            winnerScreen.SetActive(true);
            winningText1.SetActive(true);
            winningText2.SetActive(true);
            winnerPicture.SetActive(true);
            winnerPicture.GetComponent<Image>().sprite = sprites[winners[0]];
        }
        stateGame = E_GAME_STATE.ENDSCREEN;
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
