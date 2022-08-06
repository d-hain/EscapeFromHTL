using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    /// <summary>
    /// Play the game
    /// (Load the GroundLevel Scene)
    /// </summary>
    public void PlayGame() {
        SceneManager.LoadScene("GroundLevel");
    }

    /// <summary>
    /// Quit the game using the function <see cref="Application.Quit()"/>
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}