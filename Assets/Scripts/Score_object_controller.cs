using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Score_object_controller : MonoBehaviour
{
    private float coolDown = 6.5f, coolDownCounter, DestroyCounter, DestroyCool = 8f; // ���� ��Ÿ��
    public GameObject scorePrefab; // ������ȭ�� ����
    public GameObject spawnPoint; // ���� 
    public GameObject ball;
    private bool isempty = true;
  
    private Queue<GameObject> pools = new Queue<GameObject>(); // Ǯ

    void Start()
    {
        coolDownCounter = coolDown;
        DestroyCounter = DestroyCool;
        

    }

    void Update()
    {
        coolDownCounter -= Time.deltaTime;
        if(!isempty) DestroyCounter -= Time.deltaTime;

        if (coolDownCounter < 0)
        {
            // Ǯ���� ������Ʈ�� �����ͼ� Ȱ��ȭ�մϴ�.

            if (pools.Count <= 8)
            {
                GameObject obj = Instantiate(scorePrefab, Return_RandomPosition(), Quaternion.Euler(-90f, -90f, -30f)); ;


                transform.rotation = Quaternion.Euler(-90f, -90f, -30f);

                obj.SetActive(true);
                pools.Enqueue(obj);
                coolDownCounter = coolDown;
                isempty = false;
            }
            else coolDownCounter = coolDown;
        }

        if (DestroyCounter < 0)
        {
            // Ǯ���� ������Ʈ�� �����ͼ� ��Ȱ��ȭ�մϴ�.
            if (pools.Count >= 0)
            {
                GameObject obj = pools.Peek();
                Debug.Log("delete complete");
                pools.Dequeue();
                Destroy(obj);
                DestroyCounter = DestroyCool;


                if (pools.Count == 0) isempty = true;
            }
            else DestroyCounter = DestroyCool;
        }


    }

    Vector3 Return_RandomPosition()
    {
        Vector3 ori = spawnPoint.transform.position;
        float range_X = Random.Range(-1 * 1.8f, 2.2f);
        float range_Z = Random.Range(-2.2f, 2.3f);
        return new Vector3(range_X, ori.y + 0.5f, range_Z);
    }

    public void reset()
    {

        while(pools.Count > 0)
        {
            GameObject obj = pools.Peek();
            Debug.Log("delete complete");
            pools.Dequeue();
            Destroy(obj);
            DestroyCounter = DestroyCool;
            isempty = true;
        }
    }
}