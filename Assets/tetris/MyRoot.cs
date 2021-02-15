using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace strange.examples.myfirstproject
{
	public class MyRoot : ContextView
	{
	
		void Awake()
		{
			context = new MyContext(this);
		}
	}
}

