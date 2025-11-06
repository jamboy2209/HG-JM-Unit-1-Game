using Unity.VisualScripting;
using UnityEngine;

public class DuckDetatcher : MonoBehaviour
{
    public GameObject Duck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collider platform)
    {
        if (platform.gameObject.CompareTag("Respawn"))
        { Duck.transform.parent = null; }
    }
}
