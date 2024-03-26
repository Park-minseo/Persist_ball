using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moving : MonoBehaviour
{
    public Slider aimSlider;
    public float aim = 2.4f;
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        aimSlider = GameObject.FindWithTag("AimSlider").GetComponent<Slider>();

        if (PlayerPrefs.HasKey("aim"))
        {
            aim = PlayerPrefs.GetFloat("aim");
            aimSlider.value = aim; // Slider 값도 갱신
        }
    }
    // Move object using accelerometer

    void Update()
    {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);
        aim = aimSlider.value;
    
    }

    
}