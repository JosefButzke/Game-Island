using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public int distanceInteract = 20;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        int layerMask = 9;
        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(playerBody.position, transform.TransformDirection(Vector3.forward), out hit, distanceInteract, layerMask))
        {
            if (hit.collider.gameObject.CompareTag("Item"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<ItemPickup>().PickUp();
                }
            }

            if (hit.collider.gameObject.CompareTag("Resource"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    bool canDestroy = hit.collider.gameObject.GetComponent<Resource>().Harvest(1);

                    if(canDestroy) {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
