using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Speeds")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float rotationSpeed;

    [Header("Limits")]
    [SerializeField] private float minRotationX;
    [SerializeField] private float maxRotationX;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;


    private float currentRotation;
    private float currentZoom;

    private Camera cam;

    private float inputScroll;
    private float mouseX;
    private float mouseY;
    private float moveX;
    private float moveZ;

    private void Start()
    {
        cam = Camera.main;
        currentRotation = -50f;
        currentZoom = transform.localPosition.y;
        
    }

    private void Update()
    {
        GetInputs();
        ControlZoom();
        MoveCamera();
        if (Input.GetMouseButton(1))
            RotateCamera();
        
    }

    private void GetInputs()
    {
        inputScroll = Input.GetAxis("Mouse ScrollWheel");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }


    private void ControlZoom()
    {
        currentZoom += inputScroll * -zoomSpeed;

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * currentZoom;
    }

    private void RotateCamera()
    {
        currentRotation += -mouseY * rotationSpeed;
        currentRotation = Mathf.Clamp(currentRotation, minRotationX, maxRotationX);

        transform.eulerAngles = new Vector3(currentRotation, transform.eulerAngles.y + (mouseX * rotationSpeed), 0f);
    }

    private void MoveCamera()
    {
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
        dir.y = 0f;

        Vector3 newPos = transform.position + dir * (moveSpeed * (currentZoom / maxZoom)) * Time.deltaTime;
        if(newPos.x > -60f && newPos.x < 90f && newPos.z > -60f && newPos.z < 90)
            transform.position = newPos;


    }

}
