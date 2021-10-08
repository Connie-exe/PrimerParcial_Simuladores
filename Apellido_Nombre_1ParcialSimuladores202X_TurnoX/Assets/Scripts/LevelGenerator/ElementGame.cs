using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGame : MonoBehaviour
{
    public GameObject prefab;
    public Vector2 vector;

    public ElementGame()
    {

    }
    public ElementGame(GameObject prefab, Vector2 vector)
    {
        this.prefab = prefab;
        this.vector = vector;
    }
}
