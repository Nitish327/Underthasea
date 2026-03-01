using UnityEngine;

public class AddMeshCollidersToChildren : MonoBehaviour
{
    [Header("Settings")]
    public bool makeConvex = false;     // Set true if needed (e.g., for Rigidbody use)
    public bool runOnStart = true;      // Automatically run on Start

    void Start()
    {
        if (runOnStart)
            AddColliders();
    }

    [ContextMenu("Add Mesh Colliders To Children")]
    public void AddColliders()
    {
        // Get all MeshFilters in children (including inactive)
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(true);

        foreach (MeshFilter mf in meshFilters)
        {
            GameObject obj = mf.gameObject;

            // Skip if it already has a MeshCollider
            if (obj.GetComponent<MeshCollider>() == null)
            {
                MeshCollider collider = obj.AddComponent<MeshCollider>();
                collider.convex = makeConvex;  // Important: large cave meshes usually false
            }
        }

        Debug.Log("MeshColliders added to all children.");
    }
}