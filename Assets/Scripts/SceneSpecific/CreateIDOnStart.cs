using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Super useless. Should find a better way to do this. Only for plantComps in the starting screen to initialize ID
public class CreateIDOnStart : MonoBehaviour {

	void Start () {
        GetComponent<PlantComponentFrontEnd>().PlantComponent.CreateID(0);
        gameObject.name = GetComponent<PlantComponentFrontEnd>().PlantComponent.GetID();
    }
	
}
