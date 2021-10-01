using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Parameters that control the turn speed and the amount of ease-in/out time. Adjust to suit taste.
    public float rotationSpeed = 360;
    public float smoothTime = 0.1f;
    public float walkSpeed = 5.0f;
    private float _angle, _speed, _currentVelocityAngular, _currentVelocityTranslational;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        var main = Camera.main;
        _mainCamera = main ? main : throw new ArgumentNullException(nameof(_mainCamera));
    }

    // Update is called once per frame
    void Update()
    {
        // Handle rotation toward camera
        
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var directionToFace = mousePosition - transform.position;
        var targetAngle = Vector2.SignedAngle(Vector2.up, directionToFace);
        _angle = Mathf.SmoothDampAngle(_angle, targetAngle, ref _currentVelocityAngular, smoothTime, rotationSpeed);
        transform.eulerAngles = new Vector3(0, 0, _angle);
        
        // Handle movement
        _speed = Mathf.SmoothDamp(_speed, walkSpeed, ref _currentVelocityTranslational, smoothTime, walkSpeed);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (_speed * Time.deltaTime), Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * (_speed* Time.deltaTime), Space.World);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime), Space.World);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * (_speed * Time.deltaTime), Space.World);
        }
        
        // Snap movement to edge of screen by converting transform.position to its in-camera equivalent and clamping it
        var cameraPos = _mainCamera.WorldToViewportPoint(transform.position);
        cameraPos.x = Mathf.Clamp01(cameraPos.x);
        cameraPos.y = Mathf.Clamp01(cameraPos.y);
        transform.position = _mainCamera.ViewportToWorldPoint(cameraPos);
    }
}
