
using UnityEngine;
using UnityEngine.Rendering;


public enum CameraCmdBuffer1
{
    DRAW_RENDERER,
    DRAW_RENDERER_TARGET,
    DRAW_MESH,
    DRAW_MESH_TARGET
}

public class Graphics07CmdBufferCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Material cmdMat;
    public Renderer cubeRenderer;
    public Mesh cubRendererMesh;
    public RenderTexture target;
    public Color clearColor = Color.red;
    public int triCount = 6;
    public float radius = 5;

    private CommandBuffer cmdBuffer;
    private Mesh mesh;

    private void OnRenderObject() 
    {
        this.DrawRenderer();
       //DrawRendererToTarget();
        //DrawMesh();
        //DrawMeshToTarget();
    }
    public void DrawRenderer()
    {
        cmdBuffer.Clear();
        cmdBuffer.DrawRenderer(cubeRenderer, cmdMat);
    }

    public void DrawRendererToTarget()
    {
        cmdBuffer.Clear();
        cmdBuffer.SetRenderTarget(target);
        cmdBuffer.ClearRenderTarget(true, true, clearColor);
        cmdBuffer.DrawRenderer(cubeRenderer, cmdMat);
    }

    public void DrawMesh()
    {
        cmdBuffer.Clear();
        cmdBuffer.DrawMesh(cubRendererMesh, Matrix4x4.identity, cmdMat);
    }

    public void DrawMeshToTarget()
    {
        cmdBuffer.Clear();
        cmdBuffer.SetRenderTarget(target);
        cmdBuffer.ClearRenderTarget(true, true, clearColor);

        cmdBuffer.DrawMesh(mesh, Matrix4x4.identity, cmdMat);
    }

    private void Start()
    {
        cmdBuffer = new CommandBuffer() { name = "CameraCmdBuffer" };

        mainCamera.AddCommandBuffer(CameraEvent.AfterForwardOpaque, cmdBuffer);

        if (mesh == null)
        {
            mesh = Graphics00Mesh.Instance.GetMesh(triCount, radius);
        }

        if(cubRendererMesh == null && cubeRenderer !=null)
        {
            cubRendererMesh = cubeRenderer.GetComponent<MeshFilter>().mesh;
        }

       // DrawRenderer();
    }

    private void OnValidate()
    {
        mesh = Graphics00Mesh.Instance.GetMesh(triCount, radius);
    }

    private void OnDisable()
    {
        mainCamera.RemoveAllCommandBuffers();
    }
}