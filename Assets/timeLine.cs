using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeLine : MonoBehaviour
{
    private Image timeBar;
    [SerializeField] private float mapTime = 20f;
    public float timeLeft, timeLeftSpeed = 0f;
    public bool timeToDie = false;
    private bool timeLevelAdd = false;
    public int Counter;

    void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = mapTime;
    }

    void Update()
    {
        if(timeLeft > 0)
        {
            timeToDie = false;
            timeLeft -= Time.deltaTime + timeLeftSpeed;
            timeBar.fillAmount = timeLeft  / mapTime;
            //Debug.Log(timeLeft);
            //Debug.Log(mapTime);
            timeBar.color = new Vector4((1 - timeBar.fillAmount), (timeBar.fillAmount) , timeBar.color.b, timeBar.color.a);
        }
        else
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / mapTime;
            timeBar.color = new Vector4((1 - timeBar.fillAmount), (timeBar.fillAmount), timeBar.color.b, timeBar.color.a);

            timeToDie = true;
        }
        if (timeBar.fillAmount >=1 )
        {
            timeLevelUp();
        }
    }

    private void timeLevelUp()
    {
        timeLevelAdd = true;
        if (timeLevelAdd)
        {
            timeLeft = mapTime;
            Counter++;
            Debug.Log(Counter);
            gameController.Instance.addlevel(Counter);
        }
        timeLevelAdd = false;
    }

}
