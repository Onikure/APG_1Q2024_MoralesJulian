using UnityEngine;

public class Exit : MonoBehaviour
{
    // Function to quit the application
    public void ExitGame()
    {
        // Exit the application
        Application.Quit();

        // Note: This won't work in the Unity Editor. Add this line to test in the Editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
