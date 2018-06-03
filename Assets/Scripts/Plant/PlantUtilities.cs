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
        TreeVisitor instantiateVisitor = InstantiateComponent;

        // Instantiate each root componenent
        TreeNode.Traverse(p.roots, plant.transform, instantiateVisitor);

        // Instantiate each stem componenet
        if (p.stem.componentType != PlantComponentType.Empty) {
            GameObject stem = Instantiate(Resources.Load("Prefabs/Stem")) as GameObject;
            stem.transform.SetParent(plant.transform);
            stem.transform.localPosition = Vector3.up; // TODO: arbitrary amount up so it looks good now
        }
        return plant;
    }

    // For TreeVisitor delegate
    static void InstantiateComponent(PlantComponent comp, Transform plant) {
        Debug.Log("instantiated comp ID: " + comp.GetID());
        GameObject newComp = Instantiate(Resources.Load("Prefabs/Roots")) as GameObject; // TODO: will need a switch for components
        newComp.GetComponent<PlantComponentFrontEnd>().PlantComponent = comp;

        if (comp.componentType != PlantComponentType.Empty) {
            //Transform parent;
            if (comp.parent == null) {
                newComp.GetComponent<PlantComponentFrontEnd>().parent = plant;
                //parent = plant;
                Debug.Log("parent = plant");
            } else {
                //parent = GameObject.Find(comp.parent.GetID()).transform;
                newComp.GetComponent<PlantComponentFrontEnd>().parent = GameObject.Find(comp.parent.GetID()).transform;
                Debug.Log("parent = " + newComp.GetComponent<PlantComponentFrontEnd>().parent.name);
            }

            newComp.transform.SetParent(newComp.GetComponent<PlantComponentFrontEnd>().parent);
            newComp.name = comp.GetID();
            Debug.Log("instantiated comp name: " + newComp.name);
            newComp.transform.localPosition = Vector3.up * 0.5f; // TODO: arbitrary amount up so it looks good now
        }
    }
}
