using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EndScreenManager : MonoBehaviour {
    public Text[] PlayerScores;
    public RectTransform[] PlayerMellow;
    public Text[] commentText;
    public JudgeManager judge;
    private bool[] finished = new bool[4];
    private bool[] finishedDuplicate = new bool[4];
    private bool readyPickWinner;
    private bool winnerPicked;
    // Use this for initialization
    void Start () {
        StartCoroutine(StartShowScore());
    }
	
	// Update is called once per frame
	void Update () {
        if (finished[0] && finished[1] && finished[2] && finished[3] && !finishedDuplicate[0])
        {

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
            

        }
        if(winnerPicked)
        {
            GameController.instance.ShowWinner();
        }
    }
    IEnumerator StartShowScore()
    {
        StartCoroutine(ShowScore(0));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowScore(2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowScore(1));
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
