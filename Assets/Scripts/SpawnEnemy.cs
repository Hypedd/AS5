using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;
//랜덤 오류 방지하기 위해 기입

public class SpawnEnemy : MonoBehaviour
{
    public GameObject prefab;
    public float startTime;
    public float endTime;
    public float spawnRate;
    public float randomRange;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate);
        Invoke("CancelInvoke", endTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Instantiate(prefab, GetRandomPosition(), GetRandomRotation());
    }

    //randomRange범위 안에 무작위 위치 반환
    Vector3 GetRandomPosition()
    {
        float random_X = Random.Range(randomRange * -1, randomRange);
        float random_Z = Random.Range(randomRange * -1, randomRange);
        Vector3 RandomPosition=new Vector3(random_X, 0, random_Z);
        return transform.position+RandomPosition;
    }

    Quaternion GetRandomRotation()
    {
        float random_Y=Random.Range(0, 180);
        Quaternion RandomRotation=Quaternion.Euler(new Vector3(0,random_Y,0));
        return RandomRotation;
    }
}
