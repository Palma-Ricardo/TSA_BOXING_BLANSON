using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience_Script : MonoBehaviour
{
    private GameObject[] Audience;
    private bool Moving;

    // Start is called before the first frame update
    void Start()
    {

        Audience = gameObject.GetComponentsInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Moving)
        {
            StartCoroutine(Wait());
        }
        
        
    }

    IEnumerator Wait()
    {
        Moving = true;
        foreach (Transform person in transform)
        {
            person.localScale = new Vector3(1,Random.Range(1,3),1);

        }
        yield return new WaitForSeconds(1);
        Moving = false;

    }
}
