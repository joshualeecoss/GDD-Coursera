using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    public float speed;

    public void Start()
    {
        speed = Random.Range(-10.0f, 10.0f);
    }


    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, speed * Time.deltaTime);
    }
}
