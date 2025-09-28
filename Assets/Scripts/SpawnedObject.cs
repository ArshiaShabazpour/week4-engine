using UnityEngine;

public class SpawnedObject
{
    public GameObject Instance { get; private set; }

    public SpawnedObject(GameObject prefab, Vector3 position)
    {
        Instance = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public void Destroy()
    {
        if (Instance != null)
            Object.Destroy(Instance);
    }
}
