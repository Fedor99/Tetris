using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class MyContext : MVCSContext
{
	public MyContext(MonoBehaviour view) : base(view)
	{
	}

	public MyContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
	{
	}
		
	protected override void mapBindings()
	{
        injectionBinder.Bind<IMatrixManager>().ToValue(new MatrixManager(10, 10)).ToSingleton();

        {
            GameObject obj = new GameObject("ShapeManager");
            ShapeManager shapeM = obj.AddComponent<ShapeManager>();
            shapeM.length = 3;
            shapeM.Initialize();
            injectionBinder.Bind<IShapeManager>().ToValue(shapeM).ToSingleton();
        }

        {
            GameObject obj = new GameObject("TouchControl");
            obj.AddComponent<TouchControl>();
            injectionBinder.Bind<ITouchControl>().ToValue(obj.GetComponent<TouchControl>()).ToSingleton();
        }

        //View/Mediator binding
        //This Binding instantiates a new MyMediator whenever as TetrisView
        //Fires its Awake method. The Mediator communicates to/from the View
        //and to/from the App. This keeps dependencies between the view and the app
        //separated.
        mediationBinder.Bind<TetrisView>().To<MyMediator>();
			
		//Event/Command binding
		//commandBinder.Bind(ExampleEvent.REQUEST_WEB_SERVICE).To<CallWebServiceCommand>();
		//The START event is fired as soon as mappings are complete.
		//Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.
		commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();
	}
}

