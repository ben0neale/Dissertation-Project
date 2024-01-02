using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    MeshRenderer meshRend;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();

        mat = meshRend.material;
        mat.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
    }

}
