using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLineMove : MonoBehaviour
{
    public float LineMoveSpeed = 5f;

    
    void Update()
    {
        transform.position = transform.position + (Vector3.left * (-LineMoveSpeed) * Time.deltaTime);
    }
}
