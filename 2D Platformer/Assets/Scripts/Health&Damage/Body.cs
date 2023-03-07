using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles behavior for getting hit with a bullet in the body
/// </summary>
public class Body : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The health component associated with this body")]
    public Health associatedHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Damage>() != null && associatedHealth != null)
        {
            if (collision.gameObject.GetComponent<Damage>().teamId != associatedHealth.teamId)
            {
                if (gameObject.tag == "Flying")
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    associatedHealth.TakeDamage(collision.GetComponent<Damage>().damageAmount);
                    if (collision.gameObject.tag == "Bullet")
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }
        }

    }
}