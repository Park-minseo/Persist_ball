using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gold_manager : MonoBehaviour
{

    private int gold = 0;
    public TextMeshProUGUI gold_text;
  
    // Start is called before the first frame update
    void Start()
    {
        gold = PlayerPrefs.GetInt("gold_data");
        gold_text.text = "" + gold;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gold_update()
    {
        gold =PlayerPrefs.GetInt("gold_data", gold);
        gold_text.text = ""+gold;
    }
}
