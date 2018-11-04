using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour {

	[SerializeField]
	private GameObject defaultResourceUI;

	[SerializeField]
	private CustomUIPrefab[] customPrefabs;
	private Dictionary<ResourceType, GameObject> customPrefabDict = new Dictionary<ResourceType, GameObject>();

	private void Awake() {
		AddCustomPrefabdInDict();
		ResourceHandler.Instance.ResourceCreated += CreateResourceUI;
	}

	private void CreateResourceUI(ResourceType type){
		GameObject prefab = defaultResourceUI;
		if(customPrefabDict.ContainsKey(type))
			prefab = customPrefabDict[type];
		
		GameObject uiResoureObject = Instantiate(prefab);
		SetResourceUI(uiResoureObject, type);
		SetUIPosition(uiResoureObject);		
		
	}

	private void SetUIPosition(GameObject obj){
		RectTransform transform = obj.transform as RectTransform;
		transform.parent = this.transform;
	}

	private void SetResourceUI(GameObject obj, ResourceType type){
		ResourceUI resourceUI = obj.GetComponent<ResourceUI>();

		if(resourceUI == null){
			Debug.LogError("Prefab for type " + type + " did not have ResourceUI component");
			return;
		}

		resourceUI.SetResource(type);
	}

	private void AddCustomPrefabdInDict(){
		foreach (CustomUIPrefab prefab in customPrefabs)
		{
			if(customPrefabDict.ContainsKey(prefab.Type)){
				Debug.LogError("Key " + prefab.Type + "appears twice in custom prefabs. Skipping second key.");
				continue;
			}

			customPrefabDict.Add(prefab.Type, prefab.CustomPrefab);
		}
	}

	[System.Serializable]//TODO should we make a serializable dictionary? 
	private class CustomUIPrefab{
		[SerializeField]
		private ResourceType type;
		[SerializeField]
		private GameObject customPrefab;

		public ResourceType Type{ get{ return type; }}
		public GameObject CustomPrefab { get { return customPrefab; }}
	}

}
