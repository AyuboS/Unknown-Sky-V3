using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManipulatedObject : MonoBehaviour
{
    public int sizeStatus = 0;
    public bool animationFinished = true;
    public bool isfreezed = false;
    public void UpdateSizeStatus(int inp)
    {
        int newSizeStatus = sizeStatus + inp;
        if (newSizeStatus >= -1 && newSizeStatus <= 1)
        {
            sizeStatus = newSizeStatus;
        }
    }
}
