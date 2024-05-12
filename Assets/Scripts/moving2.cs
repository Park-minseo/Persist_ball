using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 300f;
    public float aim;

    private float reset_time = 0f;
    void Start()
    {
        Input.gyro.enabled = true;

        if (PlayerPrefs.HasKey("aim_setting")) aim = PlayerPrefs.GetFloat("aim_setting");
        else aim = 2.4f;
    }

    // Update is called once per frame
    void Update()
    {
        reset_time += Time.deltaTime;

        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);


        if(reset_time >=7f)
        {
            reset_time = 0f;
            Input.gyro.enabled = false;
            Input.gyro.enabled = true;
        }

    
    }
}
