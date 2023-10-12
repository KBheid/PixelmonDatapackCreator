using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyFormListItem : MonoBehaviour
{
	public AnyFormSelectionEditor editor;
	public TMPro.TMP_Text textItem;

	public void OnClick()
	{
		editor.SelectForm(this);
	}
}
