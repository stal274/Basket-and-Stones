﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ComicsPage", menuName = "PageOfComics")] // выпадающее меню редактора "Create"
public class PageData : ScriptableObject
{
	// Start is called before the first frame update
	[SerializeField]
	private Sprite _page;
	public Sprite ImageGet()
	{
		return _page;
	}
}
