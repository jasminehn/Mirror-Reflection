using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isReached;
    void Start()
    {
        isReached = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
