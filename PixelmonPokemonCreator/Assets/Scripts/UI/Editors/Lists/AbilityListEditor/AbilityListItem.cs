using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityListItem : MonoBehaviour
{
	public TMPro.TMP_Text textItem;
	public AbilityListEditor editor;

	public void Select()
	{
		editor.SelectAbility(this);
	}
}
