using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject marshmallow;
    public Transform spawnLocation;
    public float minDesiredMarshmallow;
    public float maxDesiredMarshmallow;

    private GameObject newMarshmallow;
    private bool isCreated = false;

    // Use this for initialization

    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (isCreated == false && Input.GetKeyDown(KeyCode.Space))
        {
            isCreated = true;
            newMarshmallow = Instantiate(marshmallow, spawnLocation.position, spawnLocation.rotation) as GameObject;
            newMarshmallow.GetComponent<MarsmallowBehavior>().applyColorIndex(Random.Range(minDesiredMarshmallow, maxDesiredMarshmallow));
        }
    }
}
