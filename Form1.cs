using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using iText.Layout;
using iText.Layout.Element;
using System.Drawing.Imaging;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Image;
using Topaz;

// Alias the namespaces to avoid ambiguity
using PdfImage = iText.Layout.Element.Image;
using DrawingImage = System.Drawing.Image;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas;

namespace SOFA_Generator
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point lastPoint = Point.Empty;
        private Bitmap signatureBitmap;
        private string excelFilePath = @"D:\Users\shane\SOFA King\SOFA.xlsx";  // Default path for the Excel file

        public Form1()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            InitializeComponent();
            signatureBitmap = new Bitmap(signaturePanel.Width, signaturePanel.Height);
            signaturePanel.Paint += new PaintEventHandler(signaturePanel_Paint);
            signaturePanel.MouseDown += new MouseEventHandler(signaturePanel_MouseDown);
            signaturePanel.MouseMove += new MouseEventHandler(signaturePanel_MouseMove);
            signaturePanel.MouseUp += new MouseEventHandler(signaturePanel_MouseUp);
            btnSearch.Click += new EventHandler(this.btnSearch_Click);
            statusComboBox.SelectedIndexChanged += new EventHandler(this.statusComboBox_SelectedIndexChanged);
            btnReset.Click += new EventHandler(this.btnReset_Click);
            InitializeStampComboBox();
            sigPlusNET1.SetTabletState(1);
            sigPlusNET1.SetJustifyMode(0);
            InitializeUnitComboBox();
            HideFormFields();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideFormFields();
            LoadIssuerNames();
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
            MSFcheckBox.Visible = false;
            catPaxComboBox.Visible = false;
            checkedListBox1.Visible = false;
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

        private void InitializeUnitComboBox()
        {
            // Ensure the Excel file path is set
            if (string.IsNullOrEmpty(excelFilePath))
            {
                MessageBox.Show("Please select an Excel file first.");
                return;
            }

            FileInfo fileInfo = new FileInfo(excelFilePath);

            try
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    // Get the "Defenders" worksheet
                    ExcelWorksheet defendersSheet = package.Workbook.Worksheets["Defenders"];

                    if (defendersSheet == null)
                    {
                        MessageBox.Show("The 'Defenders' sheet was not found in the Excel file.");
                        return;
                    }

                    // Clear the existing items in the ComboBox
                    unitComboBox.Items.Clear();

                    // Iterate through the rows in the Defenders sheet
                    int startRow = 2; // Assuming the first row is a header
                    for (int row = startRow; row <= defendersSheet.Dimension.End.Row; row++)
                    {
                        string unitName = defendersSheet.Cells[row, 2].Text; // Column 2 for unit names

                        if (!string.IsNullOrEmpty(unitName))
                        {
                            unitComboBox.Items.Add(unitName);
                        }
                    }

                    // Optionally, select the first item in the ComboBox if there are items
                    if (unitComboBox.Items.Count > 0)
                    {
                        unitComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading units: {ex.Message}");
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
            checkedListBox1.Visible = true;
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
            // Capture and trim the DoD ID input
            string dodId = dodIdTextBox.Text.Trim();

            // Check if DoD ID is empty
            if (string.IsNullOrEmpty(dodId))
            {
                MessageBox.Show("Please enter a DoD ID.");
                return;
            }

            // Validate the DoD ID
            if (!IsValidDoDId(dodId))
            {
                MessageBox.Show("Please enter a valid 10-digit DoD ID that starts at 1000000000.");
                return;
            }

            // Always reset the form before populating with new data
            ResetForm();

            // Search for the DoD ID in the Excel file
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
            FileInfo fileInfo = new FileInfo(excelFilePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
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
                { "Stamp", worksheet.Cells[rowIndex, 23].Text }  // Column 23 for "Stamp"
            };
                    return data;
                }
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
            permit1TextBox.Text = data["Permit #1"];

            // Handle date fields with proper checks
            if (DateTime.TryParse(data["Issue 1"], out DateTime issue1Date))
            {
                issue1DateTimePicker.Value = issue1Date;
            }

            if (DateTime.TryParse(data["Exp 1"], out DateTime exp1Date))
            {
                exp1DateTimePicker.Value = exp1Date;
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
            int autoJeepIndex = checkedListBox1.Items.IndexOf("Auto/Jeep");
            if (autoJeepIndex >= 0)
            {
                checkedListBox1.SetItemChecked(autoJeepIndex, true);
            }

            // Check if MSF is "True" and ensure Motorcycle is checked
            if (data.ContainsKey("MSF") && data["MSF"] == "True")
            {
                MSFcheckBox.Checked = true;
                MSFcheckBox.Visible = true;

                int motorcycleIndex = checkedListBox1.Items.IndexOf("Motorcycle");
                if (motorcycleIndex >= 0)
                {
                    checkedListBox1.SetItemChecked(motorcycleIndex, true);

                    // Ensure CAT/PAX field is populated and visible when Motorcycle is checked
                    catLabel.Visible = true;
                    catPaxComboBox.Visible = true;

                    // Check if the CAT/PAX value exists in the ComboBox items
                    string catPaxValue = data["CAT/PAX"];
                    var matchingItem = catPaxComboBox.Items.Cast<string>().FirstOrDefault(item => item == catPaxValue);

                    if (matchingItem != null)
                    {
                        catPaxComboBox.SelectedItem = matchingItem;
                    }
                }
            }

            // Make sure the checkedListBox is visible if any vehicle is involved
            checkedListBox1.Visible = checkedListBox1.CheckedItems.Count > 0;

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

        private void LoadIssuerNames()
        {
            // Ensure the Excel file path is set
            if (string.IsNullOrEmpty(excelFilePath))
            {
                MessageBox.Show("Please select an Excel file first.");
                return;
            }

            FileInfo fileInfo = new FileInfo(excelFilePath);

            // Open the Excel package
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                // Get the "Defenders" worksheet
                ExcelWorksheet defendersSheet = package.Workbook.Worksheets["Defenders"];

                if (defendersSheet == null)
                {
                    MessageBox.Show("The 'Defenders' sheet was not found in the Excel file.");
                    return;
                }

                // Clear the existing items in the ComboBox
                issuerComboBox.Items.Clear();

                // Iterate through the rows in the Defenders sheet
                int startRow = 2; // Assuming the first row is a header
                for (int row = startRow; row <= defendersSheet.Dimension.End.Row; row++)
                {
                    string defenderName = defendersSheet.Cells[row, 1].Text; // Column 1 for Defender names

                    if (!string.IsNullOrEmpty(defenderName))
                    {
                        issuerComboBox.Items.Add(defenderName);
                    }
                }

                if (issuerComboBox.Items.Count > 0)
                {
                    issuerComboBox.SelectedIndex = 0; // Optionally select the first item
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(excelFilePath);
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path of specified file
                    excelFilePath = openFileDialog.FileName;
                    MessageBox.Show($"Excel file path set to: {excelFilePath}", "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Load issuer names after selecting the file
                    LoadIssuerNames();
                }
            }
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
            exp1DateTimePicker.Value = DateTime.Today.AddDays(90); // Default expiry date to 90 days in the future
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

            // Reset all rank-related controls and make them invisible
            militaryRankComboBox.SelectedIndex = -1;
            civilianRankComboBox.SelectedIndex = -1;
            militaryRankComboBox.Visible = false;
            civilianRankComboBox.Visible = false;
            naLabel.Visible = false;

            // Reset unit and status combo boxes
            unitComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = -1; // Set last to avoid triggering events prematurely

            // Clear checked items in the checkedListBox1, but don't hide it
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.SetItemChecked(index, false);
            }

            // Reset MSF checkbox and hide it
            MSFcheckBox.Checked = false;
            MSFcheckBox.Visible = false;

            // Hide CAT/PAX combo box and label
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
            try
            {
                // Step 1: Capture the signature and save it
                using (Bitmap bitmap = new Bitmap(signaturePanel.Width, signaturePanel.Height))
                {
                    signaturePanel.DrawToBitmap(bitmap, new Rectangle(0, 0, signaturePanel.Width, signaturePanel.Height));

                    // Save the signature image to a temporary file
                    string filePath = Path.Combine(Path.GetTempPath(), "signatureCapture.jpg");
                    bitmap.Save(filePath, ImageFormat.Jpeg);

                    // Step 2: Set the paths for the PDF
                    string pdfTemplatePath = @"D:\Users\shane\SOFA King\Form4EJ.pdf";
                    string outputPdfPath = @"D:\Users\shane\SOFA King\Form4EJ_Filled.pdf";

                    // Step 3: Trim the selected unit value from the combo box to remove leading/trailing spaces
                    string unitValue = (unitComboBox.SelectedItem?.ToString() ?? "").Trim();

                    // Step 4: Prepare form data
                    var formData = new Dictionary<string, string>
                    {
                        { "NAME", lastNameTextBox.Text + ", " + firstNameTextBox.Text },  // For PDF
                        { "Last Name", lastNameTextBox.Text },  // For Excel
                        { "First Name", firstNameTextBox.Text },  // For Excel
                        { "DoD ID #", dodIdTextBox.Text },
                        { "Status", statusComboBox.SelectedItem?.ToString() ?? "" },
                        { "Stamp", stampComboBox.SelectedItem?.ToString() ?? "" },
                        { "Rank", GetSelectedRank() },
                        { "UNIT", unitValue ?? "" },
                        { "Unit", unitValue ?? "" },
                        { "ISSUE", issue1DateTimePicker.Value.ToShortDateString() },
                        { "Exp", exp1DateTimePicker.Value.ToShortDateString() },
                        { "SEX", sexComboBox.SelectedItem?.ToString() ?? "" },
                        { "DOB", dobDateTimePicker.Value.ToShortDateString() },
                        { "HEIGHT", heightTextBox.Text },
                        { "WEIGHT", weightTextBox.Text },
                        { "HAIRCOLOR", hairColorComboBox.SelectedItem?.ToString() ?? "" },
                        { "EYECOLOR", eyeColorComboBox.SelectedItem?.ToString() ?? "" },
                        { "ISSUER", issuerComboBox.SelectedItem?.ToString() ?? "" },
                        { "AUTO/JEEP", checkedListBox1.CheckedItems.Contains("Auto/Jeep") ? "True" : "False" },
                        { "MOTORCYCLE", checkedListBox1.CheckedItems.Contains("Motorcycle") ? "True" : "False" },
                        { "GLASSES/CONTACTS", restrictionsBox.Checked ? "Yes" : "No" },
                        { "CAT/PAX", catPaxComboBox.SelectedItem?.ToString() ?? "" },
                        { "Remarks", remarksBox.Text },
                        { "MSF", MSFcheckBox.Checked ? "True" : "False" }  // <--- Make sure MSF is included here
                    };


                    // Step 5: Decide where to save the permit (Permit #1 or Permit #2)
                    FileInfo fileInfo = new FileInfo(excelFilePath);
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        // Check if the worksheet is empty
                        if (worksheet.Dimension == null || worksheet.Dimension.End.Row == 0)
                        {
                            MessageBox.Show("The worksheet is empty or not properly loaded.");
                            return;
                        }

                        int rowIndex = -1;

                        // Find row by DoD ID
                        for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                        {
                            if (worksheet.Cells[i, 6].Text == formData["DoD ID #"])  // Column 6 for "DoD ID #"
                            {
                                rowIndex = i;
                                break;
                            }
                        }

                        // Check if rowIndex was found
                        if (rowIndex == -1)
                        {
                            MessageBox.Show("No matching DoD ID found. Creating a new row.");
                            rowIndex = worksheet.Dimension.End.Row + 1;
                        }

                        // Ensure rowIndex is within valid range
                        if (rowIndex <= 0 || rowIndex > worksheet.Dimension.End.Row + 1)
                        {
                            MessageBox.Show($"Row index {rowIndex} is out of range.");
                            return;
                        }

                        // Check if Permit #1 is already filled for this user
                        string existingPermit1 = worksheet.Cells[rowIndex, 7].Text; // Column 7 = Permit #1

                        if (string.IsNullOrEmpty(existingPermit1))
                        {
                            // Permit #1 is empty, save to Permit #1
                            formData["PERMIT"] = permit1TextBox.Text;
                            formData["ISSUE"] = issue1DateTimePicker.Value.ToShortDateString();
                            formData["Exp"] = exp1DateTimePicker.Value.ToShortDateString();
                        }
                        else
                        {
                            // Permit #1 is filled, save to or overwrite Permit #2
                            formData["PERMIT"] = permit2TextBox.Text;
                            formData["ISSUE"] = issue2DateTimePicker.Value.ToShortDateString();
                            formData["Exp"] = exp2DateTimePicker.Value.ToShortDateString();
                        }
                    }

                    // Step 6: Call CompletePdfWorkflow to generate the PDF
                    CompletePdfWorkflow(pdfTemplatePath, outputPdfPath, formData, filePath);

                    // Step 7: Save form data to Excel (this includes the correct permit)
                    SaveDataToExcel(formData);

                    // Confirmation message
                    MessageBox.Show("PDF generated and data saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the signature or generating the PDF: {ex.Message}");
            }
        }

        private void InitializeCatPaxComboBox()
        {
            catPaxComboBox.Items.Clear();
            catPaxComboBox.Items.Add("Cat 1: Up to 50cc");
            catPaxComboBox.Items.Add("Cat 2: Over 50cc up to 125cc");
            catPaxComboBox.Items.Add("Cat 3: Over 125cc up to 400cc");
            catPaxComboBox.Items.Add("Cat 4: Over 400cc up to 750cc");
            catPaxComboBox.Items.Add("Cat 5: Over 750cc");

            // Optionally, select the first item as default
            if (catPaxComboBox.Items.Count > 0)
            {
                catPaxComboBox.SelectedIndex = 0;
            }
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

                    // **Write data to the Excel file**
                    worksheet.Cells[rowIndex, 1].Value = data["Last Name"];
                    worksheet.Cells[rowIndex, 2].Value = data["First Name"];
                    worksheet.Cells[rowIndex, 3].Value = data["Status"];
                    worksheet.Cells[rowIndex, 4].Value = data["Rank"];
                    worksheet.Cells[rowIndex, 5].Value = data["Unit"];
                    worksheet.Cells[rowIndex, 6].Value = data["DoD ID #"];
                    worksheet.Cells[rowIndex, 7].Value = data["PERMIT"];   // Permit #1
                    worksheet.Cells[rowIndex, 8].Value = data["ISSUE"];    // Issue 1
                    worksheet.Cells[rowIndex, 9].Value = data["Exp"];      // Exp 1
                    worksheet.Cells[rowIndex, 10].Value = data["PERMIT"];  // Permit #2
                    worksheet.Cells[rowIndex, 11].Value = data["ISSUE"];   // Issue 2
                    worksheet.Cells[rowIndex, 12].Value = data["Exp"];     // Exp 2
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
                    worksheet.Cells[rowIndex, 23].Value = data["Stamp"];  // Write "Stamp" in column 23

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
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only show CAT/PAX and MSF when the Motorcycle checkbox is selected
            if (checkedListBox1.CheckedItems.Contains("Motorcycle"))
            {
                MSFcheckBox.Visible = true;
                MSFcheckBox.Checked = true; // Automatically check MSF if motorcycle is selected
                catPaxComboBox.Visible = true;
                catLabel.Visible = true;
            }
            else
            {
                MSFcheckBox.Visible = false;
                MSFcheckBox.Checked = false; // Uncheck and hide MSF if motorcycle is deselected
                catPaxComboBox.Visible = false;
                catLabel.Visible = false; // Hide CAT/PAX and label if motorcycle is deselected
            }

            // The AUTO/JEEP checkbox will not trigger the CAT/PAX field to become visible
            if (!checkedListBox1.CheckedItems.Contains("Motorcycle"))
            {
                catPaxComboBox.Visible = false;
                catLabel.Visible = false;
            }
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
            string[] pdfFields = { "ISSUE", "NAME", "ID", "SEX", "DOD", "HEIGHT", "WEIGHT", "Exp", "HAIRCOLOR", "EYECOLOR", "UNIT", "ISSUER", "PERMIT", "AUTO/JEEP", "MOTORCYCLE", "GLASSES/CONTACTS", "CAT/PAX", "Remarks" };

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
                    form.GetField("NAME")?.SetValue(formData["NAME"]);
                    form.GetField("ID")?.SetValue(formData["DoD ID #"]);
                    form.GetField("SEX")?.SetValue(formData["SEX"]);
                    form.GetField("DOB")?.SetValue(formData["DOB"]);
                    form.GetField("HEIGHT")?.SetValue(formData["HEIGHT"]);
                    form.GetField("WEIGHT")?.SetValue(formData["WEIGHT"]);
                    form.GetField("Exp")?.SetValue(formData["Exp"]);
                    form.GetField("HAIRCOLOR")?.SetValue(formData["HAIRCOLOR"]);
                    form.GetField("EYECOLOR")?.SetValue(formData["EYECOLOR"]);
                    form.GetField("UNIT")?.SetValue(formData["UNIT"]);
                    form.GetField("ISSUER")?.SetValue(formData["ISSUER"]);
                    form.GetField("PERMIT")?.SetValue(formData["PERMIT"]);

                    // Debug for MOTORCYCLE checkbox
                    form.GetField("AUTO/JEEP")?.SetValue(formData["AUTO/JEEP"] == "True" ? "Yes" : "Off");
                    form.GetField("MOTORCYCLE")?.SetValue(formData["MOTORCYCLE"] == "True" ? "Yes" : "Off");
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
                    imagePath = @"D:\Users\shane\SOFA King\LIMITED.png"; // Update with correct path
                    break;
                case "On Base Only":
                    imagePath = @"D:\Users\shane\SOFA King\ON_BASE.png";
                    break;
                case "TDY":
                    imagePath = @"D:\Users\shane\SOFA King\TDY.png";
                    break;
                case "Student Driver":
                    imagePath = @"D:\Users\shane\SOFA King\STUDENT_DRIVER.png";
                    break;
                case "":
                    // No stamp selected, return early
                    return;
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
                        canvas.Close();  // Ensure the canvas is finalized

                        // Optionally, remove or hide the original field if necessary
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

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            // Define paths for the template PDF, output PDF, and signature image
            string pdfTemplatePath = @"D:\Users\shane\SOFA King\Form4EJ.pdf";
            string outputPdfPath = @"D:\Users\shane\SOFA King\Form4EJ_Filled.pdf";
            string signatureImagePath = @"C:\Users\shane\AppData\Local\Temp\signatureCapture.jpeg";

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
    { "AUTO/JEEP", checkedListBox1.CheckedItems.Contains("Auto/Jeep") ? "True" : "False" },
    { "MOTOCYCLE", checkedListBox1.CheckedItems.Contains("Motorcycle") ? "True" : "False" },
    { "GLASSES/CONTACTS", restrictionsBox.Checked ? "Yes" : "No" },
    { "CAT/PAX", catPaxComboBox.SelectedItem?.ToString() ?? "" },
    { "Remarks", remarksBox.Text }
};


            // Call the CompletePdfWorkflow method to handle everything: form fields, signature, etc.
            CompletePdfWorkflow(pdfTemplatePath, outputPdfPath, formData, signatureImagePath);
        }

        private void btnGeneratePermitNumber_Click(object sender, EventArgs e)
        {
            string nextPermitNumber = GenerateNextPermitNumber();
            if (groupBox2.Visible)
            {
                permit2TextBox.Text = nextPermitNumber;
            }
            else
            {
                permit1TextBox.Text = nextPermitNumber;
            }
        }

        private string GenerateNextPermitNumber()
        {
            FileInfo fileInfo = new FileInfo(excelFilePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                HashSet<int> usedPermitNumbers = new HashSet<int>(); // Use HashSet for faster lookup

                for (int rowIndex = 2; rowIndex <= worksheet.Dimension.End.Row; rowIndex++)
                {
                    string permit1NumberStr = worksheet.Cells[rowIndex, 7].Text;
                    string permit2NumberStr = worksheet.Cells[rowIndex, 10].Text;

                    if (int.TryParse(permit1NumberStr, out int permit1Number))
                    {
                        usedPermitNumbers.Add(permit1Number);
                    }

                    if (int.TryParse(permit2NumberStr, out int permit2Number))
                    {
                        usedPermitNumbers.Add(permit2Number);
                    }
                }

                int nextPermitNumber = 1;
                while (usedPermitNumbers.Contains(nextPermitNumber))
                {
                    nextPermitNumber++;
                }

                return nextPermitNumber.ToString("D6");
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

        private void eyeColorLabel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void sexLabel_Click(object sender, EventArgs e)
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            // Code to handle when group box 3 is entered
        }
        private void UpdateSignaturePanel()
        {
            signaturePanel.Refresh(); // Call this after saving or loading a signature
        }
        private void remarksBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void sigPlusNET1_Click(object sender, EventArgs e)
        {

        }
    }
}