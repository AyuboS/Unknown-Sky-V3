using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManipulation : MonoBehaviour
{
    public float incrementValue = 5;
    public float scaleDuration = 0.5f;
    public Camera playerCamera;
    public float maxDistance = 400f;
    public LayerMask interactableLayer;
    void Update()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            if (hit.collider != null && ((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    StartCoroutine(IncreaseScale(hit.collider.transform, hit.collider.GetComponent<SizeManipulatedObject>()));
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    StartCoroutine(DecreaseScale(hit.collider.transform, hit.collider.GetComponent<SizeManipulatedObject>()));
                }
            }
        }

    }

   IEnumerator IncreaseScale(Transform transformObj, SizeManipulatedObject size)
    {
        if (size.sizeStatus < 1 && size.animationFinished)
        {
            Vector3 initialScale = transformObj.localScale;
            Vector3 targetScale = initialScale * incrementValue;
            float elapsedTime = 0f;

            size.animationFinished = false;

            while (elapsedTime < scaleDuration)
            {
                transformObj.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

             size.animationFinished = true;

            transformObj.localScale = targetScale;
            size.UpdateSizeStatus(1);
        }
    }

    IEnumerator DecreaseScale(Transform transformObj, SizeManipulatedObject size)
    {
        if (size.sizeStatus > -1 && size.animationFinished)
        {
            Vector3 initialScale = transformObj.localScale;
            Vector3 targetScale = initialScale / incrementValue;
            float elapsedTime = 0f;

            size.animationFinished = false;

            while (elapsedTime < scaleDuration)
            {
                transformObj.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / scaleDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            size.animationFinished = true;
            
            transformObj.localScale = targetScale;
            size.UpdateSizeStatus(-1);
        }
    }
}
