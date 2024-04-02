using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_object_controller : MonoBehaviour
{
    BoxCollider range2Collider;
    public GameObject bulletPrefab; // 프리팹화된 탄환
    public GameObject spawnPoint; // 생성 지점
    private int poolSize = 7; // 풀 크기
    private float coolDown = 6.5f, coolDownCounter, DestroyCounter, DestroyCool = 15f; // 생성 쿨타임
    private List<GameObject> pools = new List<GameObject>(); // 풀
    void Start()
    {
        range2Collider = spawnPoint.GetComponent<BoxCollider>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            pools.Add(bullet);
        } // 풀 초기화

        coolDownCounter = coolDown;
        DestroyCounter = DestroyCool; // DestroyCounter 초기화
    }

    // Update is called once per frame
    void Update()
    {
        coolDownCounter -= Time.deltaTime;
        DestroyCounter -= Time.deltaTime; // DestroyCounter 감소

        float x = range2Collider.transform.eulerAngles.x;
        float y = range2Collider.transform.eulerAngles.y;
        float z = range2Collider.transform.eulerAngles.z;
        
        for(int i = 0; i < poolSize; i++)
        {
            if (pools[i].activeInHierarchy)
            {
                pools[i].transform.position = new Vector3(pools[i].transform.position.x, range2Collider.transform.position.y + 0.35f, pools[i].transform.position.z);
                pools[i].transform.rotation = Quaternion.Euler(new Vector3(-90 + x, y, z)); }
        }
        if (coolDownCounter < 0)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (!pools[i].activeInHierarchy)
                {
                    pools[i].transform.position = Return_RandomPosition();
                    pools[i].transform.rotation = Quaternion.Euler(new Vector3(-90, y, z));
                    pools[i].SetActive(true); // 보석 생성
                    break;
                }
            }
            coolDownCounter = coolDown;
        }

        if (DestroyCounter < 0) // 수정: DestoryCounter를 DestroyCounter로 변경
        {
            for (int i = 0; i < poolSize; i++) // 수정: i = poolSize - 1을 제거하여 전체 풀을 확인
            {
                if (pools[i].activeInHierarchy) // 수정: activeInHierarchy가 true인지 확인하여 보석이 활성화되어 있는지 확인
                {
                    pools[i].SetActive(false); // 보석 파괴
                    break;
                }
            }
            DestroyCounter = DestroyCool;
        }
    }
    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = spawnPoint.transform.position;
        float range_X = Random.Range(-1 * 1.8f, 2.2f);
        float range_Z = Random.Range(-2.2f, 2.3f);
        Vector3 RandomPosition = new Vector3(range_X, 0.6f, range_Z);
        return RandomPosition;
    }
}
