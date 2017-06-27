using UnityEngine;
using System.Collections;

public class BackGroundScroller : MonoBehaviour {

    MeshRenderer mr;
    Material mat;
    public float speed;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }


	void Update () {

        Vector2 offset = mat.mainTextureOffset;

        offset.y = Time.time / speed;

        mat.mainTextureOffset = offset;
	
	}
}
