using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_object : MonoBehaviour
{
    BoxCollider spawnCollider;
    public GameObject bulletPrefab; // 프리팹화된 공
    public GameObject spawnPoint; // 생성 지점
    // Start is called before the first frame update
    void Start()
    {
        spawnCollider = spawnPoint.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = spawnPoint.transform.position.x;
        float y = spawnPoint.transform.position.y;
        float z = spawnPoint.transform.position.z;
        bulletPrefab.transform.rotation = Quaternion.Euler(new Vector3(x,0,y));
    }
}
