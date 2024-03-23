using UnityEditor;

public static class ToolBox
{
    #if UNITY_EDITOR
    public static bool IsApplicationPlaying => EditorApplication.isPlaying;
    #endif
}
