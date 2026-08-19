
namespace SOFA_Generator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lastNameTextBox = new TextBox();
            firstNameTextBox = new TextBox();
            dodIdTextBox = new TextBox();
            signaturePanel = new Panel();
            sigPlusNET1 = new Topaz.SigPlusNET();
            btnSaveSignature = new Button();
            btnRequestSignature = new Button();
            btnGeneratePermitNumber = new Button();
            permit1TextBox = new TextBox();
            issue1DateTimePicker = new DateTimePicker();
            exp1DateTimePicker = new DateTimePicker();
            permit2TextBox = new TextBox();
            issue2DateTimePicker = new DateTimePicker();
            exp2DateTimePicker = new DateTimePicker();
            btnSearch = new Button();
            catPaxComboBox = new ComboBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            dobDateTimePicker = new DateTimePicker();
            heightTextBox = new TextBox();
            weightTextBox = new TextBox();
            hairColorComboBox = new ComboBox();
            eyeColorComboBox = new ComboBox();
            sexLabel = new Label();
            dobLabel = new Label();
            heightLabel = new Label();
            weightLabel = new Label();
            hairColorLabel = new Label();
            eyeColorLabel = new Label();
            rankLabel = new Label();
            statusLabel = new Label();
            lastNameLabel = new Label();
            firstNameLabel = new Label();
            unitLabel = new Label();
            catLabel = new Label();
            restrictionsBox = new CheckBox();
            remarksBox = new TextBox();
            remarksLabel = new Label();
            issuerComboBox = new ComboBox();
            issuerLabel = new Label();
            btnBrowse = new Button();
            fontDialog1 = new FontDialog();
            sexComboBox = new ComboBox();
            statusComboBox = new ComboBox();
            militaryRankComboBox = new ComboBox();
            civilianRankComboBox = new ComboBox();
            naLabel = new Label();
            unitComboBox = new ComboBox();
            btnReset = new Button();
            pictureBox1 = new PictureBox();
            picturebutton = new Button();
            signaturegroupBox = new GroupBox();
            picturegroupBox = new GroupBox();
            stampLabel = new Label();
            stampComboBox = new ComboBox();
            msfTextBox = new TextBox();
            MSFlabel = new Label();
            autoJeepCheckBox = new CheckBox();
            motorcycleCheckBox = new CheckBox();
            label2 = new Label();
            PermitSearchTextBox = new TextBox();
            PermitSearch = new Button();
            LanguageButton = new Button();
            PermitcheckBox = new CheckBox();
            mopedCheckBox = new CheckBox();
            otherCheckBox = new CheckBox();
            signaturePanel.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            signaturegroupBox.SuspendLayout();
            picturegroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // lastNameTextBox
            // 
            lastNameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lastNameTextBox.Location = new Point(150, 105);
            lastNameTextBox.Margin = new Padding(3, 2, 3, 2);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.Size = new Size(132, 29);
            lastNameTextBox.TabIndex = 7;
            lastNameTextBox.Visible = false;
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            firstNameTextBox.Location = new Point(150, 135);
            firstNameTextBox.Margin = new Padding(3, 2, 3, 2);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.Size = new Size(132, 29);
            firstNameTextBox.TabIndex = 8;
            firstNameTextBox.Visible = false;
            // 
            // dodIdTextBox
            // 
            dodIdTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dodIdTextBox.Location = new Point(321, 15);
            dodIdTextBox.Margin = new Padding(3, 2, 3, 2);
            dodIdTextBox.Name = "dodIdTextBox";
            dodIdTextBox.Size = new Size(135, 29);
            dodIdTextBox.TabIndex = 1;
            // 
            // signaturePanel
            // 
            signaturePanel.BorderStyle = BorderStyle.FixedSingle;
            signaturePanel.Controls.Add(sigPlusNET1);
            signaturePanel.Location = new Point(31, 86);
            signaturePanel.Margin = new Padding(3, 2, 3, 2);
            signaturePanel.Name = "signaturePanel";
            signaturePanel.Size = new Size(203, 39);
            signaturePanel.TabIndex = 11;
            signaturePanel.Visible = false;
            // 
            // sigPlusNET1
            // 
            sigPlusNET1.BackColor = Color.White;
            sigPlusNET1.ForeColor = Color.Black;
            sigPlusNET1.Location = new Point(-1, 0);
            sigPlusNET1.Margin = new Padding(3, 2, 3, 2);
            sigPlusNET1.Name = "sigPlusNET1";
            sigPlusNET1.Size = new Size(203, 38);
            sigPlusNET1.TabIndex = 0;
            sigPlusNET1.Click += sigPlusNET1_Click;
            // 
            // btnSaveSignature
            // 
            btnSaveSignature.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveSignature.Location = new Point(623, 345);
            btnSaveSignature.Margin = new Padding(3, 2, 3, 2);
            btnSaveSignature.Name = "btnSaveSignature";
            btnSaveSignature.Size = new Size(118, 56);
            btnSaveSignature.TabIndex = 30;
            btnSaveSignature.Text = "Print";
            btnSaveSignature.UseVisualStyleBackColor = true;
            btnSaveSignature.Visible = false;
            btnSaveSignature.Click += btnSaveSignature_Click;
            // 
            // btnRequestSignature
            // 
            btnRequestSignature.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRequestSignature.Location = new Point(5, 11);
            btnRequestSignature.Margin = new Padding(3, 2, 3, 2);
            btnRequestSignature.Name = "btnRequestSignature";
            btnRequestSignature.Size = new Size(256, 30);
            btnRequestSignature.TabIndex = 28;
            btnRequestSignature.Text = "Request Signature";
            btnRequestSignature.UseVisualStyleBackColor = true;
            btnRequestSignature.Visible = false;
            btnRequestSignature.Click += btnRequestSignature_Click;
            // 
            // btnGeneratePermitNumber
            // 
            btnGeneratePermitNumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGeneratePermitNumber.Location = new Point(623, 285);
            btnGeneratePermitNumber.Margin = new Padding(3, 2, 3, 2);
            btnGeneratePermitNumber.Name = "btnGeneratePermitNumber";
            btnGeneratePermitNumber.Size = new Size(118, 56);
            btnGeneratePermitNumber.TabIndex = 3;
            btnGeneratePermitNumber.Text = "Save";
            btnGeneratePermitNumber.UseVisualStyleBackColor = true;
            btnGeneratePermitNumber.Visible = false;
            btnGeneratePermitNumber.Click += btnGeneratePermitNumber_Click;
            // 
            // permit1TextBox
            // 
            permit1TextBox.BackColor = Color.LightSteelBlue;
            permit1TextBox.BorderStyle = BorderStyle.FixedSingle;
            permit1TextBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            permit1TextBox.ForeColor = SystemColors.WindowFrame;
            permit1TextBox.Location = new Point(269, 29);
            permit1TextBox.Margin = new Padding(3, 2, 3, 2);
            permit1TextBox.Name = "permit1TextBox";
            permit1TextBox.Size = new Size(75, 23);
            permit1TextBox.TabIndex = 22;
            permit1TextBox.Visible = false;
            // 
            // issue1DateTimePicker
            // 
            issue1DateTimePicker.CustomFormat = "MM/dd/yyyy";
            issue1DateTimePicker.Format = DateTimePickerFormat.Custom;
            issue1DateTimePicker.Location = new Point(15, 30);
            issue1DateTimePicker.Margin = new Padding(3, 2, 3, 2);
            issue1DateTimePicker.Name = "issue1DateTimePicker";
            issue1DateTimePicker.Size = new Size(122, 23);
            issue1DateTimePicker.TabIndex = 20;
            issue1DateTimePicker.Visible = false;
            // 
            // exp1DateTimePicker
            // 
            exp1DateTimePicker.CustomFormat = "MM/dd/yyyy";
            exp1DateTimePicker.Format = DateTimePickerFormat.Custom;
            exp1DateTimePicker.Location = new Point(142, 29);
            exp1DateTimePicker.Margin = new Padding(3, 2, 3, 2);
            exp1DateTimePicker.Name = "exp1DateTimePicker";
            exp1DateTimePicker.Size = new Size(122, 23);
            exp1DateTimePicker.TabIndex = 21;
            exp1DateTimePicker.Visible = false;
            exp1DateTimePicker.ValueChanged += exp1DateTimePicker_ValueChanged;
            // 
            // permit2TextBox
            // 
            permit2TextBox.BackColor = Color.LightSteelBlue;
            permit2TextBox.BorderStyle = BorderStyle.FixedSingle;
            permit2TextBox.ForeColor = SystemColors.WindowFrame;
            permit2TextBox.Location = new Point(267, 29);
            permit2TextBox.Margin = new Padding(3, 2, 3, 2);
            permit2TextBox.Name = "permit2TextBox";
            permit2TextBox.Size = new Size(76, 23);
            permit2TextBox.TabIndex = 25;
            permit2TextBox.Visible = false;
            permit2TextBox.TextChanged += permit2TextBox_TextChanged;
            // 
            // issue2DateTimePicker
            // 
            issue2DateTimePicker.CustomFormat = "MM/dd/yyyy";
            issue2DateTimePicker.Format = DateTimePickerFormat.Custom;
            issue2DateTimePicker.Location = new Point(18, 30);
            issue2DateTimePicker.Margin = new Padding(3, 2, 3, 2);
            issue2DateTimePicker.Name = "issue2DateTimePicker";
            issue2DateTimePicker.Size = new Size(120, 23);
            issue2DateTimePicker.TabIndex = 23;
            issue2DateTimePicker.Visible = false;
            issue2DateTimePicker.ValueChanged += issue2DateTimePicker_ValueChanged;
            // 
            // exp2DateTimePicker
            // 
            exp2DateTimePicker.CustomFormat = "MM/dd/yyyy";
            exp2DateTimePicker.Format = DateTimePickerFormat.Custom;
            exp2DateTimePicker.Location = new Point(143, 30);
            exp2DateTimePicker.Margin = new Padding(3, 2, 3, 2);
            exp2DateTimePicker.Name = "exp2DateTimePicker";
            exp2DateTimePicker.Size = new Size(120, 23);
            exp2DateTimePicker.TabIndex = 24;
            exp2DateTimePicker.Visible = false;
            exp2DateTimePicker.ValueChanged += exp2DateTimePicker_ValueChanged;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(463, 15);
            btnSearch.Margin = new Padding(3, 2, 3, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(118, 30);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "ID Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // catPaxComboBox
            // 
            catPaxComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            catPaxComboBox.Items.AddRange(new object[] { "Cat 0A: Bicycles", "Cat 0B: Electric Bicycles", "Cat 1: Mopeds 50cc or less", "Cat 2: Motorcycles 125cc or less", "Cat 3: Motorcycles 400cc or less", "Cat 4: Motorcycles 750cc or less", "Cat 5: Motorcycles over 750cc" });
            catPaxComboBox.Location = new Point(412, 445);
            catPaxComboBox.Margin = new Padding(3, 2, 3, 2);
            catPaxComboBox.Name = "catPaxComboBox";
            catPaxComboBox.Size = new Size(169, 23);
            catPaxComboBox.TabIndex = 19;
            catPaxComboBox.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(186, 18);
            label1.Name = "label1";
            label1.Size = new Size(129, 21);
            label1.TabIndex = 25;
            label1.Text = "EDI-PI / DoD ID #";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(issue1DateTimePicker);
            groupBox1.Controls.Add(exp1DateTimePicker);
            groupBox1.Controls.Add(permit1TextBox);
            groupBox1.Location = new Point(321, 76);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(357, 77);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "First Permit";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(issue2DateTimePicker);
            groupBox2.Controls.Add(exp2DateTimePicker);
            groupBox2.Controls.Add(permit2TextBox);
            groupBox2.Location = new Point(683, 76);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(360, 77);
            groupBox2.TabIndex = 32;
            groupBox2.TabStop = false;
            groupBox2.Text = "New Permit";
            // 
            // dobDateTimePicker
            // 
            dobDateTimePicker.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobDateTimePicker.Format = DateTimePickerFormat.Custom;
            dobDateTimePicker.Location = new Point(150, 196);
            dobDateTimePicker.Margin = new Padding(3, 2, 3, 2);
            dobDateTimePicker.Name = "dobDateTimePicker";
            dobDateTimePicker.Size = new Size(132, 29);
            dobDateTimePicker.TabIndex = 10;
            dobDateTimePicker.ValueChanged += dobDateTimePicker_ValueChanged;
            // 
            // heightTextBox
            // 
            heightTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightTextBox.Location = new Point(150, 226);
            heightTextBox.Margin = new Padding(3, 2, 3, 2);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.Size = new Size(132, 29);
            heightTextBox.TabIndex = 11;
            // 
            // weightTextBox
            // 
            weightTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightTextBox.Location = new Point(150, 256);
            weightTextBox.Margin = new Padding(3, 2, 3, 2);
            weightTextBox.Name = "weightTextBox";
            weightTextBox.Size = new Size(132, 29);
            weightTextBox.TabIndex = 12;
            // 
            // hairColorComboBox
            // 
            hairColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            hairColorComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hairColorComboBox.FormattingEnabled = true;
            hairColorComboBox.ItemHeight = 21;
            hairColorComboBox.Items.AddRange(new object[] { "Black", "Brown", "Blonde", "Red", "Gray", "Other" });
            hairColorComboBox.Location = new Point(150, 286);
            hairColorComboBox.Margin = new Padding(3, 2, 3, 2);
            hairColorComboBox.Name = "hairColorComboBox";
            hairColorComboBox.Size = new Size(132, 29);
            hairColorComboBox.TabIndex = 13;
            hairColorComboBox.SelectedIndexChanged += hairColorComboBox_SelectedIndexChanged;
            // 
            // eyeColorComboBox
            // 
            eyeColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            eyeColorComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eyeColorComboBox.FormattingEnabled = true;
            eyeColorComboBox.ItemHeight = 21;
            eyeColorComboBox.Items.AddRange(new object[] { "Brown", "Blue", "Green", "Hazel", "Gray", "Other" });
            eyeColorComboBox.Location = new Point(150, 318);
            eyeColorComboBox.Margin = new Padding(3, 2, 3, 2);
            eyeColorComboBox.Name = "eyeColorComboBox";
            eyeColorComboBox.Size = new Size(132, 29);
            eyeColorComboBox.TabIndex = 14;
            // 
            // sexLabel
            // 
            sexLabel.AutoSize = true;
            sexLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sexLabel.Location = new Point(25, 167);
            sexLabel.Name = "sexLabel";
            sexLabel.Size = new Size(34, 21);
            sexLabel.TabIndex = 35;
            sexLabel.Text = "Sex";
            sexLabel.Click += sexLabel_Click_1;
            // 
            // dobLabel
            // 
            dobLabel.AutoSize = true;
            dobLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobLabel.Location = new Point(25, 201);
            dobLabel.Name = "dobLabel";
            dobLabel.Size = new Size(42, 21);
            dobLabel.TabIndex = 36;
            dobLabel.Text = "DOB";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightLabel.Location = new Point(25, 229);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(56, 21);
            heightLabel.TabIndex = 37;
            heightLabel.Text = "Height";
            // 
            // weightLabel
            // 
            weightLabel.AutoSize = true;
            weightLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightLabel.Location = new Point(25, 259);
            weightLabel.Name = "weightLabel";
            weightLabel.Size = new Size(59, 21);
            weightLabel.TabIndex = 38;
            weightLabel.Text = "Weight";
            // 
            // hairColorLabel
            // 
            hairColorLabel.AutoSize = true;
            hairColorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hairColorLabel.Location = new Point(25, 289);
            hairColorLabel.Name = "hairColorLabel";
            hairColorLabel.Size = new Size(81, 21);
            hairColorLabel.TabIndex = 39;
            hairColorLabel.Text = "Hair Color";
            // 
            // eyeColorLabel
            // 
            eyeColorLabel.AutoSize = true;
            eyeColorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eyeColorLabel.Location = new Point(25, 320);
            eyeColorLabel.Name = "eyeColorLabel";
            eyeColorLabel.Size = new Size(76, 21);
            eyeColorLabel.TabIndex = 40;
            eyeColorLabel.Text = "Eye Color";
            // 
            // rankLabel
            // 
            rankLabel.AutoSize = true;
            rankLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rankLabel.Location = new Point(162, 76);
            rankLabel.Name = "rankLabel";
            rankLabel.Size = new Size(45, 21);
            rankLabel.TabIndex = 42;
            rankLabel.Text = "Rank";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(25, 76);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(52, 21);
            statusLabel.TabIndex = 43;
            statusLabel.Text = "Status";
            statusLabel.Click += statusLabel_Click;
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lastNameLabel.Location = new Point(25, 107);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(84, 21);
            lastNameLabel.TabIndex = 44;
            lastNameLabel.Text = "Last Name";
            lastNameLabel.Click += lastNameLabel_Click;
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            firstNameLabel.Location = new Point(25, 137);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(108, 21);
            firstNameLabel.TabIndex = 45;
            firstNameLabel.Text = "First Name MI";
            // 
            // unitLabel
            // 
            unitLabel.AutoSize = true;
            unitLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unitLabel.Location = new Point(25, 350);
            unitLabel.Name = "unitLabel";
            unitLabel.Size = new Size(39, 21);
            unitLabel.TabIndex = 46;
            unitLabel.Text = "Unit";
            unitLabel.Click += unitLabel_Click_1;
            // 
            // catLabel
            // 
            catLabel.AutoSize = true;
            catLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            catLabel.Location = new Point(324, 443);
            catLabel.Name = "catLabel";
            catLabel.Size = new Size(73, 21);
            catLabel.TabIndex = 48;
            catLabel.Text = "Category";
            catLabel.Click += catLabel_Click;
            // 
            // restrictionsBox
            // 
            restrictionsBox.AutoSize = true;
            restrictionsBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restrictionsBox.Location = new Point(25, 410);
            restrictionsBox.Margin = new Padding(3, 2, 3, 2);
            restrictionsBox.Name = "restrictionsBox";
            restrictionsBox.Size = new Size(147, 25);
            restrictionsBox.TabIndex = 17;
            restrictionsBox.Text = "Glasses/Contacts";
            restrictionsBox.UseVisualStyleBackColor = true;
            restrictionsBox.CheckedChanged += restrictionsBox_CheckedChanged;
            // 
            // remarksBox
            // 
            remarksBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            remarksBox.Location = new Point(785, 158);
            remarksBox.Margin = new Padding(3, 2, 3, 2);
            remarksBox.Name = "remarksBox";
            remarksBox.Size = new Size(260, 29);
            remarksBox.TabIndex = 27;
            remarksBox.TextChanged += remarksBox_TextChanged;
            // 
            // remarksLabel
            // 
            remarksLabel.AutoSize = true;
            remarksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            remarksLabel.Location = new Point(702, 160);
            remarksLabel.Name = "remarksLabel";
            remarksLabel.Size = new Size(74, 21);
            remarksLabel.TabIndex = 52;
            remarksLabel.Text = "Remarks:";
            remarksLabel.Click += remarksLabel_Click;
            // 
            // issuerComboBox
            // 
            issuerComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            issuerComboBox.FormattingEnabled = true;
            issuerComboBox.Location = new Point(446, 158);
            issuerComboBox.Margin = new Padding(3, 2, 3, 2);
            issuerComboBox.Name = "issuerComboBox";
            issuerComboBox.Size = new Size(210, 29);
            issuerComboBox.TabIndex = 26;
            issuerComboBox.SelectedIndexChanged += issuerComboBox_SelectedIndexChanged;
            // 
            // issuerLabel
            // 
            issuerLabel.AutoSize = true;
            issuerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            issuerLabel.Location = new Point(321, 158);
            issuerLabel.Name = "issuerLabel";
            issuerLabel.Size = new Size(111, 21);
            issuerLabel.TabIndex = 54;
            issuerLabel.Text = "Issuing Offcial:";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(25, 16);
            btnBrowse.Margin = new Padding(3, 2, 3, 2);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(130, 30);
            btnBrowse.TabIndex = 0;
            btnBrowse.Text = "SOFA Database 📂";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // sexComboBox
            // 
            sexComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sexComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sexComboBox.Items.AddRange(new object[] { "M", "F", "O" });
            sexComboBox.Location = new Point(150, 165);
            sexComboBox.Margin = new Padding(3, 2, 3, 2);
            sexComboBox.Name = "sexComboBox";
            sexComboBox.Size = new Size(132, 29);
            sexComboBox.TabIndex = 9;
            // 
            // statusComboBox
            // 
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusComboBox.FormattingEnabled = true;
            statusComboBox.Items.AddRange(new object[] { "AD", "R/G", "CIV", "CTR", "DEP" });
            statusComboBox.Location = new Point(88, 76);
            statusComboBox.Margin = new Padding(3, 2, 3, 2);
            statusComboBox.Name = "statusComboBox";
            statusComboBox.Size = new Size(57, 27);
            statusComboBox.TabIndex = 5;
            statusComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // militaryRankComboBox
            // 
            militaryRankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            militaryRankComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            militaryRankComboBox.Items.AddRange(new object[] { "E-1", "E-2", "E-3", "E-4", "E-5", "E-6", "E-7", "E-8", "E-9", "W-1", "W-2", "W-3", "W-4", "W-5", "O-1", "O-2", "O-3", "O-4", "O-5", "O-6", "O-7", "O-8", "O-9", "O-10" });
            militaryRankComboBox.Location = new Point(215, 76);
            militaryRankComboBox.Margin = new Padding(3, 2, 3, 2);
            militaryRankComboBox.Name = "militaryRankComboBox";
            militaryRankComboBox.Size = new Size(67, 27);
            militaryRankComboBox.TabIndex = 6;
            militaryRankComboBox.Visible = false;
            militaryRankComboBox.SelectedIndexChanged += militaryRankComboBox_SelectedIndexChanged;
            // 
            // civilianRankComboBox
            // 
            civilianRankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            civilianRankComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            civilianRankComboBox.Items.AddRange(new object[] { "GS-1", "GS-2", "GS-3", "GS-4", "GS-5", "GS-6", "GS-7", "GS-8", "GS-9", "GS-10", "GS-11", "GS-12", "GS-13", "GS-14", "GS-15", "SES" });
            civilianRankComboBox.Location = new Point(215, 76);
            civilianRankComboBox.Margin = new Padding(3, 2, 3, 2);
            civilianRankComboBox.Name = "civilianRankComboBox";
            civilianRankComboBox.Size = new Size(67, 27);
            civilianRankComboBox.TabIndex = 58;
            civilianRankComboBox.Visible = false;
            civilianRankComboBox.SelectedIndexChanged += civilianRankComboBox_SelectedIndexChanged;
            // 
            // naLabel
            // 
            naLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            naLabel.Location = new Point(220, 76);
            naLabel.Name = "naLabel";
            naLabel.Size = new Size(52, 27);
            naLabel.TabIndex = 59;
            naLabel.Text = "N/A";
            naLabel.Visible = false;
            naLabel.Click += naLabel_Click;
            // 
            // unitComboBox
            // 
            unitComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            unitComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unitComboBox.FormattingEnabled = true;
            unitComboBox.Location = new Point(150, 350);
            unitComboBox.Margin = new Padding(3, 2, 3, 2);
            unitComboBox.Name = "unitComboBox";
            unitComboBox.Size = new Size(132, 27);
            unitComboBox.TabIndex = 15;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.IndianRed;
            btnReset.Location = new Point(941, 14);
            btnReset.Margin = new Padding(3, 2, 3, 2);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(103, 30);
            btnReset.TabIndex = 4;
            btnReset.Text = "Clear Form";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(5, 51);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 202);
            pictureBox1.TabIndex = 63;
            pictureBox1.TabStop = false;
            // 
            // picturebutton
            // 
            picturebutton.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            picturebutton.Location = new Point(5, 12);
            picturebutton.Margin = new Padding(3, 2, 3, 2);
            picturebutton.Name = "picturebutton";
            picturebutton.Size = new Size(256, 30);
            picturebutton.TabIndex = 29;
            picturebutton.Text = "Take Picture";
            picturebutton.UseVisualStyleBackColor = true;
            picturebutton.Click += picturebutton_Click;
            // 
            // signaturegroupBox
            // 
            signaturegroupBox.Controls.Add(btnRequestSignature);
            signaturegroupBox.Controls.Add(signaturePanel);
            signaturegroupBox.Location = new Point(321, 223);
            signaturegroupBox.Margin = new Padding(3, 2, 3, 2);
            signaturegroupBox.Name = "signaturegroupBox";
            signaturegroupBox.Padding = new Padding(3, 2, 3, 2);
            signaturegroupBox.Size = new Size(267, 183);
            signaturegroupBox.TabIndex = 65;
            signaturegroupBox.TabStop = false;
            // 
            // picturegroupBox
            // 
            picturegroupBox.Controls.Add(pictureBox1);
            picturegroupBox.Controls.Add(picturebutton);
            picturegroupBox.Location = new Point(777, 223);
            picturegroupBox.Margin = new Padding(3, 2, 3, 2);
            picturegroupBox.Name = "picturegroupBox";
            picturegroupBox.Padding = new Padding(3, 2, 3, 2);
            picturegroupBox.Size = new Size(267, 257);
            picturegroupBox.TabIndex = 66;
            picturegroupBox.TabStop = false;
            // 
            // stampLabel
            // 
            stampLabel.AutoSize = true;
            stampLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stampLabel.Location = new Point(25, 379);
            stampLabel.Name = "stampLabel";
            stampLabel.Size = new Size(54, 21);
            stampLabel.TabIndex = 67;
            stampLabel.Text = "Stamp";
            // 
            // stampComboBox
            // 
            stampComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stampComboBox.FormattingEnabled = true;
            stampComboBox.Items.AddRange(new object[] { "", "Student Driver", "On Base Only", "TDY", "Limited" });
            stampComboBox.Location = new Point(151, 379);
            stampComboBox.Margin = new Padding(3, 2, 3, 2);
            stampComboBox.Name = "stampComboBox";
            stampComboBox.Size = new Size(133, 29);
            stampComboBox.TabIndex = 16;
            // 
            // msfTextBox
            // 
            msfTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            msfTextBox.Location = new Point(412, 410);
            msfTextBox.Margin = new Padding(3, 2, 3, 2);
            msfTextBox.Name = "msfTextBox";
            msfTextBox.Size = new Size(169, 29);
            msfTextBox.TabIndex = 68;
            // 
            // MSFlabel
            // 
            MSFlabel.AutoSize = true;
            MSFlabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MSFlabel.Location = new Point(324, 413);
            MSFlabel.Name = "MSFlabel";
            MSFlabel.Size = new Size(41, 21);
            MSFlabel.TabIndex = 69;
            MSFlabel.Text = "MSF";
            // 
            // autoJeepCheckBox
            // 
            autoJeepCheckBox.AutoSize = true;
            autoJeepCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            autoJeepCheckBox.Location = new Point(25, 439);
            autoJeepCheckBox.Margin = new Padding(3, 2, 3, 2);
            autoJeepCheckBox.Name = "autoJeepCheckBox";
            autoJeepCheckBox.Size = new Size(99, 25);
            autoJeepCheckBox.TabIndex = 70;
            autoJeepCheckBox.Text = "Auto/Jeep";
            autoJeepCheckBox.UseVisualStyleBackColor = true;
            autoJeepCheckBox.CheckedChanged += autoJeepCheckBox_CheckedChanged;
            // 
            // motorcycleCheckBox
            // 
            motorcycleCheckBox.AutoSize = true;
            motorcycleCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            motorcycleCheckBox.Location = new Point(162, 439);
            motorcycleCheckBox.Margin = new Padding(3, 2, 3, 2);
            motorcycleCheckBox.Name = "motorcycleCheckBox";
            motorcycleCheckBox.Size = new Size(106, 25);
            motorcycleCheckBox.TabIndex = 71;
            motorcycleCheckBox.Text = "Motorcycle";
            motorcycleCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.InactiveBorder;
            label2.Location = new Point(879, 515);
            label2.Name = "label2";
            label2.Size = new Size(130, 15);
            label2.TabIndex = 72;
            label2.Text = "Powered by Project Arc";
            label2.Click += label2_Click_3;
            // 
            // PermitSearchTextBox
            // 
            PermitSearchTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PermitSearchTextBox.Location = new Point(641, 16);
            PermitSearchTextBox.Margin = new Padding(3, 2, 3, 2);
            PermitSearchTextBox.Name = "PermitSearchTextBox";
            PermitSearchTextBox.Size = new Size(135, 29);
            PermitSearchTextBox.TabIndex = 73;
            // 
            // PermitSearch
            // 
            PermitSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PermitSearch.Location = new Point(782, 14);
            PermitSearch.Margin = new Padding(3, 2, 3, 2);
            PermitSearch.Name = "PermitSearch";
            PermitSearch.Size = new Size(118, 30);
            PermitSearch.TabIndex = 74;
            PermitSearch.Text = "Permit Search";
            PermitSearch.UseVisualStyleBackColor = true;
            // 
            // LanguageButton
            // 
            LanguageButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LanguageButton.Location = new Point(1015, 497);
            LanguageButton.Margin = new Padding(3, 2, 3, 2);
            LanguageButton.Name = "LanguageButton";
            LanguageButton.Size = new Size(57, 33);
            LanguageButton.TabIndex = 75;
            LanguageButton.Text = "日本語";
            LanguageButton.UseVisualStyleBackColor = true;
            // 
            // PermitcheckBox
            // 
            PermitcheckBox.AutoSize = true;
            PermitcheckBox.Checked = true;
            PermitcheckBox.CheckState = CheckState.Checked;
            PermitcheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PermitcheckBox.Location = new Point(623, 234);
            PermitcheckBox.Margin = new Padding(3, 2, 3, 2);
            PermitcheckBox.Name = "PermitcheckBox";
            PermitcheckBox.Size = new Size(110, 46);
            PermitcheckBox.TabIndex = 76;
            PermitcheckBox.Text = "New Permit\r\nNumber?";
            PermitcheckBox.UseVisualStyleBackColor = true;
            PermitcheckBox.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // mopedCheckBox
            // 
            mopedCheckBox.AutoSize = true;
            mopedCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mopedCheckBox.Location = new Point(25, 468);
            mopedCheckBox.Margin = new Padding(3, 2, 3, 2);
            mopedCheckBox.Name = "mopedCheckBox";
            mopedCheckBox.Size = new Size(78, 25);
            mopedCheckBox.TabIndex = 77;
            mopedCheckBox.Text = "Moped";
            mopedCheckBox.UseVisualStyleBackColor = true;
            mopedCheckBox.CheckedChanged += VehicleCategory_CheckedChanged;
            // 
            // otherCheckBox
            // 
            otherCheckBox.AutoSize = true;
            otherCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            otherCheckBox.Location = new Point(162, 468);
            otherCheckBox.Margin = new Padding(3, 2, 3, 2);
            otherCheckBox.Name = "otherCheckBox";
            otherCheckBox.Size = new Size(69, 25);
            otherCheckBox.TabIndex = 78;
            otherCheckBox.Text = "Other";
            otherCheckBox.UseVisualStyleBackColor = true;
            otherCheckBox.CheckedChanged += VehicleCategory_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1070, 530);
            Controls.Add(otherCheckBox);
            Controls.Add(mopedCheckBox);
            Controls.Add(PermitcheckBox);
            Controls.Add(LanguageButton);
            Controls.Add(PermitSearch);
            Controls.Add(PermitSearchTextBox);
            Controls.Add(label2);
            Controls.Add(motorcycleCheckBox);
            Controls.Add(autoJeepCheckBox);
            Controls.Add(MSFlabel);
            Controls.Add(msfTextBox);
            Controls.Add(stampComboBox);
            Controls.Add(stampLabel);
            Controls.Add(picturegroupBox);
            Controls.Add(signaturegroupBox);
            Controls.Add(btnReset);
            Controls.Add(unitComboBox);
            Controls.Add(statusComboBox);
            Controls.Add(sexComboBox);
            Controls.Add(issuerLabel);
            Controls.Add(issuerComboBox);
            Controls.Add(remarksLabel);
            Controls.Add(remarksBox);
            Controls.Add(restrictionsBox);
            Controls.Add(catLabel);
            Controls.Add(unitLabel);
            Controls.Add(firstNameLabel);
            Controls.Add(lastNameLabel);
            Controls.Add(statusLabel);
            Controls.Add(rankLabel);
            Controls.Add(eyeColorLabel);
            Controls.Add(hairColorLabel);
            Controls.Add(weightLabel);
            Controls.Add(heightLabel);
            Controls.Add(dobLabel);
            Controls.Add(sexLabel);
            Controls.Add(eyeColorComboBox);
            Controls.Add(hairColorComboBox);
            Controls.Add(weightTextBox);
            Controls.Add(heightTextBox);
            Controls.Add(dobDateTimePicker);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(catPaxComboBox);
            Controls.Add(btnSearch);
            Controls.Add(btnGeneratePermitNumber);
            Controls.Add(btnSaveSignature);
            Controls.Add(dodIdTextBox);
            Controls.Add(firstNameTextBox);
            Controls.Add(lastNameTextBox);
            Controls.Add(btnBrowse);
            Controls.Add(militaryRankComboBox);
            Controls.Add(civilianRankComboBox);
            Controls.Add(naLabel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "SOFA King";
            Load += Form1_Load;
            signaturePanel.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            signaturegroupBox.ResumeLayout(false);
            picturegroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void unitTextBox_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox dodIdTextBox;
        private System.Windows.Forms.Panel signaturePanel;
        private System.Windows.Forms.Button btnSaveSignature;
        private System.Windows.Forms.Button btnRequestSignature;
        private System.Windows.Forms.Button btnGeneratePermitNumber;
        private System.Windows.Forms.TextBox permit1TextBox;
        private System.Windows.Forms.DateTimePicker issue1DateTimePicker;
        private System.Windows.Forms.DateTimePicker exp1DateTimePicker;
        private System.Windows.Forms.TextBox permit2TextBox;
        private System.Windows.Forms.DateTimePicker issue2DateTimePicker;
        private System.Windows.Forms.DateTimePicker exp2DateTimePicker;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox catPaxComboBox;
        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DateTimePicker dobDateTimePicker;
        private TextBox heightTextBox;
        private TextBox weightTextBox;
        private ComboBox hairColorComboBox;
        private ComboBox eyeColorComboBox;
        private Label sexLabel;
        private Label dobLabel;
        private Label heightLabel;
        private Label weightLabel;
        private Label hairColorLabel;
        private Label eyeColorLabel;
        private Label rankLabel;
        private Label statusLabel;
        private Label lastNameLabel;
        private Label firstNameLabel;
        private Label unitLabel;
        private Label catLabel;
        private CheckBox restrictionsBox;
        private TextBox remarksBox;
        private Label remarksLabel;
        private ComboBox issuerComboBox;
        private Label issuerLabel;
        private System.Windows.Forms.Button btnBrowse;
        private FontDialog fontDialog1;
        private ComboBox sexComboBox;
        private ComboBox statusComboBox;
        private ComboBox militaryRankComboBox;
        private ComboBox civilianRankComboBox;
        private Label naLabel;
        private ComboBox unitComboBox;
        private System.Windows.Forms.Button btnReset;
        private Topaz.SigPlusNET sigPlusNET1;
        private PictureBox pictureBox1;
        private Button picturebutton;
        private GroupBox signaturegroupBox;
        private GroupBox picturegroupBox;
        private Label stampLabel;
        private ComboBox stampComboBox;
        private TextBox msfTextBox;
        private Label MSFlabel;
        private CheckBox autoJeepCheckBox;
        private CheckBox motorcycleCheckBox;
        private Label label2;
        private TextBox PermitSearchTextBox;
        private Button PermitSearch;
        private Button LanguageButton;
        private CheckBox PermitcheckBox;
        private CheckBox mopedCheckBox;
        private CheckBox otherCheckBox;
    }
}