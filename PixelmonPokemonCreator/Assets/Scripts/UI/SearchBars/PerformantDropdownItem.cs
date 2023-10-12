using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformantDropdownItem : MonoBehaviour
{
    public TMPro.TMP_Text textItem;
    public PerformantLargeDropdown dropdownParent;

	public void Select()
	{
		dropdownParent.SelectOption(textItem.text);
	}
}
