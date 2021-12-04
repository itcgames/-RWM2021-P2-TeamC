using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CameraMover))]
public class TransitionEditor : Editor
{
	private CameraMover cM;
	private ScreenTransition sT;
	private Camera mainCam;

	private void OnEnable()
	{
		// Method 1
		cM = (CameraMover) target;
		mainCam = Camera.main;
		sT = mainCam.GetComponent<ScreenTransition>();
	}

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Add Point"))
		{
			cM.AddPoint(sT.transitionPoint);
		}

		if (GUILayout.Button("Remove Last Point"))
		{
			cM.RemoveLastPoint();
		}

		// Draw default inspector after button...
		base.OnInspectorGUI();
	}
}