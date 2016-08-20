using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FireManager : MonoBehaviour {

    [Tooltip("A modifier that is multiplied to the fire calculator, slows (if lower than 1) or fasten (if bigger than one) the fire propagation ")]
    public float flamabilityModifier=1;
    [Tooltip("Size of the fire at the center")]
    public int fireCenterSize;
    [Tooltip("Intensity of the fire at the center")]
    public int centerIntensity;
    public Vector2 mapSize;
    public GroundController[][] map;
    public GroundController groundTile;
	// Use this for initialization
	void Start () {
        map = new GroundController[(int)mapSize.x][];

        for (int x=0;x<mapSize.x;x++)
        {
            map[x] = new GroundController[(int)mapSize.y];
            for (int y=0;y<mapSize.y;y++)
            {
                GroundController newGC=Instantiate(groundTile);
                newGC.transform.position = new Vector3(x+transform.position.x-mapSize.x/2, 0, y + transform.position.y - mapSize.y / 2);
                newGC.transform.Rotate(new Vector3(0f, Random.Range(0f,360f),0f));
                newGC.node = new Node(flamabilityModifier);
                map[x][y] = newGC;
            }
        }
        //assign neighbours
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (x + 1 < mapSize.x)
                {
                    map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.EST,map[x + 1][y].node));
                    if (y - 1 >= 0)
                        map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.SOUTH_EAST,map[x + 1][y - 1].node));
                    if (y + 1 < mapSize.y)
                        map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.NORTH_EAST,map[x + 1][y + 1].node));
                }
                if (x - 1 >= 0)
                {
                    map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.WEST,map[x - 1][y].node));

                    if (y - 1 >= 0)
                        map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.SOUTH_WEST, map[x - 1][y - 1].node));
                    if (y + 1 < mapSize.y)
                        map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.NORTH_WEST,map[x - 1][y + 1].node));
                }
                if (y + 1 < mapSize.y)
                    map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.NORTH,map[x][y+1].node));
                if (y - 1 >= 0)
                    map[x][y].node.neighbours.Add(new KeyValuePair<E_SIDE, Node>(E_SIDE.SOUTH,map[x][y-1].node));
            }
        }
     for(int x =0;x<fireCenterSize;x++)
        {
            for (int y = 0; y < fireCenterSize; y++)
            {
                map[x + (int)(mapSize.x / 2f )- (int)(fireCenterSize / 2f)][y + (int)(mapSize.y / 2f) - (int)(fireCenterSize / 2f)].node.flameLevel = centerIntensity;
               
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
