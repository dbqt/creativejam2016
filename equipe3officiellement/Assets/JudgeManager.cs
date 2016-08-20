using UnityEngine;
using System.Collections;

public class JudgeManager : MonoBehaviour {
    
    enum E_GOAL_PROXIMITY { VERYFAR=0,MEDIUM=2,CLOSE=3,PERFECT=5};
    string[][] comments;
	// Use this for initialization
	void Start () {
        comments = new string[3][];
        comments[(int)E_GOAL_PROXIMITY.VERYFAR] = new string[] { "Smore far than close ! ", "You're Fire...d", "More chicken than marshmallow", "As bad as my puns", "Hot as a hutt" };
        comments[(int)E_GOAL_PROXIMITY.MEDIUM] = new string[] { "Smore close than far! ", "TIL marshmallow cooking is not for everyone", "Maybe it is the branch", "Hot as a hutt" };
        comments[(int)E_GOAL_PROXIMITY.CLOSE] = new string[] { "Boy Scout Worthy", "You nearly succeeded at cooking marshmallow!", "Delicious", "Mallowdelic" };
        comments[(int)E_GOAL_PROXIMITY.PERFECT] = new string[] { "Certified Smore expert (or was it snores)", "As good as lucky charms!", "Sugar God", "Cupcake, kitkat and lollipop got nothing on that marshmallow" };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
