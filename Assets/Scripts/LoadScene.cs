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

    // FUNCTION TO SHOW CREDITS POPUP PAGE
    public void ShowCreditsPopUp()
    {
        GUIUpdater.Instance.ShowCreditsPopUp(); // Show the credits pop-up
    }

    // FUNCTION TO HIDE CREDITS POPUP PAGE
    public void HideCreditsPopUp()
    {
        GUIUpdater.Instance.HideCreditsPopUp(); // Hide the credits pop-up
    }

    // FUNCTION TO SHOW MARKET POPUP PAGE
    public void ShowMarketPopUp()
    {
        GUIUpdater.Instance.LoadGeneralStoreGUI(); // Show the market pop-up
    }

    // FUNCTION TO HIDE MARKET POPUP PAGE
    public void HideMarketPopUp()
    {
        GUIUpdater.Instance.HideGeneralStoreGUI(); // Hide the market pop-up
    }

    // FUNCTION TO SHOW FISH MARKET POPUP PAGE
    public void ShowFishMarketPopUp()
    {
        GUIUpdater.Instance.LoadFishMarketGUI(); // Show The Fish Market Pop-Up
    }

    // FUNCTION TO HIDE FISH MARKET POPUP PAGE
    public void HideFishMarketPopUp()
    {
        GUIUpdater.Instance.HideFishMarketGUI(); // Hides The Fish Market Pop-Up
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

    // FUNCTION TO QUIT GAME
    public void Quit()
    {
        Debug.Log("Quit button pressed!");

        Application.Quit();  // Quit the game (works in a built app)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stops Play Mode in Editor
#endif
    }

    // TEMPORTARY FUNCTION TO TEST
    public void TestFunction()
    {
        Debug.Log("Buy/Sold Button Was Pressed!");
    }
}
