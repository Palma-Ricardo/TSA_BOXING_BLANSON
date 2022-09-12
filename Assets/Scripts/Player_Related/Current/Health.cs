using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //I made this universal health script a while back notice the functions

    public float health;
    public float MaxHealth = 100;
    public bool isDead = false;



    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void Head(int damage)
    {
        health -= damage * 2;
    }
    public void Chest(int damage)
    {
        health -= damage;
    }
    public void Arm(int damage)
    {
        health *= (damage * .5f);
    }
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Game_Manager.TheWinner = gameObject.name;
            Game_Manager.DeathCount += 1;
            Game_Manager.Players.Remove(this.gameObject);
            Animator MyAnim = GetComponentInChildren<Animator>();
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z);
            MyAnim.SetTrigger("isDead");
            Time.timeScale = .25f;
        }
        
    }
}
