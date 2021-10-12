using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text txt_timer;
    public float timer;
    public static float waitTime; 

    void Start()
    {        
        waitTime = timer;
        txt_timer.text = timer.ToString();
    }
    void Update()
    {
        SetTimer();
    }

    public void SetTimer()
    {
            waitTime -= Time.deltaTime;
            txt_timer.text = waitTime.ToString();
            if (waitTime <= 0)
            {                
                SceneManager.LoadScene(0);
            }
    }
}
