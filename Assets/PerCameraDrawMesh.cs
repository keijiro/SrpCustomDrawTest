using UnityEngine;
using UnityEngine.Experimental.Rendering;

[ExecuteInEditMode]
public class PerCameraDrawMesh : MonoBehaviour
{
    [SerializeField] Camera _targetCamera = null;
    [SerializeField] Mesh _mesh = null;
    [SerializeField] Material _material = null;

    bool _initialized;

    void OnDisable()
    {
        if (_initialized)
        {
            RenderPipeline.beginCameraRendering -= OnBeginCameraRendering; // SRP
            Camera.onPreCull -= OnBeginCameraRendering; // Legacy
            _initialized = false;
        }
    }

    void Update()
    {
        if (!_initialized)
        {
            RenderPipeline.beginCameraRendering += OnBeginCameraRendering; // SRP
            Camera.onPreCull += OnBeginCameraRendering; // Legacy
            _initialized = true;
        }
    }

    void OnBeginCameraRendering(Camera camera)
    {
        if (camera == _targetCamera && _mesh != null && _material != null)
            Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, _material, gameObject.layer, camera);
    }
}
