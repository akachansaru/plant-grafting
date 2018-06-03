using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worktable : MonoBehaviour {
    // UNDONE: Add functionality to prune plant here
    public Transform rootLocation;
    public Transform stemLocation;
    public Transform leafLocation;
    public GameObject blankPlant;

    public static Plant plant; // PlantOptions uses this to add the plant that was clicked on
    private PlantComponent addedComponent;

    public void Start() {
        if (plant != null) { // This will be if a plant was selected to do work on
            GameObject plantGO = PlantUtilities.InstantiatePlant(plant, blankPlant, transform);
            plantGO.transform.position = rootLocation.position;
        } else { // This is for creating a new plant. Only roots should be added here
            plant = Instantiate(blankPlant, transform).GetComponent<PlantFrontEnd>().Plant;
            Debug.Log("New plant");
        }
    }

    // Changed in flatten
    public Vector3 SnapToWorktable(PlantComponentFrontEnd plantComponent, Vector3 originalPosition) {
        Vector3 location;
        switch (plantComponent.GetPlantComponentType()) {
            case PlantComponentType.Roots:
            if (plant.roots.Count == 0) {
                location = rootLocation.position;
                addedComponent = plantComponent.PlantComponent;
            } else {
                location = originalPosition;
            }
            break;
            case PlantComponentType.Stem:
            if (plant == null || plant.stem.componentType != PlantComponentType.Empty) { // change to plant.roots == nulll
                location = originalPosition;
            } else {
                location = stemLocation.position;
                addedComponent = plantComponent.PlantComponent;
            }
            break;
            case PlantComponentType.Leaves:
            if (plant == null || plant.leaves.componentType != PlantComponentType.Empty) {
                location = originalPosition;
            } else {
                location = leafLocation.position;
                addedComponent = plantComponent.PlantComponent;
            }
            break;
            default:
            location = originalPosition;
            Debug.Log("could not snap to place");
            break;
        }
        return location;
    }
    // Changed in flatten
    public void StartGrowing() {
        if (plant.roots.Count == 0) {
            CreateNewPlant();
        } else {
            Graft();
        }
    }

    /// <summary>
    /// Add the new plant to the plant list to be saved
    /// </summary>
    private void CreateNewPlant() {
        if (addedComponent.componentType == PlantComponentType.Roots) {
            //Plant newPlant = new Plant(addedComponent);
            addedComponent.locked = true;
            //GlobalControl.Instance.savedValues.availablePlants.Add(newPlant);
            Debug.Log("added comp: " + addedComponent);
            Debug.Log("plant: " + plant);
            Debug.Log("roots: " + plant.roots);
            //plant.roots.AddChild(addedComponent);
            //plant.roots = new TreeNode(addedComponent);
            plant.roots.Add(addedComponent); // = new List<PlantComponent>();
            GlobalControl.Instance.savedValues.availablePlants.Add(plant);
            GlobalControl.Instance.Save();
        } else {
            Debug.Log("Error: only roots should be added when creating new plant.");
        }
    }

    private void Graft() {
        GlobalControl.Instance.savedValues.availablePlants.Find(i => i.GetID() == plant.GetID()).AddComponent(addedComponent);
        addedComponent.locked = true;
        GlobalControl.Instance.Save();
    }
}
