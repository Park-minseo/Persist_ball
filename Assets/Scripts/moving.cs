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

        if (PlayerPrefs.HasKey("aim_setting"))
        {
            aim = PlayerPrefs.GetFloat("aim_setting");
            aimSlider.value = PlayerPrefs.GetFloat("aim_setting");
        }
    }
    // Move object using accelerometer

    void Update()
    {
        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);
        aim = aimSlider.value;

    }
    public void SaveAim()
    {
        PlayerPrefs.SetFloat("aim_setting", aim);
        PlayerPrefs.Save();
    }
    public void ResetAim()
    {
        aim = 2.4f;
        aimSlider.value = 2.4f;
        PlayerPrefs.SetFloat("aim_setting", aim);
        PlayerPrefs.Save();
    }

}