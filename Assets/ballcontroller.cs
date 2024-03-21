using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontroller : MonoBehaviour
{
    private Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform objectTransform = GetComponent<Transform>();

        // 오브젝트의 현재 위치를 가져옵니다.
        Vector3 currentPosition = objectTransform.position;

        // 만약 Y 좌표가 -10 미만이면
        if (currentPosition.y < -5)
        {
            // 오브젝트를 (0, 2, 0)으로 이동합니다.
            objectTransform.position = new Vector3(0, 2, 0);
            rb.velocity = Vector3.zero;
        }
    }
}
