using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform player; // The Player Transform
    public Vector3 frontHouseTeleportCoordinates; // Teleport Coordinates
    public Vector3 frontHouseTeleportRotationEuler; // Teleport Rotation in Euler Angles
    public string farmSceneName = "Scene1-Farm";

    // FUNCTION TO LOAD SCENE BY NAME
    public void LadScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }

    // FUNCTION TO SKIP TO NEXT DAY
    public void SkipToNextDay(string name)
    {
        LightingManager.Instance.SleepToNextDay();
        TeleportPlayerToFrontOfHouse();
    }

    // FUNCTION FOR WHE PLAYER PASSES OUT
    public void PassedOut()
    {
        Debug.Log("Player has passed out. Loading farm scene and advancing to next day.");
        SceneManager.sceneLoaded += OnFarmLoadedAfterPassOut;
        SceneManager.LoadScene(farmSceneName);
        LightingManager.Instance.AdvanceToNextDay(true);
        TeleportPlayerToFrontOfHouse();
        GUIUpdater.Instance.HidePassedOutPopUp();
    }

    // TEMP FUNCTION FOR TESTING
    public void PrintTempMessage()
    {
        Debug.Log("This is a temporary message for testing purposes.");
    }

    // CALLBACK WHEN FARM SCENE LOADS AFTER PASSING OUT
    private void OnFarmLoadedAfterPassOut(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == farmSceneName)
        {
            // Remove listener so it runs once
            SceneManager.sceneLoaded -= OnFarmLoadedAfterPassOut;

            // Advance the day AFTER scene loads
            LightingManager.Instance.AdvanceToNextDay(true);

            // Hide popup
            GUIUpdater.Instance.HidePassedOutPopUp();

            // Teleport player
            TeleportPlayerToFrontOfHouse();
        }
    }

    // FUNCTION TO TELEPORT PLAYER TO FRONT OF HOUSE
    public void TeleportPlayerToFrontOfHouse()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            Transform player = playerObj.transform;
            player.position = frontHouseTeleportCoordinates;
            player.rotation = Quaternion.Euler(frontHouseTeleportRotationEuler);
            Debug.Log($"Player teleported to {frontHouseTeleportCoordinates}");
        }
        else
        {
            Debug.LogWarning("Player not found when trying to teleport after pass out.");
        }
    }
}
