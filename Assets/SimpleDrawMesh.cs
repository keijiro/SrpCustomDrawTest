using UnityEngine;

[ExecuteInEditMode]
public class SimpleDrawMesh : MonoBehaviour
{
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;

    void Update()
    {
        if (_mesh != null && _material != null)
            Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, _material, gameObject.layer);
    }
}
