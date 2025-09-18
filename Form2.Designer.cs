namespace BlockGo_ControlPanel
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnFirst = new Button();
            btnSecond = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label1.Location = new Point(45, 45);
            label1.Name = "label1";
            label1.Size = new Size(243, 29);
            label1.TabIndex = 0;
            label1.Text = "玩家要先手還是要後手";
            // 
            // btnFirst
            // 
            btnFirst.Font = new Font("Microsoft JhengHei UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnFirst.Location = new Point(12, 114);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(152, 66);
            btnFirst.TabIndex = 1;
            btnFirst.Text = "先手";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += btnFirst_Click;
            // 
            // btnSecond
            // 
            btnSecond.Font = new Font("Microsoft JhengHei UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnSecond.Location = new Point(170, 114);
            btnSecond.Name = "btnSecond";
            btnSecond.Size = new Size(152, 66);
            btnSecond.TabIndex = 2;
            btnSecond.Text = "後手";
            btnSecond.UseVisualStyleBackColor = true;
            btnSecond.Click += btnSecond_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 192);
            Controls.Add(btnSecond);
            Controls.Add(btnFirst);
            Controls.Add(label1);
            Name = "Form2";
            Text = "先手決定";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnFirst;
        private Button btnSecond;
    }
}