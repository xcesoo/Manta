using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FShipmentsReadyForPickUp
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
        searchTextBox = new System.Windows.Forms.TextBox();
        flowDataPanel = new System.Windows.Forms.FlowLayoutPanel();
        columnUserControl = new Manta.Presentation.Controls.Shipment();
        SuspendLayout();
        // 
        // searchTextBox
        // 
        searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        searchTextBox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        searchTextBox.Location = new System.Drawing.Point(13, 13);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.Size = new System.Drawing.Size(732, 29);
        searchTextBox.TabIndex = 0;
        searchTextBox.KeyDown += searchTextBox_KeyDown;
        // 
        // flowDataPanel
        // 
        flowDataPanel.AutoScroll = true;
        flowDataPanel.Location = new System.Drawing.Point(12, 80);
        flowDataPanel.Name = "flowDataPanel";
        flowDataPanel.Size = new System.Drawing.Size(732, 470);
        flowDataPanel.TabIndex = 1;
        // 
        // columnUserControl
        // 
        columnUserControl.Location = new System.Drawing.Point(13, 48);
        columnUserControl.Name = "columnUserControl";
        columnUserControl.Size = new System.Drawing.Size(643, 24);
        columnUserControl.TabIndex = 3;
        // 
        // FShipmentsReadyForPickUp
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(756, 551);
        Controls.Add(columnUserControl);
        Controls.Add(flowDataPanel);
        Controls.Add(searchTextBox);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FShipmentsReadyForPickUp";
        ResumeLayout(false);
        PerformLayout();
    }
    private Manta.Presentation.Controls.Shipment columnUserControl;

    private System.Windows.Forms.FlowLayoutPanel flowDataPanel;

    private System.Windows.Forms.TextBox searchTextBox;

    #endregion
}