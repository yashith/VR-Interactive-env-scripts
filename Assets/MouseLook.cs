using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSentisivity = 500;
    float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xMouse = Input.GetAxis("Mouse X") * mouseSentisivity * Time.deltaTime;
        float yMouse = Input.GetAxis("Mouse Y") * mouseSentisivity * Time.deltaTime;

        yRotation -= yMouse;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * xMouse);
    }
}
