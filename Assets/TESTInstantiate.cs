using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTInstantiate : MonoBehaviour
{
    [SerializeField] GameObject testPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(testPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
