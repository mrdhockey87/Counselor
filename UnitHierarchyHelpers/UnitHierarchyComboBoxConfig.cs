using System;
using System.Windows.Forms;

namespace CounselQuickPlatinum.UnitHierarchyHelpers
{
    /// <summary>
    /// Configuration class for unit hierarchy ComboBox operations.
    /// Provides a standardized way to handle ComboBox Leave events for unit hierarchy entities.
    /// </summary>
    public class UnitHierarchyComboBoxConfig
    {
        /// <summary>
        /// The display name of the entity type (e.g., "Battalion", "Unit", "Platoon")
        /// </summary>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Function to check if an entity with the given name already exists
        /// </summary>
        public Func<string, bool> ExistsCheck { get; set; }

        /// <summary>
        /// Function to create a new entity with the given name, returns the new entity's ID
        /// </summary>
        public Func<string, int> CreateEntity { get; set; }

        /// <summary>
        /// The ComboBox control that this configuration applies to
        /// </summary>
        public ComboBox TargetComboBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the UnitHierarchyComboBoxConfig class
        /// </summary>
        /// <param name="entityTypeName">Display name of the entity type</param>
        /// <param name="existsCheck">Function to check if entity exists</param>
        /// <param name="createEntity">Function to create new entity</param>
        /// <param name="targetComboBox">ComboBox control to operate on</param>
        public UnitHierarchyComboBoxConfig(string entityTypeName, Func<string, bool> existsCheck,
            Func<string, int> createEntity, ComboBox targetComboBox)
        {
            EntityTypeName = entityTypeName;
            ExistsCheck = existsCheck;
            CreateEntity = createEntity;
            TargetComboBox = targetComboBox;
        }
    }
}