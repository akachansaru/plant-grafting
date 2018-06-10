using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlantUtilities : MonoBehaviour {

    public static GameObject InstantiatePlant(Plant p, GameObject blankPlant, Transform transform) {
        Debug.Log("IntantiatePlant");
        GameObject plant = Instantiate(blankPlant, transform);
        plant.GetComponent<PlantFrontEnd>().Plant = p;

        // Instantiate each root componenent
        foreach (PlantComponent r in p.roots) {
            InstantiateComponent(r, plant.transform);
        }

        // Instantiate each stem componenet
        foreach (PlantComponent s in p.stems) {
            InstantiateComponent(s, plant.transform);
        }

        return plant;
    }

    static void InstantiateComponent(PlantComponent comp, Transform plant) {
        string prefabType;
        switch (comp.componentType) {
            case PlantComponentType.Roots:
            prefabType = ConstantValues.PrefabRoots;
            break;
            case PlantComponentType.Stem:
            prefabType = ConstantValues.PrefabStem;
            break;
            case PlantComponentType.Leaves:
            prefabType = ConstantValues.PrefabLeaves;
            break;
            default:
            prefabType = ConstantValues.PrefabRoots;
            Debug.LogError("Could not instantiate componenet. Prefab type bad");
            break;
        }
        GameObject compGO = Instantiate(Resources.Load(ConstantValues.PathPrefabs + "/" + prefabType)) as GameObject;
        compGO.GetComponent<PlantComponentFrontEnd>().PlantComponent = comp;

        if (comp.componentType != PlantComponentType.Empty) {
            if (comp.parent != null )
                Debug.LogError("parent 1: " + comp.parent.GetID());
            if (comp.parent == null) {
                compGO.GetComponent<PlantComponentFrontEnd>().parent = plant;
                Debug.Log("parent = plant");
            } else {
                Debug.LogError("newComp: " + compGO);
                Debug.LogError("parent: " + comp.parent.GetID());
                compGO.GetComponent<PlantComponentFrontEnd>().parent = GameObject.Find(comp.parent.GetID()).transform;
                Debug.Log("parent = " + compGO.GetComponent<PlantComponentFrontEnd>().parent.name);
            }

            compGO.transform.SetParent(compGO.GetComponent<PlantComponentFrontEnd>().parent);
            compGO.name = comp.GetID();
            Debug.Log("instantiated comp name: " + compGO.name);
            compGO.transform.localPosition = Vector3.up * 0.5f; // TODO: arbitrary amount up so it looks good now
        }
    }
}
