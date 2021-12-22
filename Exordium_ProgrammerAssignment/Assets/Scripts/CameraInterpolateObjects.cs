using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraInterpolateObjects
{
    [Header("Settings")]
    public int stopTimeOnEndObject;
    public float speedBetwenEndEndObjects;
    public float speedBetwenStartEndObjects;
    [Header("Trigger Camera Move")]
    public List<GameObject> triggerStart;
    [Header("Camera Move To")]
    public List<GameObject> endObject;

}
