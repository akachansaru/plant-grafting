using UnityEngine;
using UnityEngine.EventSystems;
using System;

public enum PlantComponentType { Roots, Stem, Leaves, Empty };

// This is added to each plant component game object to adjust attributes in the editor
public class PlantComponentFrontEnd : MonoBehaviour {
    public PlantComponent PlantComponent;
    public Transform parent;

    public PlantComponentType GetPlantComponentType () {
        return PlantComponent.componentType;
    }
}

// This is what is actually saved
[Serializable]
public class PlantComponent {

    [SerializeField]
    private string CompID;
    public PlantComponentType componentType;
    public PlantComponent parent;
    public string color;
    public bool locked; // Can't be overridden by another component of the same type
    //public Plant plant; // TODO: Plant this component is part of. Would ID be better?

    // PlantColor color;
    // growth stage
        // save each branch segment, leaf, etc.
    // temp needs
    // light needs

    public string GetID() {
        return CompID;
    }

    /// <summary>
    /// Shouldn't use this. Only for initialzing the default things in the inpector
    /// </summary>
    /// <returns></returns>
    public void CreateID() {
        Debug.Log("create CompID " + CompID + " with " + parent);
        // TODO: Make the ID more unique for the comps all created at the same time when growing tics. 
        // Maybe a number that increments for the round or something
        CompID = componentType + DateTime.Now.ToString();
    }

    public PlantComponent(PlantComponentType compType, string c) {
        componentType = compType;
        CreateID();
        color = c;
        locked = false;
    }
}
