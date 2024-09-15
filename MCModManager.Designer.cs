namespace MinecraftModManager
{
    partial class MinecraftModManager
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            ModGroupsGouping = new GroupBox();
            NewGroupNameTextbox = new TextBox();
            DeleteSelectedModGroup = new Button();
            AddNewModGroup = new Button();
            ModGroupItembox = new ListBox();
            label4 = new Label();
            OpenModPath = new Button();
            OpenResourcePath = new Button();
            ModsGroupBox = new GroupBox();
            button8 = new Button();
            button7 = new Button();
            ModCheckList = new CheckedListBox();
            button5 = new Button();
            button6 = new Button();
            label5 = new Label();
            ModGroupsGouping.SuspendLayout();
            ModsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(255, 128, 0);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Consolas", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(710, 37);
            label1.TabIndex = 0;
            label1.Text = "Mincraft Mod Manager";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(136, 52);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = ".../Path/...";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(489, 22);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(136, 79);
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = ".../Path/...";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(489, 22);
            textBox2.TabIndex = 2;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.Location = new Point(12, 52);
            label2.Name = "label2";
            label2.Size = new Size(118, 21);
            label2.TabIndex = 3;
            label2.Text = "🗁Mod folder:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(12, 79);
            label3.Name = "label3";
            label3.Size = new Size(118, 21);
            label3.TabIndex = 4;
            label3.Text = "🗁Resource root:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ModGroupsGouping
            // 
            ModGroupsGouping.Controls.Add(NewGroupNameTextbox);
            ModGroupsGouping.Controls.Add(DeleteSelectedModGroup);
            ModGroupsGouping.Controls.Add(AddNewModGroup);
            ModGroupsGouping.Controls.Add(ModGroupItembox);
            ModGroupsGouping.Controls.Add(label4);
            ModGroupsGouping.Location = new Point(12, 106);
            ModGroupsGouping.Name = "ModGroupsGouping";
            ModGroupsGouping.Size = new Size(339, 406);
            ModGroupsGouping.TabIndex = 5;
            ModGroupsGouping.TabStop = false;
            ModGroupsGouping.Text = "Mod Groups";
            // 
            // NewGroupNameTextbox
            // 
            NewGroupNameTextbox.Location = new Point(6, 349);
            NewGroupNameTextbox.Name = "NewGroupNameTextbox";
            NewGroupNameTextbox.PlaceholderText = "New group name";
            NewGroupNameTextbox.Size = new Size(327, 22);
            NewGroupNameTextbox.TabIndex = 4;
            // 
            // DeleteSelectedModGroup
            // 
            DeleteSelectedModGroup.FlatAppearance.BorderColor = Color.Red;
            DeleteSelectedModGroup.Location = new Point(181, 377);
            DeleteSelectedModGroup.Name = "DeleteSelectedModGroup";
            DeleteSelectedModGroup.Size = new Size(152, 21);
            DeleteSelectedModGroup.TabIndex = 3;
            DeleteSelectedModGroup.Text = "Delete selected🗑️";
            DeleteSelectedModGroup.UseVisualStyleBackColor = true;
            DeleteSelectedModGroup.Click += DeleteSelectedModGroup_Click;
            // 
            // AddNewModGroup
            // 
            AddNewModGroup.Location = new Point(6, 377);
            AddNewModGroup.Name = "AddNewModGroup";
            AddNewModGroup.Size = new Size(152, 21);
            AddNewModGroup.TabIndex = 2;
            AddNewModGroup.Text = "➕Add new group";
            AddNewModGroup.UseVisualStyleBackColor = true;
            AddNewModGroup.Click += AddNewModGroup_Click;
            // 
            // ModGroupItembox
            // 
            ModGroupItembox.FormattingEnabled = true;
            ModGroupItembox.ItemHeight = 14;
            ModGroupItembox.Location = new Point(6, 51);
            ModGroupItembox.Name = "ModGroupItembox";
            ModGroupItembox.Size = new Size(327, 256);
            ModGroupItembox.TabIndex = 1;
            ModGroupItembox.SelectedIndexChanged += ModGroupItembox_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.Location = new Point(6, 27);
            label4.Name = "label4";
            label4.Size = new Size(327, 21);
            label4.TabIndex = 0;
            label4.Text = "Available mod groups";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // OpenModPath
            // 
            OpenModPath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpenModPath.Location = new Point(631, 51);
            OpenModPath.Name = "OpenModPath";
            OpenModPath.Size = new Size(67, 21);
            OpenModPath.TabIndex = 4;
            OpenModPath.Text = "...";
            OpenModPath.UseVisualStyleBackColor = true;
            OpenModPath.Click += button3_Click;
            // 
            // OpenResourcePath
            // 
            OpenResourcePath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpenResourcePath.Location = new Point(631, 78);
            OpenResourcePath.Name = "OpenResourcePath";
            OpenResourcePath.Size = new Size(67, 21);
            OpenResourcePath.TabIndex = 6;
            OpenResourcePath.Text = "...";
            OpenResourcePath.UseVisualStyleBackColor = true;
            OpenResourcePath.Click += OpenResourcePath_Click;
            // 
            // ModsGroupBox
            // 
            ModsGroupBox.Controls.Add(button8);
            ModsGroupBox.Controls.Add(button7);
            ModsGroupBox.Controls.Add(ModCheckList);
            ModsGroupBox.Controls.Add(button5);
            ModsGroupBox.Controls.Add(button6);
            ModsGroupBox.Controls.Add(label5);
            ModsGroupBox.Enabled = false;
            ModsGroupBox.Location = new Point(357, 106);
            ModsGroupBox.Name = "ModsGroupBox";
            ModsGroupBox.Size = new Size(339, 406);
            ModsGroupBox.TabIndex = 6;
            ModsGroupBox.TabStop = false;
            ModsGroupBox.Text = "Mods";
            // 
            // button8
            // 
            button8.Location = new Point(6, 377);
            button8.Name = "button8";
            button8.Size = new Size(327, 21);
            button8.TabIndex = 8;
            button8.Text = "Unload mods";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(6, 350);
            button7.Name = "button7";
            button7.Size = new Size(327, 21);
            button7.TabIndex = 7;
            button7.Text = "Load mods";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // ModCheckList
            // 
            ModCheckList.FormattingEnabled = true;
            ModCheckList.Location = new Point(6, 51);
            ModCheckList.Name = "ModCheckList";
            ModCheckList.Size = new Size(327, 259);
            ModCheckList.TabIndex = 4;
            // 
            // button5
            // 
            button5.FlatAppearance.BorderColor = Color.Red;
            button5.Location = new Point(181, 323);
            button5.Name = "button5";
            button5.Size = new Size(152, 21);
            button5.TabIndex = 3;
            button5.Text = "Delete selected🗑️";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(6, 323);
            button6.Name = "button6";
            button6.Size = new Size(152, 21);
            button6.TabIndex = 2;
            button6.Text = "➕Add new mod";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.Location = new Point(6, 27);
            label5.Name = "label5";
            label5.Size = new Size(327, 21);
            label5.TabIndex = 0;
            label5.Text = "Available mods in groups";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MinecraftModManager
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(710, 524);
            Controls.Add(ModsGroupBox);
            Controls.Add(OpenResourcePath);
            Controls.Add(OpenModPath);
            Controls.Add(ModGroupsGouping);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MaximizeBox = false;
            MaximumSize = new Size(726, 563);
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            MinimumSize = new Size(726, 563);
            Name = "MinecraftModManager";
            Text = "MinecraftModManager";
            Load += Form1_Load;
            ModGroupsGouping.ResumeLayout(false);
            ModGroupsGouping.PerformLayout();
            ModsGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;
        private GroupBox ModGroupsGouping;
        private Button DeleteSelectedModGroup;
        private Button AddNewModGroup;
        private ListBox ModGroupItembox;
        private Label label4;
        private Button OpenModPath;
        private Button OpenResourcePath;
        private GroupBox ModsGroupBox;
        private CheckedListBox ModCheckList;
        private Button button5;
        private Button button6;
        private Label label5;
        private Button button8;
        private Button button7;
        private TextBox NewGroupNameTextbox;
    }
}
