using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    [Header("Collision Settings")]
    [Tooltip("Choose The Opposing Player Layer")]
    public LayerMask collisionLayer;
    [Tooltip("Controls Size/Damage")]
    public float radius = 1f;
    public int damage = 10;

    public GameObject hitFXPrefab;

    void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);

        if (hit.Length > 0)
        {
            Vector3 hitFXPos = hit[0].transform.position;
            hitFXPos.y += 1.3f;

            Instantiate(hitFXPrefab, hitFXPos, Quaternion.identity);

            // this is where you should apply damage
            hit[0].GetComponent<Health>().Chest(damage);


            gameObject.SetActive(false);
        }
    }
}
