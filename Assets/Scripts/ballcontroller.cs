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
    public GameObject end_result;
    public GameObject cube;
    private float idleTime = 0f;
    private bool isMoving = false;
    public TextMeshProUGUI score_text;

    public TextMeshProUGUI end_score;
    public TextMeshProUGUI end_best;
    public GameObject gameover;
    private int score = 0;
    bool isgameover = false;

    // 최고 점수를 저장할 키
    private string bestScoreKey = "BestScore";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameover.SetActive(false);
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
                int bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);
                if (score > bestScore)
                {
                    PlayerPrefs.SetInt(bestScoreKey, score);
                    PlayerPrefs.Save();
                    end_best.text = "Best Score \n" + score;
                }
                else
                {
                    end_best.text = "Best Score \n" + bestScore;
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
            this.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
            Destroy(coll.collider.gameObject);
            score += 100;
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
}
