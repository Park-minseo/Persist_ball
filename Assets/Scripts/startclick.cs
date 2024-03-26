using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startclick : MonoBehaviour
{

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
