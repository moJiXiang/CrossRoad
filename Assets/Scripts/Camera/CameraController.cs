using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Transform frog;
   public float offsetY;
   public float zoomBase; // 设计宽度的参考值
   private float ratio;

   // private float orthographicSize = Camera.main.orthographicSize;

   private void Start()
   {
      Debug.Log("Screen width and height: " + Screen.width + " " + Screen.height);
      ratio = (float)Screen.height / (float)Screen.width;
      Camera.main.orthographicSize = zoomBase * ratio * 0.5f;
   }

   private void LateUpdate()
   {
      transform.position = new Vector3(transform.position.x, frog.transform.position.y + offsetY * ratio, transform.position.z);
   }
}
