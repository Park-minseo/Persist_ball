using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ballcontroller2 : MonoBehaviour
{
    private Rigidbody rb;
    private float idleTime = 0f;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Transform objectTransform = GetComponent<Transform>();
        Vector3 currentPosition = objectTransform.position;
        if (currentPosition.y < -5)
        {
            objectTransform.position = new Vector3(0, 2, 0);
            rb.velocity = Vector3.zero;
        }
        Vector3 currentVelocity = rb.velocity;
        if (!isMoving)
        {
            if (currentVelocity.magnitude < 0.1f) idleTime += Time.deltaTime;
            else idleTime = 0f;
        }
        if (idleTime > 1f && !isMoving)
        {
            rb.AddForce(new Vector3(0.1f, 0f, 0f), ForceMode.VelocityChange);
            isMoving = true;
        }

    }
}