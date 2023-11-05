using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectsLayers;
    public LayerMask obstaclesLayers;
    public Collider detectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders=Physics.OverlapSphere(transform.position, distance, (int)objectsLayers);

        detectedObject=null;
        for(int i=0; i<colliders.Length; i++)
        {
            Collider collider=colliders[i];

            Vector3 directionToController=Vector3.Normalize(collider.bounds.center - transform.position);

            //AI의 정면과 해당 directionToController를 -해 각도계산
            float angleToCollider=Vector3.Angle(transform.forward, directionToController);

            if(angleToCollider<angle)
            {
                if(!Physics.Linecast(transform.position, collider.bounds.center, (int)obstaclesLayers))
                {
                    detectedObject=collider;
                    break;
                }
            }
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);

        Vector3 rightDirection=Quaternion.Euler(0, angle, 0)*transform.forward;
        Gizmos.DrawRay(transform.position, rightDirection*distance);

        Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDirection * distance);

    }
}
