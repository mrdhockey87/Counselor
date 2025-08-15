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
        private DateTime _originalDate = new DateTime(0); // Track the original date value

        [Browsable(true)]
        public event EventHandler ValueChanged;

        [Browsable(true)]
        public event EventHandler DateValueChanged;

        public CQPDatePicker()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        /// <summary>
        /// Gets or sets the selected date. Returns DateTime.MinValue if no valid date is selected.
        /// Use SetDate() and GetDate() methods for more control over blank/null handling.
        /// For string-based date handling, use GetDateString() and SetDateString() methods.
        /// 
        /// Example usage:
        /// // Setting dates from data
        /// datePicker.SetDateFromData(soldier.DateOfBirth);  // Handles DateTime, null, blank values
        /// datePicker.SetDate(new DateTime(2023, 5, 15));    // Set specific date
        /// datePicker.SetDateString("2023 05 15");           // Set date from string
        /// datePicker.ClearDate();                           // Clear the control
        /// 
        /// // Getting dates
        /// DateTime selectedDate = datePicker.GetDate();     // Get selected date or special values
        /// string dateString = datePicker.GetDateString();   // Get date as "YYYY MM DD" or empty string
        /// bool hasDate = datePicker.HasValidDate();         // Check if valid date selected
        /// bool isEmpty = datePicker.IsBlank();              // Check if control is empty
        /// 
        /// // Working with database/form data
        /// if (datePicker.HasValidDate())
        /// {
        ///     soldier.DateOfBirth = datePicker.GetDate();
        ///     // OR for string format
        ///     dateStringForForm = datePicker.GetDateString();
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
                if (cboYear.SelectedItem != null && cboMonth.SelectedItem != null && cboDay.SelectedItem != null)
                {
                    int year = (int)cboYear.SelectedItem;
                    int month = int.Parse(cboMonth.SelectedItem.ToString());
                    int day = int.Parse(cboDay.SelectedItem.ToString());
                    return new DateTime(year, month, day);
                }
            }
            catch
            {
                // Invalid date combination
            }

            // Check if any fields have values but not all (partial date)
            if (cboYear.SelectedItem != null || cboMonth.SelectedItem != null || cboDay.SelectedItem != null)
            {
                return DateTime.MinValue; // Invalid/incomplete date
            }

            return new DateTime(0); // Blank/empty date (following codebase pattern)
        }

        /// <summary>
        /// Gets the selected date as a string in "YYYY MM DD" format.
        /// Returns empty string if no valid date is selected or if the control is blank.
        /// </summary>
        /// <returns>Date string in "YYYY MM DD" format or empty string</returns>
        public string GetDateString()
        {
            try
            {
                if (cboYear.SelectedItem != null && cboMonth.SelectedItem != null && cboDay.SelectedItem != null)
                {
                    int year = (int)cboYear.SelectedItem;
                    int month = int.Parse(cboMonth.SelectedItem.ToString());
                    int day = int.Parse(cboDay.SelectedItem.ToString());
                    
                    // Validate the date is constructible
                    DateTime testDate = new DateTime(year, month, day);
                    return testDate.ToString("yyyy MM dd");
                }
            }
            catch
            {
                // Invalid date combination
            }

            return string.Empty; // Return empty string for invalid or blank dates
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

                    cboYear.SelectedItem = date.Year;
                    cboMonth.SelectedItem = date.Month.ToString("00");
                    PopulateDays(); // Refresh days for the selected year/month
                    cboDay.SelectedItem = date.Day.ToString("00");
                }
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        /// <summary>
        /// Sets the date value from a string in "YYYY MM DD" format.
        /// Pass null, empty string, or whitespace to clear the control.
        /// </summary>
        /// <param name="dateString">Date string in "YYYY MM DD" format or empty/null to clear</param>
        public void SetDateString(string dateString)
        {
            _suppressEvents = true;
            try
            {
                if (string.IsNullOrWhiteSpace(dateString) || dateString.Trim() == "")
                {
                    ClearDate();
                    return;
                }

                // Try parsing the "yyyy MM dd" format
                if (DateTime.TryParseExact(dateString.Trim(), "yyyy MM dd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Ensure the year is within our range
                    if (parsedDate.Year < 1900 || parsedDate.Year > DateTime.Now.Year)
                    {
                        ClearDate();
                        return;
                    }

                    cboYear.SelectedItem = parsedDate.Year;
                    cboMonth.SelectedItem = parsedDate.Month.ToString("00");
                    PopulateDays(); // Refresh days for the selected year/month
                    cboDay.SelectedItem = parsedDate.Day.ToString("00");
                }
                else
                {
                    // If parsing fails, clear the control
                    ClearDate();
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
                cboYear.SelectedIndex = -1;
                cboMonth.SelectedIndex = -1;
                cboDay.SelectedIndex = -1;
                cboYear.Text = "";
                cboMonth.Text = "";
                cboDay.Text = "";
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
            return cboYear.SelectedIndex == -1 &&
                   cboMonth.SelectedIndex == -1 &&
                   cboDay.SelectedIndex == -1 &&
                   string.IsNullOrEmpty(cboYear.Text) &&
                   string.IsNullOrEmpty(cboMonth.Text) &&
                   string.IsNullOrEmpty(cboDay.Text);
        }

        /// <summary>
        /// Sets the date from data passed to the control, handling common database date patterns.
        /// Accepts DateTime, DateTime?, string representations, or null/empty values.
        /// This method also sets the original date value for tracking changes.
        /// For string-specific operations, use SetDateString() method.
        /// </summary>
        /// <param name="data">The date data to set</param>
        public void SetDateFromData(object data)
        {
            if (data == null || data == DBNull.Value)
            {
                ClearDate();
                SetOriginalDate(new DateTime(0)); // Set original as blank
                return;
            }

            if (data is DateTime dateTime)
            {
                SetDate(dateTime);
                SetOriginalDate(dateTime);
                return;
            }

            // Handle nullable DateTime for C# 7.3 compatibility
            if (data.GetType() == typeof(DateTime?))
            {
                DateTime? nullableDateTime = (DateTime?)data;
                if (nullableDateTime.HasValue)
                {
                    SetDate(nullableDateTime.Value);
                    SetOriginalDate(nullableDateTime.Value);
                }
                else
                {
                    ClearDate();
                    SetOriginalDate(new DateTime(0));
                }
                return;
            }

            if (data is string dateString)
            {
                if (string.IsNullOrWhiteSpace(dateString) || dateString.Trim() == "")
                {
                    ClearDate();
                    SetOriginalDate(new DateTime(0));
                    return;
                }

                // Try to parse various date formats
                if (DateTime.TryParse(dateString, out DateTime parsedDate))
                {
                    SetDate(parsedDate);
                    SetOriginalDate(parsedDate);
                    return;
                }

                // Try parsing the format used in the codebase: "yyyy MM dd"
                if (DateTime.TryParseExact(dateString, "yyyy MM dd",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out parsedDate))
                {
                    SetDate(parsedDate);
                    SetOriginalDate(parsedDate);
                    return;
                }

                // If we can't parse it, clear the control
                ClearDate();
                SetOriginalDate(new DateTime(0));
                return;
            }

            // Try converting to DateTime as a last resort
            try
            {
                DateTime convertedDate = Convert.ToDateTime(data);
                SetDate(convertedDate);
                SetOriginalDate(convertedDate);
            }
            catch
            {
                ClearDate();
                SetOriginalDate(new DateTime(0));
            }
        }

        /// <summary>
        /// Sets the date from a string and establishes it as the original date for change tracking.
        /// Accepts string in "YYYY MM DD" format or empty/null values to clear.
        /// This is useful when initially loading string date data and you want to track changes from it.
        /// </summary>
        /// <param name="dateString">Date string in "YYYY MM DD" format or empty/null to clear</param>
        public void SetDateStringFromData(string dateString)
        {
            SetDateString(dateString);
            
            // Set original date based on what was actually set
            if (string.IsNullOrWhiteSpace(dateString) || dateString.Trim() == "")
            {
                SetOriginalDate(new DateTime(0));
            }
            else if (DateTime.TryParseExact(dateString.Trim(), "yyyy MM dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                SetOriginalDate(parsedDate);
            }
            else
            {
                SetOriginalDate(new DateTime(0)); // Invalid format, treat as blank
            }
        }
        
        /// <summary>
        /// Sets the original date value used for change tracking.
        /// This should be called when initially loading data from database or setting baseline values.
        /// </summary>
        /// <param name="date">The original date to track changes against</param>
        public void SetOriginalDate(DateTime date)
        {
            _originalDate = date;
        }

        /// <summary>
        /// Gets the original date value that was initially set.
        /// </summary>
        public DateTime GetOriginalDate()
        {
            return _originalDate;
        }

        /// <summary>
        /// Returns true if the current valid date value is different from the original date value.
        /// Only considers valid dates for comparison (not blank or invalid dates).
        /// </summary>
        public bool HasDateChanged()
        {
            DateTime currentDate = GetDate();
            
            // Only fire if we have a valid current date
            if (!HasValidDate())
                return false;
                
            // Compare with original date - consider DateTime(0) as equivalent to MinValue for blanks
            if (_originalDate == new DateTime(0) || _originalDate == DateTime.MinValue)
            {
                // Original was blank/invalid, current is valid - this is a change
                return true;
            }
            
            // Both are valid dates, compare them
            return currentDate != _originalDate;
        }

        private void InitializeComboBoxes()
        {
            // Initialize events
            cboYear.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            cboMonth.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            cboDay.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            cboYear.TextChanged += ComboBox_TextChanged;
            cboMonth.TextChanged += ComboBox_TextChanged;
            cboDay.TextChanged += ComboBox_TextChanged;

            cboYear.KeyPress += ComboBox_KeyPress;
            cboMonth.KeyPress += ComboBox_KeyPress;
            cboDay.KeyPress += ComboBox_KeyPress;

            // Populate years (current year down to 1900)
            PopulateYears();

            // Populate months (01-12)
            PopulateMonths();

            // Initially populate days (will be updated based on year/month selection)
            PopulateDays();
        }

        private void PopulateYears()
        {
            cboYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= 1900; year--)
            {
                cboYear.Items.Add(year);
            }
        }

        private void PopulateMonths()
        {
            cboMonth.Items.Clear();
            for (int month = 1; month <= 12; month++)
            {
                cboMonth.Items.Add(month.ToString("00"));
            }
        }

        private void PopulateDays()
        {
            cboDay.Items.Clear();

            int daysInMonth = 31; // Default

            if (cboYear.SelectedItem != null && cboMonth.SelectedItem != null)
            {
                try
                {
                    int year = (int)cboYear.SelectedItem;
                    int month = int.Parse(cboMonth.SelectedItem.ToString());
                    daysInMonth = DateTime.DaysInMonth(year, month);
                }
                catch
                {
                    // If parsing fails, default to 31 days
                }
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                cboDay.Items.Add(day.ToString("00"));
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;

            // If year or month changed, update the days
            if (sender == cboYear || sender == cboMonth)
            {
                int selectedDay = -1;
                if (cboDay.SelectedItem != null)
                {
                    int.TryParse(cboDay.SelectedItem.ToString(), out selectedDay);
                }

                PopulateDays();

                // Try to maintain the selected day if it's still valid
                if (selectedDay > 0 && selectedDay <= cboDay.Items.Count)
                {
                    cboDay.SelectedItem = selectedDay.ToString("00");
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
                if (comboBox == cboYear)
                {
                    // Allow any digits for year (will be validated against the list)
                    return;
                }
                else if (comboBox == cboMonth)
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
                else if (comboBox == cboDay)
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
            
            // Fire DateValueChanged only when we have a valid date that differs from original
            if (HasDateChanged())
            {
                DateValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}