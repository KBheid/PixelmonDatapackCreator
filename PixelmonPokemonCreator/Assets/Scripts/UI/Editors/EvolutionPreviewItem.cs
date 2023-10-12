using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolutionPreviewItem : MonoBehaviour
{
	public RawImage image;
	public TMPro.TMP_Text text;
	public Button button;
	public Evolution evo;
	public EvolutionPreview editor;

	public void Select()
	{
		if (evo != null)
			editor.SelectEvolution(evo);
	}
}
