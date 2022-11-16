using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    int NotToDestroy = 0; 
    // Start is called before the first frame update
    void Awake()
    {
        NotToDestroy = FindObjectsOfType<DoNotDestroy>().Length;
        if (NotToDestroy > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
