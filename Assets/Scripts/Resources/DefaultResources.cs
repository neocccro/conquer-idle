using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultResources : MonoBehaviour {

	[SerializeField]
	private ResourceStartAmount[] startAmounts;

	private void Start() {
		ResourceHandler resourceHandler = ResourceHandler.Instance;
		foreach (ResourceStartAmount startAmount in startAmounts)
		{
			resourceHandler.GetResource(startAmount.Type).Amount = startAmount.StartAmount;
		}
	}

	[System.Serializable]//TODO should we make a serializable dictionary? 
	private class ResourceStartAmount{
		[SerializeField]
		private ResourceType type;
		[SerializeField]
		private double startAmount;

		public ResourceType Type{ get{ return type; }}
		public double StartAmount { get { return startAmount; }}
	}

}
