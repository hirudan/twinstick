using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    // Parameters that control the turn speed and the amount of ease-in/out time. Adjust to suit taste.
    public float rotationSpeed = 360;
    public float smoothTime = 0.1f;
    private float _angle, _currentVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var directionToFace = mousePosition - transform.position;
        var targetAngle = Vector2.SignedAngle(Vector2.up, directionToFace);
        _angle = Mathf.SmoothDampAngle(_angle, targetAngle, ref _currentVelocity, smoothTime, rotationSpeed);
        transform.eulerAngles = new Vector3(0, 0, _angle);
    }
}
