using UnityEngine;
using UnityEngine.Rendering;

public class WaveMovement : MonoBehaviour
{
    //=======================
    //Variable Initialisation
    //=======================

    public GameObject wave;

    float position = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        wave.transform.localPosition = new Vector3(Mathf.Sin(Time.time), Mathf.Cos(Time.time)*0.25f+0.5f, 0.0f);
        //wave.transform.
    }
}
