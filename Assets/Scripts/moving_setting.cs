using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class moving_setting : MonoBehaviour
{
    public TextMeshProUGUI yourText;
    public Slider aimSlider;
    public float rotationSpeed = 300f;
    public float aim = 2.4f;
    public float reset_time = 0f;
    public float fadeDuration = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        if(yourText != null) yourText.enabled = false;
        Input.gyro.enabled = true;
        aimSlider = GameObject.Find("moving slider").GetComponent<Slider>();

        if (PlayerPrefs.HasKey("aim_setting"))
        {
            aim = PlayerPrefs.GetFloat("aim_setting");
            aimSlider.value = PlayerPrefs.GetFloat("aim_setting");
        }
    }
    // Move object using accelerometer

    void Update()
    {
        reset_time += Time.deltaTime;

        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, Input.gyro.rotationRateUnbiased.y * -1 * aim);

        if (reset_time >= 7f)
        {
            reset_time = 0f;
            Input.gyro.enabled = false;
            Input.gyro.enabled = true;
        }
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
        transform.Rotate(rotationX, 0f, rotationZ);
    }
    public void SaveAim()
    {
        PlayerPrefs.SetFloat("aim_setting", aim);
        PlayerPrefs.Save();
        StartCoroutine(FadeTextInOut());
    }
    public void ResetAim()
    {
        aim = 2.4f;
        aimSlider.value = 2.4f;
        PlayerPrefs.SetFloat("aim_setting", aim);
        PlayerPrefs.Save();
        StartCoroutine(FadeTextInOut());
    }

    IEnumerator FadeTextInOut()
    {
        yourText.enabled = true;
        // �ؽ�Ʈ�� ���� ���� 0���� �����Ͽ� ���̵��� �ִϸ��̼��� �����մϴ�.
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, alpha);
            yield return null;
        }

        // �ִϸ��̼� �Ŀ��� ������ ���� ������ �����մϴ�.
        yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, 1f);

        // ���� �ð� ��� �� ���̵�ƿ� �ִϸ��̼��� �����մϴ�.
        yield return new WaitForSeconds(0.15f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, alpha);
            yield return null;
        }

        // �ִϸ��̼� �Ŀ��� �ؽ�Ʈ�� ������ ������ �ʵ��� �����մϴ�.
        yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, 0f);
        yourText.enabled = false;
    }

}