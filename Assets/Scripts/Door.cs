using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody2D))]
public class Door : MonoBehaviour {
    /// <summary>
    /// Is true when the player is inside the collider of the door
    /// </summary>
    private bool _isCollidingWithPlayer;

    public delegate void PlayerInteractEvent(string roomName);

    /// <summary>
    /// Name of the room in this format: "[A-C]{1}[0-9]{3}"
    /// </summary>
    public string roomName;

    /// <summary>
    /// The event that is fired when the interact action is performed.
    /// </summary>
    public static event PlayerInteractEvent PlayerInteract;
    
    /// <summary>
    /// Subscribes the <see cref="OnPlayerInteract"/> Method to the <see cref="PlayerActions.Interact"/> Event.
    /// </summary>
    private void Start() {
        PlayerActions.Instance.Interact += OnPlayerInteract;
    }

    /// <summary>
    /// Sets the <see cref="_isCollidingWithPlayer"/> bool to true if the door is colliding with the player.
    /// </summary>
    /// <param name="other">The collider the Door is colliding with.</param>
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _isCollidingWithPlayer = true;
        }
    }

    /// <summary>
    /// Sets the <see cref="_isCollidingWithPlayer"/> bool to false if the door is not colliding with the player anymore.
    /// </summary>
    /// <param name="other">The collider the Door is colliding with.</param>
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _isCollidingWithPlayer = false;
        }
    }

    /// <summary>
    /// Gets called when the Player presses the interact button. <br/>
    /// Then calls the <see cref="PlayerInteract"/> event when the Player is in range of the door.
    /// </summary>
    private void OnPlayerInteract() {
        if(!_isCollidingWithPlayer) return;

        PlayerInteract?.Invoke(roomName);
    }
}