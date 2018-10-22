using UnityEngine;
using UnityEngine.Experimental.Rendering;

[ExecuteInEditMode]
public class FullScreenMeshDraw : MonoBehaviour
{
    [SerializeField] Shader _shader = null;
    [SerializeField] Texture _texture = null;

    bool _initialized;
    Mesh _mesh;
    Material _material;

    void OnDisable()
    {
        if (_initialized)
        {
            RenderPipeline.beginCameraRendering -= OnBeginCameraRendering; // SRP
            Camera.onPreCull -= OnBeginCameraRendering; // Legacy

            if (Application.isPlaying)
            {
                Destroy(_mesh);
                Destroy(_material);
            }
            else
            {
                DestroyImmediate(_mesh);
                DestroyImmediate(_material);
            }

            _initialized = false;
        }
    }

    void Update()
    {
        if (!_initialized && _shader != null)
        {
            RenderPipeline.beginCameraRendering += OnBeginCameraRendering; // SRP
            Camera.onPreCull += OnBeginCameraRendering; // Legacy

            _mesh = new Mesh();
            _mesh.hideFlags = HideFlags.DontSave;
            _mesh.vertices = new Vector3[3];
            _mesh.triangles = new int [] { 0, 1, 2 };
            _mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
            _mesh.UploadMeshData(true);

            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;

            _initialized = true;
        }

        if (_material != null)
            _material.SetTexture("_MainTex", _texture);
    }

    void OnBeginCameraRendering(Camera camera)
    {
        if (_initialized && camera == GetComponent<Camera>())
            Graphics.DrawMesh(_mesh, transform.localToWorldMatrix, _material, gameObject.layer, camera);
    }
}
