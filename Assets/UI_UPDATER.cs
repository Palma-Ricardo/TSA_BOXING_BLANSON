using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UPDATER : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject Fill1;
    public GameObject Fill2;
    public bool PFound = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!PFound)
        {
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in Players)
            {
                if (!P1 & player.layer == 11)
                {
                    P1 = player;
                }
                if (!P2 & player.layer == 12)
                {
                    P2 = player;
                }
            }
        }
        


        if(P1 && P2)
        {
            Fill1.GetComponent<Image>().fillAmount = P1.GetComponent<Health>().health / P1.GetComponent<Health>().MaxHealth;
            Fill2.GetComponent<Image>().fillAmount = P2.GetComponent<Health>().health / P2.GetComponent<Health>().MaxHealth;
        }
        
    }
}
