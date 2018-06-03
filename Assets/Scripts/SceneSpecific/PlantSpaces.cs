using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For placing plants in greenhouse grid.
/// </summary>
public class PlantSpaces : MonoBehaviour {
    public GameObject spaceToPlace; // Keep this in units of 5 for now
    public GameObject blankPlant;

    void Start() {
        int i = 0;
        foreach (Plant p in GlobalControl.Instance.savedValues.availablePlants) {
            Debug.Log("saved plant" + p);
            GameObject plant = PlantUtilities.InstantiatePlant(p, blankPlant, transform); // spaceToPlace.transform.GetChild(i)
            plant.transform.localPosition = new Vector3(i, 1, 0);
            i++;
        }
    }
}
