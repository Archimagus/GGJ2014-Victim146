using UnityEngine;
using UnityEditor;

public class CustomMenuItems : MonoBehaviour
{
	[MenuItem("GameObject/RotateLeft %q")]
	static void RotateLeft () 
	{
		Selection.activeTransform.Rotate(0, -90 - (Selection.activeTransform.eulerAngles.y % 90), 0);
	}
	[MenuItem("GameObject/RotateLeft %q", true)]
	static bool ValidateRotateLeft()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}
	[MenuItem("GameObject/RotateRight %e")]
	static void RotateRight()
	{
		Selection.activeTransform.Rotate(0, 90 - (Selection.activeTransform.eulerAngles.y % 90), 0);
	}
	[MenuItem("GameObject/RotateRight %e", true)]
	static bool ValidateRotateRight()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}
	[MenuItem("GameObject/Zero Y %w")]
	static void ZeroY()
	{
		Vector3 pos = Selection.activeTransform.position;
		pos.y = 0;
		Selection.activeTransform.position = pos;
	}
	[MenuItem("GameObject/Zero Y %w", true)]
	static bool ValidateZeroY()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}

	[MenuItem("CONTEXT/Transform/Create Empty Child")]
	static void ContextCreateEmptyChild(MenuCommand command)
	{
		var go = new GameObject();
		go.transform.parent = command.context as Transform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
	}

	[MenuItem("GameObject/Create Empty Child &#c")]
	static void CreateEmptyChild()
	{
		var go = new GameObject();
		go.transform.parent = Selection.activeTransform;
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
	}
	[MenuItem("GameObject/Create Empty Child &#c", true)]
	static bool ValidateCreateEmptyChild()
	{
		// Return false if no transform is selected.
		return Selection.activeTransform != null;
	}
}
