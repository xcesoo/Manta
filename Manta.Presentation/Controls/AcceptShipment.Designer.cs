using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class AcceptShipment
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
        statusLabel = new System.Windows.Forms.Label();
        backPanel = new System.Windows.Forms.Panel();
        recipientNameLabel = new System.Windows.Forms.Label();
        backPanel.SuspendLayout();
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
        // statusLabel
        // 
        statusLabel.Dock = System.Windows.Forms.DockStyle.Right;
        statusLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        statusLabel.Location = new System.Drawing.Point(550, 0);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new System.Drawing.Size(150, 50);
        statusLabel.TabIndex = 5;
        statusLabel.Text = "Статус";
        statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // backPanel
        // 
        backPanel.Controls.Add(recipientNameLabel);
        backPanel.Controls.Add(statusLabel);
        backPanel.Controls.Add(idLabel);
        backPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        backPanel.Location = new System.Drawing.Point(0, 0);
        backPanel.Name = "backPanel";
        backPanel.Size = new System.Drawing.Size(700, 50);
        backPanel.TabIndex = 6;
        // 
        // recipientNameLabel
        // 
        recipientNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
        recipientNameLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        recipientNameLabel.Location = new System.Drawing.Point(25, 0);
        recipientNameLabel.Name = "recipientNameLabel";
        recipientNameLabel.Size = new System.Drawing.Size(150, 50);
        recipientNameLabel.TabIndex = 2;
        recipientNameLabel.Text = "Одержувач";
        recipientNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // AcceptShipment
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(backPanel);
        Size = new System.Drawing.Size(700, 50);
        backPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label recipientNameLabel;

    private System.Windows.Forms.Panel backPanel;

    private System.Windows.Forms.Label statusLabel;

    private System.Windows.Forms.Label idLabel;

    #endregion
}