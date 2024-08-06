using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingScript : MonoBehaviour
{
    public Camera playerCamera;
    public float pullForce = 10f;
    public float pushForce = 15f;
    public float holdDistance = 2f;
    public float maxDistance = 100f;
    public float moveSpeed = 10f;
    public LayerMask interactableLayer;

    private Rigidbody heldObject;
    private bool isHolding = false;
    public float rotationSpeed = 100f;
    [Header("Keybinds")]
    public KeyCode pullKey = KeyCode.E;


    void Update()
    {
        if (Input.GetKeyDown(pullKey))
        {
            PullObject();
        }

        if (Input.GetKeyUp(pullKey))
        {
            if (isHolding)
            {
                PushObject();
            }
        }

        if (isHolding)
        {
            HoldObject();
        }
    }

    void PullObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, maxDistance))
        {
            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
            {    
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                SizeManipulatedObject size = hit.collider.GetComponent<SizeManipulatedObject>();

                if (rb != null && size.sizeStatus == -1 && !size.isfreezed)
                {
                    heldObject = rb;
                    heldObject.isKinematic = true;
                    isHolding = true;
                }
            }
        }
    }

    void HoldObject()
    {
        Vector3 holdPosition = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
        heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, holdPosition, moveSpeed * Time.deltaTime);

        RotateObject();
    }

     void RotateObject()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            float rotationX = rotationSpeed * Time.deltaTime;
            heldObject.transform.Rotate(Vector3.right, rotationX, Space.World);
        }

        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float rotationZ = rotationSpeed * Time.deltaTime;
            heldObject.transform.Rotate(Vector3.forward, rotationZ, Space.World);
        }
    }

    void PushObject()
    {
        heldObject.isKinematic = false;
        heldObject.AddForce(playerCamera.transform.forward * pushForce, ForceMode.Impulse);
        heldObject = null;
        isHolding = false;
    }
}
