using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PdfImage = iText.Layout.Element.Image;

namespace SOFA_Generator
{
    public partial class Form1 : Form
    {
        private bool _hasShownFileMissingMessage = false;
        private bool isDrawing = false;
        private Point lastPoint = Point.Empty;
        private Bitmap signatureBitmap;
        private string excelFilePath = "";  // Set via the "SOFA Database" browse button to the user's local OneDrive-synced copy
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;

        public Form1()
        {
            InitializeComponent();
            signatureBitmap = new Bitmap(signaturePanel.Width, signaturePanel.Height);

            // Event hookups
            signaturePanel.Paint += new PaintEventHandler(signaturePanel_Paint);
            signaturePanel.MouseDown += new MouseEventHandler(signaturePanel_MouseDown);
            signaturePanel.MouseMove += new MouseEventHandler(signaturePanel_MouseMove);
            signaturePanel.MouseUp += new MouseEventHandler(signaturePanel_MouseUp);
            btnSearch.Click += new EventHandler(this.btnSearch_Click);
            statusComboBox.SelectedIndexChanged += new EventHandler(this.statusComboBox_SelectedIndexChanged);
            this.LanguageButton.Click += new System.EventHandler(this.LanguageButton_Click);
            motorcycleCheckBox.CheckedChanged += VehicleCategory_CheckedChanged;
            btnReset.Click += new EventHandler(this.btnReset_Click);
            PermitSearch.Click += PermitSearch_Click;
            InitializeStampComboBox();
            sigPlusNET1.SetTabletState(1);
            sigPlusNET1.SetJustifyMode(0);
            excelFilePath = TryGetOneDriveDefaultExcelPath() ?? excelFilePath;
            HideFormFields();
            LoadIssuerNames();
            InitializeUnitComboBox();
            InitializeCivilianRankComboBox();
            InitializeCatPaxComboBox();
        }

