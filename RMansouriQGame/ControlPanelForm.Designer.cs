
namespace RMansouriQGame
{
    partial class ControlPanelForm
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
            this.btnDesingForm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPlayForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDesingForm
            // 
            this.btnDesingForm.Location = new System.Drawing.Point(82, 79);
            this.btnDesingForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnDesingForm.Name = "btnDesingForm";
            this.btnDesingForm.Size = new System.Drawing.Size(212, 119);
            this.btnDesingForm.TabIndex = 0;
            this.btnDesingForm.Text = "Design";
            this.btnDesingForm.UseVisualStyleBackColor = true;
            this.btnDesingForm.Click += new System.EventHandler(this.btnDesingForm_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(524, 79);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(212, 119);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPlayForm
            // 
            this.btnPlayForm.Location = new System.Drawing.Point(303, 79);
            this.btnPlayForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlayForm.Name = "btnPlayForm";
            this.btnPlayForm.Size = new System.Drawing.Size(212, 119);
            this.btnPlayForm.TabIndex = 2;
            this.btnPlayForm.Text = "Play";
            this.btnPlayForm.UseVisualStyleBackColor = true;
            this.btnPlayForm.Click += new System.EventHandler(this.btnPlayForm_Click);
            // 
            // ControlPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 271);
            this.Controls.Add(this.btnPlayForm);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDesingForm);
            this.Name = "ControlPanelForm";
            this.Text = "QGame Control Panel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDesingForm;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPlayForm;
    }
}

