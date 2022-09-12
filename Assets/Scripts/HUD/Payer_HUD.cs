using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Payer_HUD : MonoBehaviour
{
    [Header("UI Elements")]
    public Image health;
    public Image stamina;
    public string name;
    public Health hp;
    public CombatController cc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = (hp.health / hp.MaxHealth);
        stamina.fillAmount = (cc.Stamina / cc.MaxStamina);
    }
}
