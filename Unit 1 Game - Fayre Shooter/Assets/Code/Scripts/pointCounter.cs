using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class pointCounter : MonoBehaviour
{
    private int score;

    public TextMeshProUGUI pointText;
    public GameObject winTextObject;

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
        }
    }
}
