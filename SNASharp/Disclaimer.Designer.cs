namespace SNASharp
{
    partial class DisclaimerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisclaimerForm));
            this.OKbutton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.CheckBoxRemoveDisclaimer = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OKbutton
            // 
            this.OKbutton.BackColor = System.Drawing.Color.LightGreen;
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(289, 361);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(239, 49);
            this.OKbutton.TabIndex = 1;
            this.OKbutton.Text = "that\'s cool i fully agree";
            this.OKbutton.UseVisualStyleBackColor = false;
            // 
            // NoButton
            // 
            this.NoButton.BackColor = System.Drawing.Color.LightCoral;
            this.NoButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.NoButton.Location = new System.Drawing.Point(25, 361);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(236, 49);
            this.NoButton.TabIndex = 2;
            this.NoButton.Text = "No, i am so disturbed reading thats words, i prefer leave";
            this.NoButton.UseVisualStyleBackColor = false;
            // 
            // CheckBoxRemoveDisclaimer
            // 
            this.CheckBoxRemoveDisclaimer.AutoSize = true;
            this.CheckBoxRemoveDisclaimer.Location = new System.Drawing.Point(289, 416);
            this.CheckBoxRemoveDisclaimer.Name = "CheckBoxRemoveDisclaimer";
            this.CheckBoxRemoveDisclaimer.Size = new System.Drawing.Size(180, 17);
            this.CheckBoxRemoveDisclaimer.TabIndex = 3;
            this.CheckBoxRemoveDisclaimer.Text = "Please don\'t ask me in the future";
            this.CheckBoxRemoveDisclaimer.UseVisualStyleBackColor = true;
            this.CheckBoxRemoveDisclaimer.CheckedChanged += new System.EventHandler(this.CheckBoxRemoveDisclaimer_CheckedChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(25, 9);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(503, 346);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // DisclaimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 443);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.CheckBoxRemoveDisclaimer);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.OKbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DisclaimerForm";
            this.ShowIcon = false;
            this.Text = "SNA Sharp disclaimer";
            this.Leave += new System.EventHandler(this.DisclaimerForm_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.CheckBox CheckBoxRemoveDisclaimer;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}