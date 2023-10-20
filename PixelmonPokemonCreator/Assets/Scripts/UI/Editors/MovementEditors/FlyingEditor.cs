using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEditor : MonoBehaviour
{
	[SerializeField] TMPro.TMP_InputField minHeightInput;
	[SerializeField] TMPro.TMP_InputField maxHeightInput;
	[SerializeField] TMPro.TMP_InputField speedInput;
	[SerializeField] TMPro.TMP_InputField refreshYInput;
	[SerializeField] TMPro.TMP_InputField refreshXZInput;
	[SerializeField] TMPro.TMP_InputField refreshSpeedInput;
	[SerializeField] TMPro.TMP_InputField minTimeInput;
	[SerializeField] TMPro.TMP_InputField maxTimeInput;
	[SerializeField] TMPro.TMP_InputField flapRateInput;
	[SerializeField] TMPro.TMP_Dropdown landingMaterialDropdown;

	Form selectedform;
	bool updating = false;

	public void UpdateForm()
	{
		if (selectedform == null || selectedform.movement == null || updating)
			return;

		if (!selectedform.movement.canFly)
		{
			// IDK
		}

		selectedform.movement.flyingParameters = new Flyingparameters()
		{
			flyHeightMin = minHeightInput.text.ToIntegerOrNegativeOne(),
			flyHeightMax = maxHeightInput.text.ToIntegerOrNegativeOne(),
			flySpeedModifier = speedInput.text.ToFloatOrNegativeOne(),
			flyRefreshRateY = refreshYInput.text.ToIntegerOrNegativeOne(),
			flyRefreshRateXZ = refreshXZInput.text.ToIntegerOrNegativeOne(),
			flyRefreshRateSpeed = refreshSpeedInput.text.ToIntegerOrNegativeOne(),
			flightTimeMin = minTimeInput.text.ToIntegerOrNegativeOne(),
			flightTimeMax = maxTimeInput.text.ToIntegerOrNegativeOne(),
			landingMaterials = landingMaterialDropdown.options[landingMaterialDropdown.value].text
		};

	}

	void UpdateContent()
	{
		updating = true;

		if (selectedform == null || selectedform.movement == null || selectedform.movement.flyingParameters == null)
			return;

		Flyingparameters flyParams = selectedform.movement.flyingParameters;

		minHeightInput.text = flyParams.flyHeightMin.ToString();
		maxHeightInput.text = flyParams.flyHeightMax.ToString();
		speedInput.text = flyParams.flySpeedModifier.ToString();
		refreshYInput.text = flyParams.flyRefreshRateY.ToString();
		refreshXZInput.text = flyParams.flyRefreshRateXZ.ToString();
		refreshSpeedInput.text = flyParams.flyRefreshRateSpeed.ToString();
		minTimeInput.text = flyParams.flightTimeMin.ToString();
		maxTimeInput.text = flyParams.flightTimeMax.ToString();
		flapRateInput.text = flyParams.flapRate.ToString();
		landingMaterialDropdown.SetDropdownToStringValue(flyParams.landingMaterials);

		updating = false;
	}

	void SetForm(Form f) {
		selectedform = f;
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
