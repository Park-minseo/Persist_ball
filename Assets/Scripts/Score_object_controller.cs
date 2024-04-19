using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_object_controller : MonoBehaviour
{
    private float coolDown = 6.5f, coolDownCounter, DestroyCounter, DestroyCool = 15f; // 생성 쿨타임
    public GameObject scorePrefab; // 프리팹화된 보석
    public GameObject spawnPoint; // 생성 
    public GameObject ball;
    private int poolSize = 7; // 풀 크기
    private Queue<GameObject> pools = new Queue<GameObject>(); // 풀

    void Start()
    {
        coolDownCounter = coolDown;
        DestroyCounter = DestroyCool;

        // 풀에 오브젝트를 미리 생성해둡니다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(scorePrefab, Return_RandomPosition(), Quaternion.Euler(-90f, -90f, 30f));
            obj.transform.parent = spawnPoint.transform; // spawnPoint를 부모로 설정
            obj.SetActive(false); // 생성 후 비활성화
            pools.Enqueue(obj);
        }
    }

    void Update()
    {
        coolDownCounter -= Time.deltaTime;
        DestroyCounter -= Time.deltaTime;

        if (coolDownCounter < 0)
        {
            // 풀에서 오브젝트를 가져와서 활성화합니다.
            if (pools.Count > 0)
            {
                GameObject obj = pools.Dequeue();
                obj.SetActive(true);
                obj.transform.position = Return_RandomPosition();
                pools.Enqueue(obj);
                coolDownCounter = coolDown;
            }
        }

        if (DestroyCounter < 0)
        {
            // 풀에서 오브젝트를 가져와서 비활성화합니다.
            if (pools.Count > 0)
            {
                GameObject obj = pools.Dequeue();
                obj.SetActive(false);
                pools.Enqueue(obj);
                DestroyCounter = DestroyCool;
            }
        }


    }

    Vector3 Return_RandomPosition()
    {
        Vector3 ori = spawnPoint.transform.position;
        float range_X = Random.Range(-1 * 1.8f, 2.2f);
        float range_Z = Random.Range(-2.2f, 2.3f);
        return new Vector3(range_X, ori.y + 0.2f, range_Z);
    }
}
