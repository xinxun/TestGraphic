using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics06DrawMesh : MonoBehaviour
{
    public Mesh mesh;
    public bool useGPU = false;
    public Material[] meshMat;
    private Vector3[] worldPos = new Vector3[100];
    private Matrix4x4[] worldMats = new Matrix4x4[100];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < worldPos.Length; i++)
        {
            worldPos[i] = new Vector3(UnityEngine.Random.Range(-20f, 20f), UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
            worldMats[i] = Matrix4x4.identity;
            worldMats[i][0, 3] = worldPos[i].x;
            worldMats[i][1, 3] = worldPos[i].y;
            worldMats[i][2, 3] = worldPos[i].z;
            worldMats[i][3, 3] = 1;
        }
    }


    void Update()
    {
        if (useGPU)
        {
            Graphics.DrawMeshInstanced(mesh, 0, meshMat[0], worldMats, worldMats.Length);
            Graphics.DrawMeshInstanced(mesh, 1, meshMat[1], worldMats, worldMats.Length);
            Graphics.DrawMeshInstanced(mesh, 2, meshMat[2], worldMats, worldMats.Length);
        }
        else
        {
            foreach (var pos in worldPos)
            {
                Graphics.DrawMesh(mesh, pos, Quaternion.identity, meshMat[0], 0, Camera.main, 0);
                Graphics.DrawMesh(mesh, pos, Quaternion.identity, meshMat[1], 0, Camera.main, 1);
                Graphics.DrawMesh(mesh, pos, Quaternion.identity, meshMat[2], 0, Camera.main, 2);
            }
        }
    }
}

