using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CounselQuickPlatinum
{
    public partial class CQPDatePicker : UserControl
    {
        private bool _suppressEvents = false;
        
        [Browsable(true)]
        public event EventHandler ValueChanged;

        public CQPDatePicker()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        /// <summary>
        /// Gets or sets the selected date. Returns DateTime.MinValue if no valid date is selected.
        /// Use SetDate() and GetDate() methods for more control over blank/null handling.
        /// 
        /// Example usage:
        /// // Setting dates from data
        /// datePicker.SetDateFromData(soldier.DateOfBirth);  // Handles DateTime, null, blank values
        /// datePicker.SetDate(new DateTime(2023, 5, 15));    // Set specific date
        /// datePicker.ClearDate();                           // Clear the control
        /// 
        /// // Getting dates
        /// DateTime selectedDate = datePicker.GetDate();     // Get selected date or special values
        /// bool hasDate = datePicker.HasValidDate();         // Check if valid date selected
        /// bool isEmpty = datePicker.IsBlank();              // Check if control is empty
        /// 
        /// // Working with database/form data
        /// if (datePicker.HasValidDate())
        /// {
        ///     soldier.DateOfBirth = datePicker.GetDate();
        /// }
        /// else
        /// {
        ///     soldier.DateOfBirth = new DateTime(0);  // Use codebase pattern for blank dates
        /// }
        /// </summary>
        public DateTime Value
        {
            get
            {
                return GetDate();
            }
            set
            {
                SetDate(value);
            }
        }

        /// <summary>
        /// Gets the selected date. Returns DateTime.MinValue if no valid date is selected.
        /// Returns new DateTime(0) if the control is blank (following codebase pattern).
        /// </summary>
        public DateTime GetDate()
        {
            try
            {
                if (yearCBO.SelectedItem != null && monthCBO.SelectedItem != null && dayCBO.SelectedItem != null)
                {
                    int year = (int)yearCBO.SelectedItem;
                    int month = int.Parse(monthCBO.SelectedItem.ToString());
                    int day = int.Parse(dayCBO.SelectedItem.ToString());
                    return new DateTime(year, month, day);
                }
            }
            catch
            {
                // Invalid date combination
            }
            
            // Check if any fields have values but not all (partial date)
            if (yearCBO.SelectedItem != null || monthCBO.SelectedItem != null || dayCBO.SelectedItem != null)
            {
                return DateTime.MinValue; // Invalid/incomplete date
            }
            
            return new DateTime(0); // Blank/empty date (following codebase pattern)
        }

        /// <summary>
        /// Sets the date value. Handles blank/null dates properly.
        /// Pass DateTime.MinValue, new DateTime(0), or DateTime.MaxValue to clear the control.
        /// </summary>
        /// <param name="date">The date to set, or a special value to clear</param>
        public void SetDate(DateTime date)
        {
            _suppressEvents = true;
            try
            {
                if (date == DateTime.MinValue || date == new DateTime(0) || date == DateTime.MaxValue)
                {
                    ClearDate();
                }
                else
                {
                    // Ensure the year is within our range
                    if (date.Year < 1900 || date.Year > DateTime.Now.Year)
                    {
                        ClearDate();
                        return;
                    }

                    yearCBO.SelectedItem = date.Year;
                    monthCBO.SelectedItem = date.Month.ToString("00");
                    PopulateDays(); // Refresh days for the selected year/month
                    dayCBO.SelectedItem = date.Day.ToString("00");
                }
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        /// <summary>
        /// Clears all date selections, making the control blank.
        /// </summary>
        public void ClearDate()
        {
            _suppressEvents = true;
            try
            {
                yearCBO.SelectedIndex = -1;
                monthCBO.SelectedIndex = -1;
                dayCBO.SelectedIndex = -1;
                yearCBO.Text = "";
                monthCBO.Text = "";
                dayCBO.Text = "";
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        /// <summary>
        /// Returns true if the control has a valid, complete date selected.
        /// </summary>
        public bool HasValidDate()
        {
            DateTime date = GetDate();
            return date != DateTime.MinValue && date != new DateTime(0);
        }

        /// <summary>
        /// Returns true if the control is completely blank (no selections).
        /// </summary>
        public bool IsBlank()
        {
            return yearCBO.SelectedIndex == -1 && 
                   monthCBO.SelectedIndex == -1 && 
                   dayCBO.SelectedIndex == -1 &&
                   string.IsNullOrEmpty(yearCBO.Text) &&
                   string.IsNullOrEmpty(monthCBO.Text) &&
                   string.IsNullOrEmpty(dayCBO.Text);
        }

        /// <summary>
        /// Sets the date from data passed to the control, handling common database date patterns.
        /// Accepts DateTime, DateTime?, string representations, or null/empty values.
        /// </summary>
        /// <param name="data">The date data to set</param>
        public void SetDateFromData(object data)
        {
            if (data == null || data == DBNull.Value)
            {
                ClearDate();
                return;
            }

            if (data is DateTime dateTime)
            {
                SetDate(dateTime);
                return;
            }

            // Handle nullable DateTime for C# 7.3 compatibility
            if (data.GetType() == typeof(DateTime?))
            {
                DateTime? nullableDateTime = (DateTime?)data;
                if (nullableDateTime.HasValue)
                    SetDate(nullableDateTime.Value);
                else
                    ClearDate();
                return;
            }

            if (data is string dateString)
            {
                if (string.IsNullOrWhiteSpace(dateString) || dateString.Trim() == "")
                {
                    ClearDate();
                    return;
                }

                // Try to parse various date formats
                if (DateTime.TryParse(dateString, out DateTime parsedDate))
                {
                    SetDate(parsedDate);
                    return;
                }

                // Try parsing the format used in the codebase: "yyyy MM dd"
                if (DateTime.TryParseExact(dateString, "yyyy MM dd", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    SetDate(parsedDate);
                    return;
                }

                // If we can't parse it, clear the control
                ClearDate();
                return;
            }

            // Try converting to DateTime as a last resort
            try
            {
                DateTime convertedDate = Convert.ToDateTime(data);
                SetDate(convertedDate);
            }
            catch
            {
                ClearDate();
            }
        }

        private void InitializeComboBoxes()
        {
            // Initialize events
            yearCBO.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            monthCBO.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            dayCBO.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            
            yearCBO.TextChanged += ComboBox_TextChanged;
            monthCBO.TextChanged += ComboBox_TextChanged;
            dayCBO.TextChanged += ComboBox_TextChanged;

            yearCBO.KeyPress += ComboBox_KeyPress;
            monthCBO.KeyPress += ComboBox_KeyPress;
            dayCBO.KeyPress += ComboBox_KeyPress;

            // Populate years (current year down to 1900)
            PopulateYears();
            
            // Populate months (01-12)
            PopulateMonths();
            
            // Initially populate days (will be updated based on year/month selection)
            PopulateDays();
        }

        private void PopulateYears()
        {
            yearCBO.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= 1900; year--)
            {
                yearCBO.Items.Add(year);
            }
        }

        private void PopulateMonths()
        {
            monthCBO.Items.Clear();
            for (int month = 1; month <= 12; month++)
            {
                monthCBO.Items.Add(month.ToString("00"));
            }
        }

        private void PopulateDays()
        {
            dayCBO.Items.Clear();
            
            int daysInMonth = 31; // Default
            
            if (yearCBO.SelectedItem != null && monthCBO.SelectedItem != null)
            {
                try
                {
                    int year = (int)yearCBO.SelectedItem;
                    int month = int.Parse(monthCBO.SelectedItem.ToString());
                    daysInMonth = DateTime.DaysInMonth(year, month);
                }
                catch
                {
                    // If parsing fails, default to 31 days
                }
            }
            
            for (int day = 1; day <= daysInMonth; day++)
            {
                dayCBO.Items.Add(day.ToString("00"));
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            // If year or month changed, update the days
            if (sender == yearCBO || sender == monthCBO)
            {
                int selectedDay = -1;
                if (dayCBO.SelectedItem != null)
                {
                    int.TryParse(dayCBO.SelectedItem.ToString(), out selectedDay);
                }

                PopulateDays();

                // Try to maintain the selected day if it's still valid
                if (selectedDay > 0 && selectedDay <= dayCBO.Items.Count)
                {
                    dayCBO.SelectedItem = selectedDay.ToString("00");
                }
            }

            OnValueChanged();
        }

        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null) return;

            string text = comboBox.Text;
            if (string.IsNullOrEmpty(text)) return;

            // Find matching item in the list
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                string item = comboBox.Items[i].ToString();
                if (item.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                {
                    _suppressEvents = true;
                    try
                    {
                        comboBox.SelectedIndex = i;
                        comboBox.SelectionStart = text.Length;
                        comboBox.SelectionLength = item.Length - text.Length;
                    }
                    finally
                    {
                        _suppressEvents = false;
                    }
                    break;
                }
            }
        }

        private void ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox == null) return;

            // Allow backspace and delete
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
                return;

            // Allow digits for all comboboxes
            if (char.IsDigit(e.KeyChar))
            {
                // Validate input based on the combobox
                if (comboBox == yearCBO)
                {
                    // Allow any digits for year (will be validated against the list)
                    return;
                }
                else if (comboBox == monthCBO)
                {
                    // Only allow 0 or 1 as first digit, and appropriate second digits
                    string currentText = comboBox.Text;
                    if (comboBox.SelectionStart == 0)
                    {
                        // First digit: only 0 or 1
                        if (e.KeyChar != '0' && e.KeyChar != '1')
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    else if (comboBox.SelectionStart == 1)
                    {
                        // Second digit: depends on first digit
                        if (currentText.Length > 0)
                        {
                            if (currentText[0] == '0' && (e.KeyChar < '1' || e.KeyChar > '9'))
                            {
                                e.Handled = true;
                                return;
                            }
                            else if (currentText[0] == '1' && (e.KeyChar < '0' || e.KeyChar > '2'))
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                    else if (currentText.Length >= 2)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                else if (comboBox == dayCBO)
                {
                    // Only allow valid day entries (01-31, but will be constrained by actual days in month)
                    string currentText = comboBox.Text;
                    if (comboBox.SelectionStart == 0)
                    {
                        // First digit: 0, 1, 2, or 3
                        if (e.KeyChar < '0' || e.KeyChar > '3')
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    else if (comboBox.SelectionStart == 1)
                    {
                        // Second digit: depends on first digit
                        if (currentText.Length > 0)
                        {
                            if (currentText[0] == '0' && (e.KeyChar < '1' || e.KeyChar > '9'))
                            {
                                e.Handled = true;
                                return;
                            }
                            else if ((currentText[0] == '1' || currentText[0] == '2') && (e.KeyChar < '0' || e.KeyChar > '9'))
                            {
                                e.Handled = true;
                                return;
                            }
                            else if (currentText[0] == '3' && (e.KeyChar < '0' || e.KeyChar > '1'))
                            {
                                e.Handled = true;
                                return;
                            }
                        }
                    }
                    else if (currentText.Length >= 2)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                return;
            }

            // Block all other characters
            e.Handled = true;
        }

        private void OnValueChanged()
        {
            if (_suppressEvents) return;
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
