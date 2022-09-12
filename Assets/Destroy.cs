using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float TimeToDeath = 1f;
    public float scale = 1;


    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        Destroy(this.gameObject, TimeToDeath);

    }
}
