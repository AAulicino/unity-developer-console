using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDeveloperConsole
{
	public class HintBoxUI : MonoBehaviour
	{
		List<Text> hintPool = new List<Text>();

		GameObject template;
		int hintCount;
		int selectionIndex;

		public bool Enabled
		{
			get { return gameObject.activeSelf; }
			set
			{
				gameObject.SetActive(value);
				if(value)
					selectionIndex = 0;
			}
		}

		void Awake ()
		{
			template = transform.GetChild(0).gameObject;
		}

		public void DisplayHint (string input)
		{
			if (string.IsNullOrEmpty(input))
				Display(CommandSuggestionsHandler.GetInputHistory());
			else
				Display(CommandSuggestionsHandler.GetSuggestions(input));
		}

		public void MoveSelectionUp ()
		{
			if (selectionIndex < hintPool.Count)
				selectionIndex++;
		}

		public void MoveSelectionDown ()
		{
			if (selectionIndex > 0)
				selectionIndex++;
		}

		public string GetSelectedSuggestion ()
		{
			Enabled = false;

			if (selectionIndex >= hintCount)
				return "";

			return hintPool[selectionIndex].text;
		}

		void Display (string[] hints)
		{
			hintCount = hints.Length;

			while (hintCount > hintPool.Count)
				hintPool.Add(Instantiate(template, transform).GetComponent<Text>());

			for (int i = 0; i < hintPool.Count; i++)
			{
				if (i < hintCount)
				{
					hintPool[i].gameObject.SetActive(true);
					hintPool[i].text = hints[i];
				}
				else
					hintPool[i].gameObject.SetActive(false);
			}

			Enabled = true;
		}
	}
}