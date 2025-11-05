using Unity.VisualScripting;
using UnityEngine;

public class DuckPlatformControls : MonoBehaviour
{
    public GameObject platform;
    public GameObject duck;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if (duck.gameObject.)
    }

    private void OnTriggerEnter(Collision collision)
    {
        collision.transform.SetParent(platform.transform);
    }

    private void OnTriggerExit(Collision collision)
    {
        collision.transform.SetParent(null);
        //what
    }
}
