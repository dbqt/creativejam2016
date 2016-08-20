using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum E_SIDE { NORTH=0,WEST=1,EST=2,SOUTH=3,NORTH_EAST=4,NORTH_WEST=5,SOUTH_EAST=6,SOUTH_WEST=7}
[System.Serializable]
public class Node  {
    static int FLAMETRESHOLD = 5;
    public Node(float flamabilityModifier)
    {
        flameRate = 0;
        flameLevel = 0;
        this.flamabilityModifier = flamabilityModifier;
        neighbours = new List<KeyValuePair<E_SIDE, Node>>();
    }
    public float flameLevel;
    public float flameRate;
    private float flamabilityModifier;
    public List<KeyValuePair<E_SIDE,Node>> neighbours; 
    public void CalculateFlameRate(float dt)
    {
        flameRate = Mathf.Clamp(flameRate, -0.5f, 0.02f);
        float fireTotal = 0;
        for(int i =0;i<neighbours.Count;i++)
        {
            fireTotal += neighbours[i].Value.flameLevel + neighbours[i].Value.flameLevel*WindManager.instance.sidePower[(int)neighbours[i].Key]>0? neighbours[i].Value.flameLevel + neighbours[i].Value.flameLevel * WindManager.instance.sidePower[(int)neighbours[i].Key] : 0;
        }
        flameRate +=(dt * fireTotal * flamabilityModifier);
        
        flameLevel += flameRate;
        flameLevel = Mathf.Clamp(flameLevel, 0f, 10f);
    }
}
