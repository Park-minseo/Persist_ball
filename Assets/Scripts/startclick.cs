using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class startclick : MonoBehaviour
{

    public ScrollRect achieve;

    public void goto_Game()
    {
        SceneManager.LoadScene("Game_playing");
    }
    public void goto_Main()
    {
        SceneManager.LoadScene("Main_room");
    }

    public void goto_Setting()
    {
        SceneManager.LoadScene("Setting_room");
    }


    public void goto_achi()
    {
        if (achieve != null)
        {
            achieve.gameObject.SetActive(!achieve.gameObject.activeSelf);
        }
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
