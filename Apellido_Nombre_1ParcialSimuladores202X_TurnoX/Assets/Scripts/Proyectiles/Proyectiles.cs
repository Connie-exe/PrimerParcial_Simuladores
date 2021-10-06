using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proyectiles : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;
    public float timeUp;
    public float timeToShoot;
    void Start()
    {
        timeUp = 3f;
        timeToShoot = 0f;
    }
    void Update()
    {
        if (timeToShoot >= timeUp)
        {
            Shoot();
            timeToShoot = 0f;
        }
        timeToShoot += Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);//el gameobject bullet es igual a el bullet prefab en la posición del firepoint y con la rotación de firepoint 
        Rigidbody rb = bullet.GetComponent<Rigidbody>();//rigidbody es igual a el rigidbody de bullet
        rb.AddForce(firePoint.right * bulletForce, ForceMode.Impulse);//el rigidbody se mueve hacia adelante con la fuerza de bullet
    }
}
