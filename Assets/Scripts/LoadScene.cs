using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LadScenebyName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void PrintTempMessage()
    {
        Debug.Log("This is a temporary message for testing purposes.");
    }
}
