using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionMoveListPanel : MonoBehaviour, IMoveListEditor
{
	[SerializeField] EvolutionEditor editor;
	[SerializeField] Transform listItemHolder;
    [SerializeField] MoveListItem listItemPrefab;
	[SerializeField] Button removeButton;
	[SerializeField] TMPro.TMP_InputField moveSelector;

	Evolution selectedEvolution;
	List<string> moves;
	MoveListItem selectedMove;


	void SelectEvolution(Evolution e)
	{
		selectedEvolution = e;

		ClearList();
		PopulateList();

		moveSelector.interactable = e != null;
	}

	public void SelectMoveListItem(MoveListItem moveListItem)
	{
		selectedMove = moveListItem;

		removeButton.interactable = selectedMove != null;
	}

	// Called from button
	public void AddMove()
	{
		AddMove(moveSelector.text);

		selectedEvolution.moves = moves.ToArray();
	}
	public void AddMove(string move)
	{
		MoveListItem it = Instantiate(listItemPrefab, listItemHolder);
		it.editor = this;
		it.textItem.text = move;

		moves.Add(move);
		selectedMove = it;
	}

	public void Remove()
	{
		if (selectedMove == null)
		{
			removeButton.interactable = false;
			return;
		}

		moves.Remove(selectedMove.textItem.text);
		Destroy(selectedMove.gameObject);

		removeButton.interactable = false;

		selectedEvolution.moves = moves.ToArray();
	}

	void ClearList()
	{
		foreach (Transform t in listItemHolder)
		{
			Destroy(t.gameObject);
		}
		moves.Clear();
	}

	void PopulateList()
	{
		if (selectedEvolution == null)
			return;

		if (selectedEvolution.moves == null)
			return;

		foreach (string m in selectedEvolution.moves)
		{
			AddMove(m);
		}
	}

	private void OnEnable()
	{
		moves = new List<string>();

		SelectEvolution(editor.selectedEvolution);
		EvolutionEditor.OnEvolutionChanged += SelectEvolution;
	}

	private void OnDisable()
	{
		SelectEvolution(null);
		EvolutionEditor.OnEvolutionChanged -= SelectEvolution;
	}
}
