using UnityEngine;
using UnityEditor;

namespace Array2DEditor
{
    public abstract class TestArrayDrawer : AbstractPropertyDrawer
    {
        private const float paddingLeft = 4f;
        private const float foldoutWidth = 14f;
        private const float paddingRight = 5f;

        private const float verticalScrollBarWidth = 12f;

        private static float FieldWidth => EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth
                                                                             - paddingLeft - paddingRight -
                                                                             foldoutWidth;

        private static float LineHeight => EditorGUIUtility.singleLineHeight;
        private const float lineMargin = 5f;
        private const float lastLineMargin = 2f;

        private static readonly Vector2 cellSpacing = new Vector2(5f, 5f);

        private const float spacingBetweenFoldoutAndFirstLine = 2f;
        private const float spacingBetweenSizeFieldAndApplyButton = 5f;
        private const float applyButtonWidth = 50f;

        private SerializedProperty gridSizeProperty;
        private SerializedProperty cellSizeProperty;
        private SerializedProperty cellsProperty;

        private Vector2Int newGridSize;
        private bool gridSizeChanged = false;
        private bool wrongGridSize = false;

        private bool initialized = false;
        private SerializedProperty property;

        #region SerializedProperty getters

        private void GetGridSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "gridSize", out gridSizeProperty);

        private void GetCellSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cellSize", out cellSizeProperty);

        private void GetCellsProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cells", out cellsProperty);

        #endregion

        static class Texts
        {
            public static readonly GUIContent gridSizeContent = new GUIContent("Grid Size",
                "NOTE: X is the number of ROWS and Y the number of COLUMNS.");

            public const string apply = "Apply";
            public const string wrongGridSize = "Wrong grid size.";

            public static readonly GUIContent reset = new GUIContent("Reset");
            public static readonly GUIContent changeCellSize = new GUIContent("Change Cell Size");

            public static class GridSizeDialog
            {
                public const string title = "Are you sure?";

                public const string message =
                    "You're about to reduce the width or height of the grid. This may erase some cells. Do you really want this?";

                public const string ok = "Yes";
                public const string cancel = "No";
            }
        }

        protected abstract object GetDefaultCellValue();
        protected abstract object GetCellValue(SerializedProperty cell);
        protected abstract void SetValue(SerializedProperty cell, object obj);


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            this.property = property;
            
            // Initialize properties
            GetGridSizeProperty(property);
            GetCellSizeProperty(property);
            GetCellsProperty(property);

            // Don't draw anything if we miss a property
            if (gridSizeProperty == null || cellSizeProperty == null || cellsProperty == null)
            {
                return;
            }

            // Initialize member variables
            if (!initialized)
            {
                newGridSize = gridSizeProperty.vector2IntValue;
                initialized = true;
            }

            // Begin property drawing
            EditorGUI.BeginProperty(position, label, property);

            // Display foldout
            var foldoutRect = new Rect(position)
            {
                height = LineHeight
            };
            property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(foldoutRect, property.isExpanded, label,
                menuAction: ShowHeaderContextMenu);
            EditorGUI.EndFoldoutHeaderGroup();

            // Go to next line
            position.y += LineHeight;

            if (property.isExpanded)
            {
                position.y += spacingBetweenFoldoutAndFirstLine;

                // Display gridSize label
                var gridSizeLabelRect = new Rect(position)
                {
                    width = EditorGUIUtility.labelWidth,
                    height = LineHeight
                };
                
                SetBoldDefaultFont(gridSizeChanged);
                
                EditorGUI.LabelField(gridSizeLabelRect, Texts.gridSizeContent);

                // Display gridSize field
                var gridSizeFieldRect = new Rect(gridSizeLabelRect);
                gridSizeFieldRect.x += EditorGUIUtility.labelWidth;
                gridSizeFieldRect.width = FieldWidth - spacingBetweenSizeFieldAndApplyButton - applyButtonWidth -
                                          (VerticalScrollBarIsVisible() ? verticalScrollBarWidth : 0f);
                newGridSize = EditorGUI.Vector2IntField(gridSizeFieldRect, GUIContent.none, newGridSize);
                
                SetBoldDefaultFont(false);

                // Display apply button
                var applyButtonRect = new Rect(gridSizeFieldRect);
                applyButtonRect.x += gridSizeFieldRect.width + spacingBetweenSizeFieldAndApplyButton;
                applyButtonRect.width = applyButtonWidth;

                gridSizeChanged = newGridSize != gridSizeProperty.vector2IntValue;
                wrongGridSize = (newGridSize.x <= 0 || newGridSize.y <= 0);

                GUI.enabled = gridSizeChanged && !wrongGridSize;

                if (GUI.Button(applyButtonRect, Texts.apply, EditorStyles.miniButton))
                {
                    var operationAllowed = false;

                    if (newGridSize.x < gridSizeProperty.vector2IntValue.x ||
                        newGridSize.y < gridSizeProperty.vector2IntValue.y) // Smaller grid
                    {
                        operationAllowed = EditorUtility.DisplayDialog(Texts.GridSizeDialog.title, Texts.GridSizeDialog.message,
                            Texts.GridSizeDialog.ok, Texts.GridSizeDialog.cancel);
                    }
                    else // Bigger grid
                    {
                        operationAllowed = true;
                    }

                    if (operationAllowed)
                    {
                        InitNewGrid(newGridSize);
                    }
                }

                GUI.enabled = true;

                if (wrongGridSize)
                {
                    position.y += LineHeight + lineMargin;
                    var helpBoxRect = new Rect(position)
                    {
                        height = LineHeight * 2
                    };
                    EditorGUI.HelpBox(helpBoxRect, Texts.wrongGridSize, MessageType.Error);
                    position.y += LineHeight;
                }

                position.y += LineHeight + lineMargin;
                DisplayGrid(position);
            }

