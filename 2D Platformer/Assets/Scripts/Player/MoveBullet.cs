using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to make bulletss move
/// </summary>
public class MoveBullet : MonoBehaviour
{
    [Tooltip("The distance this projectile will move each second.")]
    public float projectileSpeed = 3.0f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once per frame
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void Update()
    {
        MoveProjectile();
    }

    /// <summary>
    /// Description:
    /// Move the projectile in the direction it is heading
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    private void MoveProjectile()
    {
        // move the transform
        rb.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null && gameObject.GetComponent<Damage>() != null)
        {
            if (collision.gameObject.GetComponent<Health>().teamId != gameObject.GetComponent<Damage>().teamId)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.tag == "Foreground")
        {
            Destroy(this.gameObject);
        }
    }
}
