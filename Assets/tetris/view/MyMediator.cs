using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class MyMediator : EventMediator
{
	[Inject]
	public TetrisView view { get; set; }

    public override void OnRegister()
	{
        //Listen to the global event bus for events
        dispatcher.AddListener(MyEvent.MATRIX_UPDATE, view.onMatrixUpdate);

        view.init ();
    }
		
	public override void OnRemove()
	{
		//Clean up listeners when the view is about to be destroyed
        dispatcher.RemoveListener(MyEvent.MATRIX_UPDATE, view.onMatrixUpdate);

        Debug.Log("Mediator OnRemove");
	}
}
