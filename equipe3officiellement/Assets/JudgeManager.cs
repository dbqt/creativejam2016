using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JudgeManager : MonoBehaviour {





    
    enum E_GOAL_PROXIMITY { VERYFAR=0,MEDIUM=1,CLOSE=2,PERFECT=3};
    private string[][] comments;
    public string[] answerString = new string[4];
    public double[] proxims = new double[4];
    public List<int> winners = new List<int>();
    // Use this for initialization
    void Start () {
        
        ////debug
        //GameController.instance.marshGoal = UnityEngine.Random.Range(0, 100);
        //for(int i =0;i<4;i++)
        //    GameController.instance.marshIndex[i]= UnityEngine.Random.Range(0, 100);

        //*********************************************************


        comments = new string[4][];
        comments[(int)E_GOAL_PROXIMITY.VERYFAR] = new string[] { "Nul", "GG(Get good)", "Guignol"};
        comments[(int)E_GOAL_PROXIMITY.MEDIUM] = new string[] { "Smore close than far! ", "Meh", "...", "Ok" };
        comments[(int)E_GOAL_PROXIMITY.CLOSE] = new string[] { "Hot as a hutt", "Gui-me smore!", "Delicieux", "Très bon" };
        comments[(int)E_GOAL_PROXIMITY.PERFECT] = new string[] { "La perfection", "Sugar God", "Magnifique" };

        
        double biggestVal = -1;
        for (int i = 0; i < 4; i++)
        {
            proxims[i] = Math.Round(Mathf.Abs(GameController.instance.marshGoal- Mathf.Clamp(GameController.instance.marshIndex[i], 0, 100)) / 100,2);
            proxims[i] = Math.Abs(proxims[i] - 1);
            Debug.Log(proxims[i]);
            if (proxims[i] > biggestVal)
                biggestVal = proxims[i];
        }   
        for (int i = 0; i < 4; i++)
        {
            if(proxims[i]== biggestVal)
                winners.Add(i);
        }
        for (int i = 0; i < 4; i++)
        {
            answerString[i] = PickString(proxims[i]);
        }

    }
	string PickString(double prox)
    {
        if(prox >= 0.95f)
            return comments[(int)E_GOAL_PROXIMITY.PERFECT][UnityEngine.Random.Range(0, comments[(int)E_GOAL_PROXIMITY.PERFECT].Length)];
        else if (prox>=0.80f)
            return comments[(int)E_GOAL_PROXIMITY.CLOSE][UnityEngine.Random.Range(0, comments[(int)E_GOAL_PROXIMITY.CLOSE].Length)];
        else if (prox >= 0.40f)
            return comments[(int)E_GOAL_PROXIMITY.MEDIUM][UnityEngine.Random.Range(0, comments[(int)E_GOAL_PROXIMITY.MEDIUM].Length)];       

        return comments[(int)E_GOAL_PROXIMITY.VERYFAR][UnityEngine.Random.Range(0, comments[(int)E_GOAL_PROXIMITY.VERYFAR].Length)];


    }
    // Update is called once per frame
    void Update () {
	
	}
}
