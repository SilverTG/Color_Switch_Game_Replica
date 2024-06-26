using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCircleController : MonoBehaviour
{
    public float speed = 100;
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
