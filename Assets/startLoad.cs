using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startLoad : MonoBehaviour
{
    public void loadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void exit()
    {
        Application.Quit();
    }
}
