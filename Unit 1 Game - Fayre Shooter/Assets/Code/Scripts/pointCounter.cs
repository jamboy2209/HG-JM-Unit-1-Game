using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

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
    void SetPointText()
    {
        pointText.text = "Score " + score.ToString();

        if (score >= 15)
        {
            winTextObject.SetActive (true);

            fanfare.PlayOneShot(fanfareSound, 0.75f);
        }
    }
}
