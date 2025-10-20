using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class ParcelCashDesk
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
        parcelIdLabel = new System.Windows.Forms.Label();
        panel1 = new System.Windows.Forms.Panel();
        deleteBtn = new System.Windows.Forms.Button();
        toPayLabel = new System.Windows.Forms.Label();
        panel2 = new System.Windows.Forms.Panel();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // parcelIdLabel
        // 
        parcelIdLabel.Dock = System.Windows.Forms.DockStyle.Top;
        parcelIdLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        parcelIdLabel.Location = new System.Drawing.Point(0, 0);
        parcelIdLabel.Name = "parcelIdLabel";
        parcelIdLabel.Size = new System.Drawing.Size(168, 15);
        parcelIdLabel.TabIndex = 0;
        parcelIdLabel.Text = "Номер посилки: ";
        // 
        // panel1
        // 
        panel1.BackColor = System.Drawing.Color.FromArgb(((int)((byte)221)), ((int)((byte)238)), ((int)((byte)200)));
        panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        panel1.Controls.Add(deleteBtn);
        panel1.Controls.Add(toPayLabel);
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(parcelIdLabel);
        panel1.ForeColor = System.Drawing.Color.Black;
        panel1.Location = new System.Drawing.Point(3, 3);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(170, 79);
        panel1.TabIndex = 1;
        // 
        // deleteBtn
        // 
        deleteBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        deleteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)129)), ((int)((byte)91)));
        deleteBtn.Location = new System.Drawing.Point(140, 48);
        deleteBtn.Name = "deleteBtn";
        deleteBtn.Size = new System.Drawing.Size(25, 25);
        deleteBtn.TabIndex = 4;
        deleteBtn.UseVisualStyleBackColor = true;
        deleteBtn.Click += deleteBtn_Click;
        // 
        // toPayLabel
        // 
        toPayLabel.Dock = System.Windows.Forms.DockStyle.Top;
        toPayLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        toPayLabel.Location = new System.Drawing.Point(0, 30);
        toPayLabel.Name = "toPayLabel";
        toPayLabel.Size = new System.Drawing.Size(168, 15);
        toPayLabel.TabIndex = 3;
        toPayLabel.Text = "До сплати: ";
        // 
        // panel2
        // 
        panel2.Dock = System.Windows.Forms.DockStyle.Top;
        panel2.Location = new System.Drawing.Point(0, 15);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(168, 15);
        panel2.TabIndex = 2;
        // 
        // ParcelCashDesk
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(panel1);
        Size = new System.Drawing.Size(176, 100);
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button deleteBtn;

    private System.Windows.Forms.Label toPayLabel;
    private System.Windows.Forms.Panel panel2;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.Label parcelIdLabel;

    #endregion
}