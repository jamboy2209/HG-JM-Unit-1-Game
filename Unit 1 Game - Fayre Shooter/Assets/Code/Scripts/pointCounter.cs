using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class pointCounter : MonoBehaviour
{
    //=======================
    //Variable Initialisation
    //=======================

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
        //resets the score on the screen
        score = 0;

        SetPointText();

        //disables the win message
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //detects when an object enters the Duck Detector
    private void OnTriggerEnter(Collider other)
    {
        //checks if the object is a duck
        if (other.gameObject.CompareTag("Physics Object"))
        {
            //deactivates the duck
            other.gameObject.SetActive(false) ;    

            //increments the score
            score++;

            //choose a random quack sound clip to play
            int quackIndex = Random.Range(0, 3);

            //play the sound
            quack.PlayOneShot(quackSound[quackIndex], 0.5f);
            
            //Debug.Log(quackSound[quackIndex]);
            //Debug.Log(score);

            //update the score display
            SetPointText();
        }
    }

    //displays the score on the screen
    private void SetPointText()
    {
        //creates a string of the score
        pointText.text = "Score " + score.ToString();

        //determines the score required to win each level
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

        //checks if you have the winscore
        if (score >= winScore)
        {
            //activates win display
            winTextObject.SetActive(true);

            //plays fanfare sound
            fanfare.PlayOneShot(fanfareSound, 0.75f);
        }
    }
}
