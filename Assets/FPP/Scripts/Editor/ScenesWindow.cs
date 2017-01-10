using UnityEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class ScenesWindow : EditorWindow
{
	int updateCount = 0;
	int sceneIndex;
	string currentScene;
	List<string> scenes = new List<string>();
	List<string> sceneNames = new List<string>();

	[MenuItem("Window/Scenes Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		ScenesWindow window = (ScenesWindow)EditorWindow.GetWindow(typeof(ScenesWindow), false, "Scenes");
		window.minSize = new Vector2(100, 25);
		window.OnInspectorUpdate();
	}

	void OnGUI()
	{
		minSize = new Vector2(100, 23);
		var oldIndex = sceneIndex;
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("<<"))
		{
			sceneIndex--;
			if (sceneIndex < 0)
				sceneIndex = scenes.Count - 1;
		}
		sceneIndex = EditorGUILayout.Popup(sceneIndex, sceneNames.ToArray());
		if (GUILayout.Button(">>"))
		{
			sceneIndex++;
			if (sceneIndex >= scenes.Count)
				sceneIndex = 0;
		}
		if(sceneIndex != oldIndex)
		{
			EditorApplication.SaveCurrentSceneIfUserWantsTo();
			EditorApplication.OpenScene(scenes[sceneIndex]);
			//var cameras = Editor.FindSceneObjectsOfType(typeof(Camera));
			Selection.activeObject = Camera.main;
			var sceneView = (SceneView.sceneViews[0] as SceneView);
			sceneView.FrameSelected();
			Selection.activeObject = null;
		}
	}
	void OnInspectorUpdate()
	{
		updateCount++;
		if (updateCount > 10)
		{
			updateCount = 0;
			scenes.Clear();
			sceneNames.Clear();
			var paths = AssetDatabase.GetAllAssetPaths();
			foreach (var p in paths)
			{
				if (Path.GetExtension(p) == ".unity")
				{
					scenes.Add(p);
					sceneNames.Add(Path.GetFileNameWithoutExtension(p));
				}
			}
			currentScene = EditorApplication.currentScene;
			sceneIndex = scenes.IndexOf(currentScene);
			Repaint();
		}
	}
}