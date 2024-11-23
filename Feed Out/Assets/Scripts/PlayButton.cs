using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Function to load a scene by name
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay"); // Replace "Gameplay" with your scene name
    }
}
