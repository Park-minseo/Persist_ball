using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_object_controller : MonoBehaviour
{
    private float coolDown = 6.5f, coolDownCounter, DestroyCounter, DestroyCool = 15f; // ���� ��Ÿ��
    public GameObject scorePrefab; // ������ȭ�� ����
    public GameObject spawnPoint; // ���� 
    public GameObject ball;
    private int poolSize = 7; // Ǯ ũ��
    private Queue<GameObject> pools = new Queue<GameObject>(); // Ǯ

    void Start()
    {
        coolDownCounter = coolDown;
        DestroyCounter = DestroyCool;

        // Ǯ�� ������Ʈ�� �̸� �����صӴϴ�.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(scorePrefab, Return_RandomPosition(), Quaternion.Euler(-90f, -90f, 30f));
            obj.transform.parent = spawnPoint.transform; // spawnPoint�� �θ�� ����
            obj.SetActive(false); // ���� �� ��Ȱ��ȭ
            pools.Enqueue(obj);
        }
    }

    void Update()
    {
        coolDownCounter -= Time.deltaTime;
        DestroyCounter -= Time.deltaTime;

        if (coolDownCounter < 0)
        {
            // Ǯ���� ������Ʈ�� �����ͼ� Ȱ��ȭ�մϴ�.
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
            // Ǯ���� ������Ʈ�� �����ͼ� ��Ȱ��ȭ�մϴ�.
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
