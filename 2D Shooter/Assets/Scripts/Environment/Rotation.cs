using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform target;
    public float speed;

    public void Start()
    {
        speed = Random.Range(-10.0f, 10.0f);
    }


    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.forward, speed * Time.deltaTime);
    }
}
