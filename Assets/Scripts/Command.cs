using UnityEngine;

public class Command : ICommand
{
    private SpawnedObject spawned;
    private GameObject prefab;
    private Vector3 position;
    private float size = 1f;

    public Command(GameObject prefab, Vector3 pos, float size = 1f)
    {
        this.prefab = prefab;
        this.position = pos;
        this.size = size;
    }

    public void Execute()
    {
        spawned = new SpawnedObject(prefab, position);
        if (spawned.Instance != null)
        {
            spawned.Instance.transform.localScale = new Vector3(size, size, 1f);
        }
    }

    public void Undo()
    {
        spawned?.Destroy();
    }
}
