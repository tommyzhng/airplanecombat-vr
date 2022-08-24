//Creates an editable graph for Coefficient of Lift in relation to Angle of Attack

using UnityEngine;
[CreateAssetMenu]
public class CoefficientGraph : ScriptableObject
{
	//Set points in the graph to be editable.
	public AnimationCurve lift_Coefficient;
	public AnimationCurve drag_Coefficient;
}

