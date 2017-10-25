namespace TankBattle
{
    partial class GameplayForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
			this.displayPanel = new System.Windows.Forms.Panel();
			this.controlPanel = new System.Windows.Forms.Panel();
			this.powerLevelLabel = new System.Windows.Forms.Label();
			this.powerTrackBar = new System.Windows.Forms.TrackBar();
			this.fireButton = new System.Windows.Forms.Button();
			this.playerLabel = new System.Windows.Forms.Label();
			this.powerLabel = new System.Windows.Forms.Label();
			this.windLabel = new System.Windows.Forms.Label();
			this.angleSetter = new System.Windows.Forms.NumericUpDown();
			this.windStatusLabel = new System.Windows.Forms.Label();
			this.weaponComboBox = new System.Windows.Forms.ComboBox();
			this.weaponLabel = new System.Windows.Forms.Label();
			this.angleLabel = new System.Windows.Forms.Label();
			this.formTimer = new System.Windows.Forms.Timer(this.components);
			this.controlPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.powerTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.angleSetter)).BeginInit();
			this.SuspendLayout();
			// 
			// displayPanel
			// 
			this.displayPanel.Location = new System.Drawing.Point(0, 32);
			this.displayPanel.Name = "displayPanel";
			this.displayPanel.Size = new System.Drawing.Size(800, 600);
			this.displayPanel.TabIndex = 0;
			this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
			// 
			// controlPanel
			// 
			this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
			this.controlPanel.Controls.Add(this.powerLevelLabel);
			this.controlPanel.Controls.Add(this.powerTrackBar);
			this.controlPanel.Controls.Add(this.fireButton);
			this.controlPanel.Controls.Add(this.playerLabel);
			this.controlPanel.Controls.Add(this.powerLabel);
			this.controlPanel.Controls.Add(this.windLabel);
			this.controlPanel.Controls.Add(this.angleSetter);
			this.controlPanel.Controls.Add(this.windStatusLabel);
			this.controlPanel.Controls.Add(this.weaponComboBox);
			this.controlPanel.Controls.Add(this.weaponLabel);
			this.controlPanel.Controls.Add(this.angleLabel);
			this.controlPanel.Location = new System.Drawing.Point(0, 0);
			this.controlPanel.Name = "controlPanel";
			this.controlPanel.Size = new System.Drawing.Size(800, 32);
			this.controlPanel.TabIndex = 1;
			// 
			// powerLevelLabel
			// 
			this.powerLevelLabel.AutoSize = true;
			this.powerLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.powerLevelLabel.Location = new System.Drawing.Point(648, 7);
			this.powerLevelLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.powerLevelLabel.Name = "powerLevelLabel";
			this.powerLevelLabel.Size = new System.Drawing.Size(128, 20);
			this.powerLevelLabel.TabIndex = 6;
			this.powerLevelLabel.Text = "powerLevelLabel";
			this.powerLevelLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// powerTrackBar
			// 
			this.powerTrackBar.LargeChange = 10;
			this.powerTrackBar.Location = new System.Drawing.Point(548, 6);
			this.powerTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.powerTrackBar.Maximum = 100;
			this.powerTrackBar.Minimum = 5;
			this.powerTrackBar.Name = "powerTrackBar";
			this.powerTrackBar.Size = new System.Drawing.Size(104, 45);
			this.powerTrackBar.TabIndex = 9;
			this.powerTrackBar.Value = 5;
			// 
			// fireButton
			// 
			this.fireButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fireButton.Location = new System.Drawing.Point(724, 3);
			this.fireButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.fireButton.Name = "fireButton";
			this.fireButton.Size = new System.Drawing.Size(70, 26);
			this.fireButton.TabIndex = 10;
			this.fireButton.Text = "Fire!";
			this.fireButton.UseVisualStyleBackColor = true;
			// 
			// playerLabel
			// 
			this.playerLabel.AutoSize = true;
			this.playerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.playerLabel.Location = new System.Drawing.Point(2, 3);
			this.playerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.playerLabel.Name = "playerLabel";
			this.playerLabel.Size = new System.Drawing.Size(135, 26);
			this.playerLabel.TabIndex = 0;
			this.playerLabel.Text = "playerLabel";
			// 
			// powerLabel
			// 
			this.powerLabel.AutoSize = true;
			this.powerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.powerLabel.Location = new System.Drawing.Point(494, 8);
			this.powerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.powerLabel.Name = "powerLabel";
			this.powerLabel.Size = new System.Drawing.Size(57, 20);
			this.powerLabel.TabIndex = 5;
			this.powerLabel.Text = "Power:";
			// 
			// windLabel
			// 
			this.windLabel.AutoSize = true;
			this.windLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.windLabel.Location = new System.Drawing.Point(151, 3);
			this.windLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.windLabel.Name = "windLabel";
			this.windLabel.Size = new System.Drawing.Size(36, 13);
			this.windLabel.TabIndex = 1;
			this.windLabel.Text = "Wind";
			// 
			// angleSetter
			// 
			this.angleSetter.Location = new System.Drawing.Point(449, 9);
			this.angleSetter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.angleSetter.Name = "angleSetter";
			this.angleSetter.Size = new System.Drawing.Size(35, 20);
			this.angleSetter.TabIndex = 8;
			// 
			// windStatusLabel
			// 
			this.windStatusLabel.AutoSize = true;
			this.windStatusLabel.Location = new System.Drawing.Point(128, 16);
			this.windStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.windStatusLabel.Name = "windStatusLabel";
			this.windStatusLabel.Size = new System.Drawing.Size(85, 13);
			this.windStatusLabel.TabIndex = 2;
			this.windStatusLabel.Text = "windStatusLabel";
			// 
			// weaponComboBox
			// 
			this.weaponComboBox.FormattingEnabled = true;
			this.weaponComboBox.Location = new System.Drawing.Point(288, 9);
			this.weaponComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.weaponComboBox.Name = "weaponComboBox";
			this.weaponComboBox.Size = new System.Drawing.Size(103, 21);
			this.weaponComboBox.TabIndex = 7;
			// 
			// weaponLabel
			// 
			this.weaponLabel.AutoSize = true;
			this.weaponLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.weaponLabel.Location = new System.Drawing.Point(214, 8);
			this.weaponLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.weaponLabel.Name = "weaponLabel";
			this.weaponLabel.Size = new System.Drawing.Size(73, 20);
			this.weaponLabel.TabIndex = 3;
			this.weaponLabel.Text = "Weapon:";
			// 
			// angleLabel
			// 
			this.angleLabel.AutoSize = true;
			this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.angleLabel.Location = new System.Drawing.Point(394, 7);
			this.angleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.angleLabel.Name = "angleLabel";
			this.angleLabel.Size = new System.Drawing.Size(54, 20);
			this.angleLabel.TabIndex = 4;
			this.angleLabel.Text = "Angle:";
			// 
			// formTimer
			// 
			this.formTimer.Interval = 20;
			// 
			// GameplayForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 629);
			this.Controls.Add(this.controlPanel);
			this.Controls.Add(this.displayPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "GameplayForm";
			this.Text = "Form1";
			this.controlPanel.ResumeLayout(false);
			this.controlPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.powerTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.angleSetter)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label powerLevelLabel;
        private System.Windows.Forms.TrackBar powerTrackBar;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.NumericUpDown angleSetter;
        private System.Windows.Forms.Label windStatusLabel;
        private System.Windows.Forms.ComboBox weaponComboBox;
        private System.Windows.Forms.Label weaponLabel;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.Timer formTimer;
    }
}

