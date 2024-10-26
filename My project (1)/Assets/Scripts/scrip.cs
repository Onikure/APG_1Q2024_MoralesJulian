using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrip : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition + offset;
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
