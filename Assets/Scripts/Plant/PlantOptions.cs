using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// For selecting a plant to edit. Attached to the Plant in the Greenhouse.
/// </summary>
[RequireComponent(typeof(Plant))]
public class PlantOptions : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        // Set the plant to be worked on. There should probably be a better way to do this. Worktable.EditPlant(GetComponent<PlantFrontEnd>().Plant);
        Worktable.plant = GetComponent<PlantFrontEnd>().Plant;
        GlobalControl.Instance.Save();
        SceneManager.LoadScene("Worktable");
    }

}
