using UnityEngine;
using UnityEngine.EventSystems;
using System;

public enum PlantComponentType { Roots, Stem, Leaves, Empty };

// This is added to each plant component game object to adjust attributes in the editor
public class PlantComponentFrontEnd : MonoBehaviour {
    public Transform parent;
    public PlantComponent PlantComponent;
    
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
    public string parent;
    public string color;
    public bool locked; // Can't be overridden by another component of the same type

    // PlantColor color;
    // growth stage
    // temp needs
    // light needs

    public string GetID() {
        return CompID;
    }

    /// <summary>
    /// Shouldn't use this publically. Only for initialzing the default things in the inpector
    /// </summary>
    /// <returns></returns>
    public void CreateID(int compNum) {
        CompID = componentType + DateTime.Now.ToString() + compNum;
    }

    public PlantComponent(PlantComponentType compType, int compNum, string c) {
        componentType = compType;
        CreateID(compNum);
        color = c;
        locked = true;
    }
}
