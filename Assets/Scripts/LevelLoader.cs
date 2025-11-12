using UnityEngine;
using UnityEngine.SceneManagement; // Handles Methods To Load Scenes

public class LevelLoader : MonoBehaviour
{
    void Start() {
    
    }

    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "TownToFarmExit")
        { // If The Player Hits The Object With The "TownToFarmExit" Tag
            SceneManager.LoadScene("Scene1-Farm"); // Loads The Farm Scene
        }

        if (other.tag == "TownToBeachExit")
        { // If The Player Hits The Object With The "TownToBeachExit" Tag
            SceneManager.LoadScene("Scene3-Beach"); // Loads The Beach Scene
        }
    }
}
