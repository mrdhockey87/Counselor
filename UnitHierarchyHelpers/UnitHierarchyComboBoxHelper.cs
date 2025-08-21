using CounselQuickPlatinum.CustomExtensions;
using System;
using System.Windows.Forms;

namespace CounselQuickPlatinum.UnitHierarchyHelpers
{
    /// <summary>
    /// Helper class for handling unit hierarchy ComboBox operations across multiple forms.
    /// Provides standardized methods for creating new entities and handling ComboBox Leave events.
    /// </summary>
    public static class UnitHierarchyComboBoxHelper
    {
        /// <summary>
        /// Generic handler for unit hierarchy ComboBox Leave events that handles creation of new entries
        /// </summary>
        /// <param name="config">Configuration object containing entity-specific methods and settings</param>
        /// <param name="refreshComboBoxesAction">Action to call to refresh ComboBoxes after creating new entities</param>
        public static void HandleUnitHierarchyComboBoxLeave(UnitHierarchyComboBoxConfig config, Action refreshComboBoxesAction)
        {
            string selectedText = config.TargetComboBox.Text.Trim();

            // If text is empty after trimming, clear the selection and return
            if (string.IsNullOrEmpty(selectedText))
            {
                config.TargetComboBox.SelectedIndex = -1;
                config.TargetComboBox.Text = "";
                return;
            }

            // Apply title case after trimming
            selectedText = selectedText.ToSelectiveTitleCase();
            int selectedIndex = config.TargetComboBox.SelectedIndex;

            // Check if we need to handle this (text entered but no valid selection, or text different from selection)
            if (!ShouldProcessComboBoxText(config.TargetComboBox, selectedText, selectedIndex))
                return;

            // Check if entity exists
            bool entityExists = config.ExistsCheck(selectedText);
            if (!entityExists)
            {
                // Prompt to create new entity
                DialogResult result = MessageBox.Show(
                    $"Do you want to make a new {config.EntityTypeName}?",
                    "Confirmation",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Create the new entity
                    int newEntityId = config.CreateEntity(selectedText);

                    // Refresh ComboBoxes to include the new entity
                    refreshComboBoxesAction?.Invoke();

                    // Select the newly created entity
                    SelectNewlyCreatedEntity(config, selectedText, newEntityId);
                }
                else
                {
                    config.TargetComboBox.SelectedIndex = -1;
                }
            }
            else
            {
                // Entity exists, find and select it
                SelectExistingEntity(config.TargetComboBox, selectedText);
            }
        }

        /// <summary>
        /// Determines if ComboBox text should be processed based on selection state and text content
        /// </summary>
        /// <param name="comboBox">The ComboBox to check</param>
        /// <param name="selectedText">The formatted text from the ComboBox</param>
        /// <param name="selectedIndex">The current selected index</param>
        /// <returns>True if the text should be processed, false otherwise</returns>
        public static bool ShouldProcessComboBoxText(ComboBox comboBox, string selectedText, int selectedIndex)
        {
            // Process if: no selection but has text, or has selection but text differs from selected item
            return (selectedIndex < 0 && !string.IsNullOrEmpty(selectedText)) ||
                   (selectedIndex >= 0 && !selectedText.Equals(comboBox.Items[selectedIndex].ToString(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Selects the newly created entity in the ComboBox
        /// </summary>
        /// <param name="config">Configuration containing selection behavior</param>
        /// <param name="selectedText">The text that was used to create the entity</param>
        /// <param name="newEntityId">The ID returned from creating the entity</param>
        public static void SelectNewlyCreatedEntity(UnitHierarchyComboBoxConfig config, string selectedText, int newEntityId)
        {
            // For all entities, find by text - this works consistently for all ComboBox types
    int index = config.TargetComboBox.FindStringExact(selectedText);
    config.TargetComboBox.SelectedIndex = index >= 0 ? index : -1;
        }

        /// <summary>
        /// Selects an existing entity in the ComboBox by finding its text
        /// </summary>
        /// <param name="comboBox">ComboBox to search in</param>
        /// <param name="selectedText">Text to find and select</param>
        public static void SelectExistingEntity(ComboBox comboBox, string selectedText)
        {
            int index = comboBox.FindStringExact(selectedText);
            comboBox.SelectedIndex = index >= 0 ? index : -1;
        }
    }
}