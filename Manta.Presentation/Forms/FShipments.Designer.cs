using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FShipments
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
        SuspendLayout();
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new System.Drawing.Point(12, 12);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.Size = new System.Drawing.Size(732, 23);
        searchTextBox.TabIndex = 0;
        searchTextBox.KeyDown += textBox1_KeyDown;
        // 
        // flowDataPanel
        // 
        flowDataPanel.AutoScroll = true;
        flowDataPanel.Location = new System.Drawing.Point(12, 41);
        flowDataPanel.Name = "flowDataPanel";
        flowDataPanel.Size = new System.Drawing.Size(732, 509);
        flowDataPanel.TabIndex = 1;
        // 
        // FShipments
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(756, 551);
        Controls.Add(flowDataPanel);
        Controls.Add(searchTextBox);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FShipments";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.FlowLayoutPanel flowDataPanel;

    private System.Windows.Forms.TextBox searchTextBox;

    #endregion
}