        private static string? TryGetOneDriveDefaultExcelPath()
        {
            string[] envVars = { "OneDriveCommercial", "OneDrive" };
            foreach (var envVar in envVars)
            {
                var root = Environment.GetEnvironmentVariable(envVar);
                if (string.IsNullOrEmpty(root))
                    continue;

                var candidate = Path.Combine(root, "18 SFS Pass & Registration - - General", "SOFA King Data.xlsx");
                if (File.Exists(candidate))
                    return candidate;
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideFormFields();
            LoadIssuerNames();
            InitializeUnitComboBox();
            InitializeCivilianRankComboBox();
        }

        private bool TryOpenSofa(out ExcelPackage package)
        {
            package = null;
            try
            {
                if (string.IsNullOrEmpty(excelFilePath) || !File.Exists(excelFilePath))
                {
                    if (!_hasShownFileMissingMessage)
                    {
                        _hasShownFileMissingMessage = true;
                        MessageBox.Show("Please select an Excel file first.",
                                        "SOFA King",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    return false;
                }

                package = new ExcelPackage(new FileInfo(excelFilePath));
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Access denied. Please check file permissions.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("File is in use by another application.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error opening file: {ex.Message}",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
        }

        private void LoadIssuerNames()
        {
            if (!TryOpenSofa(out var package))
                return;

            var defendersSheet = package.Workbook.Worksheets["Defenders"];
            if (defendersSheet == null)
            {
                MessageBox.Show("The 'Defenders' sheet was not found in the Excel file.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            issuerComboBox.Items.Clear();
            for (int row = 2; row <= defendersSheet.Dimension.End.Row; row++)
            {
                var defender = defendersSheet.Cells[row, 1].Text.Trim();
                if (!string.IsNullOrEmpty(defender))
                    issuerComboBox.Items.Add(defender);
            }
            if (issuerComboBox.Items.Count > 0)
                issuerComboBox.SelectedIndex = 0;
        }

        private List<string> _allUnits = new();
        private AutoCompleteStringCollection _unitsAuto = new AutoCompleteStringCollection();
        private HashSet<string> _validUnits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        private void InitializeUnitComboBox()
        {
            if (!TryOpenSofa(out var package)) return;
            var defendersSheet = package.Workbook.Worksheets["Defenders"];
            if (defendersSheet == null) return;

            _validUnits.Clear();
            unitComboBox.Items.Clear();

            for (int row = 2; row <= defendersSheet.Dimension.End.Row; row++)
            {
                var unit = defendersSheet.Cells[row, 2].Text.Trim();
                if (!string.IsNullOrEmpty(unit) && _validUnits.Add(unit))
                    unitComboBox.Items.Add(unit);
            }

            // autocomplete stays as you liked it
            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(_validUnits.ToArray());
            unitComboBox.DropDownStyle = ComboBoxStyle.DropDown; // allow typing
            unitComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            unitComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            unitComboBox.AutoCompleteCustomSource = autoComplete;

            if (unitComboBox.Items.Count > 0)
                unitComboBox.SelectedIndex = 0;
        }

        private bool ValidateUnit()
        {
            var unitText = unitComboBox.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(unitText) || !_validUnits.Contains(unitText))
            {
                MessageBox.Show(
                    "Please select a Unit from the list. Typing a custom unit is not allowed.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                unitComboBox.Focus();
                unitComboBox.SelectAll();
                return false;
            }
            return true;
        }


        private void InitializeCivilianRankComboBox()
        {
            if (!TryOpenSofa(out var package))
                return;

            var civSheet = package.Workbook.Worksheets["Defenders"];
            if (civSheet == null)
                return;

            civilianRankComboBox.Items.Clear();
            int added = 0;
            var endRow = civSheet.Dimension?.End.Row ?? 0;
            for (int row = 2; row <= endRow; row++)
            {
                // Column C is index 3
                var rank = civSheet.Cells[row, 3].Text.Trim();
                if (!string.IsNullOrEmpty(rank))
                {
                    civilianRankComboBox.Items.Add(rank);
                    added++;
                }
            }

            if (added == 0)
            {
                MessageBox.Show("No civilian ranks were found in sheet “Defenders”, column C.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                civilianRankComboBox.SelectedIndex = 0;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.InitialDirectory = Path.GetDirectoryName(excelFilePath);
                dlg.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = dlg.FileName;
                    MessageBox.Show($"Excel file path set to: {excelFilePath}",
                                    "File Selected",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    LoadIssuerNames();
                    InitializeUnitComboBox();
                    InitializeCivilianRankComboBox();
                }
            }
        }

        private void HideFormFields()
        {
            lastNameTextBox.Visible = false;
            firstNameTextBox.Visible = false;
            permit1TextBox.Visible = false;
            issue1DateTimePicker.Visible = false;
            exp1DateTimePicker.Visible = false;
            permit2TextBox.Visible = false;
            issue2DateTimePicker.Visible = false;
            exp2DateTimePicker.Visible = false;
            signaturePanel.Visible = false;
            btnSaveSignature.Visible = false;
            btnRequestSignature.Visible = false;
            btnGeneratePermitNumber.Visible = false;
            msfTextBox.Visible = false;
            catPaxComboBox.Visible = false;
            autoJeepCheckBox.Visible = false;
            motorcycleCheckBox.Visible = false;
            dobDateTimePicker.Visible = false;
            heightTextBox.Visible = false;
            weightTextBox.Visible = false;
            hairColorComboBox.Visible = false;
            eyeColorComboBox.Visible = false;
            restrictionsBox.Visible = false;
            remarksBox.Visible = false;
            issuerComboBox.Visible = false;
            sexComboBox.Visible = false;
            statusComboBox.Visible = false;
            unitComboBox.Visible = false;
            sigPlusNET1.Visible = false;
            signaturegroupBox.Visible = false;
            picturegroupBox.Visible = false;
            stampComboBox.Visible = false;
            PermitcheckBox.Visible = false;
            mopedCheckBox.Visible = false;
            otherCheckBox.Visible = false;

            // Also hide labels
            sexLabel.Visible = false;
            dobLabel.Visible = false;
            heightLabel.Visible = false;
            weightLabel.Visible = false;
            hairColorLabel.Visible = false;
            eyeColorLabel.Visible = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            lastNameLabel.Visible = false;
            rankLabel.Visible = false;
            statusLabel.Visible = false;
            firstNameLabel.Visible = false;
            unitLabel.Visible = false;
            remarksLabel.Visible = false;
            catLabel.Visible = false;
            issuerLabel.Visible = false;
            stampLabel.Visible = false;
            MSFlabel.Visible = false;
        }

        private void statusComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Safeguard: Ensure controls are initialized
            if (statusComboBox == null || militaryRankComboBox == null || civilianRankComboBox == null || naLabel == null)
            {
                return;
            }

            // Hide all rank-related controls initially
            militaryRankComboBox.Visible = false;
            civilianRankComboBox.Visible = false;
            naLabel.Visible = false;

            // Show the appropriate control based on the selected status
            switch (statusComboBox.SelectedItem?.ToString())
            {
                case "AD":  // Active Duty
                case "R/G": // Reserves/Guard
                    militaryRankComboBox.Visible = true;
                    break;
                case "CIV": // Civilian
                    civilianRankComboBox.Visible = true;
                    break;
                case "CTR": // Contractor
                case "DEP": // Dependent
                    naLabel.Visible = true;
                    break;
            }
        }

        private void ShowFormFields(bool isExistingEntry)
        {
            ShowFormFields(isExistingEntry, signaturePanel);
        }

        private void ShowFormFields(bool isExistingEntry, Panel signaturePanel)
        {
            lastNameTextBox.Visible = true;
            firstNameTextBox.Visible = true;
            dobDateTimePicker.Visible = true;
            heightTextBox.Visible = true;
            weightTextBox.Visible = true;
            hairColorComboBox.Visible = true;
            eyeColorComboBox.Visible = true;
            restrictionsBox.Visible = true;
            remarksBox.Visible = true;
            issuerComboBox.Visible = true;
            sexComboBox.Visible = true;
            permit1TextBox.Visible = true;
            issue1DateTimePicker.Visible = true;
            exp1DateTimePicker.Visible = true;
            statusComboBox.Visible = true;
            unitComboBox.Visible = true;
            sigPlusNET1.Visible = true;
            signaturegroupBox.Visible = true;
            picturegroupBox.Visible = true;
            stampComboBox.Visible = true;
            PermitcheckBox.Visible = true;
            mopedCheckBox.Visible = true;
            otherCheckBox.Visible = true;

            // Show labels
            sexLabel.Visible = true;
            dobLabel.Visible = true;
            heightLabel.Visible = true;
            weightLabel.Visible = true;
            hairColorLabel.Visible = true;
            eyeColorLabel.Visible = true;
            sexLabel.Visible = true;
            rankLabel.Visible = true;
            statusLabel.Visible = true;
            firstNameLabel.Visible = true;
            lastNameLabel.Visible = true;
            unitLabel.Visible = true;
            remarksLabel.Visible = true;
            unitLabel.Visible = true;
            issuerLabel.Visible = true;
            stampLabel.Visible = true;

            // Show Permit 1 GroupBox since it's used for both new and existing entries
            groupBox1.Visible = true;

            // Existing entry logic
            if (isExistingEntry)
            {
                groupBox2.Visible = true;
                permit2TextBox.Visible = true;
                issue2DateTimePicker.Visible = true;
                exp2DateTimePicker.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }

            btnSaveSignature.Visible = true;
            btnRequestSignature.Visible = true;
            btnGeneratePermitNumber.Visible = true;
            signaturePanel.Visible = true;
            autoJeepCheckBox.Visible = true;
            motorcycleCheckBox.Visible = true;
            msfTextBox.Visible = true;
            UpdateVehicleCategoryFieldsVisibility();
        }

        private void UpdateVehicleCategoryFieldsVisibility()
        {
            bool showRiderFields =
                motorcycleCheckBox.Checked ||
                (mopedCheckBox != null && mopedCheckBox.Checked) ||
                (otherCheckBox != null && otherCheckBox.Checked);

            msfTextBox.Visible = showRiderFields;
            catPaxComboBox.Visible = showRiderFields;
            catLabel.Visible = showRiderFields;
            MSFlabel.Visible = showRiderFields;

            if (!showRiderFields)
            {
                msfTextBox.Clear();
                catPaxComboBox.SelectedIndex = -1;
            }
        }

        private void VehicleCategory_CheckedChanged(object sender, EventArgs e)
        {
            UpdateVehicleCategoryFieldsVisibility();
        }

        private void PermitSearch_Click(object sender, EventArgs e)
        {
            var permitNumber = PermitSearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(permitNumber))
            {
                MessageBox.Show("Please enter a permit number to search.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (!TryOpenSofa(out var package))
                return;
            var ws = package.Workbook.Worksheets[0];
            int rowFound = -1;
            for (int row = 2; row <= ws.Dimension.End.Row; row++)
            {
                if (ws.Cells[row, 7].Text.Trim() == permitNumber || ws.Cells[row, 10].Text.Trim() == permitNumber)
                {
                    rowFound = row;
                    break;
                }
            }
            if (rowFound < 0)
            {
                MessageBox.Show($"Permit number {permitNumber} not found.",
                                "SOFA King",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            var data = new Dictionary<string, string>
            {
                { "Last Name", ws.Cells[rowFound, 1].Text },
                { "First Name", ws.Cells[rowFound, 2].Text },
                { "Status", ws.Cells[rowFound, 3].Text },
                { "Rank", ws.Cells[rowFound, 4].Text },
                { "Unit", ws.Cells[rowFound, 5].Text },
                { "DoD ID #", ws.Cells[rowFound, 6].Text },
                { "Permit #1", ws.Cells[rowFound, 7].Text },
                { "Issue 1", ws.Cells[rowFound, 8].Text },
                { "Exp 1", ws.Cells[rowFound, 9].Text },
                { "Permit #2", ws.Cells[rowFound, 10].Text },
                { "Issue 2", ws.Cells[rowFound, 11].Text },
                { "Exp 2", ws.Cells[rowFound, 12].Text },
                { "MSF", ws.Cells[rowFound, 13].Text },
                { "CAT/PAX", ws.Cells[rowFound, 14].Text },
                { "Sex", ws.Cells[rowFound, 15].Text },
                { "DOB", ws.Cells[rowFound, 16].Text },
                { "Height", ws.Cells[rowFound, 17].Text },
                { "Weight", ws.Cells[rowFound, 18].Text },
                { "HairColor", ws.Cells[rowFound, 19].Text },
                { "EyeColor", ws.Cells[rowFound, 20].Text },
                { "GlassesContacts", ws.Cells[rowFound, 21].Text },
                { "Remarks", ws.Cells[rowFound, 22].Text },
                { "Stamp", ws.Cells[rowFound, 23].Text }
            };
            PopulateFormWithExistingData(data);
        }

        private void ClearFormFields()
        {
            lastNameTextBox.Text = string.Empty;
            firstNameTextBox.Text = string.Empty;
            dodIdTextBox.Text = string.Empty;
            unitComboBox.SelectedIndex = -1;
            permit1TextBox.Text = string.Empty;
            issue1DateTimePicker.Value = DateTime.Today;
            exp1DateTimePicker.Value = DateTime.Today;
            permit2TextBox.Text = string.Empty;
            issue2DateTimePicker.Value = DateTime.Today;
            exp2DateTimePicker.Value = DateTime.Today;

            // New fields
            dobDateTimePicker.Value = DateTime.Today;
            heightTextBox.Text = string.Empty;
            weightTextBox.Text = string.Empty;
            hairColorComboBox.SelectedIndex = -1;
            eyeColorComboBox.SelectedIndex = -1;
            restrictionsBox.Checked = false;
        }

        private void InitializeStampComboBox()
        {
            stampComboBox.Items.Clear();
            stampComboBox.Items.AddRange(new object[] { "", "Student Driver", "On Base Only", "TDY", "Limited" });
            stampComboBox.SelectedIndex = 0;  // Optional: Set default value

        }

        private void btnSearch_Click(object? sender, EventArgs e)
        {
            string dodId = dodIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(dodId))
            {
                MessageBox.Show("Please enter a DoD ID.");
                return;
            }

            if (!IsValidDoDId(dodId))
            {
                MessageBox.Show("Please enter a valid 10-digit DoD ID.");
                return;
            }

            // Always reset the form before populating new data
            ResetForm();

            // Check if the Excel file exists before searching
            if (!File.Exists(excelFilePath))
            {
                MessageBox.Show("Excel file not found. Please use the 'SOFA Database' button to select the correct file.");
                return;
            }

            var customerData = GetCustomerDataFromExcel(dodId);

            if (customerData != null)
            {
                PopulateFormWithExistingData(customerData);
                ShowFormFields(isExistingEntry: true);
                MessageBox.Show("Customer data found.");
            }
            else
            {
                dodIdTextBox.Text = dodId; // Keep the DoD ID in the textbox
                ShowFormFields(isExistingEntry: false);
                MessageBox.Show("DoD ID not found. Please enter new data.");
            }
        }

        private Dictionary<string, string> GetCustomerDataFromExcel(string dodId)
        {
            try
            {
                // Check if the file exists first
                if (!File.Exists(excelFilePath))
                {
                    MessageBox.Show("Excel file not found. Please select the correct file using the 'SOFA Database' button.");
                    return null!;
                }

                FileInfo fileInfo = new FileInfo(excelFilePath);
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    if (worksheet == null)
                    {
                        MessageBox.Show("The required worksheet is not available in the Excel file. Please check the file format.");
                        return null!;
                    }

                    var row = worksheet.Cells["F:F"].FirstOrDefault(c => c.Text == dodId);  // Column F for "DoD ID #"

                    if (row != null)
                    {
                        int rowIndex = row.Start.Row;
                        var data = new Dictionary<string, string>
                {
                    { "Last Name", worksheet.Cells[rowIndex, 1].Text },
                    { "First Name", worksheet.Cells[rowIndex, 2].Text },
                    { "Status", worksheet.Cells[rowIndex, 3].Text },
                    { "Rank", worksheet.Cells[rowIndex, 4].Text },
                    { "Unit", worksheet.Cells[rowIndex, 5].Text },
                    { "DoD ID #", worksheet.Cells[rowIndex, 6].Text },
                    { "Permit #1", worksheet.Cells[rowIndex, 7].Text },
                    { "Issue 1", worksheet.Cells[rowIndex, 8].Text },
                    { "Exp 1", worksheet.Cells[rowIndex, 9].Text },
                    { "Permit #2", worksheet.Cells[rowIndex, 10].Text },
                    { "Issue 2", worksheet.Cells[rowIndex, 11].Text },
                    { "Exp 2", worksheet.Cells[rowIndex, 12].Text },
                    { "MSF", worksheet.Cells[rowIndex, 13].Text },
                    { "CAT/PAX", worksheet.Cells[rowIndex, 14].Text },
                    { "Sex", worksheet.Cells[rowIndex, 15].Text },
                    { "DOB", worksheet.Cells[rowIndex, 16].Text },
                    { "Height", worksheet.Cells[rowIndex, 17].Text },
                    { "Weight", worksheet.Cells[rowIndex, 18].Text },
                    { "HairColor", worksheet.Cells[rowIndex, 19].Text },
                    { "EyeColor", worksheet.Cells[rowIndex, 20].Text },
                    { "GlassesContacts", worksheet.Cells[rowIndex, 21].Text },
                    { "Remarks", worksheet.Cells[rowIndex, 22].Text },
                    { "Stamp", worksheet.Cells[rowIndex, 23].Text },
                    { "Moped", worksheet.Cells[rowIndex, 24].Text },
                    { "Other", worksheet.Cells[rowIndex, 25].Text }
                };
                        return data;
                    }
                    else
                    {
                        MessageBox.Show("DoD ID not found in the Excel file.");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"Error: Excel file not found. {ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show($"Error: Worksheet not found or index out of range. {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }

            return null!;
        }

        private bool IsValidDoDId(string dodId)
        {
            // Trim the DoD ID to remove any leading/trailing spaces
            dodId = dodId.Trim();

            // Check the length of the DoD ID
            if (dodId.Length != 10)
            {
                MessageBox.Show($"Invalid length: {dodId.Length}. DoD ID must be 10 digits long.");
                return false;
            }

            // Attempt to parse the DoD ID as a number
            if (!long.TryParse(dodId, out long idNumber))
            {
                MessageBox.Show($"Invalid number: '{dodId}'. Could not parse as a number.");
                return false;
            }

            // Ensure the number is within the valid range for a 10-digit DoD ID
            if (idNumber < 1000000000 || idNumber > 9999999999)
            {
                MessageBox.Show($"Number out of range: {idNumber}. Valid range is 1000000000 to 9999999999.");
                return false;
            }

            return true;
        }

        private void PopulateFormWithExistingData(Dictionary<string, string> data)
        {
            // Reset the form to ensure no residual data is left from previous entries
            ResetForm();

            // Populate basic fields
            lastNameTextBox.Text = data["Last Name"];
            firstNameTextBox.Text = data["First Name"];
            dodIdTextBox.Text = data["DoD ID #"];

            // Load Permit #1 and its associated fields
            permit1TextBox.Text = data["Permit #1"];

            if (DateTime.TryParse(data["Issue 1"], out DateTime issue1Date))
            {
                issue1DateTimePicker.Value = issue1Date;
            }

            if (DateTime.TryParse(data["Exp 1"], out DateTime exp1Date))
            {
                exp1DateTimePicker.Value = exp1Date;
            }

            // If Permit #2 exists, load it into the Permit #2 fields
            if (!string.IsNullOrEmpty(data["Permit #2"]))
            {
                permit2TextBox.Text = data["Permit #2"];

                if (DateTime.TryParse(data["Issue 2"], out DateTime issue2Date))
                {
                    issue2DateTimePicker.Value = issue2Date;
                }

                if (DateTime.TryParse(data["Exp 2"], out DateTime exp2Date))
                {
                    exp2DateTimePicker.Value = exp2Date;
                }

                // Show Permit #2 fields if data exists
                groupBox2.Visible = true;
            }
            else
            {
                // Hide Permit #2 fields if no data is available
                groupBox2.Visible = false;
            }

            // Populate Status and Rank
            string status = data["Status"].Trim();
            statusComboBox.SelectedItem = statusComboBox.Items.Cast<string>().FirstOrDefault(item => item == status);

            string rank = data["Rank"].Trim();
            switch (status)
            {
                case "CIV":
                    civilianRankComboBox.Visible = true;
                    civilianRankComboBox.SelectedItem = civilianRankComboBox.Items.Cast<string>().FirstOrDefault(item => item == rank);
                    break;
                case "AD":
                case "R/G":
                case "CTR":
                    militaryRankComboBox.Visible = true;
                    militaryRankComboBox.SelectedItem = militaryRankComboBox.Items.Cast<string>().FirstOrDefault(item => item == rank);
                    break;
                default:
                    naLabel.Visible = true;
                    break;
            }

            // Populate Unit
            string unit = data["Unit"].Trim();
            unitComboBox.SelectedItem = unitComboBox.Items.Cast<string>().FirstOrDefault(item => item.Trim() == unit);

            // Populate Sex
            string sex = data["Sex"].Trim();
            sexComboBox.SelectedItem = sexComboBox.Items.Cast<string>().FirstOrDefault(item => item == sex);

            // Populate Stamp from the Excel file (column 23)
            string stamp = data["Stamp"].Trim();
            stampComboBox.SelectedItem = stampComboBox.Items.Cast<string>().FirstOrDefault(item => item == stamp);

            // Automatically check "Auto/Jeep" because it's implied when customer data is found
            autoJeepCheckBox.Checked = true;  // Since "Auto/Jeep" is always implied

            // Check if MSF is present and populate msfTextBox accordingly
            if (data.ContainsKey("MSF") && !string.IsNullOrWhiteSpace(data["MSF"]))
            {
                // MSF field has valid data
                msfTextBox.Text = data["MSF"];
                msfTextBox.Visible = true;
                motorcycleCheckBox.Checked = true;  // Only check if MSF data is valid

                // Make the CAT/PAX fields visible and select the matching item
                catPaxComboBox.Visible = true;
                catLabel.Visible = true;

                string catPaxValue = data["CAT/PAX"];
                var matchingItem = catPaxComboBox.Items.Cast<string>().FirstOrDefault(item => item == catPaxValue);

                if (matchingItem != null)
                {
                    catPaxComboBox.SelectedItem = matchingItem;
                }
                else
                {
                    catPaxComboBox.SelectedIndex = -1;  // Clear the selection if no match is found
                }
            }
            else
            {
                // MSF field is empty or doesn't exist, so uncheck Motorcycle and hide fields
                motorcycleCheckBox.Checked = false;
                msfTextBox.Visible = false;
                catPaxComboBox.Visible = false;
                catLabel.Visible = false;
            }

            // Populate other fields like DOB, Height, Weight, etc.
            if (DateTime.TryParse(data["DOB"], out DateTime dobDate))
            {
                dobDateTimePicker.Value = dobDate;
            }

            heightTextBox.Text = data["Height"];
            weightTextBox.Text = data["Weight"];
            hairColorComboBox.SelectedItem = data["HairColor"];
            eyeColorComboBox.SelectedItem = data["EyeColor"];
            restrictionsBox.Checked = data["GlassesContacts"] == "True";

            // Show form fields relevant to an existing entry
            ShowFormFields(isExistingEntry: true);
        }

        private void signaturePanel_MouseDown(object? sender, MouseEventArgs e)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void ResetForm()
        {
            // Temporarily detach the event handler to avoid triggering during reset
            statusComboBox.SelectedIndexChanged -= statusComboBox_SelectedIndexChanged;

            // Clear all textboxes, comboboxes, and other input fields
            lastNameTextBox.Clear();
            firstNameTextBox.Clear();
            permit1TextBox.Clear();
            permit2TextBox.Clear();
            dobDateTimePicker.Value = DateTime.Today.AddYears(-16); // Default DOB to 16 years ago
            issue1DateTimePicker.Value = DateTime.Today;
            exp1DateTimePicker.Value = DateTime.Today.AddDays(90); // Default expiry date
            issue2DateTimePicker.Value = DateTime.Today;
            exp2DateTimePicker.Value = DateTime.Today.AddDays(90);
            heightTextBox.Clear();
            weightTextBox.Clear();
            hairColorComboBox.SelectedIndex = -1;
            eyeColorComboBox.SelectedIndex = -1;
            sexComboBox.SelectedIndex = -1;
            remarksBox.Clear();
            stampComboBox.SelectedIndex = -1;
            restrictionsBox.Checked = false;
            sigPlusNET1.ClearTablet();
            motorcycleCheckBox.Checked = false;
            autoJeepCheckBox.Checked = false;

            // Reset rank-related controls and visibility
            militaryRankComboBox.SelectedIndex = -1;
            civilianRankComboBox.SelectedIndex = -1;
            militaryRankComboBox.Visible = false;
            civilianRankComboBox.Visible = false;
            naLabel.Visible = false;

            // Reset unit and status combo boxes
            unitComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = -1;


            msfTextBox.Clear();
            catPaxComboBox.SelectedIndex = -1;
            catPaxComboBox.Visible = false;
            catLabel.Visible = false;

            // Reattach the event handler
            statusComboBox.SelectedIndexChanged += statusComboBox_SelectedIndexChanged;
        }

        private void btnReset_Click(object? sender, EventArgs e)
        {
            // Call the ResetForm method when the Reset button is clicked
            ResetForm();
        }

        private void signaturePanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(signatureBitmap))
                {
                    g.DrawLine(Pens.Black, lastPoint, e.Location);
                }
                lastPoint = e.Location;
                signaturePanel.Invalidate();
            }
        }

        private void signaturePanel_MouseUp(object? sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void signaturePanel_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(signatureBitmap, Point.Empty);
        }

        private void btnSaveSignature_Click(object sender, EventArgs e)
        {
            // locate the already‐filled PDF
            var pdfOut = Path.Combine(baseDir, "Resources", "PDF", "Form4EJ_Filled.pdf");
            if (!File.Exists(pdfOut))
            {
                MessageBox.Show("No filled PDF found. Please press Save first.");
                return;
            }

            // print it
            PrintPdf(pdfOut);
        }

        private void PrintPdf(string pdfPath)
        {
            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
            {
                MessageBox.Show("PDF not found.");
                return;
            }

            // This path is stable on Windows 10/11
            string edgeExe = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";

            // If Edge isn't there for some reason, fall back to default PDF handler
            if (!File.Exists(edgeExe))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = pdfPath,
                    UseShellExecute = true
                });
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = edgeExe,
                Arguments = $"\"{pdfPath}\"",
                UseShellExecute = false
            });
        }

        private void InitializeCatPaxComboBox()
        {
            catPaxComboBox.Items.Clear();
            catPaxComboBox.Items.Add("Cat 0A: Bicycles");
            catPaxComboBox.Items.Add("Cat 0B: Electric Bicycles");
            catPaxComboBox.Items.Add("Cat 1: Mopeds 50cc or less");
            catPaxComboBox.Items.Add("Cat 2: Motorcycles 125cc or less");
            catPaxComboBox.Items.Add("Cat 3: Motorcycles 400cc or less");
            catPaxComboBox.Items.Add("Cat 4: Motorcycles 750cc or less");
            catPaxComboBox.Items.Add("Cat 5: Motorcycles over 750cc");

            if (catPaxComboBox.Items.Count > 0)
                catPaxComboBox.SelectedIndex = 0;
        }

        private void SaveDataToExcel(Dictionary<string, string> data)
        {
            FileInfo fileInfo = new FileInfo(excelFilePath);

            if (IsFileLocked(fileInfo))
            {
                MessageBox.Show("The Excel file is currently open in another application. Please close it and try again.");
                return;
            }

            try
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    // Ensure the worksheet has data
                    if (worksheet.Dimension == null || worksheet.Dimension.End.Row == 0)
                    {
                        MessageBox.Show("The Excel sheet is empty or has no header row.");
                        return;
                    }

                    int rowIndex = -1;

                    // Search for the row by matching the "DoD ID #" field
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)  // Assuming row 1 is headers
                    {
                        if (worksheet.Cells[i, 6].Text == data["DoD ID #"])  // Column 6 for "DoD ID #"
                        {
                            rowIndex = i;
                            break;
                        }
                    }

                    // If not found, add a new row
                    if (rowIndex == -1)
                    {
                        rowIndex = worksheet.Dimension.End.Row + 1;
                    }

                    // Save **all** the customer data (for both new and existing rows)
                    worksheet.Cells[rowIndex, 1].Value = data["Last Name"];
                    worksheet.Cells[rowIndex, 2].Value = data["First Name"];
                    worksheet.Cells[rowIndex, 3].Value = data["Status"];
                    worksheet.Cells[rowIndex, 4].Value = data["Rank"];
                    worksheet.Cells[rowIndex, 5].Value = data["Unit"];
                    worksheet.Cells[rowIndex, 6].Value = data["DoD ID #"];

                    // Save Permit #1 details
                    worksheet.Cells[rowIndex, 7].Value = data["PERMIT"];   // Permit #1
                    worksheet.Cells[rowIndex, 8].Value = data["ISSUE"];    // Issue 1
                    worksheet.Cells[rowIndex, 9].Value = data["Exp"];      // Exp 1

                    // Save Permit #2 fields if available
                    if (!string.IsNullOrEmpty(permit2TextBox.Text))
                    {
                        worksheet.Cells[rowIndex, 10].Value = data["PERMIT"];  // Permit #2
                        worksheet.Cells[rowIndex, 11].Value = data["ISSUE"];   // Issue 2
                        worksheet.Cells[rowIndex, 12].Value = data["Exp"];     // Exp 2
                    }

                    // Save additional fields like MSF, CAT/PAX, etc.
                    worksheet.Cells[rowIndex, 13].Value = data["MSF"];
                    worksheet.Cells[rowIndex, 14].Value = data["CAT/PAX"];
                    worksheet.Cells[rowIndex, 15].Value = data["SEX"];
                    worksheet.Cells[rowIndex, 16].Value = data["DOB"];
                    worksheet.Cells[rowIndex, 17].Value = data["HEIGHT"];
                    worksheet.Cells[rowIndex, 18].Value = data["WEIGHT"];
                    worksheet.Cells[rowIndex, 19].Value = data["HAIRCOLOR"];
                    worksheet.Cells[rowIndex, 20].Value = data["EYECOLOR"];
                    worksheet.Cells[rowIndex, 21].Value = data["GLASSES/CONTACTS"];
                    worksheet.Cells[rowIndex, 22].Value = data["Remarks"];
                    worksheet.Cells[rowIndex, 23].Value = data["Stamp"];
                    worksheet.Cells[rowIndex, 24].Value = data["Moped"];
                    worksheet.Cells[rowIndex, 25].Value = data["Other"];

                    // Save the Excel file
                    package.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }
        private string GetSelectedRank()
        {
            // Return the selected rank based on the status
            if (statusComboBox.SelectedItem?.ToString() == "CIV")
            {
                return civilianRankComboBox.SelectedItem?.ToString() ?? string.Empty;
            }
            else if (statusComboBox.SelectedItem?.ToString() == "AD" || statusComboBox.SelectedItem?.ToString() == "R/G")
            {
                return militaryRankComboBox.SelectedItem?.ToString() ?? string.Empty;
            }
            else
            {
                return "N/A";  // For CTR, DEP, and other cases
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }


        private void btnRequestSignature_Click(object sender, EventArgs e)
        {
            sigPlusNET1.ClearTablet();   // Clear any previous signatures
            sigPlusNET1.SetTabletState(1); // Enable the tablet
        }
        private bool CheckFileAndDirectory(string pdfTemplatePath, string outputPdfPath)
        {
            if (!File.Exists(pdfTemplatePath))
            {
                MessageBox.Show($"PDF Template not found at: {pdfTemplatePath}");
                return false;
            }

            string outputDir = Path.GetDirectoryName(outputPdfPath) ?? Path.GetTempPath();
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            return true;
        }
        private PdfAcroForm LoadPdfAndPrepareForm(string pdfTemplatePath, string outputPdfPath, out PdfDocument pdfDoc)
        {
            pdfDoc = null!;
            try
            {
                PdfReader reader = new PdfReader(pdfTemplatePath);
                PdfWriter writer = new PdfWriter(outputPdfPath);
                pdfDoc = new PdfDocument(reader, writer);

                return PdfAcroForm.GetAcroForm(pdfDoc, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load PDF: {ex.Message}");
                return null!;
            }
        }
        private void FillPdfFields(PdfAcroForm form, Dictionary<string, string> formData)
        {
            string[] pdfFields = { "ISSUE", "NAME", "ID", "SEX", "DOD", "HEIGHT", "WEIGHT", "Exp", "HAIRCOLOR", "EYECOLOR", "UNIT", "ISSUER", "PERMIT", "AUTO/JEEP", "MOTORCYCLE", "MOPED", "OTHER", "GLASSES/CONTACTS", "CAT/PAX", "Remarks" };

            foreach (string field in pdfFields)
            {
                if (formData.ContainsKey(field))
                {
                    form.GetField(field)?.SetValue(formData[field]);
                }
            }
        }
        private bool InsertSignature(PdfAcroForm form, string signatureImagePath, PdfDocument pdfDoc)
        {
            if (!File.Exists(signatureImagePath))
            {
                MessageBox.Show("Signature image file not found.");
                return false;
            }

            try
            {
                ImageData imgData = ImageDataFactory.Create(signatureImagePath);
                PdfImage img = new PdfImage(imgData);

                // Get the signature field
                PdfFormField signatureField = form.GetField("Image_af_image");
                if (signatureField != null)
                {
                    var widget = signatureField.GetWidgets()[0];
                    var rect = widget.GetRectangle().ToRectangle();

                    // Adjust image size to fit the signature field
                    img.SetAutoScale(false);
                    img.ScaleAbsolute(rect.GetWidth(), rect.GetHeight());
                    img.SetFixedPosition(rect.GetLeft(), rect.GetBottom());

                    // Insert the image onto the canvas at the correct position
                    var canvas = new Canvas(pdfDoc.GetPage(1), pdfDoc.GetPage(1).GetPageSize());
                    canvas.Add(img);
                    canvas.Close();

                    return true;
                }
                else
                {
                    MessageBox.Show("Signature image field 'Image_af_image' not found in the PDF.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting signature: {ex.Message}");
                return false;
            }
        }

        public void CompletePdfWorkflow(string pdfTemplatePath, string outputPdfPath, Dictionary<string, string> formData, string signatureImagePath)
        {
            try
            {
                // Step 1: Check if the template exists
                if (!File.Exists(pdfTemplatePath))
                {
                    MessageBox.Show($"PDF Template not found at: {pdfTemplatePath}");
                    return;
                }

                // Step 2: Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPdfPath);
                if (!Directory.Exists(outputDir))
                {
                    MessageBox.Show($"Creating output directory: {outputDir}");
                    Directory.CreateDirectory(outputDir);
                }

                // Step 3: Start processing the PDF
                using (PdfReader reader = new PdfReader(pdfTemplatePath))
                using (PdfWriter writer = new PdfWriter(outputPdfPath))
                using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
                {
                    PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                    // Fill the form fields using the available data
                    form.GetField("ISSUE")?.SetValue(formData["ISSUE"]);
                    form.GetField("NAME")?.SetValue($"{formData["Last Name"]}, {formData["First Name"]}");
                    form.GetField("ID")?.SetValue(formData["DoD ID #"]);
                    form.GetField("SEX")?.SetValue(formData["SEX"]);
                    form.GetField("DOB")?.SetValue(formData["DOB"]);
                    form.GetField("HEIGHT")?.SetValue(formData["HEIGHT"]);
                    form.GetField("WEIGHT")?.SetValue(formData["WEIGHT"]);
                    form.GetField("Exp")?.SetValue(formData["Exp"]);
                    form.GetField("HAIRCOLOR")?.SetValue(formData["HAIRCOLOR"]);
                    form.GetField("EYECOLOR")?.SetValue(formData["EYECOLOR"]);
                    form.GetField("UNIT")?.SetValue(formData["Unit"]);
                    form.GetField("ISSUER")?.SetValue(formData["ISSUER"]);
                    form.GetField("PERMIT")?.SetValue(formData["PERMIT"]);

                    // Debug for MOTORCYCLE checkbox
                    form.GetField("AUTO/JEEP")?.SetValue(formData["AUTO/JEEP"] == "Yes" ? "Yes" : "Off");
                    form.GetField("MOTORCYCLE")?.SetValue(formData["MOTORCYCLE"] == "Yes" ? "Yes" : "Off");
                    form.GetField("MOPED")?.SetValue(formData["MOPED"] == "Yes" ? "Yes" : "Off");
                    form.GetField("OTHER")?.SetValue(formData["OTHER"] == "Yes" ? "Yes" : "Off");
                    form.GetField("GLASSES/CONTACTS")?.SetValue(formData["GLASSES/CONTACTS"]);

                    // Use the mapped description for CAT/PAX in the PDF
                    string catPaxDescription = formData["CAT/PAX"];
                    form.GetField("CAT/PAX")?.SetValue(catPaxDescription);

                    form.GetField("Remarks")?.SetValue(formData["Remarks"]);
                    form.GetField("Stamp")?.SetValue(formData["Stamp"]);



                    // Insert the corresponding stamp image based on the stampComboBox selection
                    string stampSelection = stampComboBox.SelectedItem?.ToString() ?? "";
                    InsertStampImage(form, stampSelection, pdfDoc);

                    // Flatten the form
                    form.FlattenFields();

                    // Insert signature
                    if (!string.IsNullOrEmpty(signatureImagePath) && File.Exists(signatureImagePath))
                    {
                        ImageData imgData = ImageDataFactory.Create(signatureImagePath);
                        PdfImage img = new PdfImage(imgData);

                        PdfFormField signatureField = form.GetField("Image_af_image");
                        if (signatureField != null)
                        {
                            var widget = signatureField.GetWidgets()[0];
                            var rect = widget.GetRectangle().ToRectangle();

                            // Stretch the signature to fit the field
                            img.SetAutoScale(false);
                            img.ScaleAbsolute(rect.GetWidth(), rect.GetHeight());
                            img.SetFixedPosition(rect.GetLeft(), rect.GetBottom());

                            var canvas = new Canvas(pdfDoc.GetPage(1), rect);
                            canvas.Add(img);
                            canvas.Close();
                        }
                        else
                        {
                            MessageBox.Show("Signature image field not found in the PDF.");
                        }
                    }
                }

                // Step 4: Confirm the PDF was created
                if (File.Exists(outputPdfPath))
                {
                    MessageBox.Show($"PDF generated successfully at {outputPdfPath}");
                }
                else
                {
                    MessageBox.Show($"Failed to generate PDF at {outputPdfPath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }

        private void InsertStampImage(PdfAcroForm form, string stampSelection, PdfDocument pdfDoc)
        {
            string imagePath = null;

            // Determine the path of the image based on the selected stamp option
            switch (stampSelection)
            {
                case "Limited":
                    imagePath = Path.Combine(baseDir, "Resources", "Images", "LIMITED.png");
                    break;
                case "On Base Only":
                    imagePath = Path.Combine(baseDir, "Resources", "Images", "ON_BASE.png");
                    break;
                case "TDY":
                    imagePath = Path.Combine(baseDir, "Resources", "Images", "TDY.png");
                    break;
                case "Student Driver":
                    imagePath = Path.Combine(baseDir, "Resources", "Images", "STUDENT_DRIVER.png");
                    break;
                case "":
                    // Remove the image field entirely if no stamp is selected
                    RemoveStampField(form, "Image1_af_image");
                    return;  // Exit the method
            }

            if (File.Exists(imagePath))
            {
                try
                {
                    // Load the image
                    ImageData imgData = ImageDataFactory.Create(imagePath);
                    PdfImage img = new PdfImage(imgData);

                    // Get the stamp field in the PDF
                    PdfFormField stampField = form.GetField("Image1_af_image");
                    if (stampField != null)
                    {
                        var widget = stampField.GetWidgets()[0];
                        var rect = widget.GetRectangle().ToRectangle();

                        // Stretch the image to fit the field dimensions
                        img.SetAutoScale(false);
                        img.ScaleAbsolute(rect.GetWidth(), rect.GetHeight());

                        // Set the image's position on the page
                        img.SetFixedPosition(rect.GetLeft(), rect.GetBottom());

                        // Add the image to the canvas
                        var canvas = new Canvas(pdfDoc.GetPage(1), pdfDoc.GetPage(1).GetPageSize());
                        canvas.Add(img);
                        canvas.Close();

                        // Remove the original field after adding the image
                        form.RemoveField("Image1_af_image");
                    }
                    else
                    {
                        MessageBox.Show("Stamp image field 'Image1_af_image' not found in the PDF.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inserting stamp image: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show($"Stamp image not found at {imagePath}");
            }
        }

        private void RemoveStampField(PdfAcroForm form, string fieldName)
        {
            PdfFormField stampField = form.GetField(fieldName);
            if (stampField != null)
            {
                form.RemoveField(fieldName); // This removes the field entirely from the form
            }
        }

        private void FillPdf(
            string pdfTemplatePath,
            string outputPdfPath,
            Dictionary<string, string> formData,
            string signatureImagePath)
        {
            // your existing CompletePdfWorkflow body, *minus* the PrintPdf call
            CompletePdfWorkflow(pdfTemplatePath, outputPdfPath, formData, signatureImagePath);
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            if (!ValidateUnit()) return;
            var unitText = unitComboBox.Text.Trim();
            var unitCanon = _validUnits.First(u =>
                string.Equals(u, unitText, StringComparison.OrdinalIgnoreCase));
            unitComboBox.SelectedItem = unitCanon;
            var current = BuildFormDataFromUI(); // snapshot of UI

            var saved = LoadFormDataFromExcel(current["DoD ID #"]);
            if (saved != null)
            {
                // pick the fields that matter for a “did you mean to update” decision
                var differs = AreDifferent(current, saved,
                    "Last Name", "First Name", "Status", "Rank", "Unit", "PERMIT", "ISSUE", "Exp", "MSF", "CAT/PAX", "Remarks", "AUTO/JEEP", "MOTORCYCLE", "MOPED", "OTHER");

                if (differs)
                {
                    var choice = MessageBox.Show(
                        "This record has unsaved changes. Print the saved permit, or save your updates then print?",
                        "Data differs from saved record",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);

                    // Yes = Print saved, No = Save update then print, Cancel = stop
                    if (choice == DialogResult.Yes)
                    {
                        // rebuild formData from saved to guarantee PDF matches Excel
                        current = saved;
                    }
                    else if (choice == DialogResult.No)
                    {
                        // Do not stomp permit numbers: only update non-permit fields if you want
                        // or call your existing SaveDataToExcel which already targets columns
                        SaveDataToExcel(current); // this writes Unit at col 5 etc
                                                  // You might want to preserve existing permit numbers here
                    }
                    else
                    {
                        return; // canceled
                    }
                }
            }
            // Define paths for the template PDF, output PDF, and signature image
            // PDF path
            string pdfTemplatePath = Path.Combine(baseDir, "Resources", "PDF", "Form4EJ.pdf");
            string outputPdfPath = Path.Combine(baseDir, "Resources", "PDF", "Form4EJ_Filled.pdf");
            // Image paths
            string limitedImagePath = Path.Combine(baseDir, "Resources", "Images", "LIMITED.png");
            string onBaseImagePath = Path.Combine(baseDir, "Resources", "Images", "ON_BASE.png");
            string tdyImagePath = Path.Combine(baseDir, "Resources", "Images", "TDY.png");
            string studentDriverImagePath = Path.Combine(baseDir, "Resources", "Images", "STUDENT_DRIVER.png");
            string signatureImagePath = Path.Combine(baseDir, "Resources", "Images", "signatureCapture.jpeg");

            // Create a dictionary with all form data (field names must match those in the PDF)
            Dictionary<string, string> formData = new Dictionary<string, string>
{
    { "NAME", lastNameTextBox.Text + ", " + firstNameTextBox.Text },
    { "ID", dodIdTextBox.Text },
    { "ISSUE", issue1DateTimePicker.Value.ToShortDateString() },
    { "Exp", exp1DateTimePicker.Value.ToShortDateString() },
    { "SEX", sexComboBox.SelectedItem?.ToString() ?? "" },
    { "DOB", dobDateTimePicker.Value.ToShortDateString() },
    { "HEIGHT", heightTextBox.Text },
    { "WEIGHT", weightTextBox.Text },
    { "HAIRCOLOR", hairColorComboBox.SelectedItem?.ToString() ?? "" },
    { "EYECOLOR", eyeColorComboBox.SelectedItem?.ToString() ?? "" },
    { "UNIT", unitComboBox.SelectedItem?.ToString() ?? "" },
    { "ISSUER", issuerComboBox.SelectedItem?.ToString() ?? "" },
    { "PERMIT", permit1TextBox.Text },
    { "AUTO/JEEP", autoJeepCheckBox.Checked ? "Yes" : "" },
    { "MOTORCYCLE", motorcycleCheckBox.Checked ? "Yes" : "" },
    { "GLASSES/CONTACTS", restrictionsBox.Checked ? "Yes" : "" },
    { "CAT/PAX", catPaxComboBox.SelectedItem?.ToString() ?? "" },
    { "Remarks", remarksBox.Text },
    { "MOPED", mopedCheckBox.Checked ? "Yes" : "" },
    { "OTHER", otherCheckBox.Checked ? "Yes" : "" },

};


            // Call the CompletePdfWorkflow method to handle everything: form fields, signature, etc.
            CompletePdfWorkflow(pdfTemplatePath, outputPdfPath, formData, signatureImagePath);
        }

        private Dictionary<string, string> BuildFormDataFromUI()
        {
            return new Dictionary<string, string>
    {
        {"Last Name", lastNameTextBox.Text},
        {"First Name", firstNameTextBox.Text},
        {"DoD ID #", dodIdTextBox.Text},
        {"Status", statusComboBox.SelectedItem?.ToString() ?? ""},
        {"Rank", GetSelectedRank()},
        {"Unit", unitComboBox.SelectedItem?.ToString() ?? ""},
        {"Stamp", stampComboBox.SelectedItem?.ToString() ?? ""},
        {"PERMIT", string.IsNullOrEmpty(permit2TextBox.Text) ? permit1TextBox.Text : permit2TextBox.Text},
        {"ISSUE", (string.IsNullOrEmpty(permit2TextBox.Text) ? issue1DateTimePicker.Value : issue2DateTimePicker.Value).ToShortDateString()},
        {"Exp",   (string.IsNullOrEmpty(permit2TextBox.Text) ? exp1DateTimePicker.Value   : exp2DateTimePicker.Value).ToShortDateString()},
        {"MSF",   msfTextBox.Text},
        {"CAT/PAX", catPaxComboBox.SelectedItem?.ToString() ?? ""},
        {"SEX",   sexComboBox.SelectedItem?.ToString() ?? ""},
        {"DOB",   dobDateTimePicker.Value.ToShortDateString()},
        {"HEIGHT", heightTextBox.Text},
        {"WEIGHT", weightTextBox.Text},
        {"HAIRCOLOR", hairColorComboBox.SelectedItem?.ToString() ?? ""},
        {"EYECOLOR", eyeColorComboBox.SelectedItem?.ToString() ?? ""},
        {"GLASSES/CONTACTS", restrictionsBox.Checked ? "Yes" : ""},
        {"Remarks", remarksBox.Text},
        {"ISSUER", issuerComboBox.SelectedItem?.ToString() ?? ""},
        {"AUTO/JEEP", autoJeepCheckBox.Checked ? "Yes" : ""},
        {"MOTORCYCLE", motorcycleCheckBox.Checked ? "Yes" : ""},
        { "MOPED", mopedCheckBox.Checked ? "Yes" : "" },
        { "OTHER", otherCheckBox.Checked ? "Yes" : "" },
    };
        }

        private Dictionary<string, string>? LoadFormDataFromExcel(string dodId)
        {
            var fi = new FileInfo(excelFilePath);
            using var pkg = new OfficeOpenXml.ExcelPackage(fi);
            var ws = pkg.Workbook.Worksheets[0];
            if (ws?.Dimension == null) return null;

            int row = -1;
            for (int i = 2; i <= ws.Dimension.End.Row; i++)
                if (ws.Cells[i, 6].Text == dodId) { row = i; break; }

            if (row == -1) return null;

            // Map the same fields you save in SaveDataToExcel
            var dict = new Dictionary<string, string>
    {
        {"Last Name", ws.Cells[row,1].Text},
        {"First Name", ws.Cells[row,2].Text},
        {"Status", ws.Cells[row,3].Text},
        {"Rank", ws.Cells[row,4].Text},
        {"Unit", ws.Cells[row,5].Text},
        {"DoD ID #", ws.Cells[row,6].Text},
        {"PERMIT", ws.Cells[row,10].Text != "" ? ws.Cells[row,10].Text : ws.Cells[row,7].Text},
        {"ISSUE",  ws.Cells[row,10].Text != "" ? ws.Cells[row,11].Text : ws.Cells[row,8].Text},
        {"Exp",    ws.Cells[row,10].Text != "" ? ws.Cells[row,12].Text : ws.Cells[row,9].Text},
        {"MSF", ws.Cells[row, 13].Text},
        {"CAT/PAX", ws.Cells[row, 14].Text},
    };
            return dict;
        }

        private bool AreDifferent(Dictionary<string, string> a, Dictionary<string, string> b, params string[] keys)
        {
            foreach (var k in keys)
            {
                a.TryGetValue(k, out var av);
                b.TryGetValue(k, out var bv);
                if (!string.Equals(av?.Trim() ?? "", bv?.Trim() ?? "", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }


        private void LanguageButton_Click(object sender, EventArgs e)
        {
            using (var dlg = new Form())
            {
                dlg.Text = "近日公開予定";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new Size(360, 180);

                var lbl = new Label
                {
                    Text = "言語切替機能は現在開発中です。\nまだ実装されていません。",
                    Font = new Font("Meiryo", 14F, FontStyle.Regular),
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var btn = new Button
                {
                    Text = "OK",
                    Dock = DockStyle.Bottom,
                    Height = 40
                };
                btn.Click += (s, a) => dlg.Close();

                dlg.Controls.Add(lbl);
                dlg.Controls.Add(btn);
                dlg.ShowDialog(this);
            }
        }

        private void btnGeneratePermitNumber_Click(object sender, EventArgs e)
        {
            if (!ValidateUnit()) return;
            var unitText = unitComboBox.Text.Trim();
            var unitCanon = _validUnits.First(u =>
                string.Equals(u, unitText, StringComparison.OrdinalIgnoreCase));
            unitComboBox.SelectedItem = unitCanon;   // lock it in

            // 0) If unchecked, skip new-permit and just re-print / re-save.
            bool assigningNewPermit = PermitcheckBox.Checked;

            // 1) Open Excel
            if (!TryOpenSofa(out var package)) return;
            var ws = package.Workbook.Worksheets[0];

            int nextNum = 0;
            string paddedNext = "";
            TextBox? assignedPermitBox = null;

            if (assigningNewPermit)
            {
                // 2) Collect used permit numbers
                var used = new HashSet<int>();
                for (int r = 2; r <= ws.Dimension.End.Row; r++)
                {
                    if (int.TryParse(ws.Cells[r, 7].Text, out int p1)) used.Add(p1);
                    if (int.TryParse(ws.Cells[r, 10].Text, out int p2)) used.Add(p2);
                }

                // 3) Find lowest-unused positive integer
                nextNum = 1;
                while (used.Contains(nextNum)) nextNum++;

                // 4) Format with padding
                paddedNext = nextNum.ToString("D6");


                // 5) Populate into the first empty slot
                // This is only a preview — since the workbook lives on a
                // OneDrive-synced share, another workstation could grab the
                // same "next available" number before we save. It gets
                // re-validated against the freshest copy right before the
                // actual write, below.
                if (string.IsNullOrWhiteSpace(permit1TextBox.Text))
                {
                    permit1TextBox.Text = paddedNext;
                    assignedPermitBox = permit1TextBox;
                }
                else
                {
                    permit2TextBox.Text = paddedNext;
                    assignedPermitBox = permit2TextBox;
                }
            }

            // 6) Capture signature to temp JPEG
            string sigPath = Path.Combine(Path.GetTempPath(), "signatureCapture.jpg");
            using (var bmp = new Bitmap(signaturePanel.Width, signaturePanel.Height))
            {
                signaturePanel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                bmp.Save(sigPath, ImageFormat.Jpeg);
            }

            // 7) Build formData (make sure this dictionary is defined earlier in your method)
            var formData = new Dictionary<string, string>
            {
                {"Last Name", lastNameTextBox.Text},
                {"First Name", firstNameTextBox.Text},
                {"DoD ID #", dodIdTextBox.Text},
                {"Status", statusComboBox.SelectedItem?.ToString() ?? ""},
                {"Rank", GetSelectedRank()},
                { "Unit", unitComboBox.SelectedItem?.ToString() ?? "" },
                {"Stamp", stampComboBox.SelectedItem?.ToString() ?? ""},
                {"PERMIT", permit2TextBox.Text != "" ? permit2TextBox.Text : permit1TextBox.Text},
                {"ISSUE", (permit2TextBox.Text != ""
                             ? issue2DateTimePicker.Value
                             : issue1DateTimePicker.Value)
                             .ToShortDateString()},
                {"Exp",   (permit2TextBox.Text != ""
                             ? exp2DateTimePicker.Value
                             : exp1DateTimePicker.Value)
                             .ToShortDateString()},
                {"MSF",   msfTextBox.Text},
                {"CAT/PAX", catPaxComboBox.SelectedItem?.ToString() ?? ""},
                {"SEX",   sexComboBox.SelectedItem?.ToString() ?? ""},
                {"DOB",   dobDateTimePicker.Value.ToShortDateString()},
                {"HEIGHT", heightTextBox.Text},
                {"WEIGHT", weightTextBox.Text},
                {"HAIRCOLOR", hairColorComboBox.SelectedItem?.ToString() ?? ""},
                {"EYECOLOR", eyeColorComboBox.SelectedItem?.ToString() ?? ""},
                {"GLASSES/CONTACTS", restrictionsBox.Checked ? "Yes" : "No"},
                {"Remarks", remarksBox.Text},
                {"ISSUER", issuerComboBox.SelectedItem?.ToString() ?? ""},
                {"AUTO/JEEP", autoJeepCheckBox.Checked ? "Yes" : "No"},
                {"MOTORCYCLE", motorcycleCheckBox.Checked ? "Yes" : "No"},
                {"MOPED", mopedCheckBox.Checked ? "Yes" : "No" },
                {"OTHER", otherCheckBox.Checked ? "Yes" : "No" },
            };

            // 8) Write to Excel (find or append row) — only one block!
            var fileInfo = new FileInfo(excelFilePath);
            using (var pkg = new ExcelPackage(fileInfo))
            {
                var sheet = pkg.Workbook.Worksheets[0];
                int row = -1;
                for (int i = 2; i <= sheet.Dimension.End.Row; i++)
                    if (sheet.Cells[i, 6].Text == formData["DoD ID #"])
                    {
                        row = i;
                        break;
                    }
                if (row == -1)
                    row = sheet.Dimension.End.Row + 1;

                // Decide whether to use Permit1 or Permit2 slot
                // Only touch the permit columns when we actually assigned a new
                // permit number above — otherwise nextNum is still 0 and would
                // stamp "000000" into the sheet on a plain reprint/re-save.
                if (assigningNewPermit)
                {
                    // Re-validate the number against this just-reopened copy of
                    // the file, right before writing. The workbook lives on a
                    // OneDrive-synced share used by multiple workstations, so
                    // the "next available" number picked earlier (when the
                    // button was clicked) may have gone stale — another PC's
                    // save could have synced down in the meantime. Re-checking
                    // here, as close to the save as possible, shrinks that
                    // race window from "however long the tech spent on the
                    // form" down to milliseconds. It cannot fully eliminate it
                    // (a truly simultaneous save from another machine, whose
                    // change hasn't synced down yet, is still possible), so
                    // this is a mitigation, not a guarantee.
                    var usedNow = new HashSet<int>();
                    for (int r = 2; r <= sheet.Dimension.End.Row; r++)
                    {
                        if (int.TryParse(sheet.Cells[r, 7].Text, out int up1)) usedNow.Add(up1);
                        if (int.TryParse(sheet.Cells[r, 10].Text, out int up2)) usedNow.Add(up2);
                    }
                    if (usedNow.Contains(nextNum))
                    {
                        nextNum = 1;
                        while (usedNow.Contains(nextNum)) nextNum++;
                        paddedNext = nextNum.ToString("D6");

                        if (assignedPermitBox != null)
                            assignedPermitBox.Text = paddedNext;
                        formData["PERMIT"] = paddedNext;
                    }

                    var existingP1 = sheet.Cells[row, 7].Text.Trim();
                    if (string.IsNullOrEmpty(existingP1))
                    {
                        var cell = sheet.Cells[row, 7];
                        cell.Value = nextNum;
                        cell.Style.Numberformat.Format = "000000";

                        var cellIssue = sheet.Cells[row, 8];
                        cellIssue.Value = formData["ISSUE"];
                        var cellExp = sheet.Cells[row, 9];
                        cellExp.Value = formData["Exp"];
                    }
                    else
                    {
                        var cell = sheet.Cells[row, 10];
                        cell.Value = nextNum;
                        cell.Style.Numberformat.Format = "000000";

                        sheet.Cells[row, 11].Value = formData["ISSUE"];
                        sheet.Cells[row, 12].Value = formData["Exp"];
                    }
                }

                // Now write the rest of your fields once
                sheet.Cells[row, 1].Value = formData["Last Name"];
                sheet.Cells[row, 2].Value = formData["First Name"];
                sheet.Cells[row, 3].Value = formData["Status"];
                sheet.Cells[row, 4].Value = formData["Rank"];
                sheet.Cells[row, 5].Value = formData["Unit"];
                sheet.Cells[row, 6].Value = formData["DoD ID #"];
                sheet.Cells[row, 13].Value = formData["MSF"];
                sheet.Cells[row, 14].Value = formData["CAT/PAX"];
                sheet.Cells[row, 15].Value = formData["SEX"];
                sheet.Cells[row, 16].Value = formData["DOB"];
                sheet.Cells[row, 17].Value = formData["HEIGHT"];
                sheet.Cells[row, 18].Value = formData["WEIGHT"];
                sheet.Cells[row, 19].Value = formData["HAIRCOLOR"];
                sheet.Cells[row, 20].Value = formData["EYECOLOR"];
                sheet.Cells[row, 21].Value = formData["GLASSES/CONTACTS"];
                sheet.Cells[row, 22].Value = formData["Remarks"];
                sheet.Cells[row, 23].Value = formData["Stamp"];
                sheet.Cells[row, 24].Value = formData["MOPED"];
                sheet.Cells[row, 25].Value = formData["OTHER"];

                const int maxAttempts = 5;
                int attempts = 0;
                while (true)
                {
                    try
                    {
                        pkg.Save();
                        break;  // ← stop looping once we succeed
                    }
                    catch (InvalidOperationException ex)
                    {
                        // dig into the nested InnerException chain
                        var io = ex.InnerException?.InnerException as IOException;
                        if (io == null)
                            throw;    // it wasn’t the “file in use” case, re‐throw

                        // file is locked by Excel
                        attempts++;
                        if (attempts >= maxAttempts)
                        {
                            MessageBox.Show(
                                "Could not save SOFA.xlsx because it’s open in another program.\n" +
                                "Please close the file and try again.",
                                "File In Use",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }

                        Thread.Sleep(500);
                    }
                }

                if (assigningNewPermit)
                {
                    // Safety net for the residual race that the recheck above can't
                    // close: scan the now-saved sheet for any other row also holding
                    // this permit number. Won't catch a collision whose OneDrive sync
                    // lands after this point, but it catches most local overlaps and
                    // lets the tech fix it on the spot instead of it surfacing later.
                    int duplicateCount = 0;
                    for (int r = 2; r <= sheet.Dimension.End.Row; r++)
                    {
                        if (r == row) continue;
                        if (int.TryParse(sheet.Cells[r, 7].Text, out int dp1) && dp1 == nextNum) duplicateCount++;
                        if (int.TryParse(sheet.Cells[r, 10].Text, out int dp2) && dp2 == nextNum) duplicateCount++;
                    }
                    if (duplicateCount > 0)
                    {
                        MessageBox.Show(
                            $"Permit number {paddedNext} also appears on another row.\n" +
                            "This can happen if two workstations assigned a permit at nearly the same time. " +
                            "Please check the SOFA King Data sheet and correct the duplicate.",
                            "Possible Duplicate Permit Number",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }

                // 9) Generate the filled PDF
                var pdfTpl = Path.Combine(baseDir, "Resources", "PDF", "Form4EJ.pdf");
                var pdfOut = Path.Combine(baseDir, "Resources", "PDF", "Form4EJ_Filled.pdf");
                FillPdf(pdfTpl, pdfOut, formData, sigPath);
            }
        }


        private void exp1DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            exp1DateTimePicker.CustomFormat = "MM/dd/yyyy";
        }

        private void issue2DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            issue2DateTimePicker.CustomFormat = "MM/dd/yyyy";
        }

        private void exp2DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            exp2DateTimePicker.CustomFormat = "MM/dd/yyyy";
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Add any logic needed when the last name text changes
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            // Add any logic needed when the first name text changes
        }

        private void permit2TextBox_TextChanged(object sender, EventArgs e)
        {
            // Add any logic needed when the permit #2 text changes
        }

        private void msfTextBox_TextChanged(object sender, EventArgs e)
        {
            // Add any logic needed when the MSF text changes
        }

        private void catPaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add any logic needed when the CAT/PAX text changes
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void hairColorLabel_Click(object sender, EventArgs e)
        {

        }

        private void heightLabel_Click(object sender, EventArgs e)
        {

        }

        private void dobLabel_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void unitLabel_Click(object sender, EventArgs e)
        {

        }

        private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }


        private void issuerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void restrictionsBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void hairColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void civilianRankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnSearch_Click_1(object sender, EventArgs e)
        {

        }

        private void sexLabel_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void unitLabel_Click_1(object sender, EventArgs e)
        {

        }

        private void picturebutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This feature is disabled.");
        }

        private void catLabel_Click(object sender, EventArgs e)
        {

        }

        private void remarksLabel_Click(object sender, EventArgs e)
        {

        }
        private void statusLabel_Click(object sender, EventArgs e)
        {
            // Code to handle status label click
        }

        private void lastNameLabel_Click(object sender, EventArgs e)
        {
            // Code to handle last name label click
        }

        private void militaryRankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Code to handle military rank selection change
        }

        private void naLabel_Click(object sender, EventArgs e)
        {
            // Code to handle NA label click
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            // Code to handle reset button click
        }


        private void remarksBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sigPlusNET1_Click(object sender, EventArgs e)
        {

        }

        private void autoJeepCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_3(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}