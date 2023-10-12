using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeListItem : MonoBehaviour
{
	public TypeSelectionEditor editor;
	public TMPro.TMP_Text textItem;

	public void OnClick()
	{
		editor.SelectType(this);
	}
}
