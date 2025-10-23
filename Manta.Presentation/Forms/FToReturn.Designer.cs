using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FToReturn
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
        flowDataPanel = new System.Windows.Forms.FlowLayoutPanel();
        infoPanel = new System.Windows.Forms.Panel();
        returnRequest = new System.Windows.Forms.Button();
        selectAll = new System.Windows.Forms.CheckBox();
        panel1 = new System.Windows.Forms.Panel();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        infoPanel.SuspendLayout();
        SuspendLayout();
        // 
        // flowDataPanel
        // 
        flowDataPanel.AutoScroll = true;
        flowDataPanel.Location = new System.Drawing.Point(12, 80);
        flowDataPanel.Name = "flowDataPanel";
        flowDataPanel.Size = new System.Drawing.Size(768, 470);
        flowDataPanel.TabIndex = 0;
        // 
        // infoPanel
        // 
        infoPanel.Controls.Add(returnRequest);
        infoPanel.Controls.Add(selectAll);
        infoPanel.Controls.Add(panel1);
        infoPanel.Controls.Add(label4);
        infoPanel.Controls.Add(label3);
        infoPanel.Controls.Add(label2);
        infoPanel.Controls.Add(label1);
        infoPanel.Location = new System.Drawing.Point(12, 24);
        infoPanel.Name = "infoPanel";
        infoPanel.Size = new System.Drawing.Size(748, 50);
        infoPanel.TabIndex = 1;
        // 
        // returnRequest
        // 
        returnRequest.Dock = System.Windows.Forms.DockStyle.Right;
        returnRequest.FlatAppearance.BorderSize = 0;
        returnRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        returnRequest.Location = new System.Drawing.Point(578, 0);
        returnRequest.Name = "returnRequest";
        returnRequest.Size = new System.Drawing.Size(50, 50);
        returnRequest.TabIndex = 6;
        returnRequest.Text = "✔️";
        returnRequest.UseVisualStyleBackColor = true;
        returnRequest.Click += returnRequest_Click;
        // 
        // selectAll
        // 
        selectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        selectAll.Dock = System.Windows.Forms.DockStyle.Right;
        selectAll.Font = new System.Drawing.Font("Arial", 9.75F);
        selectAll.Location = new System.Drawing.Point(628, 0);
        selectAll.Name = "selectAll";
        selectAll.Size = new System.Drawing.Size(100, 50);
        selectAll.TabIndex = 4;
        selectAll.Text = "Обрати всі";
        selectAll.UseVisualStyleBackColor = true;
        // 
        // panel1
        // 
        panel1.Dock = System.Windows.Forms.DockStyle.Right;
        panel1.Location = new System.Drawing.Point(728, 0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(20, 50);
        panel1.TabIndex = 5;
        // 
        // label4
        // 
        label4.Dock = System.Windows.Forms.DockStyle.Left;
        label4.Font = new System.Drawing.Font("Arial", 9.75F);
        label4.ForeColor = System.Drawing.Color.Black;
        label4.Location = new System.Drawing.Point(325, 0);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(150, 50);
        label4.TabIndex = 3;
        label4.Text = "Статус";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label3
        // 
        label3.Dock = System.Windows.Forms.DockStyle.Left;
        label3.Font = new System.Drawing.Font("Arial", 9.75F);
        label3.ForeColor = System.Drawing.Color.Black;
        label3.Location = new System.Drawing.Point(175, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(150, 50);
        label3.TabIndex = 2;
        label3.Text = "Одержувач";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Dock = System.Windows.Forms.DockStyle.Left;
        label2.Font = new System.Drawing.Font("Arial", 9.75F);
        label2.ForeColor = System.Drawing.Color.Black;
        label2.Location = new System.Drawing.Point(25, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(150, 50);
        label2.TabIndex = 1;
        label2.Text = "Номер телефону";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label1
        // 
        label1.Dock = System.Windows.Forms.DockStyle.Left;
        label1.Font = new System.Drawing.Font("Arial", 9.75F);
        label1.ForeColor = System.Drawing.Color.DimGray;
        label1.Location = new System.Drawing.Point(0, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(25, 50);
        label1.TabIndex = 0;
        label1.Text = "№";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // FToReturn
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(772, 590);
        Controls.Add(infoPanel);
        Controls.Add(flowDataPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "ToReturn";
        infoPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button returnRequest;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.CheckBox selectAll;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Panel infoPanel;

    private System.Windows.Forms.FlowLayoutPanel flowDataPanel;

    #endregion
}