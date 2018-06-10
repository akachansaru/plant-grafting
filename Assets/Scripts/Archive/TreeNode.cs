//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//public delegate void TreeVisitor(PlantComponent nodeData, Transform parent);

//[Serializable]
//public class TreeNode : ISerializationCallbackReceiver {
//    public PlantComponent plantComponent;
//    public List<TreeNode> children;
//    private List<string> childrenIDs; // For serializing
//    //public PlantComponent parent;

//    public TreeNode(PlantComponent plantComponent) {
//        this.plantComponent = plantComponent;
//        children = new List<TreeNode>();
//    }

//    public void AddChild(PlantComponent plantComponent) {
//        TreeNode newNode = new TreeNode(plantComponent);
//        //newNode.parent = this.plantComponent;
//        plantComponent.parent = this.plantComponent;
//        children.Add(newNode);
//    }

//    public PlantComponent GetData() {
//        return plantComponent;
//    }

//    //public PlantComponent GetParent() {
//    //    return parent;
//    //}

//    public TreeNode GetChild(int i) {
//        //foreach (TreeNode<T> n in children)
//        //    if (--i == 0)
//        //        return n;
//        return children.ElementAt(i);
//        //return null;
//    }

//    public TreeNode FindChild(TreeNode root, string ID) {
//        if (root.GetData().GetID() == ID) {
//            Debug.Log("Found child with ID " + ID);
//            return root;
//        }
//        foreach (TreeNode child in root.children) {
//            Debug.Log("Looking for child with ID " + ID);
//            return FindChild(child, ID);
//        }
//        Debug.Log("Couldn't find child with ID " + ID);
//        return null;
//    }

//    public static void Traverse(TreeNode root, Transform parent, TreeVisitor visitor) {
//        // Maybe I should ditch parent in here and figure out a way to have nodes and GOs linked up
//        Debug.Log("root: " + root);
//        Debug.Log("root.plantcomp: " + root.plantComponent);
//        visitor(root.plantComponent, parent);
//        //Debug.Log("Parent = " + parent.name);
//        foreach (TreeNode child in root.children) {
//            Traverse(child, parent, visitor);
//            //Debug.Log("Parent = " + parent.name);
//        }
//    }

//    //private List<string> Flatten(List<TreeNode> nodes) {
//    //   // TreeVisitor instantiateVisitor = InstantiateComponent;
//    //}

//    public void OnBeforeSerialize() {
//        Debug.Log("OnBeforeSerialzie");
        
//    }

//    public void OnAfterDeserialize() {
//        Debug.Log("OnAfterSerialzie");
//        throw new NotImplementedException();
//    }
//}
