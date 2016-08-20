using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {





    /*
    //USED FOR MARSHMALLOW GENERATION

    
    
    public float minDesiredMarshmallow;
    public float maxDesiredMarshmallow;

    
    private bool isCreated = false;
    */

    enum E_GAME_STATE { START_MENU,SHOWING_MARSH,PLAYING,ENDSCREEN}
    
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


    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        initialWaterLevel = waterLevel.position.y;
        x = waterLevel.position.x;
        z = waterLevel.position.z;
        marshGoal = Random.Range(minDesiredMarshmallow, maxDesiredMarshmallow);

    }

    // Update is called once per frame
    void Update()
    {
        timerStarted = true;
        timer = totalTimeToFill;
        

        if (timerStarted)
        {

            timer -= Time.deltaTime;
            waterLevel.position= new Vector3(
            x,
            Mathf.Clamp(Mathf.Lerp(targetFullWaterLevel, initialWaterLevel, (timer / totalTimeToFill)), initialWaterLevel,targetFullWaterLevel),
            z); 
        }
        if (timer < 0)
        {
            timerStarted = false;
            Debug.Log("Time's Up!");
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
}
