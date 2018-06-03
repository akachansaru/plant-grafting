using UnityEngine;
using UnityEngine.EventSystems;

// Attach to object to drag it. Dropping behavior is handled by derived scripts
public class MoveObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler {
    public Color highlightColor;

    protected static bool dragging;
    protected bool selected;
    protected Vector3 originalPosition;

    MeshRenderer mRenderer;
    Color originalColor;
    Vector3 screenPoint;
    Vector3 offset;
  //  GameObject touching;
    

    void Start() {
        mRenderer = GetComponent<MeshRenderer>();
        originalColor = mRenderer.material.color;
        originalPosition = transform.position;
        dragging = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!dragging) {
            mRenderer.material.color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!dragging) {
            mRenderer.material.color = originalColor;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // originalPosition = transform.position; // Uncomment this to be able to move around objects and leave them. Otherwise they will always snap back to where they started
        dragging = true;
        selected = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
        transform.position = cursorPosition;
    }

    // Doesn't seem to work well. Using OnEndDrag instead
    public void OnDrop(PointerEventData eventData) {
        //dragging = false;
        //selected = false;
        //RaycastHit hitInfo;
        //if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 5, 1<<8)) {
        //    Debug.Log("hit worktable");
        //    SnapToWorktable(hitInfo);
        //}
        //if (touching) {
        // Debug.Log("dropped on " + touching);
        //GetComponent<PlantComponent>().GraftTo(touching.GetComponent<PlantComponent>());
        //}
    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        //dragging = false;
        //selected = false;
        //RaycastHit hitInfo;
        //// For dragging onto worktable only (8 is worktable layer)
        //if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 5, 1 << 8)) {
        //    SnapToWorktable(hitInfo);
        //}
        //if (touching) {
        // Debug.Log("dropped on " + touching);
        //GetComponent<PlantComponent>().GraftTo(touching.GetComponent<PlantComponent>());
        //}
   }

    //void OnTriggerEnter(Collider other) {
    //    if (selected) {
    //        touching = other.gameObject;
    //    }
    //}

    //void OnTriggerExit(Collider other) {
    //    if (selected) {
    //        touching = null;
    //    }
    //}
}