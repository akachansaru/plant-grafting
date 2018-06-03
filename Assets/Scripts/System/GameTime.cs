using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour {

    public static bool growingTime;

    public float time = 0f;
    public float timer = 2f;

	void Update () {
        if (time >= timer) {
            //foreach (Plant p in GlobalControl.Instance.savedValues.availablePlants) {
            //    p.GrowPlant();
            //}
            growingTime = true;
            time = 0f;
        } else {
            growingTime = false;
            time += Time.deltaTime;
        }
	}
}
