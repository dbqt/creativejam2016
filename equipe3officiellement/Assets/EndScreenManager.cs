using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EndScreenManager : MonoBehaviour {
    public Text[] PlayerScores;
    public Sprite marsh;
    public RectTransform[] PlayerMellow;
    public Text[] commentText;
    public Image goalMarshmallow;
    public JudgeManager judge;
    public Text winnerText;
    public Draw2Controller draw2;
    public Draw2Controller draw3;
    public Draw3Controller draw;
    private bool[] finished = new bool[4];
    private bool[] finishedDuplicate = new bool[4];
    private bool readyPickWinner;
    private bool winnerPicked;
    private bool done;
    private bool doublingCheck;
    private AudioSource clappingSound;

    // Use this for initialization
    void Start () {
        clappingSound = GetComponent<AudioSource>();
        clappingSound.Play();
        StartCoroutine(StartShowScore());
        GetComponent<Animation>().Play("EndScreen");
        for (int i = 0; i < 4; i++)
        {
            Color pickedColor = GameController.instance.marshmallows[i].GetComponent<MarsmallowBehavior>().currentColor;
            pickedColor.a = 1;
            PlayerMellow[i].GetComponent<Image>().color = pickedColor;
        }
        Color pickedCol = GetMarshColor(GameController.instance.marshGoal);
        pickedCol.a = 1;
        goalMarshmallow.color = pickedCol;

    }
	Color GetMarshColor(float newColorIndex)
    {
        Color toReturn = new Color() ;
        Color white = GameController.instance.marshmallows[0].GetComponent<MarsmallowBehavior>().white;
        Color orange = GameController.instance.marshmallows[0].GetComponent<MarsmallowBehavior>().orange;
        Color black = GameController.instance.marshmallows[0].GetComponent<MarsmallowBehavior>().black;
        if (newColorIndex > 0 && newColorIndex <= 75)
            {
            toReturn = Color.Lerp(white, orange, newColorIndex / 75);

            }
            else if (newColorIndex > 75 && newColorIndex < 100)
            {
            toReturn = Color.Lerp(orange, black, (newColorIndex - 75) / 25);
            }

        return toReturn;
    }
	// Update is called once per frame
	void Update () {
        if (finished[0] && finished[1] && finished[2] && finished[3] && !finishedDuplicate[3] && !doublingCheck)
        {
            doublingCheck = true;
            int scoreToTest;
            List<int> doubles = new List<int>();
            for (int j = 0; j < 4; j++)
            {
                scoreToTest = (int)(judge.proxims[j] * 10);
                for (int i = 0; i < 4; i++)
                {
                    if ((int)(judge.proxims[i] * 10) == scoreToTest)
                    {
                        doubles.Add(i);
                    }
                }
                if (doubles.Count > 1)
                {
                    for (int i = 0; i < doubles.Count; i++)
                    {
                        StartCoroutine(ShowScoreDecimal(doubles[i]));
                    }
                }
                else
                {
                    finishedDuplicate[j] = true;
                }
                doubles.Clear();
            }
        }
        else if (finishedDuplicate[0] && finishedDuplicate[1] && finishedDuplicate[2] && finishedDuplicate[3] && !winnerPicked)
        {
            for(int i=0;i<4;i++)
            {
                commentText[i].enabled = true;
                commentText[i].text = judge.answerString[i];
            }
            winnerPicked = true;
            winnerText.text = "Player " + (judge.winners[0]+1);

        }
        if(winnerPicked && !done)
        {
            done = true;
            StartCoroutine(ShowWinner());
        }
    }
    IEnumerator ShowWinner()
    {
        yield return new WaitForSeconds(2f);
        GameController.instance.ShowWinner(judge.winners);
       
    }
    IEnumerator StartShowScore()
    {
        StartCoroutine(ShowScore(0));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowScore(1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowScore(2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowScore(3));
        yield return new WaitForSeconds(5f);
       

    }
    IEnumerator ShowScore(int playerID)
    {

        for(int i=0;i<100;i++)
        {
            PlayerScores[playerID].text = ((int)Random.Range(0, 10)).ToString();
            if (i >= 80)
                yield return new WaitForSeconds(0.001f*i*Random.Range(2,3));
            else
                yield return new WaitForSeconds(0.001f);
        }
        PlayerScores[playerID].text = ((int)Random.Range(7, 10)).ToString();
        yield return new WaitForSeconds(Random.Range(1, 3));
        PlayerScores[playerID].text = ((int)(judge.proxims[playerID]*10)).ToString();
        yield return new WaitForSeconds(1f);
        finished[playerID] = true;
    }
    IEnumerator ShowScoreDecimal(int playerID)
    {
        float score = (int)(judge.proxims[playerID] * 10);
        for (int i = 0; i < 100; i++)
        {
            PlayerScores[playerID].text = (score + "."+ (int)Random.Range(0, 9)).ToString();
            if (i >= 80)
                yield return new WaitForSeconds(0.001f * i * Random.Range(2, 3));
            else
                yield return new WaitForSeconds(0.001f);
        }
        PlayerScores[playerID].text = (score + "." + (int)Random.Range(7, 9)).ToString();
        yield return new WaitForSeconds(Random.Range(1, 3));
        PlayerScores[playerID].text = (judge.proxims[playerID] * 10f).ToString();
        yield return new WaitForSeconds(1f);
        finishedDuplicate[playerID] = true;
    }
}
