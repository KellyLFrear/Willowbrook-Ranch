using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Transform player; // The Player Transform
    public Vector3 teleportCoordinates; // Teleport Coordinates
    public Vector3 teleportRotationEuler; // Teleport Rotation in Euler Angles

    public void LadScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SkipToNextDay(string name)
    {
        LightingManager.Instance.SleepToNextDay();
        TeleportPlayerUsingCoordinates();
    }

    public void PassedOut()
    {
        LightingManager.Instance.AdvanceToNextDay(true);
        TeleportPlayerUsingCoordinates();
        GUIUpdater.Instance.HidePassedOutPopUp();
    }

    public void PrintTempMessage()
    {
        Debug.Log("This is a temporary message for testing purposes.");
    }

    public void TeleportPlayerUsingCoordinates()
    {
        if (player != null)
        {
            player.position = teleportCoordinates;
            player.rotation = Quaternion.Euler(teleportRotationEuler);

            Debug.Log($"Player teleported to {teleportCoordinates}");
        }
    }
}
