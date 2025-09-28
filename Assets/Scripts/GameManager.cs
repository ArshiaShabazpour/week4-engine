using UnityEngine;
using UnityEngine.UI;

public class GameController2D : MonoBehaviour
{
    public GameObject[] blockPrefabs;
    public Camera mainCamera;
    public Image activeBlockImage; 

    private CommandManager cmdManager;
    private int currentBlockIndex = 0;

    void Awake()
    {
        cmdManager = gameObject.AddComponent<CommandManager>();
        UpdateUI();
    }

    void Update()
    {
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentBlockIndex = i;
                UpdateUI();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            float size = DLLConfigLoader.IsLoaded ? DLLConfigLoader.BlockSize : 1f;
            var cmd = new Command(blockPrefabs[currentBlockIndex], worldPos, size);
            cmdManager.ExecuteCommand(cmd);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            cmdManager.Undo();
        }
    }

    void UpdateUI()
    {
        if (activeBlockImage == null) return;

        Color swatch = Color.white;
        string source = "none";

        var prefab = blockPrefabs[currentBlockIndex];
        if (prefab != null)
        {
            var sr = prefab.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                swatch = sr.color;
                source = "SpriteRenderer.color";
            }
            else
            {
                var childSR = prefab.GetComponentInChildren<SpriteRenderer>();
                if (childSR != null)
                {
                    swatch = childSR.color;
                    source = "ChildSpriteRenderer.color";
                }
            }
        }

        swatch.a = 1f;
        activeBlockImage.color = swatch;

        Debug.Log($"UpdateUI -> index={currentBlockIndex}, source={source}, color={swatch}");
    }

}
