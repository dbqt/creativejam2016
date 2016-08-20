using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    /*
    //USED FOR MARSHMALLOW GENERATION

    public GameObject marshmallow;
    public Transform spawnLocation;
    public float minDesiredMarshmallow;
    public float maxDesiredMarshmallow;

    private GameObject newMarshmallow;
    private bool isCreated = false;
    */

    public Transform waterLevel;
    public float totalTimeToFill;
    public float targetFullWaterLevel;

    private bool timerStarted = false;
    private float timer;
    private float initialWaterLevel;
    private float x;
    private float z;

    // Use this for initialization
    void Start () {
        initialWaterLevel = waterLevel.position.y;
        x = waterLevel.position.x;
        z = waterLevel.position.z;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timerStarted = true;
            timer = totalTimeToFill;
        }
        if (timerStarted)
        {
            Debug.Log(timer + " __ " + waterLevel.position.y);
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
