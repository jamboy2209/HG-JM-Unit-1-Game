using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class pointCounter : MonoBehaviour
{
    private int score;

    public TextMeshProUGUI pointText;
    public GameObject winTextObject;

    public AudioSource quack;
    public AudioClip[] quackSound;

    public AudioSource fanfare;
    public AudioClip fanfareSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;

        SetPointText();

        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Physics Object"))
        {
            other.gameObject.SetActive(false) ;    

            score++;

            int quackIndex = Random.Range(0, 3);

            quack.PlayOneShot(quackSound[quackIndex], 0.5f);
            Debug.Log(quackSound[quackIndex]);
            Debug.Log(score);

            SetPointText();
        }
    }
    private void SetPointText()
    {
        pointText.text = "Score " + score.ToString();

        int winScore;

        if (SceneManager.GetActiveScene().name == "Fairground Level 1")
        {
            winScore = 5;
        }
        else if (SceneManager.GetActiveScene().name == "Fairground Level 2")
        {
            winScore = 10;
        }
        else
        {
            winScore = 15;
        }

        if (score >= winScore)
        {
            winTextObject.SetActive(true);

            fanfare.PlayOneShot(fanfareSound, 0.75f);
        }
    }
}
