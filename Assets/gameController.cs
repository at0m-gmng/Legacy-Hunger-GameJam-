using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using Random = UnityEngine.Random;

public class gameController : MonoBehaviour
{
    public static gameController Instance;

    [SerializeField] private dataData dataData;

    [SerializeField] private Transform popUpText;
    [SerializeField] private GameObject eatPref;
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private TextMeshProUGUI textBar;
    [SerializeField] private TextMeshProUGUI timeLine;
    [SerializeField] private GameObject slaimPref;
    [SerializeField] private int nowLevel = 0;
    [SerializeField] private float addedScore = 0;
    [SerializeField] private float basicAddedScore;
    private float timeSlimaSpawn;
    private timeLine time;

    private List<int> randomList = new List<int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        basicAddedScore = dataData.basicAddedTime;
        time = FindObjectOfType<timeLine>();
        randomWithoutDuplicate();

        foreach (int randomList in randomList)
        {
            eatSpawner(dataData.fruitPositions[randomList]);
        }

        addlevel(nowLevel);

    }
    private void Update()
    {
        if(!FindObjectOfType<slaimController>())
        {
            timeSlimaSpawn += Time.deltaTime;
            if(timeSlimaSpawn >= 5)
            {
                Instantiate(slaimPref, parentCanvas.transform);
                timeSlimaSpawn = 0;
            }
        }
    }

    public void addlevel(int level)
    {
        if (nowLevel < dataData.maxLevel)
        {
            nowLevel += 1;
            //Debug.Log(nowLevel);
            textBar.text = "Hunger Level " + nowLevel;
            time.timeLeftSpeed += dataData.timeLeftSpeed;
            addedScore += dataData.addedTime;
        }
    }

    public void addScore(float score, bool onPoison, GameObject GameObject)
    {
        var scoreTime = score + basicAddedScore + addedScore;
        if (GameObject.layer == 3)
        {   
            if (onPoison)
            {
                time.timeLeft -= scoreTime;
                //var asd = popUpText.GetComponent<TextMeshPro>();
                //asd.text = "-" + scoreTime;
                player.GetComponent<playerController>().isDamageble();
            }
            else
            {
                time.timeLeft += scoreTime;
                //var asd = popUpText.GetComponent<TextMeshPro>();
                //asd.text = "+" + scoreTime;
                //Debug.Log(score + 5 + addedScore);
            }
        }
    }

    public void popUpTextSpawner(Transform eatCollisionObject, bool onPoison, float score)
    {
        var allScore = score + basicAddedScore + addedScore;
        var text = Instantiate(popUpText, parentCanvas.transform);
        text.transform.localPosition = eatCollisionObject.localPosition;
        var textComponent = text.GetComponent<TextMeshPro>();
        if(eatCollisionObject.gameObject.layer == 3)
        {
            if (!onPoison)
                textComponent.text = "+" + allScore;
            else
                textComponent.text = "-" + allScore.ToString("0.0");
        }
        if(eatCollisionObject.gameObject.layer == 10)
            if (!onPoison)
                textComponent.text = "+" + 1;
            else
                textComponent.text = "+" + 2;
    }

    public void eatSpawner(Vector3 startPos)
    {
        var eat = Instantiate(eatPref, parentCanvas.transform, false);
        eat.transform.localPosition = new Vector3(Random.Range(startPos.x - 30f, startPos.x + 30f), startPos.y, startPos.z);
    }

    private List<int> randomWithoutDuplicate()
    {
        var rnd = new System.Random();
        randomList = Enumerable.Range(0, dataData.fruitPositions.Count).OrderBy(x => rnd.Next()).Take(dataData.fruitPositions.Count).ToList();

        //for (int i = 0; i < myStartPositions.Length; i++)
            //Debug.Log(randomList[i]);
        return randomList;
    }
}
