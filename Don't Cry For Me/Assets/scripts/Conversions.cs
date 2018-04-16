// DCFM Team 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for converting polar to cartesian and vice-versa
public class Conversions : MonoBehaviour {
	public static Vector2 convertToPolar(Vector2 cartesianCoords) {
    float x = cartesianCoords.x;
    float y = cartesianCoords.y;
    float radius = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    float theta = Mathf.Atan2(y,x);
    return new Vector2(radius, theta);
  }
  public static Vector2 convertToCartesian(Vector2 polarCoords) {
    float radius = polarCoords.x;
    float theta = polarCoords.y;
    float x = radius*Mathf.Cos(theta);
    float y = radius*Mathf.Sin(theta);
    return new Vector2(x, y);
  }
}
