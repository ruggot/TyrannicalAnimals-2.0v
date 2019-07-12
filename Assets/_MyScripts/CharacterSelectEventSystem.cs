using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelectEventSystem : EventSystem
{
    public int lastSelectionSet { get; private set; }
    public int lastConfirmChanged { get; private set; }

    Selectable[] currentSelectedObjects = new Selectable[2];
    bool[] selectionConfirmed = new bool[2];

    public void SetSelected(int index, Selectable selected) {
        currentSelectedObjects[index] = selected;
        lastSelectionSet = index;
    }

    public Selectable GetSelected(int index) {
        return currentSelectedObjects[index];
    }

    public void ConfirmSelection(int index) {
        selectionConfirmed[index] = true;
        lastConfirmChanged = index;
    }

    public void CancelSelection(int index) {
        selectionConfirmed[index] = false;
        lastConfirmChanged = index;
    }

    public bool ConfirmedSelection(int index) {
        return selectionConfirmed[index];
    }
}
