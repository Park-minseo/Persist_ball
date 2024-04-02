using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moving : MonoBehaviour
{
    public Slider aimSlider;
    public float rotationSpeed = 300f;
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

        float rotationX = 0f;
        float rotationZ = 0f;

        if (Input.GetKey(KeyCode.W))
            rotationX += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            rotationX -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            rotationZ += rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            rotationZ -= rotationSpeed * Time.deltaTime;

        // 오브젝트를 회전합니다.
        transform.Rotate(rotationX, 0f, rotationZ);
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