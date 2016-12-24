using UnityEngine;
using System.Collections;

public class BackGroundScroller : MonoBehaviour {

    MeshRenderer mr;
    Material mat;


    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }


	void Update () {

        Vector2 offset = mat.mainTextureOffset;

        offset.y = Time.time / 10f;

        mat.mainTextureOffset = offset;
	
	}
}
