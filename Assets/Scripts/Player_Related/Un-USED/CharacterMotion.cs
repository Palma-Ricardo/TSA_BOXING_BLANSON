using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotion : MonoBehaviour
{
    public Animation anim;
    public bool Moving;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Moving = true;
            anim.Play("StepLeft");
            StartCoroutine(ReturnToIdle(.5f));
            transform.Translate(new Vector3(-.5f, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Moving = true;
            anim.Play("StepRight");
            StartCoroutine(ReturnToIdle(.5f));
        }

        if(Moving == false)
        {
            anim.Play("Idle");
        }
    }

    IEnumerator ReturnToIdle(float time)
    {

        yield return new WaitForSeconds(time);
        anim.Play("Idle");
        Moving = false;


    }
}
