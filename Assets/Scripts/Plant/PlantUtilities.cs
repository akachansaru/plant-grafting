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

    private static void InstantiateComponent(PlantComponent comp, Transform plant) {
        string prefabType;
        switch (comp.componentType) {
            case PlantComponentType.Roots:
            prefabType = ConstantValues.PrefabRoot;
            break;
            case PlantComponentType.Stem:
            prefabType = ConstantValues.PrefabStem;
            break;
            case PlantComponentType.Leaves:
            prefabType = ConstantValues.PrefabLeaves;
            break;
            default:
            prefabType = ConstantValues.PrefabRoot;
            Debug.LogError("Could not instantiate componenet. Prefab type bad");
            break;
        }
        GameObject compGO = Instantiate(Resources.Load(ConstantValues.PathPrefabs + "/" + prefabType)) as GameObject;
        compGO.GetComponent<PlantComponentFrontEnd>().PlantComponent = comp;

        if (comp.componentType != PlantComponentType.Empty) {
            if (compGO.GetComponent<PlantComponentFrontEnd>().PlantComponent.parent == "") {
                compGO.GetComponent<PlantComponentFrontEnd>().parent = plant;
                Debug.Log("parent = plant");
            } else {
                Debug.Log("comp " + comp.GetID());
                Debug.Log("newComp: " + compGO);
                Debug.Log("parent: " + comp.parent); // This parent needs to be the growthNode

                compGO.GetComponent<PlantComponentFrontEnd>().parent = GameObject.Find(comp.parent).transform;
                Debug.Log("parent = " + compGO.GetComponent<PlantComponentFrontEnd>().parent.name);
            }

            compGO.transform.SetParent(compGO.GetComponent<PlantComponentFrontEnd>().parent);
            compGO.name = comp.GetID();
            Debug.Log("instantiated comp name: " + compGO.name);
            //compGO.transform.localPosition = Vector3.up * 0.5f; // TODO: arbitrary amount up so it looks good now
            compGO.transform.localPosition = Vector3.zero;
        } else {
            Debug.LogError("Component type Empty");
        }
    }
}
