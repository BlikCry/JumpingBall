using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    [SerializeField]
    private string id;

    private static HashSet<string> initialized = new HashSet<string>();

    private void Awake()
    {
        if (initialized.Contains(id))
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        initialized.Add(id);
    }
}
