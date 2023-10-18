using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMMoveListEditor : MonoBehaviour, IMoveListEditor
{
    public TMListEditor editor;

    public TMPro.TMP_InputField moveInput;
    public Transform moveListHolder;
    public MoveListItem moveListItemPrefab;

    private MoveListItem selectedItem;
    private List<string> currentMoves = new List<string>();
    

    public void AddMove()
    {
        string moveName = moveInput.text;
        if (moveName == "")
            return;

        AddMove(moveName);
    }

    void AddMove(string moveName)
    {
        MoveListItem newItem = Instantiate(moveListItemPrefab, moveListHolder);
        newItem.editor = this;
        newItem.textItem.text = moveName;
        currentMoves.Add(moveName);

        UpdateForm();
    }

    internal void SetMoves(string[] moves)
    {
        Clear();

        if (moves == null)
            return;

        foreach (string move in moves)
        {
            AddMove(move);
        }
    }

    public void SelectMoveListItem(MoveListItem item)
    {
        selectedItem = item;
    }

    public void RemoveMove()
    {
        if (selectedItem == null)
            return;

        currentMoves.Remove(selectedItem.textItem.text);
        Destroy(selectedItem.gameObject);
        UpdateForm();
    }

    void UpdateForm()
    {
        editor.UpdateContentOf(this, currentMoves.ToArray());
    }

    void Clear()
    {
        foreach (Transform t in moveListHolder)
        {
            Destroy(t.gameObject);
        }
        currentMoves.Clear();
    }
}
