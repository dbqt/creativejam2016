using UnityEngine;
using System.Collections;

public class WindManager : MonoBehaviour {

    public static WindManager instance = null;
    [Tooltip("Range of the wind change time in seconds,x=min,y=max")]
    public Vector2 windChangeRange = new Vector2(5, 10);
    [Tooltip("Range of the wind power,x=min,y=max")]
    public Vector2 windRange=new Vector2(1,1.3f);
    public Vector2 windVector=new Vector2(0,0);
    public Camera cam;
    public float[] sidePower = new float[8];
    private float timer;


    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {

        for (int i = 0; i < 8; i++)
            sidePower[0] = 0f;
        SetWind();
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time>timer)
            SetWind();

    }
    void SetWind()
    {
        windVector = new Vector2(Random.Range(windRange.x, windRange.y), Random.Range(windRange.x, windRange.y));
        sidePower[(int)E_SIDE.EST] += windVector.x;
        sidePower[(int)E_SIDE.WEST] -= windVector.x;
        sidePower[(int)E_SIDE.SOUTH] -= windVector.y;
        sidePower[(int)E_SIDE.NORTH] += windVector.y;
        sidePower[(int)E_SIDE.NORTH_EAST] += windVector.y * 0.5f + windVector.x * 0.5f;
        sidePower[(int)E_SIDE.NORTH_WEST] += windVector.y * 0.5f - windVector.x * 0.5f;
        sidePower[(int)E_SIDE.SOUTH_WEST] += -windVector.y * 0.5f - windVector.x * 0.5f;
        sidePower[(int)E_SIDE.SOUTH_EAST] += -windVector.y * 0.5f + windVector.x * 0.5f;
        timer = Time.time  + Random.Range(windChangeRange.x, windChangeRange.y);
        cam.GetComponent<CameraShake>().Shake();
        transform.eulerAngles = new Vector3(0f, Mathf.Rad2Deg* Mathf.Atan2(-windVector.normalized.x, -windVector.normalized.y), 0f);
    }
}
