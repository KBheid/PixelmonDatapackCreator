using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class PokemonSearchBar : MonoBehaviour
{
	public UnityEvent ItemSelected;

	public TMPro.TMP_InputField inputField;
	public PerformantLargeDropdown dropdown;

	private bool fixInputFieldPosition = false;

	private Task<List<string>> currentTask;
	private CancellationTokenSource cancelSource = new CancellationTokenSource();

	public void SearchDeselected()
	{
		if (currentTask != null)
		{
			cancelSource.Cancel();
			currentTask = null;
		}
		StartCoroutine(nameof(DelayDropdownClose));
	}

	public void SearchUpdated()
	{
		dropdown.Hide();
		string searchString = inputField.text.ToLower();

		dropdown.ClearOptions();

		if (currentTask != null)
		{
			cancelSource.Cancel();
			currentTask = null;
		}

		if (!gameObject.activeInHierarchy)
			return;

		StartCoroutine(ProcessSearchRoutine(searchString));
		inputField.ActivateInputField();
	}

	IEnumerator ProcessSearchRoutine(string searchString)
	{
		currentTask = new Task<List<string>>(() => {
			var valid = PokemonManager.instance.pokemon.Where(pkmn => pkmn.name.ToLower().Contains(searchString));
			var pokeList = new List<Pokemon>(valid);
			return pokeList.ConvertAll(x => x.name);
		});
		currentTask.Start();

		bool cancelled = false;
		while (!cancelled && currentTask.IsCompleted)
		{
			cancelled = cancelSource.IsCancellationRequested;

			yield return new WaitForEndOfFrame();
		}

		if (!cancelled)
		{
			SetDropdownOptions(currentTask.Result);
		}

		currentTask = null;
		dropdown.Show();
	}

	void SetDropdownOptions(List<string> options)
	{
		dropdown.SetOptions(options);
		RefreshOptions();
		inputField.ActivateInputField();
		fixInputFieldPosition = true;
	}

	public void OptionSelected()
	{
		inputField.text = dropdown.selectedItem;
		ItemSelected.Invoke();
		dropdown.Hide();
	}

	private void RefreshOptions()
	{
		dropdown.enabled = false;
		dropdown.enabled = true;
		dropdown.Show();
	}

	private void LateUpdate()
	{
		if (fixInputFieldPosition)
		{
			inputField.MoveTextEnd(true);
			fixInputFieldPosition = false;
		}
	}

	IEnumerator DelayDropdownClose()
	{
		yield return new WaitForSeconds(0.1f);
		dropdown.Hide();
	}
}
