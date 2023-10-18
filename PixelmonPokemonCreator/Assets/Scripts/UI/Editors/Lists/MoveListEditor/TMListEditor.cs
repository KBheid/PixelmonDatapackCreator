using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TMListEditor : MonoBehaviour
{

	[SerializeField] TMMoveListEditor tutorMovesEditor;
	[SerializeField] TMMoveListEditor eggMovesEditor;
	[SerializeField] TMMoveListEditor transferMovesEditor;
	[SerializeField] TMMoveListEditor hmMovesEditor;
	[SerializeField] TMMoveListEditor trMovesEditor;
	[SerializeField] TMMoveListEditor tmMoves1Editor;
	[SerializeField] TMMoveListEditor tmMoves2Editor;
	[SerializeField] TMMoveListEditor tmMoves3Editor;
	[SerializeField] TMMoveListEditor tmMoves4Editor;
	[SerializeField] TMMoveListEditor tmMoves5Editor;
	[SerializeField] TMMoveListEditor tmMoves6Editor;
	[SerializeField] TMMoveListEditor tmMoves7Editor;
	[SerializeField] TMMoveListEditor tmMoves8Editor;
	[SerializeField] TMMoveListEditor tmMoves9Editor;
    [SerializeField] TMMoveListEditor tmMovesEditor;

	Form selectedForm;


    internal void UpdateContentOf(TMMoveListEditor tMMoveListEditor, string[] vs)
    {
		if (tMMoveListEditor == tutorMovesEditor)		selectedForm.moves.tutorMoves = vs;
		if (tMMoveListEditor == eggMovesEditor)			selectedForm.moves.eggMoves = vs;
		if (tMMoveListEditor == transferMovesEditor)	selectedForm.moves.transferMoves = vs;
		if (tMMoveListEditor == hmMovesEditor)			selectedForm.moves.hmMoves = vs;
		if (tMMoveListEditor == trMovesEditor)			selectedForm.moves.trMoves = vs;
		if (tMMoveListEditor == tmMoves1Editor)			selectedForm.moves.tmMoves1 = vs;
		if (tMMoveListEditor == tmMoves2Editor)			selectedForm.moves.tmMoves2 = vs;
		if (tMMoveListEditor == tmMoves3Editor)			selectedForm.moves.tmMoves3 = vs;
		if (tMMoveListEditor == tmMoves4Editor)			selectedForm.moves.tmMoves4 = vs;
		if (tMMoveListEditor == tmMoves5Editor)			selectedForm.moves.tmMoves5 = vs;
		if (tMMoveListEditor == tmMoves6Editor)			selectedForm.moves.tmMoves6 = vs;
		if (tMMoveListEditor == tmMoves7Editor)			selectedForm.moves.tmMoves7 = vs;
		if (tMMoveListEditor == tmMoves8Editor)			selectedForm.moves.tmMoves8 = vs;
		if (tMMoveListEditor == tmMoves9Editor)			selectedForm.moves.tmMoves9 = vs;
		if (tMMoveListEditor == tmMovesEditor)			selectedForm.moves.tmMoves = vs;
	}

    private void Start()
    {
		tutorMovesEditor.editor = this;
		eggMovesEditor.editor = this;
		transferMovesEditor.editor = this;
		hmMovesEditor.editor = this;
		trMovesEditor.editor = this;
		tmMoves1Editor.editor = this;
		tmMoves2Editor.editor = this;
		tmMoves3Editor.editor = this;
		tmMoves4Editor.editor = this;
		tmMoves5Editor.editor = this;
		tmMoves6Editor.editor = this;
		tmMoves7Editor.editor = this;
		tmMoves8Editor.editor = this;
		tmMoves9Editor.editor = this;
		tmMovesEditor.editor = this;
	}

	void UpdateContent()
	{
		if (selectedForm == null || selectedForm.moves == null)
			return;

		tutorMovesEditor	.SetMoves(selectedForm.moves.tutorMoves);
		eggMovesEditor		.SetMoves(selectedForm.moves.eggMoves);
		transferMovesEditor	.SetMoves(selectedForm.moves.transferMoves);
		hmMovesEditor		.SetMoves(selectedForm.moves.hmMoves);
		trMovesEditor		.SetMoves(selectedForm.moves.trMoves);
		tmMoves1Editor		.SetMoves(selectedForm.moves.tmMoves1);
		tmMoves2Editor		.SetMoves(selectedForm.moves.tmMoves2);
		tmMoves3Editor		.SetMoves(selectedForm.moves.tmMoves3);
		tmMoves4Editor		.SetMoves(selectedForm.moves.tmMoves4);
		tmMoves5Editor		.SetMoves(selectedForm.moves.tmMoves5);
		tmMoves6Editor		.SetMoves(selectedForm.moves.tmMoves6);
		tmMoves7Editor		.SetMoves(selectedForm.moves.tmMoves7);
		tmMoves8Editor		.SetMoves(selectedForm.moves.tmMoves8);
		tmMoves9Editor		.SetMoves(selectedForm.moves.tmMoves9);
		tmMovesEditor		.SetMoves(selectedForm.moves.tmMoves);
	}

	private void SetForm(Form f)
    {
		selectedForm = f;
		UpdateContent();
    }

    private void OnEnable()
    {
		PokemonManager.OnPokemonFormSwitched += SetForm;

		SetForm(PokemonManager.instance.selectedForm);
    }

    private void OnDisable()
	{
		PokemonManager.OnPokemonFormSwitched -= SetForm;
	}

}

