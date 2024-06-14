using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using static AchievementManager;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public int isUnlocked = 0;
        public int currentProgress;
        public int maxProgress;
        public GameObject uiInstance; // uiInstance만 사용
    }


    private int fake_gold = 0;
    public List<Achievement> achievements;
    public Transform scrollViewContent;
    public GameObject existingAchievementUI;
    public GameObject achieve2;
    public GameObject achieve3;
    public GameObject achieve4;
    public GameObject achieve5;
    public GameObject achieve6;
    public GameObject achieve7;
    public GameObject achieve8;
    public GameObject achieve9;
    public GameObject achieve10;
    public GameObject gold_script;
    private int isclear = 0;


    private void Start()
    {


        // 'Hello, World!' 도전과제 추가
        if (!achievements.Exists(a => a.name == "Hello, World!"))
        {
            achievements.Add(new Achievement
            {
                name = "Hello, World!",
                description = "보석을 1개 획득하기",
                isUnlocked = 0,
                currentProgress = 0,
                maxProgress = 1,
                uiInstance = existingAchievementUI // 기존 오브젝트를 uiInstance로 설정
            });
        }

            achievements.Add(new Achievement
            {
                name = "전설의 시작",
                description = "한 게임에서 최고기록 500점 이상 \n달성하기",
                isUnlocked = 0,
                currentProgress = 0,
                maxProgress = 500,
                uiInstance = achieve2 // 기존 오브젝트를 uiInstance로 설정
            });

        achievements.Add(new Achievement
        {
            name = "60 seconds!",
            description = "한 게임에서 죽지 않고 \n60초 이상 플레이하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 60,
            uiInstance = achieve3 // 기존 오브젝트를 uiInstance로 설정
        });
        
        achievements.Add(new Achievement
        {
            name = "느림의 미학",
            description = "감도를 최소(0.8)로 설정하고 \n888점 이상 획득하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 888,
            uiInstance = achieve4
        });

        achievements.Add(new Achievement
        {
            name = "빨리감기",
            description = "감도를 최대(5)로 설정하고 \n1555점 이상 획득하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 1555,
            uiInstance = achieve5 
        });

        achievements.Add(new Achievement
        {
            name = "Beyond the developer",
            description = "한 게임에서 최고기록 4072점 이상 \n달성하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 4071,
            uiInstance = achieve6
        });


        achievements.Add(new Achievement
        {
            name = "Lucky $lot 777",
            description = "한 게임에서 정확하게 777점 달성하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 777,
            uiInstance = achieve7
        });
        achievements.Add(new Achievement
        {
            name = "5000초를 투자했습니다",
            description = "누적 플레이 시간 \n5000초 이상 달성하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 5000,
            uiInstance = achieve8
        });
        achievements.Add(new Achievement
        {
            name = "차곡차곡",
            description = "누적 플레이 점수 \n50000점 이상 달성하기",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 50000,
            uiInstance = achieve9
        });


        LoadProgress();
        SaveProgress();
        foreach (var achievement in achievements)
        {
            UpdateAchievementUI(achievement);
        }
    }

    private void UpdateAchievementUI(Achievement achievement)
    {
        if (achievement.uiInstance != null)
        {
            Transform totalProgressTransform = achievement.uiInstance.transform.Find("total_progress1");
            if (totalProgressTransform == null)
            {
                Debug.LogError("total_progress1 not found in the prefab");
                return;
            }

            Image progressBar = totalProgressTransform.Find("progress1").GetComponent<Image>();

            progressBar.fillAmount = (float)achievement.currentProgress / achievement.maxProgress;

            TextMeshProUGUI progressText = achievement.uiInstance.transform.Find("txt_progress").GetComponent<TextMeshProUGUI>(); 
            TextMeshProUGUI title = achievement.uiInstance.transform.Find("title1").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = achievement.uiInstance.transform.Find("condition1").GetComponent<TextMeshProUGUI>();
            progressText.text = $"{achievement.currentProgress}/{achievement.maxProgress}";

            title.text = achievement.name;
            description.text = achievement.description;
        }
        else
        {
            Debug.LogError("uiInstance is null for achievement: " + achievement.name);
        }
    }

    private void SaveProgress()
    {
        foreach (var achievement in achievements)
        {
            PlayerPrefs.SetInt(achievement.name, achievement.currentProgress);

            if(achievement.currentProgress >= achievement.maxProgress)
            {
                achievement.currentProgress = achievement.maxProgress;
                //PlayerPrefs.SetInt(achievement.name + "lock", achievement.isUnlocked);
            }
        }
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        foreach (var achievement in achievements)
        {
            if (PlayerPrefs.HasKey(achievement.name))
            {
                achievement.currentProgress = PlayerPrefs.GetInt(achievement.name);
                achievement.isUnlocked = PlayerPrefs.GetInt(achievement.name + "lock");
            }
        }
    }
    public void reward0()
    {
        isclear = PlayerPrefs.GetInt(achievements[0].name +"lock");
        if (achievements[0].currentProgress < achievements[0].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[0].currentProgress >= achievements[0].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[0].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 100;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward1()
    {
       isclear = PlayerPrefs.GetInt("전설의 시작lock");
        if (achievements[1].currentProgress < achievements[1].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if(isclear == 0 && achievements[1].currentProgress >= achievements[1].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[1].name+"lock", isclear);
            if (PlayerPrefs.HasKey("gold_data"))  fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 1000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
       else if(isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward2()
    {
        int n = 2;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 500;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward3()
    {
        int n = 3;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 888;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward4()
    {
        int n = 4;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 1555;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward5()
    {
        int n = 5;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 3535;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward6()
    {
        int n = 6;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 2777;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward7()
    {
        int n = 7;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 10000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
    public void reward8()
    {
        int n = 8;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("완료하지 못한 업적입니다.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 20000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("이미 보상을 받은 업적입니다.");

        isclear = 0;
    }
}

