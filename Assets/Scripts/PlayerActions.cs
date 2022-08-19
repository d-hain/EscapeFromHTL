using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(PlayerInput))]
public class PlayerActions : MonoBehaviour {
    #region Singleton

    public static PlayerActions Instance => _instance;
    private static PlayerActions _instance;

    private void Awake() {
        if(_instance != null) {
            Debug.LogWarning("More than one instance of PlayerActions found!");
        }

        _instance = this;
    }

    #endregion

    private PlayerInput _playerInput;
    private PlayerInputActions _playerInputActions;

    #region DelegatesAndEvents

    public delegate void InteractEvent();

    /// <summary>
    /// The event that is fired when the interact action is performed.
    /// </summary>
    public event InteractEvent Interact;

    #endregion

    private void Start() {
        _playerInput = GetComponent<PlayerInput>();
        _playerInputActions = new PlayerInputActions();
    }
    
    #region OnInputMethods

    /// <summary>
    /// <b>DO NOT USE</b> <br/><br/>
    /// The method that is called when the interact action is performed.
    /// </summary>
    public void OnInteract(InputAction.CallbackContext context) {
        if(!context.performed) return;

        Interact?.Invoke();
    }

    #endregion
    
    /// <summary>
    /// Switches the current ActionMap to the Player ActionMap. <br/>
    /// Enables the Player ActionMap. <br/>
    /// Disables the UI ActionMap. <br/>
    /// </summary>
    public void SwitchCurrentActionMapToPlayer() {
        _playerInput.SwitchCurrentActionMap("Player");
        _playerInputActions.Player.Enable();
        _playerInputActions.UI.Disable();
    }
    
    /// <summary>
    /// Switch the current ActionMap to the UI ActionMap. <br/>
    /// Enables the UI ActionMap. <br/>
    /// Disables the Player ActionMap. <br/>
    /// </summary>
    public void SwitchCurrentActionMapToUI() {
        _playerInput.SwitchCurrentActionMap("UI");
        _playerInputActions.UI.Enable();
        _playerInputActions.Player.Disable();
    }
}