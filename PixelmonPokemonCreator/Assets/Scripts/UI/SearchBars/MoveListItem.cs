using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveListItem : MonoBehaviour
{
	public TMPro.TMP_Text textItem;
	public IMoveListEditor editor;

	public void Select()
	{
		editor.SelectMoveListItem(this);
	}
}
