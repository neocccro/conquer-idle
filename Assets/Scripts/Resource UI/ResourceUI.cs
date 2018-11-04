using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {

	[SerializeField]
	private Text nameText, valueText;

	private ResourceType type;
	private Resource resource;


	public virtual void SetResource(ResourceType type){
		this.type = type;

		if(resource != null)
			resource.OnValueChange -= SetValueText;

		resource = ResourceHandler.Instance.GetResource(type);
		resource.OnValueChange += SetValueText;

		SetNameText();
		SetValueText();
	}

	protected virtual void SetNameText(){
		nameText.text = type.ToString() + ":";//TODO create scriptableobject with custom texts
	}

	protected virtual void SetValueText(){
		valueText.text = resource.Amount.ToString();
	}

}
