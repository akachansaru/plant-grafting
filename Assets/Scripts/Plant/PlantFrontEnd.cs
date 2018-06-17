using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlantFrontEnd : MonoBehaviour {

    public Plant Plant;

    public void Update() {
        if (GameTime.growingTime) {
            GameObject[] rootGrowthNodes = GameObject.FindGameObjectsWithTag(ConstantValues.TagGrowingRoot);
            foreach (GameObject node in rootGrowthNodes) {
                if (node.transform.IsChildOf(transform)) {
                    InstantiateComponent(Plant.GrowPlant(node.GetComponent<GrowthNode>()), node.transform);
                    node.tag = "Untagged";
                }
            }
            GameObject[] growingStems = GameObject.FindGameObjectsWithTag(ConstantValues.TagGrowingStem);
            foreach (GameObject stem in growingStems) {
                if (stem.transform.IsChildOf(transform)) {
                    //InstantiateComponent(Plant.GrowPlant(stem.GetComponent<PlantComponentFrontEnd>().PlantComponent), stem.transform);
                    stem.tag = "Untagged";
                }
            }
        }
    }

    private GameObject InstantiateComponent(PlantComponent newComp, Transform parentCompTransform) {
        string prefabPath = ConstantValues.PathPrefabs + "/";
        switch (newComp.componentType) {
            case PlantComponentType.Roots:
            prefabPath = prefabPath + ConstantValues.PrefabRoot;
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
        newCompGO.GetComponent<PlantComponentFrontEnd>().parent = parentCompTransform;
        newCompGO.GetComponent<PlantComponentFrontEnd>().PlantComponent = newComp;
        newCompGO.name = newComp.GetID();
        newCompGO.transform.localPosition = Vector3.zero;
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
    public PlantComponent GrowPlant(GrowthNode node) {
        PlantComponent comp = node.GetComponentInParent<PlantComponentFrontEnd>().PlantComponent;
        PlantComponent newComp;
        switch (comp.componentType) {
            case PlantComponentType.Roots:
            newComp = new PlantComponent(PlantComponentType.Roots, roots.Count, roots[0].color);
            roots.Add(newComp);
            break;
            case PlantComponentType.Stem:
            newComp = new PlantComponent(PlantComponentType.Stem, stems.Count, stems[0].color);
            stems.Add(newComp);
            break;
            default:
            newComp = new PlantComponent(PlantComponentType.Empty, 0, roots[0].color);
            Debug.LogError("Trying to grow bad component. Empty comp created");
            break;
        }
        newComp.parent = node.GetID(); // THis parent needs to be the growthnode
        Debug.Log("Growing " + newComp.GetID());
        return newComp;
    }

    /// <summary>
    /// Adds the component when grafting
    /// </summary>
    /// <param name="newComponent"></param>
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
