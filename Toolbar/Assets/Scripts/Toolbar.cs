using UnityEditor;
using UnityEngine;

public class Toolbar : EditorWindow
{
    private Tools _tools;
    private string _labelText;
    
    [MenuItem("SUPER TOOLS/SUPER")]
    public static void OpenToolbar()
    {
        var window = (Toolbar)GetWindow(typeof(Toolbar));
        window.minSize = new Vector2(400, 200);
        window.Show();
    }

    private void OnEnable()
    {
        _tools = new Tools();
    }

    private void OnGUI()
    {
        DrawLabel();
        DrawButton();
    }

    private void DrawLabel()
    {
        GUILayout.Label(_labelText);
    }

    private async void DrawButton()
    {
        if (GUILayout.Button("Open web"))
        {
            _tools.OpenUrl();
            _labelText = await _tools.TryGetMessage();
        }
    }
}
