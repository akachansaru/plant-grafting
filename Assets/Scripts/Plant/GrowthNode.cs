using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthNode : MonoBehaviour {

    public string ID;

    public void Start() {
        ID = GetComponentInParent<PlantComponentFrontEnd>().PlantComponent.GetID() + gameObject.name;
    }

    public string GetID() {
        return ID;
    }
}
