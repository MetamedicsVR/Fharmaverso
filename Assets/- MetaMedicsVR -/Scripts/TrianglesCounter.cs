using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglesCounter : MonoBehaviour
{
    public int triangles = 0;

    private void Start()
    {
        triangles = CountTrianglesInChildren(transform);
    }

    private int CountTrianglesInChildren(Transform parent)
    {
        int totalTriangles = 0;
        foreach (Transform child in parent)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                MeshFilter filter = child.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                {
                    totalTriangles += filter.sharedMesh.triangles.Length / 3;
                }
            }
            SkinnedMeshRenderer skinnedMeshRenderer = child.GetComponent<SkinnedMeshRenderer>();
            if (skinnedMeshRenderer != null)
            {
                Mesh mesh = skinnedMeshRenderer.sharedMesh;
                if (mesh != null)
                {
                    totalTriangles += mesh.triangles.Length / 3;
                }
            }
            totalTriangles += CountTrianglesInChildren(child);
        }
        return totalTriangles;
    }
}