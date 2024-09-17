
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
            lastNameTextBox.Location = new Point(172, 140);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.Size = new Size(150, 34);
            lastNameTextBox.TabIndex = 7;
            lastNameTextBox.Visible = false;
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            firstNameTextBox.Location = new Point(172, 180);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.Size = new Size(150, 34);
            firstNameTextBox.TabIndex = 8;
            firstNameTextBox.Visible = false;
            // 
            // dodIdTextBox
            // 
            dodIdTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dodIdTextBox.Location = new Point(355, 21);
            dodIdTextBox.Name = "dodIdTextBox";
            dodIdTextBox.Size = new Size(252, 34);
            dodIdTextBox.TabIndex = 1;
            // 
            // signaturePanel
            // 
            signaturePanel.BorderStyle = BorderStyle.FixedSingle;
            signaturePanel.Controls.Add(sigPlusNET1);
            signaturePanel.Location = new Point(35, 151);
            signaturePanel.Name = "signaturePanel";
            signaturePanel.Size = new Size(232, 51);
            signaturePanel.TabIndex = 11;
            signaturePanel.Visible = false;
            // 
            // sigPlusNET1
            // 
            sigPlusNET1.BackColor = Color.White;
            sigPlusNET1.ForeColor = Color.Black;
            sigPlusNET1.Location = new Point(-1, -1);
            sigPlusNET1.Name = "sigPlusNET1";
            sigPlusNET1.Size = new Size(232, 51);
            sigPlusNET1.TabIndex = 0;
            sigPlusNET1.Click += sigPlusNET1_Click;
            // 
            // btnSaveSignature
            // 
            btnSaveSignature.Location = new Point(712, 578);
            btnSaveSignature.Name = "btnSaveSignature";
            btnSaveSignature.Size = new Size(130, 62);
            btnSaveSignature.TabIndex = 30;
            btnSaveSignature.Text = "Save and Print";
            btnSaveSignature.UseVisualStyleBackColor = true;
            btnSaveSignature.Visible = false;
            btnSaveSignature.Click += btnSaveSignature_Click;
            // 
            // btnRequestSignature
            // 
            btnRequestSignature.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRequestSignature.Location = new Point(6, 15);
            btnRequestSignature.Name = "btnRequestSignature";
            btnRequestSignature.Size = new Size(293, 40);
            btnRequestSignature.TabIndex = 28;
            btnRequestSignature.Text = "Request Signature";
            btnRequestSignature.UseVisualStyleBackColor = true;
            btnRequestSignature.Visible = false;
            btnRequestSignature.Click += btnRequestSignature_Click;
            // 
            // btnGeneratePermitNumber
            // 
            btnGeneratePermitNumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGeneratePermitNumber.Location = new Point(781, 19);
            btnGeneratePermitNumber.Name = "btnGeneratePermitNumber";
            btnGeneratePermitNumber.Size = new Size(252, 39);
            btnGeneratePermitNumber.TabIndex = 3;
            btnGeneratePermitNumber.Text = "Generate Permit Number";
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
            permit1TextBox.Location = new Point(307, 39);
            permit1TextBox.Name = "permit1TextBox";
            permit1TextBox.Size = new Size(85, 27);
            permit1TextBox.TabIndex = 22;
            permit1TextBox.Visible = false;
            // 
            // issue1DateTimePicker
            // 
            issue1DateTimePicker.CustomFormat = "MM/dd/yyyy";
            issue1DateTimePicker.Format = DateTimePickerFormat.Custom;
            issue1DateTimePicker.Location = new Point(17, 40);
            issue1DateTimePicker.Name = "issue1DateTimePicker";
            issue1DateTimePicker.Size = new Size(139, 27);
            issue1DateTimePicker.TabIndex = 20;
            issue1DateTimePicker.Visible = false;
            // 
            // exp1DateTimePicker
            // 
            exp1DateTimePicker.CustomFormat = "MM/dd/yyyy";
            exp1DateTimePicker.Format = DateTimePickerFormat.Custom;
            exp1DateTimePicker.Location = new Point(162, 39);
            exp1DateTimePicker.Name = "exp1DateTimePicker";
            exp1DateTimePicker.Size = new Size(139, 27);
            exp1DateTimePicker.TabIndex = 21;
            exp1DateTimePicker.Visible = false;
            exp1DateTimePicker.ValueChanged += exp1DateTimePicker_ValueChanged;
            // 
            // permit2TextBox
            // 
            permit2TextBox.BackColor = Color.LightSteelBlue;
            permit2TextBox.BorderStyle = BorderStyle.FixedSingle;
            permit2TextBox.ForeColor = SystemColors.WindowFrame;
            permit2TextBox.Location = new Point(305, 39);
            permit2TextBox.Name = "permit2TextBox";
            permit2TextBox.Size = new Size(87, 27);
            permit2TextBox.TabIndex = 25;
            permit2TextBox.Visible = false;
            permit2TextBox.TextChanged += permit2TextBox_TextChanged;
            // 
            // issue2DateTimePicker
            // 
            issue2DateTimePicker.CustomFormat = "MM/dd/yyyy";
            issue2DateTimePicker.Format = DateTimePickerFormat.Custom;
            issue2DateTimePicker.Location = new Point(21, 40);
            issue2DateTimePicker.Name = "issue2DateTimePicker";
            issue2DateTimePicker.Size = new Size(136, 27);
            issue2DateTimePicker.TabIndex = 23;
            issue2DateTimePicker.Visible = false;
            issue2DateTimePicker.ValueChanged += issue2DateTimePicker_ValueChanged;
            // 
            // exp2DateTimePicker
            // 
            exp2DateTimePicker.CustomFormat = "MM/dd/yyyy";
            exp2DateTimePicker.Format = DateTimePickerFormat.Custom;
            exp2DateTimePicker.Location = new Point(163, 40);
            exp2DateTimePicker.Name = "exp2DateTimePicker";
            exp2DateTimePicker.Size = new Size(136, 27);
            exp2DateTimePicker.TabIndex = 24;
            exp2DateTimePicker.Visible = false;
            exp2DateTimePicker.ValueChanged += exp2DateTimePicker_ValueChanged;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(613, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(125, 40);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // catPaxComboBox
            // 
            catPaxComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            catPaxComboBox.Items.AddRange(new object[] { "Cat 1: 50cc or less", "Cat 2: Motorcycles 125cc or less", "Cat 3: Motorcycles 400cc or less", "Cat 4: Motorcycles 750cc or less", "Cat 5: Motorcycles over 750cc" });
            catPaxComboBox.Location = new Point(129, 663);
            catPaxComboBox.Name = "catPaxComboBox";
            catPaxComboBox.Size = new Size(195, 28);
            catPaxComboBox.TabIndex = 19;
            catPaxComboBox.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(185, 24);
            label1.Name = "label1";
            label1.Size = new Size(164, 28);
            label1.TabIndex = 25;
            label1.Text = "EDI-PI / DoD ID #";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(issue1DateTimePicker);
            groupBox1.Controls.Add(exp1DateTimePicker);
            groupBox1.Controls.Add(permit1TextBox);
            groupBox1.Location = new Point(367, 101);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(408, 103);
            groupBox1.TabIndex = 31;
            groupBox1.TabStop = false;
            groupBox1.Text = "First Permit";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(issue2DateTimePicker);
            groupBox2.Controls.Add(exp2DateTimePicker);
            groupBox2.Controls.Add(permit2TextBox);
            groupBox2.Location = new Point(781, 101);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(412, 103);
            groupBox2.TabIndex = 32;
            groupBox2.TabStop = false;
            groupBox2.Text = "New Permit";
            // 
            // dobDateTimePicker
            // 
            dobDateTimePicker.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobDateTimePicker.Format = DateTimePickerFormat.Custom;
            dobDateTimePicker.Location = new Point(172, 262);
            dobDateTimePicker.Name = "dobDateTimePicker";
            dobDateTimePicker.Size = new Size(150, 34);
            dobDateTimePicker.TabIndex = 10;
            dobDateTimePicker.ValueChanged += dobDateTimePicker_ValueChanged;
            // 
            // heightTextBox
            // 
            heightTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightTextBox.Location = new Point(172, 302);
            heightTextBox.Name = "heightTextBox";
            heightTextBox.Size = new Size(150, 34);
            heightTextBox.TabIndex = 11;
            // 
            // weightTextBox
            // 
            weightTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightTextBox.Location = new Point(172, 342);
            weightTextBox.Name = "weightTextBox";
            weightTextBox.Size = new Size(150, 34);
            weightTextBox.TabIndex = 12;
            // 
            // hairColorComboBox
            // 
            hairColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            hairColorComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hairColorComboBox.FormattingEnabled = true;
            hairColorComboBox.ItemHeight = 28;
            hairColorComboBox.Items.AddRange(new object[] { "Black", "Brown", "Blonde", "Red", "Gray", "Other" });
            hairColorComboBox.Location = new Point(172, 382);
            hairColorComboBox.Name = "hairColorComboBox";
            hairColorComboBox.Size = new Size(150, 36);
            hairColorComboBox.TabIndex = 13;
            hairColorComboBox.SelectedIndexChanged += hairColorComboBox_SelectedIndexChanged;
            // 
            // eyeColorComboBox
            // 
            eyeColorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            eyeColorComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eyeColorComboBox.FormattingEnabled = true;
            eyeColorComboBox.ItemHeight = 28;
            eyeColorComboBox.Items.AddRange(new object[] { "Brown", "Blue", "Green", "Hazel", "Gray", "Other" });
            eyeColorComboBox.Location = new Point(172, 424);
            eyeColorComboBox.Name = "eyeColorComboBox";
            eyeColorComboBox.Size = new Size(150, 36);
            eyeColorComboBox.TabIndex = 14;
            // 
            // sexLabel
            // 
            sexLabel.AutoSize = true;
            sexLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sexLabel.Location = new Point(29, 223);
            sexLabel.Name = "sexLabel";
            sexLabel.Size = new Size(42, 28);
            sexLabel.TabIndex = 35;
            sexLabel.Text = "Sex";
            sexLabel.Click += sexLabel_Click_1;
            // 
            // dobLabel
            // 
            dobLabel.AutoSize = true;
            dobLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dobLabel.Location = new Point(29, 268);
            dobLabel.Name = "dobLabel";
            dobLabel.Size = new Size(52, 28);
            dobLabel.TabIndex = 36;
            dobLabel.Text = "DOB";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            heightLabel.Location = new Point(29, 305);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new Size(71, 28);
            heightLabel.TabIndex = 37;
            heightLabel.Text = "Height";
            // 
            // weightLabel
            // 
            weightLabel.AutoSize = true;
            weightLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            weightLabel.Location = new Point(29, 345);
            weightLabel.Name = "weightLabel";
            weightLabel.Size = new Size(75, 28);
            weightLabel.TabIndex = 38;
            weightLabel.Text = "Weight";
            // 
            // hairColorLabel
            // 
            hairColorLabel.AutoSize = true;
            hairColorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hairColorLabel.Location = new Point(29, 385);
            hairColorLabel.Name = "hairColorLabel";
            hairColorLabel.Size = new Size(101, 28);
            hairColorLabel.TabIndex = 39;
            hairColorLabel.Text = "Hair Color";
            // 
            // eyeColorLabel
            // 
            eyeColorLabel.AutoSize = true;
            eyeColorLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            eyeColorLabel.Location = new Point(29, 427);
            eyeColorLabel.Name = "eyeColorLabel";
            eyeColorLabel.Size = new Size(95, 28);
            eyeColorLabel.TabIndex = 40;
            eyeColorLabel.Text = "Eye Color";
            // 
            // rankLabel
            // 
            rankLabel.AutoSize = true;
            rankLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rankLabel.Location = new Point(185, 101);
            rankLabel.Name = "rankLabel";
            rankLabel.Size = new Size(55, 28);
            rankLabel.TabIndex = 42;
            rankLabel.Text = "Rank";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusLabel.Location = new Point(29, 101);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(65, 28);
            statusLabel.TabIndex = 43;
            statusLabel.Text = "Status";
            statusLabel.Click += statusLabel_Click;
            // 
            // lastNameLabel
            // 
            lastNameLabel.AutoSize = true;
            lastNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lastNameLabel.Location = new Point(29, 143);
            lastNameLabel.Name = "lastNameLabel";
            lastNameLabel.Size = new Size(103, 28);
            lastNameLabel.TabIndex = 44;
            lastNameLabel.Text = "Last Name";
            lastNameLabel.Click += lastNameLabel_Click;
            // 
            // firstNameLabel
            // 
            firstNameLabel.AutoSize = true;
            firstNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            firstNameLabel.Location = new Point(29, 183);
            firstNameLabel.Name = "firstNameLabel";
            firstNameLabel.Size = new Size(134, 28);
            firstNameLabel.TabIndex = 45;
            firstNameLabel.Text = "First Name MI";
            // 
            // unitLabel
            // 
            unitLabel.AutoSize = true;
            unitLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unitLabel.Location = new Point(29, 466);
            unitLabel.Name = "unitLabel";
            unitLabel.Size = new Size(49, 28);
            unitLabel.TabIndex = 46;
            unitLabel.Text = "Unit";
            unitLabel.Click += unitLabel_Click_1;
            // 
            // catLabel
            // 
            catLabel.AutoSize = true;
            catLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            catLabel.Location = new Point(29, 659);
            catLabel.Name = "catLabel";
            catLabel.Size = new Size(92, 28);
            catLabel.TabIndex = 48;
            catLabel.Text = "Category";
            catLabel.Click += catLabel_Click;
            // 
            // restrictionsBox
            // 
            restrictionsBox.AutoSize = true;
            restrictionsBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restrictionsBox.Location = new Point(29, 547);
            restrictionsBox.Name = "restrictionsBox";
            restrictionsBox.Size = new Size(181, 32);
            restrictionsBox.TabIndex = 17;
            restrictionsBox.Text = "Glasses/Contacts";
            restrictionsBox.UseVisualStyleBackColor = true;
            restrictionsBox.CheckedChanged += restrictionsBox_CheckedChanged;
            // 
            // remarksBox
            // 
            remarksBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            remarksBox.Location = new Point(863, 210);
            remarksBox.Name = "remarksBox";
            remarksBox.Size = new Size(330, 34);
            remarksBox.TabIndex = 27;
            remarksBox.TextChanged += remarksBox_TextChanged;
            // 
            // remarksLabel
            // 
            remarksLabel.AutoSize = true;
            remarksLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            remarksLabel.Location = new Point(768, 213);
            remarksLabel.Name = "remarksLabel";
            remarksLabel.Size = new Size(89, 28);
            remarksLabel.TabIndex = 52;
            remarksLabel.Text = "Remarks:";
            remarksLabel.Click += remarksLabel_Click;
            // 
            // issuerComboBox
            // 
            issuerComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            issuerComboBox.FormattingEnabled = true;
            issuerComboBox.Location = new Point(510, 210);
            issuerComboBox.Name = "issuerComboBox";
            issuerComboBox.Size = new Size(240, 36);
            issuerComboBox.TabIndex = 26;
            issuerComboBox.SelectedIndexChanged += issuerComboBox_SelectedIndexChanged;
            // 
            // issuerLabel
            // 
            issuerLabel.AutoSize = true;
            issuerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            issuerLabel.Location = new Point(367, 210);
            issuerLabel.Name = "issuerLabel";
            issuerLabel.Size = new Size(137, 28);
            issuerLabel.TabIndex = 54;
            issuerLabel.Text = "Issuing Offcial:";
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(29, 21);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(149, 40);
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
            sexComboBox.Location = new Point(172, 220);
            sexComboBox.Name = "sexComboBox";
            sexComboBox.Size = new Size(150, 36);
            sexComboBox.TabIndex = 9;
            // 
            // statusComboBox
            // 
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusComboBox.FormattingEnabled = true;
            statusComboBox.Items.AddRange(new object[] { "AD", "R/G", "CIV", "CTR", "DEP" });
            statusComboBox.Location = new Point(100, 101);
            statusComboBox.Name = "statusComboBox";
            statusComboBox.Size = new Size(65, 33);
            statusComboBox.TabIndex = 5;
            statusComboBox.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // militaryRankComboBox
            // 
            militaryRankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            militaryRankComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            militaryRankComboBox.Items.AddRange(new object[] { "E-1", "E-2", "E-3", "E-4", "E-5", "E-6", "E-7", "E-8", "E-9", "W-1", "W-2", "W-3", "W-4", "W-5", "O-1", "O-2", "O-3", "O-4", "O-5", "O-6", "O-7", "O-8", "O-9", "O-10" });
            militaryRankComboBox.Location = new Point(246, 101);
            militaryRankComboBox.Name = "militaryRankComboBox";
            militaryRankComboBox.Size = new Size(76, 33);
            militaryRankComboBox.TabIndex = 6;
            militaryRankComboBox.Visible = false;
            militaryRankComboBox.SelectedIndexChanged += militaryRankComboBox_SelectedIndexChanged;
            // 
            // civilianRankComboBox
            // 
            civilianRankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            civilianRankComboBox.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            civilianRankComboBox.Items.AddRange(new object[] { "GS-1", "GS-2", "GS-3", "GS-4", "GS-5", "GS-6", "GS-7", "GS-8", "GS-9", "GS-10", "GS-11", "GS-12", "GS-13", "GS-14", "GS-15", "SES" });
            civilianRankComboBox.Location = new Point(246, 101);
            civilianRankComboBox.Name = "civilianRankComboBox";
            civilianRankComboBox.Size = new Size(76, 33);
            civilianRankComboBox.TabIndex = 58;
            civilianRankComboBox.Visible = false;
            civilianRankComboBox.SelectedIndexChanged += civilianRankComboBox_SelectedIndexChanged;
            // 
            // naLabel
            // 
            naLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            naLabel.Location = new Point(251, 101);
            naLabel.Name = "naLabel";
            naLabel.Size = new Size(60, 36);
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
            unitComboBox.Location = new Point(172, 466);
            unitComboBox.Name = "unitComboBox";
            unitComboBox.Size = new Size(150, 33);
            unitComboBox.TabIndex = 15;
            // 
            // btnReset
            // 
            btnReset.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.IndianRed;
            btnReset.Location = new Point(1075, 18);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(118, 40);
            btnReset.TabIndex = 4;
            btnReset.Text = "Clear Form";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(6, 68);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(293, 269);
            pictureBox1.TabIndex = 63;
            pictureBox1.TabStop = false;
            // 
            // picturebutton
            // 
            picturebutton.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            picturebutton.Location = new Point(6, 16);
            picturebutton.Name = "picturebutton";
            picturebutton.Size = new Size(293, 40);
            picturebutton.TabIndex = 29;
            picturebutton.Text = "Take Picture";
            picturebutton.UseVisualStyleBackColor = true;
            picturebutton.Click += picturebutton_Click;
            // 
            // signaturegroupBox
            // 
            signaturegroupBox.Controls.Add(btnRequestSignature);
            signaturegroupBox.Controls.Add(signaturePanel);
            signaturegroupBox.Location = new Point(367, 297);
            signaturegroupBox.Name = "signaturegroupBox";
            signaturegroupBox.Size = new Size(305, 339);
            signaturegroupBox.TabIndex = 65;
            signaturegroupBox.TabStop = false;
            // 
            // picturegroupBox
            // 
            picturegroupBox.Controls.Add(pictureBox1);
            picturegroupBox.Controls.Add(picturebutton);
            picturegroupBox.Location = new Point(888, 297);
            picturegroupBox.Name = "picturegroupBox";
            picturegroupBox.Size = new Size(305, 343);
            picturegroupBox.TabIndex = 66;
            picturegroupBox.TabStop = false;
            // 
            // stampLabel
            // 
            stampLabel.AutoSize = true;
            stampLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stampLabel.Location = new Point(29, 505);
            stampLabel.Name = "stampLabel";
            stampLabel.Size = new Size(68, 28);
            stampLabel.TabIndex = 67;
            stampLabel.Text = "Stamp";
            // 
            // stampComboBox
            // 
            stampComboBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stampComboBox.FormattingEnabled = true;
            stampComboBox.Items.AddRange(new object[] { "", "Student Driver", "On Base Only", "TDY", "Limited" });
            stampComboBox.Location = new Point(173, 505);
            stampComboBox.Name = "stampComboBox";
            stampComboBox.Size = new Size(151, 36);
            stampComboBox.TabIndex = 16;
            // 
            // msfTextBox
            // 
            msfTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            msfTextBox.Location = new Point(129, 623);
            msfTextBox.Name = "msfTextBox";
            msfTextBox.Size = new Size(193, 34);
            msfTextBox.TabIndex = 68;
            // 
            // MSFlabel
            // 
            MSFlabel.AutoSize = true;
            MSFlabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MSFlabel.Location = new Point(29, 626);
            MSFlabel.Name = "MSFlabel";
            MSFlabel.Size = new Size(51, 28);
            MSFlabel.TabIndex = 69;
            MSFlabel.Text = "MSF";
            // 
            // autoJeepCheckBox
            // 
            autoJeepCheckBox.AutoSize = true;
            autoJeepCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            autoJeepCheckBox.Location = new Point(29, 585);
            autoJeepCheckBox.Name = "autoJeepCheckBox";
            autoJeepCheckBox.Size = new Size(124, 32);
            autoJeepCheckBox.TabIndex = 70;
            autoJeepCheckBox.Text = "Auto/Jeep";
            autoJeepCheckBox.UseVisualStyleBackColor = true;
            autoJeepCheckBox.CheckedChanged += autoJeepCheckBox_CheckedChanged;
            // 
            // motorcycleCheckBox
            // 
            motorcycleCheckBox.AutoSize = true;
            motorcycleCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            motorcycleCheckBox.Location = new Point(185, 585);
            motorcycleCheckBox.Name = "motorcycleCheckBox";
            motorcycleCheckBox.Size = new Size(133, 32);
            motorcycleCheckBox.TabIndex = 71;
            motorcycleCheckBox.Text = "Motorcycle";
            motorcycleCheckBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.InactiveBorder;
            label2.Location = new Point(1060, 687);
            label2.Name = "label2";
            label2.Size = new Size(162, 20);
            label2.TabIndex = 72;
            label2.Text = "Powered by Project Arc";
            label2.Click += label2_Click_3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1223, 706);
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
    }
}