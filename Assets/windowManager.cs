using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class windowManager : MonoBehaviour
{
    [SerializeField] private GameObject closeInterpface;
    [SerializeField] private TextMeshProUGUI closeInterpfaceText;
    [SerializeField] private GameObject[] buttons;

    private bool startGame = true;
    private timeLine timeLine;
    [SerializeField] private float timeAlive = 0;

    private void Start()
    {
        timeLine = FindObjectOfType<timeLine>();
    }
    void Update()
    {
        timeAlive += Time.deltaTime;
        closeInterpfaceText.text = timeAlive.ToString("0.0");

        if (timeLine.timeToDie)
            Invoke("stopGame", 1f);
        else
            Time.timeScale = 1;

        if (!startGame)
        {
            StartCoroutine(Blackout(false, closeInterpface.GetComponent<Image>().color, closeInterpface));
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            StartCoroutine(Blackout(true, closeInterpface.GetComponent<Image>().color, closeInterpface));
        }

        if (Input.GetKeyDown("escape"))  // если нажата клавиша Esc (Escape)
        {
            stopGame();
        }
    }

    public void continueGame()
    {
        if(!timeLine.timeToDie)
        {
            startGame = true;
            //Time.timeScale = 0;
            closeInterpfaceText.color = Color.white;
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            buttons[2].SetActive(false);
            //StartCoroutine(Blackout(true, closeInterpface.GetComponent<Image>().color, closeInterpface));
        }

    }

    public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void stopGame()
    {
        startGame = false;
        //Time.timeScale = 0;
        closeInterpfaceText.color = Color.white;
        buttons[0].SetActive(true);
        buttons[1].SetActive(true);
        if (!timeLine.timeToDie)
            buttons[2].SetActive(true);
        //StartCoroutine(Blackout(false, closeInterpface.GetComponent<Image>().color, closeInterpface));

    }
    IEnumerator Blackout(bool inverse, Color color, GameObject GameObject)
    {
        //Material tempMaterial = mainCamera.GetComponentInChildren<Renderer>().material;
        Color tempColor = color;
        if (!inverse)
        {
            while (tempColor.a < 1f)
            {
                yield return new WaitForEndOfFrame();
                tempColor.a = tempColor.a + 0.005f;
                GameObject.GetComponent<Image>().color = new Vector4(GameObject.GetComponent<Image>().color.r,
                                                                    GameObject.GetComponent<Image>().color.g,
                                                                    GameObject.GetComponent<Image>().color.b,
                                                                    tempColor.a);
            }
        }
        else if (inverse)
        {
            while (tempColor.a > 0f)
            {
                yield return new WaitForEndOfFrame();
                tempColor.a = tempColor.a - 0.01f;
                GameObject.GetComponent<Image>().color = new Vector4(GameObject.GetComponent<Image>().color.r,
                                                    GameObject.GetComponent<Image>().color.g,
                                                    GameObject.GetComponent<Image>().color.b,
                                                    tempColor.a);
            }
        }
    }
    public void exit()
    {
        Application.Quit();
    }
}
