using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ballcontroller : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject ball;
    private float idleTime = 0f;
    private bool isMoving = false;
    public TextMeshProUGUI score_text;
    public GameObject gameover;
    private int score = 0;
    bool isgameover = false;
    private bool touchDetected = false;

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
            ball.SetActive(false);
            isgameover = true;
            gameover.SetActive(true);
        }

        if (isgameover && !touchDetected && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchDetected = true;
                ball.SetActive(true);
                score = 0;
                gameover.SetActive(false);
                isgameover = false;
                objectTransform.position = new Vector3(0, 2, 0);
                rb.velocity = Vector3.zero;

                // 터치가 인식되고 처리되었으므로 다시 초기화합니다.
                touchDetected = false;
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
            this.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
            Destroy(coll.collider.gameObject);
            score += 100;
        }
    }
}
