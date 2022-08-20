using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTeleporter : MonoBehaviour {
    #region Singleton

    public static RoomTeleporter Instance => _instance;
    private static RoomTeleporter _instance;

    private void Awake() {
        if(FindObjectOfType(typeof(RoomTeleporter)) == null) {
            GameObject newRoomTeleporter = new GameObject();
            newRoomTeleporter.name = "RoomTeleporter";
            newRoomTeleporter.AddComponent<RoomTeleporter>();
            newRoomTeleporter.GetComponent<RoomTeleporter>().player = GameObject.FindGameObjectWithTag("Player");
            newRoomTeleporter.GetComponent<RoomTeleporter>().mainCamera =
                GameObject.FindGameObjectWithTag("MainCamera");

            _instance = newRoomTeleporter.GetComponent<RoomTeleporter>();
        } else {
            _instance = (RoomTeleporter)FindObjectOfType(typeof(RoomTeleporter));
            _instance.player = GameObject.FindGameObjectWithTag("Player");
            _instance.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        DontDestroyOnLoad(_instance);
    }

    #endregion

    public GameObject player;
    public GameObject mainCamera;

    private string _roomsSceneName;
    private string _groundLevelSceneName;

    private void Start() {
        _roomsSceneName = "Rooms";
        _groundLevelSceneName = "GroundLevel";
        Door.PlayerInteract += FindRoomCoordinates;
    }

    /// <summary>
    /// Gets called when the player presses the interact button and is in Range with a door. <br/>
    /// Finds the room with the given name. <br/>
    /// If a room was found, either the rooms scene gets loaded and the player is teleported to the room or he gets teleported back to the previous Scene.
    /// </summary>
    /// <param name="roomName">The name of the Room the Player should get teleported to.</param>
    /// <param name="toRoom">
    /// true...you want to go to the Room specified. <br/>
    /// false...you want to go out of the Room specified.
    /// </param>
    private void FindRoomCoordinates(string roomName, bool toRoom) {
        switch(roomName) {
            case "A004":
                if(toRoom) {
                    LoadScene(_roomsSceneName);
                    TeleportToRoom(2f, 11f, new Vector2(6.6f, 18f), new Vector2(6f, 10.6f));
                } else {
                    LoadScene(_groundLevelSceneName);
                    TeleportOutOfRoom(0, 0);
                }

                break;

            default:
                Debug.LogError("Room does not exist!");
                break;
        }
    }

    /// <summary>
    /// When loading the <see cref="_roomsSceneName">Rooms Scene</see> do not destroy the <see cref="player"/> and the <see cref="mainCamera">Main Camera</see>. <br/>
    /// When loading the <see cref="_groundLevelSceneName">GroundLevel Scene</see> destroy the <see cref="player"/> and the <see cref="mainCamera">Main Camera</see> that are in the DontDestroyOnLoad-List.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    private void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneAsync(sceneName)); //TODO: TELEPORT PLAYER AOIDgbwaouzhxuliwkvb dh
        Scene scene = SceneManager.GetSceneByName(sceneName);

        // Don't destroy the player and the main camera when teleporting to the Rooms scene.
        if(sceneName.Equals(_roomsSceneName)) {
            if(scene.IsValid()) {
                DontDestroyOnLoad(player);
                DontDestroyOnLoad(mainCamera);
            }
        }

        // Destroy the player and the main camera when teleporting to the GroundLevel scene.
        if(sceneName.Equals(_groundLevelSceneName)) {
            if(scene.IsValid()) {
                Destroy(player);
                Destroy(mainCamera);
            }
        }
    }

    /// <summary>
    /// Loads the Scene asynchronously
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <returns></returns>
    private IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while(!asyncLoad.isDone) {
            yield return null;
        }
    }

    /// <summary>
    /// Teleports the <see cref="player"/> to the given coordinates. <br/>
    /// </summary>
    /// <param name="posX">Position of the room on the x-Axis</param>
    /// <param name="posY">Position of the room on the y-Axis</param>
    private void TeleportOutOfRoom(float posX, float posY) {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector2(posX, posY);
    }

    /// <summary>
    /// Teleports the <see cref="player"/> to the given coordinates. <br/>
    /// Sets the <see cref="Camera.main">Main Cameras</see> <see cref="CameraController.maxPosition"/>
    /// </summary>
    /// <param name="posX">Position of the room on the x-Axis</param>
    /// <param name="posY">Position of the room on the y-Axis</param>
    /// <param name="camMaxPos">Maximum position of the <see cref="Camera.main">Main Camera</see></param>
    /// <param name="camMinPos">Minimum position of the <see cref="Camera.main">Main Camera</see></param>
    private void TeleportToRoom(float posX, float posY, Vector2 camMaxPos, Vector2 camMinPos) {
        CameraController.instance.maxPosition = camMaxPos;
        CameraController.instance.minPosition = camMinPos;
        player.transform.position = new Vector2(posX, posY);
    }
}