using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ballcontroller : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject ball;
    public int current_mode = -1;
    public GameObject end_result;
    public GameObject cube;
    private float idleTime = 0f;
    private bool isMoving = false;
    public TextMeshProUGUI score_text;

    public TextMeshProUGUI end_score;
    public TextMeshProUGUI end_best;
    public GameObject gameover;
    private int cum_score = 0;
    private int cum_time = 0;
    private int score = 0;
    bool isgameover = false;

    // 최고 점수를 저장할 키
    private string bestScoreKey = "BestScore";
    private string mod1best = "mod1bestkey";
    private string mod2best = "mod2bestkey";
    private string eatKey = "objecteat";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameover.SetActive(false);
        InvokeRepeating("time_scale", 1f, 1f);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Setting_room" || scene.name == "Main_room") current_mode = -1;
        else if (scene.name == "Game_playing") current_mode = 0;
        else if (scene.name == "mod1") current_mode = 1;
        else if (scene.name == "mod2") current_mode = 2;
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "Score : " + score;
        Transform objectTransform = GetComponent<Transform>();
        Vector3 currentPosition = objectTransform.position;

        if (currentPosition.y < -5)
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Setting_room" || scene.name == "Main_room")
            {
                objectTransform.position = new Vector3(0, 2, 0);
                rb.velocity = Vector3.zero;

            }
            else
            {
                ball.SetActive(false);
                isgameover = true;
                gameover.SetActive(true);
                end_result.SetActive(true);
                end_score.text = "Your Score \n" + score;

                if (current_mode == 0)
                {
                    int bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);

                    int cum_time_best = PlayerPrefs.GetInt("60 seconds!");
                    if (cum_time_best < cum_time)
                    {
                        PlayerPrefs.SetInt("60 seconds!", cum_time);
                    }
                    cum_score = score + PlayerPrefs.GetInt("차곡차곡");
                    cum_time = cum_time + PlayerPrefs.GetInt("1000초를 투자했습니다");

                    float aim123 = PlayerPrefs.GetFloat("aim_setting");
                    if (aim123 == 0.8f && score > PlayerPrefs.GetInt("느림의 미학")) PlayerPrefs.SetInt("느림의 미학", score);
                    else if (aim123 == 5.0f && score > PlayerPrefs.GetInt("빨리 감기")) PlayerPrefs.SetInt("빨리감기", score);

                    if (score == 777) PlayerPrefs.SetInt("Lucky $lot 777", score);
                    PlayerPrefs.SetInt("차곡차곡", cum_score);
                    PlayerPrefs.SetInt("1000초를 투자했습니다", cum_time);
                    PlayerPrefs.Save();
                    if (score > bestScore)
                    {
                        PlayerPrefs.SetInt(bestScoreKey, score);
                        PlayerPrefs.SetInt("전설의 시작", score);
                        PlayerPrefs.SetInt("Beyound the developer", score);



                        PlayerPrefs.Save();
                        end_best.text = "Best Score \n" + score;
                    }
                    else end_best.text = "Best Score \n" + bestScore;
                }
                else if (current_mode == 1)
                {
                    int bestScore = PlayerPrefs.GetInt(mod1best, 0);
                    if (score > bestScore)
                    {
                        PlayerPrefs.SetInt("적응의 신", score);
                        PlayerPrefs.SetInt(mod1best, score);
                        PlayerPrefs.Save();
                        end_best.text = "Best Score \n" + score;
                    }
                    else end_best.text = "Best Score \n" + bestScore;
                }
                else if (current_mode == 2)
                {
                    int bestScore = PlayerPrefs.GetInt(mod2best, 0);
                    if (score > bestScore)
                    {
                        PlayerPrefs.SetInt(mod2best, score);
                        PlayerPrefs.Save();
                        end_best.text = "Best Score \n" + score;
                    }
                    else end_best.text = "Best Score \n" + bestScore;
                }

            }

        }

        Vector3 currentVelocity = rb.velocity;
        if (!isMoving)
        {
            if (currentVelocity.magnitude < 0.1f) idleTime += Time.deltaTime;
            else idleTime = 0f;
        }
        if (idleTime > 1f && !isMoving)
        {
            rb.AddForce(new Vector3(0.1f, 0f, 0f), ForceMode.VelocityChange);
            isMoving = true;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.gameObject.CompareTag("score"))
        {
            this.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            Destroy(coll.collider.gameObject);
            score += 100;
            int iseat = 1;

            int eat = PlayerPrefs.GetInt(eatKey, 0);
            eat++;
            PlayerPrefs.SetInt(eatKey, eat);
            int get_gold = Random.Range(0, 4);
            if(get_gold == 0)
            {
                int fake_gold = 0;
                if (PlayerPrefs.HasKey("gold_data")) fake_gold = PlayerPrefs.GetInt("gold_data");
                fake_gold += 10;
                PlayerPrefs.SetInt("gold_data", fake_gold);
            }
            PlayerPrefs.SetInt("Hello, World!", iseat);
            PlayerPrefs.Save();
        }
    }


    public void regame()
    {
        Transform objectTransform = GetComponent<Transform>();
        ball.SetActive(true);
        score = 0;
        gameover.SetActive(false);
        isgameover = false;
        end_result.SetActive(false);
        objectTransform.position = new Vector3(0, 2, 0);
        rb.velocity = Vector3.zero;
        //Debug.Log("test");
        cube.transform.rotation = Quaternion.Euler(0, 0, 0);

        Transform ballTransform = ball.transform;
        ballTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    void time_scale()
    {
        this.transform.localScale += new Vector3(0.0001f, 0.0001f, 0.0001f);
        cum_time++;
        score += 1;
    }
}
