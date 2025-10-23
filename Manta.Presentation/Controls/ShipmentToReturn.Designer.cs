using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class ShipmentToReturn
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

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        idLabel = new System.Windows.Forms.Label();
        phoneNumberLabel = new System.Windows.Forms.Label();
        recipientNameLabel = new System.Windows.Forms.Label();
        statusLabel = new System.Windows.Forms.Label();
        check = new System.Windows.Forms.CheckBox();
        panel1 = new System.Windows.Forms.Panel();
        SuspendLayout();
        // 
        // idLabel
        // 
        idLabel.Dock = System.Windows.Forms.DockStyle.Left;
        idLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        idLabel.ForeColor = System.Drawing.Color.DimGray;
        idLabel.Location = new System.Drawing.Point(0, 0);
        idLabel.Name = "idLabel";
        idLabel.Size = new System.Drawing.Size(25, 50);
        idLabel.TabIndex = 0;
        idLabel.Text = "№";
        idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // phoneNumberLabel
        // 
        phoneNumberLabel.Dock = System.Windows.Forms.DockStyle.Left;
        phoneNumberLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        phoneNumberLabel.Location = new System.Drawing.Point(25, 0);
        phoneNumberLabel.Name = "phoneNumberLabel";
        phoneNumberLabel.Size = new System.Drawing.Size(150, 50);
        phoneNumberLabel.TabIndex = 1;
        phoneNumberLabel.Text = "Номер телефону";
        phoneNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // recipientNameLabel
        // 
        recipientNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
        recipientNameLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        recipientNameLabel.Location = new System.Drawing.Point(175, 0);
        recipientNameLabel.Name = "recipientNameLabel";
        recipientNameLabel.Size = new System.Drawing.Size(150, 50);
        recipientNameLabel.TabIndex = 2;
        recipientNameLabel.Text = "Одержувач";
        recipientNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // statusLabel
        // 
        statusLabel.Dock = System.Windows.Forms.DockStyle.Left;
        statusLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        statusLabel.Location = new System.Drawing.Point(325, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(150, 50);
        statusLabel.TabIndex = 3;
        statusLabel.Text = "Статус";
        statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // check
        // 
        check.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
        check.Dock = System.Windows.Forms.DockStyle.Right;
        check.Location = new System.Drawing.Point(650, 0);
        check.Name = "check";
        check.Size = new System.Drawing.Size(50, 50);
        check.TabIndex = 4;
        check.UseVisualStyleBackColor = true;
        // 
        // panel1
        // 
        panel1.Dock = System.Windows.Forms.DockStyle.Fill;
        panel1.Location = new System.Drawing.Point(475, 0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(175, 50);
        panel1.TabIndex = 5;
        // 
        // ShipmentToReturn
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(panel1);
        Controls.Add(check);
        Controls.Add(statusLabel);
        Controls.Add(recipientNameLabel);
        Controls.Add(phoneNumberLabel);
        Controls.Add(idLabel);
        Size = new System.Drawing.Size(700, 50);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.CheckBox check;

    private System.Windows.Forms.Label recipientNameLabel;
    private System.Windows.Forms.Label statusLabel;

    private System.Windows.Forms.Label phoneNumberLabel;

    private System.Windows.Forms.Label idLabel;

    #endregion
}