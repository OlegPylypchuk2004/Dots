using UnityEditor;
using UnityEngine;

public class GridEditorWindow : EditorWindow
{
    private int _width = 5;
    private int _height = 5;
    private bool[,] _values;
    private Vector2 _scrollPosition;
    private string _saveName = "New Grid";
    private string _savePath = "Assets/Resources/Data/Grids/";

    [MenuItem("Tools/Grid Editor")]
    public static void OpenWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    private void OnEnable()
    {
        ValidateSize();
        Generate();
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);

        _saveName = EditorGUILayout.TextField("Save Name", _saveName);
        _savePath = EditorGUILayout.TextField("Save Path", _savePath);

        int width = EditorGUILayout.IntField("Width", _width);
        int height = EditorGUILayout.IntField("Height", _height);

        if (width != _width || height != _height)
        {
            _width = Mathf.Max(1, width);
            _height = Mathf.Max(1, height);

            Generate();
        }

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        for (int y = 0; y < _height; y++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < _width; x++)
            {
                _values[x, y] = EditorGUILayout.Toggle(_values[x, y], GUILayout.Width(20), GUILayout.Height(20));
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Enable all"))
        {
            SetAllGrid(true);
        }

        if (GUILayout.Button("Disable all"))
        {
            SetAllGrid(false);
        }

        if (GUILayout.Button("Save"))
        {
            Save();
        }
    }

    private void Generate()
    {
        _values = new bool[_width, _height];
    }

    private void Save()
    {
        if (string.IsNullOrWhiteSpace(_saveName))
        {
            Debug.LogError("Name cannot be empty");
            return;
        }

        if (string.IsNullOrWhiteSpace(_savePath))
        {
            Debug.LogError("Save path cannot be empty");
            return;
        }

        if (!_savePath.EndsWith("/"))
        {
            _savePath += "/";
        }

        GridData gridData = ScriptableObject.CreateInstance<GridData>();
        gridData.Width = _width;
        gridData.Height = _height;
        gridData.Values = new bool[_width * _height];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                gridData.Values[y * _width + x] = _values[x, y];
            }
        }

        string fullPath = _savePath + _saveName + ".asset";

        if (!AssetDatabase.IsValidFolder(_savePath.TrimEnd('/')))
        {
            Debug.LogError($"Invalid save path: {_savePath}");
            return;
        }

        AssetDatabase.CreateAsset(gridData, fullPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Grid '{_saveName}' saved at: {fullPath}");
    }

    private void SetAllGrid(bool value)
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _values[x, y] = value;
            }
        }
    }

    private void ValidateSize()
    {
        _width = Mathf.Max(1, _width);
        _height = Mathf.Max(1, _height);
    }
}