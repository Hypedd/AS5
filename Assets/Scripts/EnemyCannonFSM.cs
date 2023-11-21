using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCannonFSM : MonoBehaviour
{
    public enum EnemyState { Idle, AttackPlayer }
    public EnemyState currentState;
    public Sight sightSensor;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.Idle) { Idle(); }
        else { AttackPlayer(); }
    }

    void Idle()
    {
        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }
    void AttackPlayer()
    {
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.Idle;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
