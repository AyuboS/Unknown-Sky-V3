using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulation : MonoBehaviour
{
    public Camera playerCamera;
    public float timeSlowDuration = 2f; 
    public float timeSlowFactor = 0.5f; 
    public float maxDistance = 400f;

    public KeyCode timeSlowKey = KeyCode.R;
    public LayerMask interactableObject;

    void Update()
    {
        if (Input.GetKeyDown(timeSlowKey))
        {
            SlowDownCenterObject();
        }
    }

    void SlowDownCenterObject()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, interactableObject))
        {
            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & interactableObject) != 0)
            {
                StartCoroutine(SlowDownTime(hit.collider.GetComponent<Rigidbody>(), hit.collider.GetComponent<SizeManipulatedObject>()));
            }
        }
    }

    IEnumerator SlowDownTime(Rigidbody targetRigidbody, SizeManipulatedObject size)
    {
        targetRigidbody.isKinematic = true;
        size.isfreezed = true;
        targetRigidbody.useGravity = false;
        targetRigidbody.velocity *= timeSlowFactor;

        yield return new WaitForSeconds(timeSlowDuration);

        targetRigidbody.isKinematic = false;
        size.isfreezed = false;
        targetRigidbody.useGravity = true;

    }
}
