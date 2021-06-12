using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenHyperlinks : MonoBehaviour, IPointerClickHandler
{

    private TMP_Text text;
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        text = GetComponent<TMP_Text>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, mainCamera);
        if( linkIndex != -1 ) { // was a link clicked?
            var linkInfo = text.textInfo.linkInfo[linkIndex];
            
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
