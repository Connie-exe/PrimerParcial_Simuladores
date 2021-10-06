﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Velocidad : MonoBehaviour
{
    public int speed = 7;

    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("jugador"))
        {
            Debug.Log("Hola");
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}

