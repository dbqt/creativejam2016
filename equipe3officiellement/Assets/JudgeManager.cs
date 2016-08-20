using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JudgeManager : MonoBehaviour {





    
    enum E_GOAL_PROXIMITY { VERYFAR=0,MEDIUM=1,CLOSE=2,PERFECT=3};
    string[][] comments;
	// Use this for initialization
	void Start () {
        //debug
        GameManager.instance.marshGoal = Random.Range(0, 100);
        for(int i =0;i<4;i++)
            GameManager.instance.marshIndex[i]= Random.Range(0, 100);




        comments = new string[4][];
        comments[(int)E_GOAL_PROXIMITY.VERYFAR] = new string[] { "Smore far than close ! ", "You're Fire...d", "More chicken than marshmallow", "As bad as my puns", "Hot as a hutt" };
        comments[(int)E_GOAL_PROXIMITY.MEDIUM] = new string[] { "Smore close than far! ", "TIL marshmallow cooking is not for everyone", "Maybe it is the branch", "Hot as a hutt" };
        comments[(int)E_GOAL_PROXIMITY.CLOSE] = new string[] { "Boy Scout Worthy", "You nearly succeeded at cooking marshmallow!", "Delicious", "Mallowdelic" };
        comments[(int)E_GOAL_PROXIMITY.PERFECT] = new string[] { "Certified Smore expert (or was it snores)", "As good as lucky charms!", "Sugar God", "Cupcake, kitkat and lollipop got nothing on that marshmallow" };

        float[] proxims= new float[4];
        float smallestVal = 3000;
        for (int i = 0; i < 4; i++)
        {
            proxims[i] = Mathf.Abs(GameManager.instance.marshGoal - GameManager.instance.marshIndex[i]) / 100;
            if (proxims[i] < smallestVal)
                smallestVal = proxims[i];
        }

        List<int> winners= new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if(proxims[0]==smallestVal)
                winners.Add(i+1);
        }


        
        string[] answerString=new string[4];
        for (int i = 0; i < 4; i++)
        {
            answerString[i] = PickString(proxims[i]);
        }

    }
	string PickString(float prox)
    {
        if(prox>=70 )
            return comments[(int)E_GOAL_PROXIMITY.VERYFAR][Random.Range(0, comments[(int)E_GOAL_PROXIMITY.VERYFAR].Length)];
        else if (prox >= 40)
            return comments[(int)E_GOAL_PROXIMITY.MEDIUM][Random.Range(0, comments[(int)E_GOAL_PROXIMITY.MEDIUM].Length)];       
        else if (prox >= 15)      
            return comments[(int)E_GOAL_PROXIMITY.CLOSE][Random.Range(0, comments[(int)E_GOAL_PROXIMITY.CLOSE].Length)];       
        return comments[(int)E_GOAL_PROXIMITY.PERFECT][Random.Range(0, comments[(int)E_GOAL_PROXIMITY.PERFECT].Length)];
    }
    // Update is called once per frame
    void Update () {
	
	}
}
