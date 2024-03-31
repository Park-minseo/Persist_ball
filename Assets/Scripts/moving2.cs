using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving2 : MonoBehaviour
{
    // Start is called before the first frame update

    public float aim;
    void Start()
    {
        Input.gyro.enabled = true;

        if (PlayerPrefs.HasKey("aim_setting"))
        {
            aim = PlayerPrefs.GetFloat("aim_setting");

        }
        else aim = 2.4f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);
    }
}
