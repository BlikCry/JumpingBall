using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class SyncTextSize : MonoBehaviour
{
    [SerializeField]
    private TMP_Text syncTo;
    [SerializeField]
    private TMP_Text syncFrom;
    
    private void Update()
    {
        if (syncFrom is null || syncTo is null)
            return;
        syncTo.fontSize = syncFrom.fontSize;
    }
}
