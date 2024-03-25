using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }
    // Move object using accelerometer

    void Update()
    {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x * 2.418f, 0, Input.gyro.rotationRateUnbiased.y * -2.418f);
    }
}