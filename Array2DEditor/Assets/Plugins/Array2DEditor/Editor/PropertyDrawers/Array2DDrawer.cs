using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Array2DEditor
{
    public abstract class Array2DDrawer : PropertyDrawer
    {
        private static float LineHeight => EditorGUIUtility.singleLineHeight;
        
        private const float firstLineMargin = 5f;
        private const float lastLineMargin = 2f;

        private static readonly Vector2 cellSpacing = new Vector2(5f, 5f);

        private SerializedProperty thisProperty;
        private SerializedProperty gridSizeProperty;
        private SerializedProperty cellSizeProperty;
        private SerializedProperty cellsProperty;

        #region SerializedProperty getters

        private void GetGridSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "gridSize", out gridSizeProperty);

        private void GetCellSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cellSize", out cellSizeProperty);

        private void GetCellsProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cells", out cellsProperty);

        #endregion

        #region Texts
        
        static class Texts
        {
            public static readonly GUIContent reset = new GUIContent("Reset");
            public static readonly GUIContent changeGridSize = new GUIContent("Change Grid Size");
            public static readonly GUIContent changeCellSize = new GUIContent("Change Cell Size");

            public const string gridSizeLabel = "Grid Size";
            public const string cellSizeLabel = "Cell Size";
        }
        
        #endregion

        #region Abstract and virtual methods

        protected virtual Vector2Int GetDefaultCellSizeValue() => new Vector2Int(32, 16);
        
        protected abstract object GetDefaultCellValue();
        protected abstract object GetCellValue(SerializedProperty cell);
        protected abstract void SetValue(SerializedProperty cell, object obj);
        
        #endregion


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            thisProperty = property;

            // Initialize properties
            GetGridSizeProperty(property);
            GetCellSizeProperty(property);
            GetCellsProperty(property);

            // Don't draw anything if we miss a property
            if (gridSizeProperty == null || cellSizeProperty == null || cellsProperty == null)
            {
                return;
            }
            
            // Initialize cell size to default value if not already done
            if (cellSizeProperty.vector2IntValue == default)
            {
                cellSizeProperty.vector2IntValue = GetDefaultCellSizeValue();
            }

            position = EditorGUI.IndentedRect(position);

            // Begin property drawing
            EditorGUI.BeginProperty(position, label, property);

            // Display foldout
            var foldoutRect = new Rect(position)
            {
                height = LineHeight
            };

            // We're using EditorGUI.IndentedRect to draw our Rects, and it already takes the indentLevel into account, so we must set it to 0.
            // This allows the PropertyDrawer to handle nested variables correctly.
            // More info: https://answers.unity.com/questions/1268850/how-to-properly-deal-with-editorguiindentlevel-in.html
            EditorGUI.indentLevel = 0;

            label.tooltip = $"Size: {gridSizeProperty.vector2IntValue.x}x{gridSizeProperty.vector2IntValue.y}";

            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, label,
                menuAction: ShowHeaderContextMenu);
            EditorGUI.EndFoldoutHeaderGroup();

            // Go to next line
            position.y += LineHeight;

            if (property.isExpanded)
            {
                position.y += firstLineMargin;

                DisplayGrid(position);
            }

            EditorGUI.EndProperty();
        }

        private void ShowHeaderContextMenu(Rect position)
        {
            var menu = new GenericMenu();
            menu.AddItem(Texts.reset, false, OnReset);
            menu.AddSeparator(""); // An empty string will create a separator at the top level
            menu.AddItem(Texts.changeGridSize, false, OnChangeGridSize);
            menu.AddItem(Texts.changeCellSize, false, OnChangeCellSize);
            menu.DropDown(position);
        }

        private void OnReset()
        {
            InitNewGrid(gridSizeProperty.vector2IntValue);
        }

        private void OnChangeGridSize()
        {
            EditorWindowVector2IntField.ShowWindow(gridSizeProperty.vector2IntValue, InitNewGridAndRestorePreviousValues, Texts.gridSizeLabel);
        }
        
        private void OnChangeCellSize()
        {
            EditorWindowVector2IntField.ShowWindow(cellSizeProperty.vector2IntValue, SetNewCellSize, Texts.cellSizeLabel);
        }

        private void SetNewCellSize(Vector2Int newCellSize)
        {
            cellSizeProperty.vector2IntValue = newCellSize;
            thisProperty.serializedObject.ApplyModifiedProperties();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);

            GetGridSizeProperty(property);
            GetCellSizeProperty(property);

            if (property.isExpanded)
            {
                height += firstLineMargin;

                height += gridSizeProperty.vector2IntValue.y * (cellSizeProperty.vector2IntValue.y + cellSpacing.y) - cellSpacing.y; // Cells lines
                
                height += lastLineMargin;
            }

            return height;
        }

        private void InitNewGridAndRestorePreviousValues(Vector2Int newSize)
        {
            var previousGrid = GetGridValues();
            var previousGridSize = gridSizeProperty.vector2IntValue;
            
            InitNewGrid(newSize);

            for (var y = 0; y < newSize.y; y++)
            {
                var row = GetRowAt(y);
                
                for (var x = 0; x < newSize.x; x++)
                {
                    var cell = row.GetArrayElementAtIndex(x);
                    
                    if (x < previousGridSize.x && y < previousGridSize.y)
                    {
                        SetValue(cell, previousGrid[y][x]);
                    }
                }
            }
            
            thisProperty.serializedObject.ApplyModifiedProperties();
        }

        private void InitNewGrid(Vector2Int newSize)
        {
            cellsProperty.ClearArray();

            for (var y = 0; y < newSize.y; y++)
            {
                cellsProperty.InsertArrayElementAtIndex(y); // Insert a new row
                var row = GetRowAt(y); // Get the new row
                row.ClearArray(); // Clear it

                for (var x = 0; x < newSize.x; x++)
                {
                    row.InsertArrayElementAtIndex(x);

                    var cell = row.GetArrayElementAtIndex(x);

                    SetValue(cell, GetDefaultCellValue());
                }
            }

            gridSizeProperty.vector2IntValue = newSize;
            thisProperty.serializedObject.ApplyModifiedProperties();
        }

        private object[][] GetGridValues()
        {
            var arr = new object[gridSizeProperty.vector2IntValue.y][];
            
            for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
            {
                arr[y] = new object[gridSizeProperty.vector2IntValue.x];
                
                for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
                {
                    arr[y][x] = GetCellValue(GetRowAt(y).GetArrayElementAtIndex(x));
                }
            }

            return arr;
        }

        private void DisplayGrid(Rect position)
        {
            var cellRect = new Rect(position.x, position.y, cellSizeProperty.vector2IntValue.x,
                cellSizeProperty.vector2IntValue.y);

            for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
            {
                for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
                {
                    var pos = new Rect(cellRect)
                    {
                        x = cellRect.x + (cellRect.width + cellSpacing.x) * x,
                        y = cellRect.y + (cellRect.height + cellSpacing.y) * y
                    };

                    var property = GetRowAt(y).GetArrayElementAtIndex(x);

                    if (property.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        var match = Regex.Match(property.type, @"PPtr<\$(.+)>");
                        if (match.Success)
                        {
                            var objectType = match.Groups[1].ToString();
                            var assemblyName = "UnityEngine";
                            EditorGUI.ObjectField(pos, property, System.Type.GetType($"{assemblyName}.{objectType}, {assemblyName}"), GUIContent.none);
                        }
                    }
                    else
                        EditorGUI.PropertyField(pos, property, GUIContent.none);
                }
            }
        }
        
        private SerializedProperty GetRowAt(int idx)
        {
            return cellsProperty.GetArrayElementAtIndex(idx).FindPropertyRelative("row");
        }
        
        private void TryFindPropertyRelative(SerializedProperty parent, string relativePropertyPath, out SerializedProperty prop)
        {
            prop = parent.FindPropertyRelative(relativePropertyPath);

            if (prop == null)
            {
                Debug.LogError($"Couldn't find variable \"{relativePropertyPath}\" in {parent.name}");
            }
        }
        
        #region Debug

        private void DrawDebugRect(Rect rect) => DrawDebugRect(rect, new Color(1f, 0f, 1f, .2f));

        private void DrawDebugRect(Rect rect, Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            var prevBoxTex = GUI.skin.box.normal.background;
            GUI.skin.box.normal.background = texture;
            GUI.Box(rect, GUIContent.none);
            GUI.skin.box.normal.background = prevBoxTex;
        }

        #endregion
    }
}