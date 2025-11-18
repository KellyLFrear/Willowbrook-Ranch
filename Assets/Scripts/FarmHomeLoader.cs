using UnityEngine;

public class FarmHomeLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FarmHomeTag") // If The Player Walks Into The "FarmHome" Tag
        {
            Debug.Log("This Would Make The House Appear!"); // Temporary Debug Log Message
            Debug.Log("This Would Make The House Appear!"); // Temporary Debug Log Message
            Debug.Log("This Would Make The House Appear!"); // Temporary Debug Log Message
        }
    }
}
