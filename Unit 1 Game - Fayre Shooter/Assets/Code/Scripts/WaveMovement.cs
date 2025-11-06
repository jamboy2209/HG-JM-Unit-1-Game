using UnityEngine;
using UnityEngine.Rendering;

public class WaveMovement : MonoBehaviour
{
    //=======================
    //Variable Initialisation
    //=======================

    public GameObject wave;

    private Vector3 startPos;
    private float offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = wave.transform.position;
        offset = startPos.z*25f;
        Debug.Log(startPos);
    }

    // Update is called once per frame
    void Update()
    {
        wave.transform.position = startPos + new Vector3(Mathf.Sin((Time.time)+offset)*0.25f, Mathf.Cos(Time.time+offset)*0.125f, 0.0f);
        //wave.transform.
    }
}
