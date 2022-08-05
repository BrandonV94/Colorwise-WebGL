using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    [SerializeField] bool pulse = false;
    [SerializeField] SpriteRenderer pieceSR;
    [SerializeField] int countDown = 12;


    // Start is called before the first frame update
    void Awake()
    {
        pieceSR = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckForPulse();
    }
    
    void CheckForPulse()
    {
        if (pulse)
        {
            StartCoroutine(Pulse());
        }
        pulse = false;
    }

    IEnumerator Pulse()
    {
        for(; countDown> 0; countDown--)
        {
            if (pieceSR.sortingOrder == 0)
            {
                pieceSR.sortingOrder = 2;
            }
            else
            {
                pieceSR.sortingOrder = 0;
            }
            yield return new WaitForSeconds(.25f);
        }
        countDown = 12;
        pieceSR.sortingOrder = 0;
        yield break;    // Breaks the coroutine after flashign for a few seconds
    }
    
}
