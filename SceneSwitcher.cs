using UnityEditor;
using UnityEditor.SceneManagement;

public static class SceneSwitcher
{
    static string scenePath;
    static bool saveScene, cancelSave;

    static void LoadScene()
    {
        if(cancelSave) { return; }

        //Save the actual scene you are currently working in, before loading the new one
        if (saveScene) { EditorSceneManager.SaveOpenScenes(); }

        //if "Don't Save" was clicked, the scene will still load, but the above line will not run to actually save the scene
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }

    /// <summary>
    /// Creates a Dislay Box that will only appear in the Editor, when the current scene is "dirty" (contains unsaved changes)
    /// </summary>
    static void AskToSave()
    {
        int result = EditorUtility.DisplayDialogComplex("Unsaved Scene Changes!", "You have unsaved changes in: " + EditorSceneManager.GetActiveScene().name + "\nAny unsaved changes will be discarded."
            , "Save", "Don't Save", "Cancel");

        saveScene = (result == 0);
        cancelSave = (result == 2);
    }

    #region All Scenes to (manually) add to the top menu bar, in our own Menu Item dropdown

    [MenuItem("Switch Scenes/Menu Scene")]
    static void MenuScene()
    {
        scenePath = "Assets/Scenes/MenuScene.unity";

        if (EditorSceneManager.GetActiveScene().isDirty) { AskToSave(); }

        LoadScene();
    }

    [MenuItem("Switch Scenes/Another Scene")]
    static void AnotherScene()
    {
        scenePath = "Assets/Scenes/AnotherScene.unity";

        if (EditorSceneManager.GetActiveScene().isDirty) { AskToSave(); }

        LoadScene();
    }

    #endregion
}
