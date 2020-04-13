using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera2D : MonoBehaviour
{
    public Camera cam;

    [Header("Panning")]
    public bool panningEnabled = true;
    [Range(-5, 5)]
    public float panSpeed = -0.06f;
    public bool keyboardInput = false;
    public bool inverseKeyboard = false;

    [Header("Zoom")]
    public bool zoomEnabled = true;
    public bool linkedZoomDrag = true;
    public float maxZoom = 10;
    [Range(0.01f, 10)]
    public float minZoom = 0.5f;
    [Range(0.1f, 10f)]
    public float zoomStepSize = 0.5f;

    [Header("Limit Camera Area")]
    public bool clampCamera = true;
    public float cameraMaxY = 50;
    public float cameraMinY = -50;
    public float cameraMaxX = 50;
    public float cameraMinX = -50;

    // private vars

    Vector3 bl;
    Vector3 tr;

    void Start() {
        if (cam == null) {
            cam = Camera.main;
        }
    }

    void Update() {
        if (panningEnabled) {
            panControl();
        }

        if (zoomEnabled) {
            zoomControl();
        }


        if (clampCamera) {
            cameraClamp();
        }
    }

    //click and drag
    public void panControl() {
        // if mouse is down
        if (Input.GetMouseButton(0)) {
            float x = Input.GetAxis("Mouse X") * panSpeed;
            float y = Input.GetAxis("Mouse Y") * panSpeed;

            if (linkedZoomDrag) {
                x *= Camera.main.orthographicSize;
                y *= Camera.main.orthographicSize;
            }

            transform.Translate(x, y, 0);
        }

        // if keyboard input is allowed
        if (keyboardInput) {
            float x = -Input.GetAxis("Horizontal") * panSpeed;
            float y = -Input.GetAxis("Vertical") * panSpeed;

            if (linkedZoomDrag) {
                x *= Camera.main.orthographicSize;
                y *= Camera.main.orthographicSize;
            }

            if (inverseKeyboard) {
                x = -x;
                y = -y;
            }
            transform.Translate(x, y, 0);
        }
    }

    // managae zooming
    public void zoomControl() {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && Camera.main.orthographicSize > minZoom) // forward
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - zoomStepSize;

            // Keep soze above 0 or error.
            if (Camera.main.orthographicSize < 0.01f) {
                Camera.main.orthographicSize = 0.01f;
            }
        }

        if ((Input.GetAxis("Mouse ScrollWheel") < 0) && Camera.main.orthographicSize < maxZoom) // back            
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + zoomStepSize;
        }
    }

    // Clamp Camera to bounds
    private void cameraClamp() {
        tr = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, -transform.position.z));
        bl = cam.ScreenToWorldPoint(new Vector3(0, 0, -transform.position.z));

        if (tr.x > cameraMaxX) {
            transform.position = new Vector3(transform.position.x - (tr.x - cameraMaxX), transform.position.y, transform.position.z);
        }

        if (tr.y > cameraMaxY) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (tr.y - cameraMaxY), transform.position.z);
        }

        if (bl.x < cameraMinX) {
            transform.position = new Vector3(transform.position.x + (cameraMinX - bl.x), transform.position.y, transform.position.z);
        }

        if (bl.y < cameraMinY) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (cameraMinY - bl.y), transform.position.z);
        }
    }
}
