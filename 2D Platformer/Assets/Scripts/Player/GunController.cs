using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A class which controls the player shooting
/// </summary>

public class GunController : MonoBehaviour
{
    [Header("GameObject/Component References")]
    [Tooltip("The projectile to be fired")]
    public GameObject projectilePrefab = null;
    [Tooltip("The effect to play when firing")]
    public GameObject gunEffectPrefab = null;
    [Tooltip("The transform in the heirarchy which holds projectiles if any")]
    public Transform projectileHolder = null;
    public PlayerController playerController = null;
    public WalkingEnemy enemyController = null;
    public GameManager gameManager = null;

    [Header("Input")]
    [Tooltip("Whether this shooting controller is controled by the player")]
    public bool isPlayerControlled = false;

    [Header("Firing Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    public float fireRate = 0f;
    public int enemyFireGroup = 0;
    private int enemyTimesFired = 0;
    private bool enemyCanFire = true;

    // The last time this component was fired
    private float lastFired = Mathf.NegativeInfinity;

    //The input manager which manages player input
    private InputManager inputManager = null;

    /// <summary>
    /// Description:
    /// Standard unity function that runs every frame
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Update()
    {
        ProcessInput();
    }

    /// <summary>
    /// Description:
    /// Standard unity function that runs when the script starts
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Start()
    {
        SetupInput();
    }

    /// <summary>
    /// Description:
    /// Attempts to set up input if this script is player controlled and input is not already correctly set up 
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    void SetupInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager == null)
            {
                inputManager = InputManager.instance;
            }
            if (inputManager == null)
            {
                Debug.LogError("Player Shooting Controller can not find an InputManager in the scene, there needs to be one in the " +
                    "scene for it to run");
            }
        }
    }

    /// <summary>
    /// Description:
    /// Reads input from the input manager
    /// Inputs:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    void ProcessInput()
    {
        if (isPlayerControlled)
        {
            if (inputManager.firePressed)
            {
                Fire();
            }
        }
        else
        {
            EnemyFire();
        }
    }

    /// <summary>
    /// Description:
    /// Fires a projectile if possible
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void Fire()
    {
        // If the cooldown is over fire a projectile
        if ((Time.timeSinceLevelLoad - lastFired) > fireRate)
        {
            // Launches a projectile
            SpawnProjectile();
            enemyTimesFired += 1;
            if (enemyTimesFired == enemyFireGroup && !isPlayerControlled)
            {
                enemyCanFire = false;
            }

            // Restart the cooldown
            lastFired = Time.timeSinceLevelLoad;
        }
    }

    /// <summary>
    /// Description:
    /// Spawns a projectile and sets it up
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void SpawnProjectile()
    {
        // Check that the prefab is valid
        if (projectilePrefab != null)
        {
            SpawnFireEffect();
            // Create the projectile
            GameObject projectileGameObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity, null);

            if (isPlayerControlled)
            {
                if (playerController.facing == PlayerController.PlayerDirection.Right)
                {
                    projectileGameObject.transform.right = transform.right.normalized;
                }
                else if (playerController.facing == PlayerController.PlayerDirection.Left)
                {
                    projectileGameObject.transform.right = -transform.right.normalized;
                }
            }
            else if (!isPlayerControlled)
            {
                if (enemyController.GetComponent<SpriteRenderer>().flipX)
                {
                    projectileGameObject.transform.right = transform.right.normalized;
                }
                else if (!enemyController.GetComponent<SpriteRenderer>().flipX)
                {
                    projectileGameObject.transform.right = -transform.right.normalized;
                }
            }


            // Keep the heirarchy organized
            if (projectileHolder != null)
            {
                projectileGameObject.transform.SetParent(projectileHolder);
            }
            StartCoroutine("BulletVanish", projectileGameObject);
        }
    }

    private IEnumerator BulletVanish(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }

    private void SpawnFireEffect()
    {
        if (gunEffectPrefab != null)
        {
            Instantiate(gunEffectPrefab, transform.position, transform.rotation, null);
        }
    }

    private void EnemyFire()
    {
        if (enemyCanFire)
        {
            Fire();

            if (enemyTimesFired == enemyFireGroup)
            {
                StartCoroutine("PauseFire");
            }
        }

    }

    private IEnumerator PauseFire()
    {
        yield return new WaitForSeconds(2);
        enemyCanFire = true;
        enemyTimesFired = 0;
    }


}
