using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [System.Serializable]
    public class Achievement
    {
        public string name;
        public string description;
        public bool isUnlocked;
        public int currentProgress;
        public int maxProgress;
        public GameObject uiInstance; // uiInstance�� ���
    }


    public List<Achievement> achievements;
    public Transform scrollViewContent;
    public GameObject existingAchievementUI;

    public GameObject achieve2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadProgress();

        // 'Hello, World!' �������� �߰�
        if (!achievements.Exists(a => a.name == "Hello, World!"))
        {
            achievements.Add(new Achievement
            {
                name = "Hello, World!",
                description = "������ 1�� �̻� ȹ���ϼ���",
                isUnlocked = false,
                currentProgress = 3,
                maxProgress = 5,
                uiInstance = existingAchievementUI // ���� ������Ʈ�� uiInstance�� ����
            });
        }

            achievements.Add(new Achievement
            {
                name = "������ ����",
                description = "�� ���ӿ��� �ְ��� 2000����\n ����ϼ���",
                isUnlocked = false,
                currentProgress = 1500,
                maxProgress = 2000,
                uiInstance = achieve2 // ���� ������Ʈ�� uiInstance�� ����
            });
       

        foreach (var achievement in achievements)
        {
            UpdateAchievementUI(achievement);
        }
    }

    public void AddProgress(string achievementName, int amount)
    {
        var achievement = achievements.Find(a => a.name == achievementName);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.currentProgress += amount;
            if (achievement.currentProgress >= achievement.maxProgress)
            {
                achievement.currentProgress = achievement.maxProgress;
                achievement.isUnlocked = true;
                // TODO: Add unlocked logic (e.g., show notification)
            }
            SaveProgress();
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
                achievement.isUnlocked = achievement.currentProgress >= achievement.maxProgress;
            }
        }
    }
}
