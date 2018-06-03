using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Add anything that needs saving here and GlobalControl.cs will save and load it
/// </summary>
[Serializable]
public class SaveValues {
    public bool musicMuted;
    public float musicVolume;
    public List<Plant> availablePlants;
}
