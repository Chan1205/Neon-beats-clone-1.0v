using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDis : MonoBehaviour
{
    float DePlatformCounter = 1f;
    
    void Awake()
    {
        
    }

    
    void Update()
    {
        Invoke("DePlatform", 2f);
        Invoke("ActivatePlatform", 2f);
    }

    void DePlatform()
    {
        gameObject.SetActive(false);
    }

    void ActivatePlatform()
    {
        gameObject.SetActive(true);
    }

    void Platform()
    {
        
    }
}
