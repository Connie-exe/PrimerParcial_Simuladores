using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public GameObject timer_decor;
    public Image fillimage;
    public float timer;
    public static float waitTime; 

    void Start()
    {        
        waitTime = timer;
        timer_decor.SetActive(false);

    }
    void Update()
    {
        SetTimer();
    }

    public void SetTimer()
    {
            timer_decor.SetActive(true);
            waitTime -= Time.deltaTime;
            fillimage.fillAmount = waitTime / timer;

            if (waitTime <= 0)
            {
                timer_decor.SetActive(false);
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
    }
}
