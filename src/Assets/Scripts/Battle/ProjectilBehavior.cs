using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilBehavior : MonoBehaviour
{
    public float Speed = 9.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }
}
