using UnityEditor;
using UnityEngine;

namespace Array2DEditor
{
    [CustomPropertyDrawer(typeof(TestArrayInt))]
    public class TestArrayDrawer : AbstractPropertyDrawer
    {
        private const float paddingLeft = 4f;
        private const float foldoutWidth = 14f;
        private const float paddingRight = 5f;

        private const float verticalScrollBarWidth = 12f;

        private static float FieldWidth => EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth
                                           - paddingLeft - paddingRight - foldoutWidth;

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

        protected Vector2Int newGridSize;
        private bool gridSizeChanged = false;
        private bool wrongGridSize = false;
        
        private bool initialized = false;

        #region SerializedProperty getters

        private void GetGridSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "gridSize", out gridSizeProperty);

        private void GetCellSizeProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cellSize", out cellSizeProperty);

        private void GetCellsProperty(SerializedProperty property) =>
            TryFindPropertyRelative(property, "cells", out cellsProperty);

        #endregion


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
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
                EditorGUI.LabelField(gridSizeLabelRect,
                    new GUIContent("Grid Size", "NOTE: X is the number of ROWS and Y the number of COLUMNS."));

                // Display gridSize field
                var gridSizeFieldRect = new Rect(gridSizeLabelRect);
                gridSizeFieldRect.x += EditorGUIUtility.labelWidth;
                gridSizeFieldRect.width = FieldWidth - spacingBetweenSizeFieldAndApplyButton - applyButtonWidth -
                                          (VerticalScrollBarIsVisible() ? verticalScrollBarWidth : 0f);
                newGridSize = EditorGUI.Vector2IntField(gridSizeFieldRect, GUIContent.none, newGridSize);

                // Display apply button
                var applyButtonRect = new Rect(gridSizeFieldRect);
                applyButtonRect.x += gridSizeFieldRect.width + spacingBetweenSizeFieldAndApplyButton;
                applyButtonRect.width = applyButtonWidth;

                gridSizeChanged = newGridSize != gridSizeProperty.vector2IntValue;
                wrongGridSize = (newGridSize.x <= 0 || newGridSize.y <= 0);

                GUI.enabled = gridSizeChanged && !wrongGridSize;

                if (GUI.Button(applyButtonRect, "Apply", EditorStyles.miniButton))
                {
                    var operationAllowed = false;

                    if (newGridSize.x < gridSizeProperty.vector2IntValue.x ||
                        newGridSize.y < gridSizeProperty.vector2IntValue.y) // Smaller grid
                    {
                        operationAllowed = EditorUtility.DisplayDialog("Are you sure?",
                            "You're about to reduce the width or height of the grid. This may erase some cells. Do you really want this?",
                            "Yes", "No");
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
                    EditorGUI.HelpBox(helpBoxRect, "Wrong grid size.", MessageType.Error);
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
            menu.AddItem(new GUIContent("Change Cell Size"), false, OnChangeCellSize);
            menu.DropDown(position);
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
                height += gridSizeProperty.vector2IntValue.y * (cellSizeProperty.vector2IntValue.y + cellSpacing.y) -
                          cellSpacing.y; // Cells lines
                height += lastLineMargin;
            }

            return height;
        }

        private void InitNewGrid(Vector2Int newSize)
        {
            /*cellsProperty.ClearArray();

            for (var x = 0; x < newSize.x; x++)
            {
                cellsProperty.InsertArrayElementAtIndex(x);
                var row = GetRowAt(x);

                for (var y = 0; y < newSize.y; y++)
                {
                    row.InsertArrayElementAtIndex(y);

                    SetValue(row.GetArrayElementAtIndex(y), x, y);
                }
            }*/

            gridSizeProperty.vector2IntValue = newGridSize;
        }

        protected SerializedProperty GetRowAt(int idx)
        {
            return cellsProperty.GetArrayElementAtIndex(idx).FindPropertyRelative("row");
        }

        protected virtual void SetValue(SerializedProperty cell, int x, int y)
        {
            var previousCells = GetCells();

            cell.intValue = default;

            if (x < gridSizeProperty.vector2IntValue.x && y < gridSizeProperty.vector2IntValue.y)
            {
                cell.intValue = previousCells[x, y];
            }
        }

        private int[,] GetCells()
        {
            var result = new int[cellsProperty.arraySize, cellsProperty.arraySize];

            // :TODO:

            return result;
        }

        private void DisplayGrid(Rect position)
        {
            var cellRect = new Rect(position.x, position.y, cellSizeProperty.vector2IntValue.x,
                cellSizeProperty.vector2IntValue.y);

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
    }
}
