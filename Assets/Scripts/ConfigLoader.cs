using ConfigDLL;
using UnityEngine;

[DefaultExecutionOrder(-100)] 
public class DLLConfigLoader : MonoBehaviour
{
    public static bool IsLoaded { get; private set; } = false;
    public static float PlayerJumpForce { get; private set; } = 150000f;
    public static float PlayerMoveSpeed { get; private set; } = 150000f;
    public static float BlockSize { get; private set; } = 10000f;

    void Awake()
    {
        try
        {
            PlayerJumpForce = Config.PlayerJumpForce;
            PlayerMoveSpeed = Config.PlayerMoveSpeed;
            BlockSize = Config.BlockSize;
            IsLoaded = true;
        }
        catch
        {
            IsLoaded = false;
            Debug.LogWarning("DLLConfigLoader: failed to read Config from DLL; using defaults.");
        }

        Debug.Log($"DLL config loaded: Jump={PlayerJumpForce}, Speed={PlayerMoveSpeed}, BlockSize={BlockSize}");
    }
}
