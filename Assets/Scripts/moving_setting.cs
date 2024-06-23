using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class moving_setting : MonoBehaviour
{
    public TextMeshProUGUI yourText;
    public Slider aimSlider;
    public TMP_InputField aimInputField;
    public float rotationSpeed = 300f;
    public float aim = 2.4f;
    public float reset_time = 0f;
    public float fadeDuration = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        if (yourText != null) yourText.enabled = false;
        Input.gyro.enabled = true;

        aimSlider = GameObject.Find("moving slider").GetComponent<Slider>();
        aimInputField = GameObject.Find("setting_input").GetComponent<TMP_InputField>();

        aimSlider.minValue = 0.8f;
        aimSlider.maxValue = 5f;

        if (PlayerPrefs.HasKey("aim_setting"))
        {
            aim = PlayerPrefs.GetFloat("aim_setting");
            aimSlider.value = aim;
            aimInputField.text = aim.ToString("F1");
        }
        else
        {
            aimSlider.value = aim;
            aimInputField.text = aim.ToString("F1");
        }

        aimSlider.onValueChanged.AddListener(delegate { SliderValueChanged(); });
        aimInputField.onEndEdit.AddListener(delegate { InputFieldValueChanged(); });
    }

    void Update()
    {
        reset_time += Time.deltaTime;

        transform.Rotate(Input.gyro.rotationRateUnbiased.x * aim, 0, -1 * Input.gyro.rotationRateUnbiased.y * aim);

        if (reset_time >= 7f)
        {
            reset_time = 0f;
            Input.gyro.enabled = false;
            Input.gyro.enabled = true;
        }
    }

    public void SliderValueChanged()
    {
        aim = aimSlider.value;
        aimInputField.text = aim.ToString("F1");
    }

    public void InputFieldValueChanged()
    {
        if (float.TryParse(aimInputField.text, out float newAim))
        {
            if (newAim < aimSlider.minValue || newAim > aimSlider.maxValue)
            {
                aim = 2.4f;
                aimInputField.text = aim.ToString("F1");
                aimSlider.value = aim;
            }
            else
            {
                aim = newAim;
                aimSlider.value = aim;
            }
        }
        else
        {
            aimInputField.text = aim.ToString("F1");
        }
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
        aimInputField.text = "2.4";
        PlayerPrefs.SetFloat("aim_setting", aim);
        PlayerPrefs.Save();
        StartCoroutine(FadeTextInOut());
    }

    IEnumerator FadeTextInOut()
    {
        yourText.enabled = true;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, alpha);
            yield return null;
        }

        yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, 1f);

        yield return new WaitForSeconds(0.15f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, alpha);
            yield return null;
        }

        yourText.color = new Color(yourText.color.r, yourText.color.g, yourText.color.b, 0f);
        yourText.enabled = false;
    }
}
