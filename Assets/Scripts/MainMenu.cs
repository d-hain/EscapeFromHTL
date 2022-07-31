using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    /**
     * Play the game
     * (Load the GroundLevel Scene)
     */
    public void PlayGame() {
        SceneManager.LoadScene("GroundLevel");
    }

    /**
     * Quit the Application appropriately
     */
    public void QuitGame() {
        Application.Quit();
    }
}