using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_event : MonoBehaviour
{
    public Slider aimSlider;
    public float aim = 2.4f;

    // Start is called before the first frame update
    void Start()
    {
        aimSlider = GameObject.FindWithTag("AimSlider").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveAim()
    {
        PlayerPrefs.SetFloat("aim", aim);
        PlayerPrefs.Save();
    }
    public void ResetAim()
    {
        aim = 2.4f;
        aimSlider.value = 2.4f;
        PlayerPrefs.SetFloat("aim", aim);
        PlayerPrefs.Save();
    }
}
