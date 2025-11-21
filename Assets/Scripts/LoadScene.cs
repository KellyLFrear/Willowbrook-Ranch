using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform player; // The Player Transform
    public Vector3 frontHouseTeleportCoordinates; // Teleport Coordinates
    public Vector3 frontHouseTeleportRotationEuler; // Teleport Rotation in Euler Angles

    public void LadScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SkipToNextDay(string name)
    {
        LightingManager.Instance.SleepToNextDay();
        TeleportPlayerToFrontOfHouse();
    }

    public void PassedOut()
    {
        LightingManager.Instance.AdvanceToNextDay(true);
        TeleportPlayerToFrontOfHouse();
        GUIUpdater.Instance.HidePassedOutPopUp();
    }

    public void PrintTempMessage()
    {
        Debug.Log("This is a temporary message for testing purposes.");
    }

    public void TeleportPlayerToFrontOfHouse()
    {
        if (player != null)
        {
            player.position = frontHouseTeleportCoordinates;
            player.rotation = Quaternion.Euler(frontHouseTeleportRotationEuler);

            Debug.Log($"Player teleported to {frontHouseTeleportCoordinates}");
        }
    }
}
