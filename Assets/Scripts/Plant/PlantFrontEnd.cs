using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlantFrontEnd : MonoBehaviour {

    public Plant Plant;

    public void Update() {
        if (GameTime.growingTime) {
            foreach (GameObject root in GameObject.FindGameObjectsWithTag(ConstantValues.TagGrowingRoot)) {
                if (root.transform.IsChildOf(transform)) {
                    InstantiateComponent(Plant.GrowPlant(root.GetComponent<PlantComponentFrontEnd>().PlantComponent), root.transform);
                    //g.tag = "Untagged";
                }
            }
            foreach (GameObject stem in GameObject.FindGameObjectsWithTag(ConstantValues.TagGrowingStem)) {
                if (stem.transform.IsChildOf(transform)) {
                    InstantiateComponent(Plant.GrowPlant(stem.GetComponent<PlantComponentFrontEnd>().PlantComponent), stem.transform);
                    //g.tag = "Untagged";
                }
            }
        }
    }

    private GameObject InstantiateComponent(PlantComponent newComp, Transform parentCompTransform) {
        string prefabPath = ConstantValues.PathPrefabs + "/";
        switch (newComp.componentType) {
            case PlantComponentType.Roots:
            prefabPath = prefabPath + ConstantValues.PrefabRoots;
            break;
            case PlantComponentType.Stem:
            prefabPath = prefabPath + ConstantValues.PrefabStem;
            break;
            case PlantComponentType.Leaves:
            prefabPath = prefabPath + ConstantValues.PrefabLeaves;
            break;
            default:
            Debug.LogError("PrefabPath bad");
            break;
        }
        GameObject newCompGO = Instantiate(Resources.Load(prefabPath), parentCompTransform) as GameObject;
        newCompGO.GetComponent<PlantComponentFrontEnd>().PlantComponent = newComp;
        newCompGO.GetComponent<PlantComponentFrontEnd>().parent = parentCompTransform;
        newCompGO.name = newComp.GetID();
        newCompGO.transform.localPosition = Vector3.up * 0.5f; // TODO: arbitrary amount up so it looks good now
        return newCompGO;
    }
}

[Serializable]
public class Plant {
    [SerializeField]
    private string PlantID;
    public List<PlantComponent> roots;
    public List<PlantComponent> stems;
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
        CreateID();
    }

    /// <summary>
    /// Grows the plant components each tic
    /// </summary>
    /// <param name="frontEnd"></param>
    /// <returns></returns>
    public PlantComponent GrowPlant(PlantComponent comp) {
        PlantComponent newComp;
        switch (comp.componentType) {
            case PlantComponentType.Roots:
            newComp = new PlantComponent(PlantComponentType.Roots, roots[0].color);
            roots.Add(newComp);
            break;
            case PlantComponentType.Stem:
            newComp = new PlantComponent(PlantComponentType.Stem, stems[0].color);
            stems.Add(newComp);
            break;
            default:
            newComp = new PlantComponent(PlantComponentType.Empty, roots[0].color);
            Debug.LogError("Trying to grow bad component. Empty comp created");
            break;
        }
        newComp.parent = comp;
        Debug.Log("Growing " + newComp.GetID());
        return newComp;
    }

    // Graft?
    public void AddComponent(PlantComponent newComponent) {
        switch (newComponent.componentType) {
            case PlantComponentType.Roots:
            Debug.LogError("Shouldn't add roots this way");
            break;
            case PlantComponentType.Stem:
            stems.Add(newComponent);
            break;
            case PlantComponentType.Leaves:
            leaves = newComponent;
            break;
        }
    }
}
