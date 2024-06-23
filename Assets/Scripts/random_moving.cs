using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class random_moving : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 300f;
    public float aim;
    private float coolDown = 0f;
    public TextMeshProUGUI aim_text;

    private float reset_time = 0f;
    private float aimreset_time = 0f;
    void Start()
    {
        Input.gyro.enabled = true;

        aim = Random.Range(0.8f, 5.0f);
        aim = Mathf.Floor(aim * 10f) / 10f;
        aim_text.text = "현재 감도 : " + aim;
    }

    // Update is called once per frame
    void Update()
    {
        reset_time += Time.deltaTime;
        aimreset_time += Time.deltaTime;
        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);


        if(reset_time >=7f)
        {
            reset_time = 0f;
            Input.gyro.enabled = false;
            Input.gyro.enabled = true;
        }
        if(aimreset_time >= 3.8f)
        {
            aimreset_time = 0f;
            aim = Random.Range(0.8f, 5.0f);
            aim = Mathf.Floor(aim * 10f) / 10f;
            aim_text.text = "현재 감도 : " + aim;
        }
    
    }
}
