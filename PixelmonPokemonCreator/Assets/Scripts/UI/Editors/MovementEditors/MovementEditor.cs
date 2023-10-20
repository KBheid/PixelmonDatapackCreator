using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementEditor : MonoBehaviour
{
	[SerializeField] Toggle rideableToggle;
	[SerializeField] Toggle flyToggle;
	[SerializeField] Toggle surfToggle;
	[SerializeField] Toggle rideShoulderToggle;

	Form selectedForm;
	bool updating = false;

	void UpdateContent()
	{
		updating = true;

		if (selectedForm == null)
			return;

		rideableToggle.isOn = selectedForm.movement.rideable;
		flyToggle.isOn = selectedForm.movement.canFly;
		surfToggle.isOn = selectedForm.movement.canSurf;
		rideShoulderToggle.isOn = selectedForm.movement.canRideShoulder;

		updating = false;
	}

	public void UpdateForm()
	{
		if (selectedForm == null || updating)
			return;

		bool anyValueSet = rideShoulderToggle.isOn || flyToggle.isOn || surfToggle.isOn || rideShoulderToggle.isOn;

		if (!anyValueSet)
			return;

		if (selectedForm.movement == null)
			selectedForm.movement = new Movement();

		selectedForm.movement.rideable = rideableToggle.isOn;
		selectedForm.movement.canFly = flyToggle.isOn;
		selectedForm.movement.canSurf = surfToggle.isOn;
		selectedForm.movement.canRideShoulder = rideShoulderToggle.isOn;

		// Propogate changes
		PokemonManager.instance.SelectForm(selectedForm);
	}

	void SetForm(Form f)
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
