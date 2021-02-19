using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TestArray))]
public class TestArrayDrawer : PropertyDrawer
{
    private const float paddingLeft = 4f;
    private const float foldoutWidth = 14f;
    private const float paddingRight = 5f;
    
    private float FieldWidth => EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth
                                - paddingLeft - paddingRight - foldoutWidth;

    private float LineHeight => EditorGUIUtility.singleLineHeight;
    private const float lineMargin = 5f;
    
    private readonly Vector2 cellSpacing = new Vector2(5f, 5f);
    
    private const float spacingBetweenSizeFieldAndApplyButton = 5f;
    private const float applyButtonWidth = 50f;

    private SerializedProperty gridSizeProperty;
    private SerializedProperty cellSizeProperty;
    private SerializedProperty cellsProperty;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GetGridSizeProperty(property);
        GetCellSizeProperty(property);
        GetCellsProperty(property);

        if (gridSizeProperty == null || cellsProperty == null)
        {
            return; // Don't draw anything if we miss a property
        }

        EditorGUI.BeginProperty(position, label, property);

        var foldoutRect = new Rect(position)
        {
            height = LineHeight
        };
        property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, label);
        EditorGUI.EndFoldoutHeaderGroup();

        position.y += LineHeight;

        if (property.isExpanded)
        {
            var gridSizeLabelRect = new Rect(position)
            {
                width = EditorGUIUtility.labelWidth,
                height = LineHeight
            };

            var gridSizeFieldRect = new Rect(gridSizeLabelRect);
            gridSizeFieldRect.x += EditorGUIUtility.labelWidth;
            gridSizeFieldRect.width = FieldWidth - spacingBetweenSizeFieldAndApplyButton - applyButtonWidth;

            var applyButtonRect = new Rect(gridSizeFieldRect);
            applyButtonRect.x += gridSizeFieldRect.width + spacingBetweenSizeFieldAndApplyButton;
            applyButtonRect.width = applyButtonWidth;

            EditorGUI.LabelField(gridSizeLabelRect, "Grid Size");
            EditorGUI.PropertyField(gridSizeFieldRect, gridSizeProperty, GUIContent.none);

            if (GUI.Button(applyButtonRect, "Apply", EditorStyles.miniButton))
            {
                
            }

            position.y += LineHeight + lineMargin; // Skip to next line

            var cellSizeLabelRect = new Rect(position)
            {
                width = EditorGUIUtility.labelWidth,
                height = LineHeight
            };

            var cellSizeFieldRect = new Rect(cellSizeLabelRect);
            cellSizeFieldRect.x += EditorGUIUtility.labelWidth;
            cellSizeFieldRect.width = FieldWidth - spacingBetweenSizeFieldAndApplyButton - applyButtonWidth;

            applyButtonRect = new Rect(cellSizeFieldRect);
            applyButtonRect.x += cellSizeFieldRect.width + spacingBetweenSizeFieldAndApplyButton;
            applyButtonRect.width = applyButtonWidth;

            EditorGUI.LabelField(cellSizeLabelRect, "Cell Size");
            EditorGUI.PropertyField(cellSizeFieldRect, cellSizeProperty, GUIContent.none);

            if (GUI.Button(applyButtonRect, "Apply", EditorStyles.miniButton))
            {
                
            }

            position.y += LineHeight + lineMargin;
            DisplayGrid(position);
        }
        
        EditorGUI.EndProperty();
    }
    
    #region SerializedProperty getters
    private void GetGridSizeProperty(SerializedProperty property) => TryFindPropertyRelative(property, "gridSize", out gridSizeProperty);
    private void GetCellSizeProperty(SerializedProperty property) => TryFindPropertyRelative(property, "cellSize", out cellSizeProperty);
    private void GetCellsProperty(SerializedProperty property) => TryFindPropertyRelative(property, "cells", out cellsProperty);
    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = base.GetPropertyHeight(property, label);
        
        GetGridSizeProperty(property);
        GetCellSizeProperty(property);

        if (property.isExpanded)
        {
            height += LineHeight + lineMargin; // GridSize line
            height += LineHeight + lineMargin; // CellSize line
            height += gridSizeProperty.vector2IntValue.y * (cellSizeProperty.vector2IntValue.y + cellSpacing.y) - cellSpacing.y; // Cells lines
        }

        return height;
    }

    private void DisplayGrid(Rect position)
    {
        var cellRect = new Rect(position.x, position.y, cellSizeProperty.vector2IntValue.x, cellSizeProperty.vector2IntValue.y);

        for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
        {
            for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
            {
                var pos = new Rect(cellRect)
                {
                    x = cellRect.x + (cellRect.width + cellSpacing.x) * x,
                    y = cellRect.y + (cellRect.height + cellSpacing.y) * y
                };

                EditorGUI.IntField(pos, 0);
            }
        }
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
