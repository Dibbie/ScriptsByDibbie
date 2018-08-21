using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class CollapsableUI : MonoBehaviour, IPointerClickHandler {

    //Place this script on any UI parent object that contains the following hierarchy:
    //Parent object (Text, Button, Checkbox, etc...)
    //Child Panel (contains all UI elements that should be controlled by the parent object)
    //
    //Make sure the title and arrow both have "Raycast Target" unchecked, for easier use.

    public Text title;
    public Image arrow;
    public GameObject content;

    public bool collapsed;

    [Space]

    public UnityEvent onStateChanged;

    void Start() { ChangeState(collapsed); }

	public void ChangeState()
    {
        collapsed = !collapsed;
        content.SetActive(!collapsed); //you can set an animation to change states instead of toggle if you prefer
        Vector3 flip = arrow.rectTransform.localScale;
        flip.x *= -1f;
        arrow.rectTransform.localScale = flip;

        title.fontStyle = (!collapsed) ? FontStyle.Normal : FontStyle.Bold;
        if(onStateChanged != null) { onStateChanged.Invoke(); }
    }

    void ChangeState(bool condition)
    {
        collapsed = condition;
        content.SetActive(!collapsed); //you can set an animation to change states instead of toggle if you prefer
        Vector3 flip = arrow.rectTransform.localScale;
        flip.x *= -1f;
        arrow.rectTransform.localScale = flip;

        title.fontStyle = (!collapsed) ? FontStyle.Normal : FontStyle.Bold;
        if (onStateChanged != null) { onStateChanged.Invoke(); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeState(!collapsed);
    }
}
