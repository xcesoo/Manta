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
        label1 = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // acceptedFlowPanel
        // 
        acceptedFlowPanel.AutoScroll = true;
        acceptedFlowPanel.Location = new System.Drawing.Point(13, 139);
        acceptedFlowPanel.Name = "acceptedFlowPanel";
        acceptedFlowPanel.Size = new System.Drawing.Size(748, 439);
        acceptedFlowPanel.TabIndex = 0;
        // 
        // acceptTextBox
        // 
        acceptTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        acceptTextBox.Font = new System.Drawing.Font("Arial", 14.25F);
        acceptTextBox.Location = new System.Drawing.Point(13, 48);
        acceptTextBox.Name = "acceptTextBox";
        acceptTextBox.Size = new System.Drawing.Size(748, 29);
        acceptTextBox.TabIndex = 1;
        acceptTextBox.KeyDown += acceptTextBox_KeyDown;
        acceptTextBox.KeyPress += acceptTextBox_KeyPress;
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Arial", 9.75F);
        label1.Location = new System.Drawing.Point(12, 9);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(747, 25);
        label1.TabIndex = 2;
        label1.Text = "Введіть номер посилки\r\n";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // panel1
        // 
        panel1.Controls.Add(label4);
        panel1.Controls.Add(label3);
        panel1.Controls.Add(label2);
        panel1.Location = new System.Drawing.Point(13, 83);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(700, 50);
        panel1.TabIndex = 3;
        // 
        // label4
        // 
        label4.Dock = System.Windows.Forms.DockStyle.Right;
        label4.Font = new System.Drawing.Font("Arial", 9.75F);
        label4.Location = new System.Drawing.Point(550, 0);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(150, 50);
        label4.TabIndex = 5;
        label4.Text = "Статус";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        label3.Dock = System.Windows.Forms.DockStyle.Left;
        label3.Font = new System.Drawing.Font("Arial", 9.75F);
        label3.Location = new System.Drawing.Point(25, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(150, 50);
        label3.TabIndex = 4;
        label3.Text = "Одержувач";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Dock = System.Windows.Forms.DockStyle.Left;
        label2.Font = new System.Drawing.Font("Arial", 9.75F);
        label2.Location = new System.Drawing.Point(0, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(25, 50);
        label2.TabIndex = 3;
        label2.Text = "№";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // FAcceptShipments
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(772, 590);
        Controls.Add(panel1);
        Controls.Add(label1);
        Controls.Add(acceptTextBox);
        Controls.Add(acceptedFlowPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FAcceptShipments";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.TextBox acceptTextBox;

    private System.Windows.Forms.FlowLayoutPanel acceptedFlowPanel;

    #endregion
}