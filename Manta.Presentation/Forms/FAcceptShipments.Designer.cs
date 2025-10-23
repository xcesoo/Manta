using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FAcceptShipments
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
        acceptedFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
        acceptTextBox = new System.Windows.Forms.TextBox();
        SuspendLayout();
        // 
        // acceptedFlowPanel
        // 
        acceptedFlowPanel.AutoScroll = true;
        acceptedFlowPanel.Location = new System.Drawing.Point(13, 56);
        acceptedFlowPanel.Name = "acceptedFlowPanel";
        acceptedFlowPanel.Size = new System.Drawing.Size(748, 522);
        acceptedFlowPanel.TabIndex = 0;
        // 
        // acceptTextBox
        // 
        acceptTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        acceptTextBox.Font = new System.Drawing.Font("Arial", 14.25F);
        acceptTextBox.Location = new System.Drawing.Point(13, 13);
        acceptTextBox.Name = "acceptTextBox";
        acceptTextBox.Size = new System.Drawing.Size(748, 29);
        acceptTextBox.TabIndex = 1;
        acceptTextBox.KeyDown += acceptTextBox_KeyDown;
        acceptTextBox.KeyPress += acceptTextBox_KeyPress;
        // 
        // FAcceptShipments
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(772, 590);
        Controls.Add(acceptTextBox);
        Controls.Add(acceptedFlowPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FAcceptShipments";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox acceptTextBox;

    private System.Windows.Forms.FlowLayoutPanel acceptedFlowPanel;

    #endregion
}