            EditorGUI.EndProperty();
        }

        private void ShowHeaderContextMenu(Rect position)
        {
            var menu = new GenericMenu();
            menu.AddItem(Texts.reset, false, OnReset);
            menu.AddSeparator(""); // An empty string will create a separator at the top level
            menu.AddItem(Texts.changeCellSize, false, OnChangeCellSize);
            menu.DropDown(position);
        }

        private void OnReset()
        {
            InitNewGrid(gridSizeProperty.vector2IntValue, restorePreviousValues: false);
        }

        private void OnChangeCellSize()
        {
            ChangeCellSizeWindow.ShowWindow(cellSizeProperty);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = base.GetPropertyHeight(property, label);

            GetGridSizeProperty(property);
            GetCellSizeProperty(property);

            if (property.isExpanded)
            {
                height += spacingBetweenFoldoutAndFirstLine;
                height += LineHeight + lineMargin; // GridSize line
                if (wrongGridSize)
                {
                    height += 2 * LineHeight + lineMargin;
                }

                height += gridSizeProperty.vector2IntValue.y * (cellSizeProperty.vector2IntValue.y + cellSpacing.y) - cellSpacing.y; // Cells lines
                height += lastLineMargin;
            }

            return height;
        }

        private void InitNewGrid(Vector2Int newSize, bool restorePreviousValues = true)
        {
            var previousGrid = GetGridValues();

            cellsProperty.ClearArray();

            for (var x = 0; x < newSize.x; x++)
            {
                cellsProperty.InsertArrayElementAtIndex(x);
                var row = GetRowAt(x);

                for (var y = 0; y < newSize.y; y++)
                {
                    row.InsertArrayElementAtIndex(y);

                    var cell = row.GetArrayElementAtIndex(y);

                    SetValue(cell, GetDefaultCellValue());

                    // The grid just got bigger, we try to retrieve the previous value of the cell
                    if (restorePreviousValues && x < gridSizeProperty.vector2IntValue.x && y < gridSizeProperty.vector2IntValue.y)
                    {
                        SetValue(cell, previousGrid[x][y]);
                    }
                }
            }

            gridSizeProperty.vector2IntValue = newGridSize;
            
            property.serializedObject.ApplyModifiedProperties();
        }

        private object[][] GetGridValues()
        {
            var arr = new object[gridSizeProperty.vector2IntValue.x][];
            for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
            {
                arr[x] = new object[gridSizeProperty.vector2IntValue.y];
                for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
                {
                    arr[x][y] = GetCellValue(GetRowAt(x).GetArrayElementAtIndex(y));
                }
            }

            return arr;
        }

        private void DisplayGrid(Rect position)
        {
            var cellRect = new Rect(position.x, position.y, cellSizeProperty.vector2IntValue.x,
                cellSizeProperty.vector2IntValue.y);

            for (var x = 0; x < gridSizeProperty.vector2IntValue.x; x++)
            {
                var row = GetRowAt(x);
                
                for (var y = 0; y < gridSizeProperty.vector2IntValue.y; y++)
                {
                    var pos = new Rect(cellRect)
                    {
                        // :NOTE: x is the number of ROWS and y of COLUMNS so we have to take care of this here
                        x = cellRect.x + (cellRect.width + cellSpacing.x) * y,
                        y = cellRect.y + (cellRect.height + cellSpacing.y) * x
                    };
                    EditorGUI.PropertyField(pos, row.GetArrayElementAtIndex(y), GUIContent.none);
                }
            }
        }
        
        private SerializedProperty GetRowAt(int idx)
        {
            return cellsProperty.GetArrayElementAtIndex(idx).FindPropertyRelative("row");
        }
    }
}