using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CompositeSelectionResponse : MonoBehaviour, ISelectionResponse, IChangeable
{
    [SerializeField] private GameObject selectionResponseHolder;
    private Transform _currentSelection;
    private List<ISelectionResponse> _selectionResponses;
    private int _currentIndex;

    private void Start()
    {
        _selectionResponses = selectionResponseHolder.GetComponents<ISelectionResponse>().ToList();
    }

    [ContextMenu("Next")]
    public void Next()
    {
        _selectionResponses[_currentIndex].OnDeselect(_currentSelection);
        _currentIndex = (_currentIndex + 1) % _selectionResponses.Count;
        _selectionResponses[_currentIndex].OnSelect(_currentSelection);

    }

    public void OnSelect(Transform selection)
    {
        _currentSelection = selection;
        if (HasSelection())
            _selectionResponses[_currentIndex].OnSelect(selection);
    }

    public void OnDeselect(Transform selection)
    {
        _currentSelection = null;
        if (HasSelection())
            _selectionResponses[_currentIndex].OnDeselect(selection);
    }

    private bool HasSelection()
    {
        return _currentIndex > -1 && _currentIndex < _selectionResponses.Count;
    }
}
