using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Vector3Serializable {
    public int x, y, z;

    public Vector3Serializable(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 ToVector3() {
        return new Vector3(x, y, z);
    }
}

