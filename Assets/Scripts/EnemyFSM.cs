using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState{GoToBase, AttackBase, ChasePlayer, AttackPlayer}
    public EnemyState currentState;
    public Sight sightSensor;
    public Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    private NavMeshAgent agent;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;

    private void Awake() 
    {
        baseTransform=GameObject.Find("Base").transform;
        agent=GetComponentInParent<NavMeshAgent>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState==EnemyState.GoToBase){GoToBase();}
        else if(currentState==EnemyState.AttackBase){AttackBase();}
        else if (currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        else{AttackPlayer();}

    }

    void GoToBase() 
    {
        agent.isStopped = false;

        if (sightSensor.detectedObject!=null)
        {
            currentState=EnemyState.ChasePlayer;
        }

        float distanceToBase=Vector3.Distance(transform.position, baseTransform.position);

        if(distanceToBase<baseAttackDistance)
        {
            currentState=EnemyState.AttackBase;
        }

        agent.SetDestination(baseTransform.position);
    
    }
    void AttackBase() 
    { 
        agent.isStopped=true;
        LookTo(baseTransform.position);
        Shoot();

    }
    void ChasePlayer() 
    {
        agent.isStopped = false;

        if (sightSensor.detectedObject==null)
        {
            currentState=EnemyState.GoToBase;
            return;
        } 

        float distanceToPlayer=Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if(distanceToPlayer<=playerAttackDistance)
        {
            currentState=EnemyState.AttackPlayer;
        }

        agent.SetDestination(sightSensor.detectedObject.transform.position);

    }
    void AttackPlayer() 
    {
        agent.isStopped = true;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if(distanceToPlayer>playerAttackDistance*1.1f)
        {
            currentState=EnemyState.ChasePlayer;
        }

    }

    private void OnDrawGizmos() 
    {
        Gizmos.color=Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);    
    }

    void Shoot()
    {
        var timeSinceLastShoot=Time.time-lastShootTime;
        if(timeSinceLastShoot>fireRate)
        {
            lastShootTime=Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition=Vector3.Normalize(targetPosition-transform.parent.position);
        directionToPosition.y=0;
        transform.parent.forward=directionToPosition;
    }
}
