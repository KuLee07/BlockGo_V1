namespace BlockGo_ControlPanel
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            GameModeSelect = new ComboBox();
            btnOK = new Button();
            txtMsg = new RichTextBox();
            btnConfirm = new Button();
            btnCancel = new Button();
            PlayerShapeBox = new ComboBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            txtMCTStime = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // GameModeSelect
            // 
            GameModeSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            GameModeSelect.Font = new Font("Microsoft JhengHei UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 136);
            GameModeSelect.FormattingEnabled = true;
            GameModeSelect.Items.AddRange(new object[] { "MCTS VS MCTS", "玩家 VS MCTS" });
            GameModeSelect.Location = new Point(625, 552);
            GameModeSelect.Name = "GameModeSelect";
            GameModeSelect.Size = new Size(283, 50);
            GameModeSelect.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(914, 552);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(63, 50);
            btnOK.TabIndex = 1;
            btnOK.Text = "GO";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // txtMsg
            // 
            txtMsg.Location = new Point(625, 426);
            txtMsg.Name = "txtMsg";
            txtMsg.ReadOnly = true;
            txtMsg.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtMsg.Size = new Size(283, 120);
            txtMsg.TabIndex = 2;
            txtMsg.Text = "*系統訊息*";
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnConfirm.Location = new Point(775, 370);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(133, 50);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "確定落子";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnCancel.Location = new Point(625, 370);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(133, 50);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "清除落子";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PlayerShapeBox
            // 
            PlayerShapeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PlayerShapeBox.FormattingEnabled = true;
            PlayerShapeBox.Location = new Point(849, 337);
            PlayerShapeBox.Name = "PlayerShapeBox";
            PlayerShapeBox.Size = new Size(59, 27);
            PlayerShapeBox.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(775, 340);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 6;
            label1.Text = "棋型代號";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(625, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(345, 272);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(625, 326);
            label2.Name = "label2";
            label2.Size = new Size(50, 38);
            label2.TabIndex = 8;
            label2.Text = "MCTS\r\n次數";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtMCTStime
            // 
            txtMCTStime.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 136);
            txtMCTStime.Location = new Point(681, 326);
            txtMCTStime.MaxLength = 10000;
            txtMCTStime.Name = "txtMCTStime";
            txtMCTStime.Size = new Size(77, 37);
            txtMCTStime.TabIndex = 9;
            txtMCTStime.Text = "500";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(982, 612);
            Controls.Add(txtMCTStime);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(PlayerShapeBox);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(txtMsg);
            Controls.Add(btnOK);
            Controls.Add(GameModeSelect);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox GameModeSelect;
        private Button btnOK;
        private RichTextBox txtMsg;
        private Button btnConfirm;
        private Button btnCancel;
        private ComboBox PlayerShapeBox;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox txtMCTStime;
    }
}
