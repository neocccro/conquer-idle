using System;

public class Resource {
	private double resourceAmount = 0;

	public double Amount{ 
		get { return resourceAmount; } 
		set {
			resourceAmount = value;
			if(OnValueChange != null)
				OnValueChange();
		}
	}
	public event Action OnValueChange;
}