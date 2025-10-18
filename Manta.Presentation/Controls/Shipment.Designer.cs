using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class Shipment
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
        amountDueLabel = new System.Windows.Forms.Label();
        cashdeskBtn = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // idLabel
        // 
        idLabel.Dock = System.Windows.Forms.DockStyle.Left;
        idLabel.Location = new System.Drawing.Point(0, 0);
        idLabel.Name = "idLabel";
        idLabel.Size = new System.Drawing.Size(100, 50);
        idLabel.TabIndex = 0;
        idLabel.Text = "Номер замовлення";
        idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // phoneNumberLabel
        // 
        phoneNumberLabel.Dock = System.Windows.Forms.DockStyle.Left;
        phoneNumberLabel.Location = new System.Drawing.Point(100, 0);
        phoneNumberLabel.Name = "phoneNumberLabel";
        phoneNumberLabel.Size = new System.Drawing.Size(200, 50);
        phoneNumberLabel.TabIndex = 1;
        phoneNumberLabel.Text = "Номер телефону";
        phoneNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // recipientNameLabel
        // 
        recipientNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
        recipientNameLabel.Location = new System.Drawing.Point(300, 0);
        recipientNameLabel.Name = "recipientNameLabel";
        recipientNameLabel.Size = new System.Drawing.Size(200, 50);
        recipientNameLabel.TabIndex = 2;
        recipientNameLabel.Text = "Одержувач";
        recipientNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // amountDueLabel
        // 
        amountDueLabel.Dock = System.Windows.Forms.DockStyle.Left;
        amountDueLabel.Location = new System.Drawing.Point(500, 0);
        amountDueLabel.Name = "amountDueLabel";
        amountDueLabel.Size = new System.Drawing.Size(100, 50);
        amountDueLabel.TabIndex = 3;
        amountDueLabel.Text = "Оплата";
        amountDueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // cashdeskBtn
        // 
        cashdeskBtn.Dock = System.Windows.Forms.DockStyle.Right;
        cashdeskBtn.FlatAppearance.BorderSize = 0;
        cashdeskBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        cashdeskBtn.Location = new System.Drawing.Point(635, 0);
        cashdeskBtn.Name = "cashdeskBtn";
        cashdeskBtn.Size = new System.Drawing.Size(65, 50);
        cashdeskBtn.TabIndex = 4;
        cashdeskBtn.Text = "Додати в кошик";
        cashdeskBtn.UseVisualStyleBackColor = true;
        // 
        // Shipment
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(cashdeskBtn);
        Controls.Add(amountDueLabel);
        Controls.Add(recipientNameLabel);
        Controls.Add(phoneNumberLabel);
        Controls.Add(idLabel);
        Size = new System.Drawing.Size(700, 50);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button cashdeskBtn;

    private System.Windows.Forms.Label amountDueLabel;

    private System.Windows.Forms.Label recipientNameLabel;

    private System.Windows.Forms.Label phoneNumberLabel;

    private System.Windows.Forms.Label idLabel;

    #endregion
}