using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArrowSelect : MonoBehaviour
{
    public Text selectionText;

    public List<string> selections = new List<string>();

    //set the index only, in the Editor to start on that selection at runtime
    public SelectionData currentSelection;

    [Space]

    //called on each arrow navigation
    public SelectionChanged selectionChanged;

    #region Events
    [Serializable]
    public class SelectionChanged : UnityEvent<bool> { }

    /// <summary>
    /// Called whenever <seealso cref="SelectionData.SetSelected"/> is called. Subscribe to this event in Start() or Awake()
    /// <para>Requires <seealso cref="_SelectionChanged(SelectionData, int, bool)"/></para>
    /// </summary>
    public event _SelectionChanged OnSelectionChanged;

    /// <summary>
    /// Called whenever <seealso cref="SelectionData.SetSelected"/> is called, to provide details on the selection
    /// </summary>
    /// <param name="selection">Selection data containing the current index and text</param>
    /// <param name="iteration">Value of the iteration before selection data was set</param>
    /// <param name="isNext">Condition determining if the selection was a positive next or a negative previous iteration</param>
    public delegate void _SelectionChanged(SelectionData selection, int iteration, bool isNext);
    #endregion

    void Start () {
        if(currentSelection.index < 0) { currentSelection.index = 0; }

        if (selections.Count > 0)
        {
            currentSelection.text = selections[currentSelection.index];
            selectionText.text = currentSelection.text;
        }
	}

    /// <summary>
    /// Add to the ArrowSelect "Next" button event, through the Inspector
    /// </summary>
    public void SelectNext()
    {
        try
        {
            currentSelection.SetSelected(selections, +1);
            currentSelection.CallEvent(OnSelectionChanged);
            selectionText.text = currentSelection.text;
        }
        catch (Exception ex) { throw ex; }

        if(selectionChanged != null) { selectionChanged.Invoke(true); }
    }

    /// <summary>
    /// Add to the ArrowSelect "Prev" button event, through the Inspector
    /// </summary>
    public void SelectPrev()
    {
        try
        {
            currentSelection.SetSelected(selections, -1);
            currentSelection.CallEvent(OnSelectionChanged);
            selectionText.text = currentSelection.text;
        }
        catch (Exception ex) { throw ex; }

        if (selectionChanged != null) { selectionChanged.Invoke(false); }
    }

    [Serializable]
    public class SelectionData
    {
        public string text;
        public int index;

        private int iterationValue;

        /// <summary>
        /// Set the selection text and index based on passed in information. The index will automatically loop at min or max value.
        /// </summary>
        /// <param name="content">List of strings to access</param>
        /// <param name="iteration">Iteration value (+1 or -1 for example) to modify the access index of 'content'</param>
        public void SetSelected(List<string> content, int iteration)
        {
            iterationValue = iteration;

            index += iteration;
            if(index > content.Count - 1) { index = 0; } else if(index < 0) { index = content.Count - 1; }

            text = content[index];
        }

        /// <summary>
        /// Call this function to include the event delegate on iteration
        /// </summary>
        /// <param name="selection"></param>
        public void CallEvent(ArrowSelect._SelectionChanged selection)
        {
            if(selection != null) { selection.Invoke(this, iterationValue, (iterationValue > 0)); }
        }
    }
}
