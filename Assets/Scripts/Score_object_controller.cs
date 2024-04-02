using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_object_controller : MonoBehaviour
{
    BoxCollider range2Collider;
    public GameObject bulletPrefab; // ������ȭ�� źȯ
    public GameObject spawnPoint; // ���� ����
    private int poolSize = 7; // Ǯ ũ��
    private float coolDown = 6.5f, coolDownCounter, DestroyCounter, DestroyCool = 15f; // ���� ��Ÿ��
    private List<GameObject> pools = new List<GameObject>(); // Ǯ
    void Start()
    {
        range2Collider = spawnPoint.GetComponent<BoxCollider>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            pools.Add(bullet);
        } // Ǯ �ʱ�ȭ

        coolDownCounter = coolDown;
        DestroyCounter = DestroyCool; // DestroyCounter �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        coolDownCounter -= Time.deltaTime;
        DestroyCounter -= Time.deltaTime; // DestroyCounter ����

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
                    pools[i].SetActive(true); // ���� ����
                    break;
                }
            }
            coolDownCounter = coolDown;
        }

        if (DestroyCounter < 0) // ����: DestoryCounter�� DestroyCounter�� ����
        {
            for (int i = 0; i < poolSize; i++) // ����: i = poolSize - 1�� �����Ͽ� ��ü Ǯ�� Ȯ��
            {
                if (pools[i].activeInHierarchy) // ����: activeInHierarchy�� true���� Ȯ���Ͽ� ������ Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
                {
                    pools[i].SetActive(false); // ���� �ı�
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
