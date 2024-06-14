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
        public GameObject uiInstance; // uiInstance�� ���
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


        // 'Hello, World!' �������� �߰�
        if (!achievements.Exists(a => a.name == "Hello, World!"))
        {
            achievements.Add(new Achievement
            {
                name = "Hello, World!",
                description = "������ 1�� ȹ���ϱ�",
                isUnlocked = 0,
                currentProgress = 0,
                maxProgress = 1,
                uiInstance = existingAchievementUI // ���� ������Ʈ�� uiInstance�� ����
            });
        }

            achievements.Add(new Achievement
            {
                name = "������ ����",
                description = "�� ���ӿ��� �ְ��� 500�� �̻� \n�޼��ϱ�",
                isUnlocked = 0,
                currentProgress = 0,
                maxProgress = 500,
                uiInstance = achieve2 // ���� ������Ʈ�� uiInstance�� ����
            });

        achievements.Add(new Achievement
        {
            name = "60 seconds!",
            description = "�� ���ӿ��� ���� �ʰ� \n60�� �̻� �÷����ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 60,
            uiInstance = achieve3 // ���� ������Ʈ�� uiInstance�� ����
        });
        
        achievements.Add(new Achievement
        {
            name = "������ ����",
            description = "������ �ּ�(0.8)�� �����ϰ� \n888�� �̻� ȹ���ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 888,
            uiInstance = achieve4
        });

        achievements.Add(new Achievement
        {
            name = "��������",
            description = "������ �ִ�(5)�� �����ϰ� \n1555�� �̻� ȹ���ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 1555,
            uiInstance = achieve5 
        });

        achievements.Add(new Achievement
        {
            name = "Beyond the developer",
            description = "�� ���ӿ��� �ְ��� 4072�� �̻� \n�޼��ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 4071,
            uiInstance = achieve6
        });


        achievements.Add(new Achievement
        {
            name = "Lucky $lot 777",
            description = "�� ���ӿ��� ��Ȯ�ϰ� 777�� �޼��ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 777,
            uiInstance = achieve7
        });
        achievements.Add(new Achievement
        {
            name = "5000�ʸ� �����߽��ϴ�",
            description = "���� �÷��� �ð� \n5000�� �̻� �޼��ϱ�",
            isUnlocked = 0,
            currentProgress = 0,
            maxProgress = 5000,
            uiInstance = achieve8
        });
        achievements.Add(new Achievement
        {
            name = "��������",
            description = "���� �÷��� ���� \n50000�� �̻� �޼��ϱ�",
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
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[0].currentProgress >= achievements[0].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[0].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 100;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward1()
    {
       isclear = PlayerPrefs.GetInt("������ ����lock");
        if (achievements[1].currentProgress < achievements[1].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if(isclear == 0 && achievements[1].currentProgress >= achievements[1].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[1].name+"lock", isclear);
            if (PlayerPrefs.HasKey("gold_data"))  fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 1000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
       else if(isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward2()
    {
        int n = 2;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 500;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward3()
    {
        int n = 3;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 888;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward4()
    {
        int n = 4;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 1555;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward5()
    {
        int n = 5;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 3535;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward6()
    {
        int n = 6;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 2777;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward7()
    {
        int n = 7;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 10000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
    public void reward8()
    {
        int n = 8;
        isclear = PlayerPrefs.GetInt(achievements[n].name + "lock");
        if (achievements[n].currentProgress < achievements[n].maxProgress)
            AndroidToast.I.ShowToastMessage("�Ϸ����� ���� �����Դϴ�.");

        else if (isclear == 0 && achievements[n].currentProgress >= achievements[n].maxProgress)
        {
            isclear = 1;
            PlayerPrefs.SetInt(achievements[n].name + "lock", isclear);
            if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
            fake_gold += 20000;
            PlayerPrefs.SetInt("gold_data", fake_gold);

            gold_script.GetComponent<gold_manager>().gold_update();
        }
        else if (isclear == 1) AndroidToast.I.ShowToastMessage("�̹� ������ ���� �����Դϴ�.");

        isclear = 0;
    }
}

