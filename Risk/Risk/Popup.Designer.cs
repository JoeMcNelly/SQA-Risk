namespace Risk
{
    partial class Popup
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
            this.label1 = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.playerOneName = new System.Windows.Forms.TextBox();
            this.playerTwoName = new System.Windows.Forms.TextBox();
            this.playerThreeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.playerFourName = new System.Windows.Forms.TextBox();
            this.playerOneColor = new System.Windows.Forms.Button();
            this.playerTwoColor = new System.Windows.Forms.Button();
            this.playerThreeColor = new System.Windows.Forms.Button();
            this.playerFourColor = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.playerSixColor = new System.Windows.Forms.Button();
            this.playerFiveColor = new System.Windows.Forms.Button();
            this.playerSixName = new System.Windows.Forms.TextBox();
            this.playerFiveName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Players:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(283, 227);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 2;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.Next);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Risk.Properties.Resources.risk_usa;
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 242);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBox1.Location = new System.Drawing.Point(176, 25);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // playerOneName
            // 
            this.playerOneName.Location = new System.Drawing.Point(176, 78);
            this.playerOneName.Margin = new System.Windows.Forms.Padding(2);
            this.playerOneName.Name = "playerOneName";
            this.playerOneName.Size = new System.Drawing.Size(76, 20);
            this.playerOneName.TabIndex = 5;
            this.playerOneName.Visible = false;
            // 
            // playerTwoName
            // 
            this.playerTwoName.Location = new System.Drawing.Point(176, 101);
            this.playerTwoName.Margin = new System.Windows.Forms.Padding(2);
            this.playerTwoName.Name = "playerTwoName";
            this.playerTwoName.Size = new System.Drawing.Size(76, 20);
            this.playerTwoName.TabIndex = 6;
            this.playerTwoName.Visible = false;
            // 
            // playerThreeName
            // 
            this.playerThreeName.Location = new System.Drawing.Point(176, 124);
            this.playerThreeName.Margin = new System.Windows.Forms.Padding(2);
            this.playerThreeName.Name = "playerThreeName";
            this.playerThreeName.Size = new System.Drawing.Size(76, 20);
            this.playerThreeName.TabIndex = 7;
            this.playerThreeName.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Player Names";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Color";
            this.label3.Visible = false;
            // 
            // playerFourName
            // 
            this.playerFourName.Location = new System.Drawing.Point(176, 147);
            this.playerFourName.Margin = new System.Windows.Forms.Padding(2);
            this.playerFourName.Name = "playerFourName";
            this.playerFourName.Size = new System.Drawing.Size(76, 20);
            this.playerFourName.TabIndex = 10;
            this.playerFourName.Visible = false;
            // 
            // playerOneColor
            // 
            this.playerOneColor.BackColor = System.Drawing.SystemColors.Control;
            this.playerOneColor.Location = new System.Drawing.Point(249, 78);
            this.playerOneColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerOneColor.Name = "playerOneColor";
            this.playerOneColor.Size = new System.Drawing.Size(21, 19);
            this.playerOneColor.TabIndex = 11;
            this.playerOneColor.UseVisualStyleBackColor = false;
            this.playerOneColor.Visible = false;
            this.playerOneColor.Click += new System.EventHandler(this.playerOneChooseColor);
            // 
            // playerTwoColor
            // 
            this.playerTwoColor.Location = new System.Drawing.Point(249, 102);
            this.playerTwoColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerTwoColor.Name = "playerTwoColor";
            this.playerTwoColor.Size = new System.Drawing.Size(21, 19);
            this.playerTwoColor.TabIndex = 12;
            this.playerTwoColor.UseVisualStyleBackColor = true;
            this.playerTwoColor.Visible = false;
            this.playerTwoColor.Click += new System.EventHandler(this.playerTwoChooseColor);
            // 
            // playerThreeColor
            // 
            this.playerThreeColor.Location = new System.Drawing.Point(249, 125);
            this.playerThreeColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerThreeColor.Name = "playerThreeColor";
            this.playerThreeColor.Size = new System.Drawing.Size(21, 19);
            this.playerThreeColor.TabIndex = 13;
            this.playerThreeColor.UseVisualStyleBackColor = true;
            this.playerThreeColor.Visible = false;
            this.playerThreeColor.Click += new System.EventHandler(this.playerThreeChooseColor);
            // 
            // playerFourColor
            // 
            this.playerFourColor.Location = new System.Drawing.Point(249, 147);
            this.playerFourColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerFourColor.Name = "playerFourColor";
            this.playerFourColor.Size = new System.Drawing.Size(21, 19);
            this.playerFourColor.TabIndex = 14;
            this.playerFourColor.UseVisualStyleBackColor = true;
            this.playerFourColor.Visible = false;
            this.playerFourColor.Click += new System.EventHandler(this.playerFourChooseColor);
            // 
            // back
            // 
            this.back.Enabled = false;
            this.back.Location = new System.Drawing.Point(173, 227);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 15;
            this.back.Text = "Back";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.ClickBack);
            // 
            // playerSixColor
            // 
            this.playerSixColor.Location = new System.Drawing.Point(249, 194);
            this.playerSixColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerSixColor.Name = "playerSixColor";
            this.playerSixColor.Size = new System.Drawing.Size(21, 19);
            this.playerSixColor.TabIndex = 19;
            this.playerSixColor.UseVisualStyleBackColor = true;
            this.playerSixColor.Visible = false;
            this.playerSixColor.Click += new System.EventHandler(this.playerSixChooseColor);
            // 
            // playerFiveColor
            // 
            this.playerFiveColor.Location = new System.Drawing.Point(249, 172);
            this.playerFiveColor.Margin = new System.Windows.Forms.Padding(2);
            this.playerFiveColor.Name = "playerFiveColor";
            this.playerFiveColor.Size = new System.Drawing.Size(21, 19);
            this.playerFiveColor.TabIndex = 18;
            this.playerFiveColor.UseVisualStyleBackColor = true;
            this.playerFiveColor.Visible = false;
            this.playerFiveColor.Click += new System.EventHandler(this.playerFiveChooseColor);
            // 
            // playerSixName
            // 
            this.playerSixName.Location = new System.Drawing.Point(176, 194);
            this.playerSixName.Margin = new System.Windows.Forms.Padding(2);
            this.playerSixName.Name = "playerSixName";
            this.playerSixName.Size = new System.Drawing.Size(76, 20);
            this.playerSixName.TabIndex = 17;
            this.playerSixName.Visible = false;
            // 
            // playerFiveName
            // 
            this.playerFiveName.Location = new System.Drawing.Point(176, 171);
            this.playerFiveName.Margin = new System.Windows.Forms.Padding(2);
            this.playerFiveName.Name = "playerFiveName";
            this.playerFiveName.Size = new System.Drawing.Size(76, 20);
            this.playerFiveName.TabIndex = 16;
            this.playerFiveName.Visible = false;
            // 
            // Popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 262);
            this.ControlBox = false;
            this.Controls.Add(this.playerSixColor);
            this.Controls.Add(this.playerFiveColor);
            this.Controls.Add(this.playerSixName);
            this.Controls.Add(this.playerFiveName);
            this.Controls.Add(this.back);
            this.Controls.Add(this.playerFourColor);
            this.Controls.Add(this.playerThreeColor);
            this.Controls.Add(this.playerTwoColor);
            this.Controls.Add(this.playerOneColor);
            this.Controls.Add(this.playerFourName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playerThreeName);
            this.Controls.Add(this.playerTwoName);
            this.Controls.Add(this.playerOneName);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.label1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Popup";
            this.Text = "Select the number of players (2-6)";
            this.Load += new System.EventHandler(this.Popup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox playerOneName;
        private System.Windows.Forms.TextBox playerTwoName;
        private System.Windows.Forms.TextBox playerThreeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox playerFourName;
        private System.Windows.Forms.Button playerOneColor;
        private System.Windows.Forms.Button playerTwoColor;
        private System.Windows.Forms.Button playerThreeColor;
        private System.Windows.Forms.Button playerFourColor;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button playerSixColor;
        private System.Windows.Forms.Button playerFiveColor;
        private System.Windows.Forms.TextBox playerSixName;
        private System.Windows.Forms.TextBox playerFiveName;
    }
}