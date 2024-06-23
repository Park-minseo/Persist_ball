using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Profile_manager : MonoBehaviour
{

    public TextMeshProUGUI eat_score;
    public TextMeshProUGUI mode1;
    public TextMeshProUGUI mode2;
    public TextMeshProUGUI mode3;
    private string bestScoreKey = "BestScore";
    private string mod1best = "mod1bestkey";
    private string mod2best = "mod2bestkey";
    private string eatKey = "objecteat";
    // Start is called before the first frame update
    void Start()
    {
        int mode1best = PlayerPrefs.GetInt(bestScoreKey, 0);
        int mode2best = PlayerPrefs.GetInt(mod1best, 0);
        int mode3best = PlayerPrefs.GetInt(mod2best, 0);
        int eat = PlayerPrefs.GetInt(eatKey, 0);

        mode1.text = "클래식 모드 최고기록 : " + mode1best;
        mode2.text = "랜덤주기 모드 최고기록 : " + mode2best;
        mode3.text = "더볼 모드 최고기록 : " + mode3best;
        eat_score.text = "지금까지 획득한 보석의 개수\n" + eat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
