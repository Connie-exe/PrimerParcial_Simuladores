using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    //private IEnumerator coroutine;
    public virtual void OnCollisionEnter(Collision other)//si el objeto colisiona con...
    {
        if (other.gameObject.CompareTag("jugador"))//con el objeto de etiqueta jugador
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            //StartCoroutine(DestroyObject());
        }
    }

    //IEnumerator DestroyObject()
    //{
    //    yield return new WaitForSeconds(4f);
    //    Destroy(this.gameObject);
    //}
}
