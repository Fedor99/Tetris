/// An example Command
/// ============================
/// This Command puts a new ExampleView into the scene.
/// Note how the ContextView (i.e., the GameObject our Root was attached to)
/// is injected for use.
/// 
/// All Commands must override the Execute method. The Command is automatically
/// cleaned up when Execute has completed, unless Retain is called (more on that
/// in the OpenWebPageCommand).
/// 
using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class StartCommand : EventCommand
{		
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
		
	public override void Execute()
	{
		GameObject go = new GameObject();
		go.name = "ExampleView";
		go.AddComponent<TetrisView>();
		go.transform.parent = contextView.transform;

        GameObject blockCollider = (GameObject)Resources.Load("BlockCollider");
        float offset = 2.75f;
        int number = 10;
        // Instantiate collider block matrix
        for (int x = 0; x < number; x++)
        {
            for (int y = 0; y < number; y++)
            {
                GameObject createdObject = GameObject.Instantiate(blockCollider, new Vector3(offset * x, offset * y, 5), Quaternion.Euler(0, 0, 0));
                createdObject.GetComponent<Identifier2D>().ID = new Vector2(x, y);
            }
        }
    }
}
