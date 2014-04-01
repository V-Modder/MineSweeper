namespace MineSweeper
{
    partial class EndForm
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
            this.lbl_won = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_retry = new System.Windows.Forms.Button();
            this.lbl_lost = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_won
            // 
            this.lbl_won.AutoSize = true;
            this.lbl_won.Location = new System.Drawing.Point(13, 13);
            this.lbl_won.Name = "lbl_won";
            this.lbl_won.Size = new System.Drawing.Size(235, 13);
            this.lbl_won.TabIndex = 0;
            this.lbl_won.Text = "Herzlichen Glückwunsch! Sie haben gewonnen!";
            this.lbl_won.Visible = false;
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(30, 94);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 1;
            this.btn_exit.Text = "Beenden";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_retry
            // 
            this.btn_retry.Location = new System.Drawing.Point(140, 94);
            this.btn_retry.Name = "btn_retry";
            this.btn_retry.Size = new System.Drawing.Size(75, 23);
            this.btn_retry.TabIndex = 2;
            this.btn_retry.Text = "Neues Spiel";
            this.btn_retry.UseVisualStyleBackColor = true;
            this.btn_retry.Click += new System.EventHandler(this.btn_retry_Click);
            // 
            // lbl_lost
            // 
            this.lbl_lost.AutoSize = true;
            this.lbl_lost.Location = new System.Drawing.Point(79, 13);
            this.lbl_lost.Name = "lbl_lost";
            this.lbl_lost.Size = new System.Drawing.Size(81, 13);
            this.lbl_lost.TabIndex = 3;
            this.lbl_lost.Text = "Leider Verloren!";
            this.lbl_lost.Visible = false;
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(13, 52);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(34, 13);
            this.lbl_time.TabIndex = 4;
            this.lbl_time.Text = "Zeit:  ";
            // 
            // EndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 161);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.lbl_lost);
            this.Controls.Add(this.btn_retry);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.lbl_won);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EndForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "EndForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_won;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_retry;
        private System.Windows.Forms.Label lbl_lost;
        private System.Windows.Forms.Label lbl_time;
    }
}