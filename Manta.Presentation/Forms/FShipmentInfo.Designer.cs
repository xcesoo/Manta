using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FShipmentInfo
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
        topPanel = new System.Windows.Forms.Panel();
        shipmentDetails = new System.Windows.Forms.Label();
        flowInfoPanel = new System.Windows.Forms.FlowLayoutPanel();
        topPanel.SuspendLayout();
        SuspendLayout();
        // 
        // topPanel
        // 
        topPanel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)250)), ((int)((byte)250)));
        topPanel.Controls.Add(shipmentDetails);
        topPanel.Dock = System.Windows.Forms.DockStyle.Top;
        topPanel.Location = new System.Drawing.Point(0, 0);
        topPanel.Name = "topPanel";
        topPanel.Size = new System.Drawing.Size(772, 50);
        topPanel.TabIndex = 1;
        // 
        // shipmentDetails
        // 
        shipmentDetails.Dock = System.Windows.Forms.DockStyle.Left;
        shipmentDetails.Font = new System.Drawing.Font("Arial", 9.75F);
        shipmentDetails.Location = new System.Drawing.Point(0, 0);
        shipmentDetails.Name = "shipmentDetails";
        shipmentDetails.Size = new System.Drawing.Size(175, 50);
        shipmentDetails.TabIndex = 1;
        shipmentDetails.Text = "Деталі відправлення";
        shipmentDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // flowInfoPanel
        // 
        flowInfoPanel.Location = new System.Drawing.Point(12, 56);
        flowInfoPanel.Name = "flowInfoPanel";
        flowInfoPanel.Size = new System.Drawing.Size(748, 522);
        flowInfoPanel.TabIndex = 1;
        // 
        // FShipmentInfo
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(772, 590);
        Controls.Add(flowInfoPanel);
        Controls.Add(topPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FShipmentInfo";
        topPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label shipmentDetails;

    private System.Windows.Forms.FlowLayoutPanel flowInfoPanel;

    private System.Windows.Forms.Panel topPanel;

    #endregion
}