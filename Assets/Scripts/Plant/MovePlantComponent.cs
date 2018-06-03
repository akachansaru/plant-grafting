using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Snaps the plant component into place on the pot or plant when dropped over the worktable
[RequireComponent(typeof(PlantComponentFrontEnd))]
public class MovePlantComponent : MoveObject {

    public override void OnEndDrag(PointerEventData eventData) {
        dragging = false;
        selected = false;
        RaycastHit hitInfo;
        // For dragging onto worktable only (8 is worktable layer)
        if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 5, 1 << 8)) {
            transform.position = hitInfo.collider.GetComponent<Worktable>().SnapToWorktable(GetComponent<PlantComponentFrontEnd>(), 
                originalPosition);
        } else {
            transform.position = originalPosition;
        }
    }

    //void SnapToWorktable(RaycastHit hitInfo) {
    //    Vector3 location;
    //    switch (GetComponent<PlantComponentFrontEnd>().GetPlantComponentType()) {
    //        case PlantComponentType.Roots:
    //        location = hitInfo.collider.GetComponent<Worktable>().rootLocation.position;
    //        break;
    //        case PlantComponentType.Stem:
    //        location = hitInfo.collider.GetComponent<Worktable>().stemLocation.position;
    //        break;
    //        case PlantComponentType.Leaves:
    //        location = hitInfo.collider.GetComponent<Worktable>().leafLocation.position;
    //        break;
    //        default:
    //        location = originalPosition;
    //        Debug.Log("could not snap to place");
    //        break;
    //    }
    //    hitInfo.collider.GetComponent<Worktable>().addedComponent = GetComponent<PlantComponentFrontEnd>().PlantComponent;
    //    transform.position = location;
    //}
}
