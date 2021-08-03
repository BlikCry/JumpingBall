using UnityEngine;
using Yodo1.MAS;

internal class AdScript: MonoBehaviour
{
    public static AdScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Yodo1U3dMas.InitializeSdk();
    }
}