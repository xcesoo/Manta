using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FOptions
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        userLabel = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        deliveryPointIdTextBox = new System.Windows.Forms.TextBox();
        deliveryPoinIdLabel = new System.Windows.Forms.Label();
        userTextBox = new System.Windows.Forms.TextBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // userLabel
        // 
        userLabel.Dock = System.Windows.Forms.DockStyle.Top;
        userLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        userLabel.Location = new System.Drawing.Point(0, 0);
        userLabel.Name = "userLabel";
        userLabel.Size = new System.Drawing.Size(224, 19);
        userLabel.TabIndex = 0;
        userLabel.Text = "User:";
        // 
        // panel1
        // 
        panel1.Controls.Add(deliveryPointIdTextBox);
        panel1.Controls.Add(deliveryPoinIdLabel);
        panel1.Controls.Add(userTextBox);
        panel1.Controls.Add(userLabel);
        panel1.Location = new System.Drawing.Point(12, 12);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(224, 98);
        panel1.TabIndex = 1;
        // 
        // deliveryPointIdTextBox
        // 
        deliveryPointIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        deliveryPointIdTextBox.Dock = System.Windows.Forms.DockStyle.Top;
        deliveryPointIdTextBox.Font = new System.Drawing.Font("Arial", 9.75F);
        deliveryPointIdTextBox.Location = new System.Drawing.Point(0, 60);
        deliveryPointIdTextBox.Name = "deliveryPointIdTextBox";
        deliveryPointIdTextBox.Size = new System.Drawing.Size(224, 22);
        deliveryPointIdTextBox.TabIndex = 3;
        deliveryPointIdTextBox.KeyDown += deliveryPointIdTextBox_KeyDown;
        // 
        // deliveryPoinIdLabel
        // 
        deliveryPoinIdLabel.Dock = System.Windows.Forms.DockStyle.Top;
        deliveryPoinIdLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        deliveryPoinIdLabel.Location = new System.Drawing.Point(0, 41);
        deliveryPoinIdLabel.Name = "deliveryPoinIdLabel";
        deliveryPoinIdLabel.Size = new System.Drawing.Size(224, 19);
        deliveryPoinIdLabel.TabIndex = 2;
        deliveryPoinIdLabel.Text = "Delivery Point Id:";
        // 
        // userTextBox
        // 
        userTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        userTextBox.Dock = System.Windows.Forms.DockStyle.Top;
        userTextBox.Font = new System.Drawing.Font("Arial", 9.75F);
        userTextBox.Location = new System.Drawing.Point(0, 19);
        userTextBox.Name = "userTextBox";
        userTextBox.Size = new System.Drawing.Size(224, 22);
        userTextBox.TabIndex = 1;
        userTextBox.KeyDown += userTextBox_KeyDown;
        // 
        // FOptions
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(740, 512);
        Controls.Add(panel1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FOptions";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    private System.Windows.Forms.TextBox deliveryPointIdTextBox;
    private System.Windows.Forms.Label deliveryPoinIdLabel;

    private System.Windows.Forms.TextBox userTextBox;

    private System.Windows.Forms.Label userLabel;
    private System.Windows.Forms.Panel panel1;

    #endregion
}