using UnityEngine;
using UnityEngine.SceneManagement;

public class MarketLoader : MonoBehaviour
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
        if (other.tag == "GeneralStoreTag") // If The Player Walks Into The "GeneralMarket" Tag
        {
            Debug.Log("This Would Make The Market Appear!"); // Temporary Debug Log Message
            Debug.Log("This Would Make The Market Appear!"); // Temporary Debug Log Message
            Debug.Log("This Would Make The Market Appear!"); // Temporary Debug Log Message
        }
    }
  }
