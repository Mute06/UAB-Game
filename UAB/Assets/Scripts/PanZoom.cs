using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PanZoom : MonoBehaviour
{

    [MinMaxSlider(1, 10)] public Vector2 minMaxZoom;
    Vector3 touchStart;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (ModeController.Instance.currentMode == Modes.Moving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position += direction;
            }
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

    }

    private void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minMaxZoom.x, minMaxZoom.y);
    }
}
