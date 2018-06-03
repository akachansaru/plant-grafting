using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlantFrontEnd : MonoBehaviour {

    public Plant Plant;

    public void Update() {
        if (GameTime.growingTime) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("GrowingRoot")) {
                if (g.transform.IsChildOf(transform)) {
                    InstantiateComponent(Plant.GrowPlant(g.GetComponent<PlantComponentFrontEnd>()), g.transform);
                    //g.tag = "Untagged";
                }
            }
        }
    }

    private GameObject InstantiateComponent(PlantComponent p, Transform t) {
        GameObject newPlantGO = Instantiate(Resources.Load("Prefabs/Roots"), t) as GameObject;
        newPlantGO.GetComponent<PlantComponentFrontEnd>().PlantComponent = p;
        newPlantGO.GetComponent<PlantComponentFrontEnd>().parent = t;
        newPlantGO.name = p.GetID();
        return newPlantGO;
    }
}

[Serializable]
public class Plant {
   [SerializeField]
    private string PlantID;
    public TreeNode roots;
    public PlantComponent stem;
    public PlantComponent leaves;
    public Vector3Serializable position; // Not sure if I need this

    public string GetID() {
        return PlantID;
    }

    private void CreateID() {
        Debug.Log("Creating plantID");
        PlantID = DateTime.Now.ToString();
    }

    public Plant() {
        //roots = new TreeNode(new PlantComponent(PlantComponentType.Empty, "Empty"));
        //roots.Add(new PlantComponent(PlantComponentType.Empty, "Empty"));
       // stem = new PlantComponent(PlantComponentType.Empty, "Empty");
        //leaves = new PlantComponent(PlantComponentType.Empty, "Empty");
        CreateID();
    }

    public PlantComponent GrowPlant(PlantComponentFrontEnd frontEnd) {
        PlantComponent p = new PlantComponent(PlantComponentType.Roots, roots.GetData().color);
        //roots.AddChild(p); // THis needs to be added to the componenent that is actually growing, not the root node root
        roots.FindChild(roots, frontEnd.PlantComponent.GetID()).AddChild(p);
        Debug.Log("Growing " + p.GetID());
        // PlantUtilities.InstantiateComponent(p);
        //stem.Grow();
        //leaves.Grow();
        return p;
    }

    // Graft?
    public void AddComponent(PlantComponent newComponent) {
        switch (newComponent.componentType) {
            case PlantComponentType.Roots:
            Debug.Log("Shouldn't add roots this way");
            break;
            case PlantComponentType.Stem:
            stem = newComponent;
            break;
            case PlantComponentType.Leaves:
            leaves = newComponent;
            break;
        }
    }
}
