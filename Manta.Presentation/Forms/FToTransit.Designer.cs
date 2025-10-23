using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FToTransit
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
        vehicleSearch = new System.Windows.Forms.TextBox();
        label = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        toTransit = new System.Windows.Forms.Button();
        selectAll = new System.Windows.Forms.CheckBox();
        panel2 = new System.Windows.Forms.Panel();
        label4 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // flowDataPanel
        // 
        flowDataPanel.AutoScroll = true;
        flowDataPanel.Location = new System.Drawing.Point(13, 136);
        flowDataPanel.Name = "flowDataPanel";
        flowDataPanel.Size = new System.Drawing.Size(768, 442);
        flowDataPanel.TabIndex = 0;
        // 
        // vehicleSearch
        // 
        vehicleSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        vehicleSearch.Font = new System.Drawing.Font("Arial", 14.25F);
        vehicleSearch.Location = new System.Drawing.Point(13, 45);
        vehicleSearch.Name = "vehicleSearch";
        vehicleSearch.Size = new System.Drawing.Size(747, 29);
        vehicleSearch.TabIndex = 1;
        vehicleSearch.KeyDown += vehicleSearch_KeyDown;
        // 
        // label
        // 
        label.Font = new System.Drawing.Font("Arial", 9.75F);
        label.Location = new System.Drawing.Point(13, 18);
        label.Name = "label";
        label.Size = new System.Drawing.Size(747, 25);
        label.TabIndex = 2;
        label.Text = "Введіть номер машини";
        // 
        // panel1
        // 
        panel1.Controls.Add(toTransit);
        panel1.Controls.Add(selectAll);
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(label4);
        panel1.Controls.Add(label3);
        panel1.Controls.Add(label2);
        panel1.Controls.Add(label1);
        panel1.Location = new System.Drawing.Point(12, 80);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(748, 50);
        panel1.TabIndex = 3;
        // 
        // toTransit
        // 
        toTransit.Dock = System.Windows.Forms.DockStyle.Right;
        toTransit.FlatAppearance.BorderSize = 0;
        toTransit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        toTransit.Location = new System.Drawing.Point(578, 0);
        toTransit.Name = "toTransit";
        toTransit.Size = new System.Drawing.Size(50, 50);
        toTransit.TabIndex = 6;
        toTransit.Text = "✔️";
        toTransit.UseVisualStyleBackColor = true;
        toTransit.Click += toTransit_Click;
        // 
        // selectAll
        // 
        selectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
        selectAll.Dock = System.Windows.Forms.DockStyle.Right;
        selectAll.Font = new System.Drawing.Font("Arial", 9.75F);
        selectAll.ForeColor = System.Drawing.Color.Black;
        selectAll.Location = new System.Drawing.Point(628, 0);
        selectAll.Name = "selectAll";
        selectAll.Size = new System.Drawing.Size(100, 50);
        selectAll.TabIndex = 5;
        selectAll.Text = "Обрати всі";
        selectAll.UseVisualStyleBackColor = true;
        // 
        // panel2
        // 
        panel2.Dock = System.Windows.Forms.DockStyle.Right;
        panel2.Location = new System.Drawing.Point(728, 0);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(20, 50);
        panel2.TabIndex = 4;
        // 
        // label4
        // 
        label4.Dock = System.Windows.Forms.DockStyle.Left;
        label4.Font = new System.Drawing.Font("Arial", 9.75F);
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
        label1.Location = new System.Drawing.Point(0, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(25, 50);
        label1.TabIndex = 0;
        label1.Text = "№";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // FToTransit
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(772, 590);
        Controls.Add(panel1);
        Controls.Add(label);
        Controls.Add(vehicleSearch);
        Controls.Add(flowDataPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FToTransit";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button toTransit;

    private System.Windows.Forms.CheckBox selectAll;

    private System.Windows.Forms.Panel panel2;

    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.Label label;

    private System.Windows.Forms.TextBox vehicleSearch;

    private System.Windows.Forms.FlowLayoutPanel flowDataPanel;


    #endregion
}