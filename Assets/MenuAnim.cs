using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{

    public float TimeTilSlow = 1.0f;
    public float SlowAmountBefore = .5f;
    public float SlowAmount = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Slow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Slow()
    {
        Time.timeScale = TimeTilSlow;
        yield return new WaitForSeconds(TimeTilSlow);
        Time.timeScale = SlowAmount;
    }
}
