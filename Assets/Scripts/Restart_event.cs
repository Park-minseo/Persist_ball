using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart_event : MonoBehaviour
{
   private void OnMouseDown()
    {
        GameObject.Find("Player Ball").GetComponent<ballcontroller>().regame();
        Debug.Log("touch");
    }
}
