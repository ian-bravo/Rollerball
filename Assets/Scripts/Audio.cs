using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    void Awake()
    {
        //audio won't be destroyed between scenes
        DontDestroyOnLoad(transform.gameObject);
    }
}
