// DCFM Team 2018

using UnityEngine;

// have camera follow character, but lag so character seems to move instead of planet
public class CameraFollow : MonoBehaviour {
  public Transform target;

  public float smoothSpeed = .1f;

  void FixedUpdate() { // run late update to make sure the target has already moved
    Vector2 transformPolarPosition = Conversions.convertToPolar(transform.position);
    Vector2 targetPolarPosition = Conversions.convertToPolar(target.position);
    Vector3 offset = new Vector3(0,0,-1);
    Vector2 desiredPosition = targetPolarPosition;
    Vector2 smoothedPosition = Vector3.Lerp(transformPolarPosition, desiredPosition, smoothSpeed);
    Vector2 polarPosition = smoothedPosition;
    transform.position = (Vector3) Conversions.convertToCartesian(polarPosition) + offset;
    // Camera is in 3D, and Quaternion is used to define rotation of 3D objects.
    transform.rotation = Quaternion.Euler(0, 0, transformPolarPosition.y * Mathf.Rad2Deg - 90);
  }
}
