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

        // ������Ʈ�� ���� ��ġ�� �����ɴϴ�.
        Vector3 currentPosition = objectTransform.position;

        // ���� Y ��ǥ�� -10 �̸��̸�
        if (currentPosition.y < -5)
        {
            // ������Ʈ�� (0, 2, 0)���� �̵��մϴ�.
            objectTransform.position = new Vector3(0, 2, 0);
            rb.velocity = Vector3.zero;
        }
    }
}
