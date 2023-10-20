using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingOffsetEditor : MonoBehaviour
{
	[SerializeField] TMPro.TMP_InputField standingXInput;
	[SerializeField] TMPro.TMP_InputField standingYInput;
	[SerializeField] TMPro.TMP_InputField standingZInput;
	[SerializeField] TMPro.TMP_InputField ridingXInput;
	[SerializeField] TMPro.TMP_InputField ridingYInput;
	[SerializeField] TMPro.TMP_InputField ridingZInput;

	Form selectedForm;
	bool updating = false;

	void UpdateContent()
	{
		if (selectedForm == null || updating)
			return;

		updating = true;

		bool rideable = selectedForm.movement.rideable;

		standingXInput.interactable = rideable;
		standingYInput.interactable = rideable;
		standingZInput.interactable = rideable;
		ridingXInput.interactable	= rideable;
		ridingYInput.interactable	= rideable;
		ridingZInput.interactable	= rideable;
		
		// If null, empty out
		if (selectedForm.movement == null || selectedForm.movement.ridingOffsets == null || 
			selectedForm.movement.ridingOffsets.standing == null || 
			selectedForm.movement.ridingOffsets.moving == null)
		{
			standingXInput.text = "";
			standingYInput.text = "";
			standingZInput.text = "";

			ridingXInput.text = "";
			ridingYInput.text = "";
			ridingZInput.text = "";
		}
		else
		{
			standingXInput.text = selectedForm.movement.ridingOffsets.standing.x.ToString();
			standingYInput.text = selectedForm.movement.ridingOffsets.standing.y.ToString();
			standingZInput.text = selectedForm.movement.ridingOffsets.standing.z.ToString();

			ridingXInput.text = selectedForm.movement.ridingOffsets.moving.x.ToString();
			ridingYInput.text = selectedForm.movement.ridingOffsets.moving.y.ToString();
			ridingZInput.text = selectedForm.movement.ridingOffsets.moving.z.ToString();
		}



		updating = false;
	}

	public void UpdateForm()
	{
		if (selectedForm == null || updating)
			return;

		if (selectedForm.movement == null)
			selectedForm.movement = new Movement();


		// If we have all unset values, set to null
		if (AllEmpty())
		{
			selectedForm.movement.ridingOffsets = null;
			return;
		}


		if (selectedForm.movement.ridingOffsets == null)
		{
			selectedForm.movement.ridingOffsets = new Ridingoffsets();
		}

		selectedForm.movement.ridingOffsets.standing = new Standing()
		{
			x = standingXInput.text.ToFloatOrNegativeOne(),
			y = standingYInput.text.ToFloatOrNegativeOne(),
			z = standingZInput.text.ToFloatOrNegativeOne()
		};

		selectedForm.movement.ridingOffsets.moving = new Moving()
		{
			x = ridingXInput.text.ToFloatOrNegativeOne(),
			y = ridingYInput.text.ToFloatOrNegativeOne(),
			z = ridingZInput.text.ToFloatOrNegativeOne()
		};

		//PokemonManager.instance.SelectForm(selectedForm);
	}

	bool AllEmpty()
	{
		string value = string.Concat(
			standingXInput.text,
			standingYInput.text,
			standingZInput.text,
			ridingXInput.text,
			ridingYInput.text,
			ridingZInput.text);

		return value == "";
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
