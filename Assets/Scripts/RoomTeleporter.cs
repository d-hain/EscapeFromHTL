using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleporter : MonoBehaviour {
    #region Singleton

    public static RoomTeleporter Instance => _instance;
    private static RoomTeleporter _instance;

    private void Awake() {
        if(_instance != null) {
            Debug.LogWarning("More than one instance of RoomTeleporter found!");
        }

        _instance = this;
    }

    #endregion

    public GameObject player;

    private SceneAsset _roomsScene;
    private AsyncOperation _sceneAsync;
    
    private void Start() {
        _roomsScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Rooms.unity");
        Door.PlayerInteract += FindRoomCoordinates;
    }

    /// <summary>
    /// Gets called when the player presses the interact button and is in Range with a door. <br/>
    /// Finds the room with the given name. <br/>
    /// If a room was found, the rooms scene gets loaded and the player is teleported to the room.
    /// </summary>
    /// <param name="roomName">The name of the Room the Player should get teleported to.</param>
    private void FindRoomCoordinates(string roomName) {
        switch(roomName) {
            case "A004": //TODO: add the back rooms and more rooms
                LoadScene();
                TeleportToRoom(2, 11, new Vector2(6.6f, 18f), new Vector2(6f, 10.6f));
                break;

            default:
                Debug.LogError("Room does not exist!");
                break;
        }
    }

    /// <summary>
    /// Load the Rooms scene and do not destroy the <see cref="player"/> and the <see cref="Camera.main">Main Camera</see>.
    /// </summary>
    private void LoadScene() {
        SceneManager.LoadSceneAsync(_roomsScene.name);
        Scene scene = SceneManager.GetSceneByName(_roomsScene.name);
        if(scene.IsValid()) {
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(Camera.main);
        }
    }

    /// <summary>
    /// Teleports the <see cref="player"/> to the given coordinates. <br/>
    /// Sets the <see cref="Camera.main">Main Cameras</see> <see cref="CameraController.maxPosition"/>
    /// </summary>
    /// <param name="posX">Position of the room on the x-Axis</param>
    /// <param name="posY">Position of the room on the y-Axis</param>
    /// <param name="camMaxPos">Maximum position of the <see cref="Camera.main">Main Camera</see></param>
    /// <param name="camMinPos">Minimum position of the <see cref="Camera.main">Main Camera</see></param>
    private void TeleportToRoom(int posX, int posY, Vector2 camMaxPos, Vector2 camMinPos) {
        CameraController.instance.maxPosition = camMaxPos;
        CameraController.instance.minPosition = camMinPos;
        player.transform.position = new Vector2(posX, posY);
    }
}