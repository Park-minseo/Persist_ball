using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class startclick : MonoBehaviour
{

    public ScrollRect achieve;
    public GameObject profile;
    public int isclear1 = 0, isclear2 = 0;

    public void goto_Game()
    {
        SceneManager.LoadScene("ModeSelect_room");
    }
    public void goto_Main()
    {
        SceneManager.LoadScene("Main_room");
    }

    public void goto_Setting()
    {
        SceneManager.LoadScene("Setting_room");
    }
    public void mode1()
    {
        SceneManager.LoadScene("Game_playing");
    }
    public void mode2()
    {
        isclear1 = PlayerPrefs.GetInt("느림의 미학lock");
        isclear2 = PlayerPrefs.GetInt("빨리감기lock");
        if(isclear1 == 1 && isclear2 == 1) SceneManager.LoadScene("mod1");
        else AndroidToast.I.ShowToastMessage("'느림의 미학', '빨리감기' 두 업적을 모두 클리어해주세요.");
    }
    public void mode3()
    {
        SceneManager.LoadScene("mod2");
    }




    public void goto_achi()
    {
        if (achieve != null)
        {
            achieve.gameObject.SetActive(!achieve.gameObject.activeSelf);
            profile.gameObject.SetActive(false);
        }
    }

    public void goto_profile()
    {
        if (profile != null)
        {
            achieve.gameObject.SetActive(false);
            profile.gameObject.SetActive(!profile.gameObject.activeSelf);
        }
    }

    public void goto_shop()
    {
        AndroidToast.I.ShowToastMessage("Coming Soon!");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
