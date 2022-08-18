using UnityEngine;

public class CameraController : MonoBehaviour {
    #region Singleton

    public static CameraController instance;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one instance of CameraController found");
        }

        instance = this;
    }

    #endregion
    
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    
    private void Update() {
        if(target.Equals(null)) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void FixedUpdate() {
        if(!target.Equals(null)) {
            if(transform.position != target.position) {
                        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
                        
                        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
                        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
                        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
            }
        }
        
    }
}