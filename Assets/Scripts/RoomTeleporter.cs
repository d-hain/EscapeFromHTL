using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomTeleporter : MonoBehaviour {
    /// <summary>
    /// Name of the room in this format: "[A-Z][0-9]{3}"
    /// </summary>
    public string roomName;
    public GameObject player;

    private SceneAsset _roomsScene;
    private AsyncOperation _sceneAsync;

    private void Awake() {
        _roomsScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Rooms.unity");
    }

    /// <summary>
    /// Finds the room with the given name. <br/>
    /// If a room was found the rooms scene gets loaded and the player is teleported to the room.
    /// </summary>
    private void FindRoomCoordinates() {
        switch(roomName) {
            case "A004": //TODO: add the back rooms and more rooms
                LoadScene();
                TeleportToRoom(2, 11, new Vector2(6.6f, 18f), new Vector2(5.4f, 10f));
                break;

            default:
                Debug.LogError("Room does not exist");
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
    /// Sets the <see cref="Camera.main">Main Cameras</see> <see cref="CameraController.instance.maxPosition"/>
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

    private void OnTriggerStay2D(Collider2D other) {
        if(Input.GetKeyDown("Interact")) {
            if(other.gameObject.CompareTag("Player")) {
                FindRoomCoordinates();
            }
        }
    }
}