using System;
using System.Collections.Generic;

public class ResourceHandler {

	private static ResourceHandler instance;
	public static ResourceHandler Instance{
		get{
			if(instance == null)
				instance = new ResourceHandler();
			
			return instance;
		}
	}

	public event Action<ResourceType> ResourceCreated;
	private Dictionary<ResourceType, Resource> resources = new Dictionary<ResourceType, Resource>();
	
	public Resource GetResource(ResourceType resource){
		if(!resources.ContainsKey(resource)){
			resources.Add(resource, new Resource());
			if(ResourceCreated != null)
				ResourceCreated(resource);
		}

		return resources[resource];
	}
	 
}


