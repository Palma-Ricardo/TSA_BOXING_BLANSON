using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     //We need this to only happen when we punch

        //Its running our trigger check.... but we have it disabled.... is disabling at runtime an issue? RESEARCH TIME
    private void OnTriggerEnter(Collider other)
    {

        if(other.name != this.transform.parent.name)
        {
            Debug.Log("Colliding With: " + other.name+""+ other.tag);

            if (other.tag == "Head")
            {
                other.gameObject.GetComponent<Health>().Head(5);
                Debug.Log("Colliding With: Head");
            }
            if (other.tag == "Body")
            {
                other.gameObject.GetComponent<Health>().Chest(5);
                Debug.Log("Droped To: "+other.gameObject.GetComponent<Health>().health+ " Health From The "+gameObject.name);
            }
            else
            {
                other.gameObject.GetComponent<Health>().Chest(5);
            }
        }
        
    }
}
