namespace Pexeso
{
    partial class DifficultyForm
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
            this.difficultyLabel = new System.Windows.Forms.Label();
            this.difficultyComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // difficultyLabel
            // 
            this.difficultyLabel.AutoSize = true;
            this.difficultyLabel.Location = new System.Drawing.Point(75, 10);
            this.difficultyLabel.Name = "difficultyLabel";
            this.difficultyLabel.Size = new System.Drawing.Size(119, 20);
            this.difficultyLabel.TabIndex = 0;
            this.difficultyLabel.Text = "Select difficulty:";
            // 
            // difficultyComboBox
            // 
            this.difficultyComboBox.FormattingEnabled = true;
            this.difficultyComboBox.Items.AddRange(new object[] {
            "Easy",
            "Hard"});
            this.difficultyComboBox.Location = new System.Drawing.Point(16, 33);
            this.difficultyComboBox.Name = "difficultyComboBox";
            this.difficultyComboBox.Size = new System.Drawing.Size(239, 28);
            this.difficultyComboBox.TabIndex = 1;
            // 
            // ChooseButton
            // 
            this.ChooseButton.Location = new System.Drawing.Point(13, 68);
            this.ChooseButton.Name = "ChooseButton";
            this.ChooseButton.Size = new System.Drawing.Size(118, 55);
            this.ChooseButton.TabIndex = 2;
            this.ChooseButton.Text = "Choose";
            this.ChooseButton.UseVisualStyleBackColor = true;
            this.ChooseButton.Click += new System.EventHandler(this.ChooseButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(137, 67);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(118, 55);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // DifficultyForm
            // 
            this.AcceptButton = this.ChooseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 134);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ChooseButton);
            this.Controls.Add(this.difficultyComboBox);
            this.Controls.Add(this.difficultyLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DifficultyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.DifficultyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label difficultyLabel;
        private System.Windows.Forms.ComboBox difficultyComboBox;
        private System.Windows.Forms.Button ChooseButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}