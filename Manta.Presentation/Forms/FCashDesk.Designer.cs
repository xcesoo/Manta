using System.ComponentModel;

namespace Manta.Presentation.Forms;

partial class FCashDesk
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
        cashDeskLabel = new System.Windows.Forms.Label();
        parcelsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
        payBtn = new System.Windows.Forms.Button();
        clearBtn = new System.Windows.Forms.Button();
        panel1 = new System.Windows.Forms.Panel();
        toPayLabel = new System.Windows.Forms.Label();
        panel2 = new System.Windows.Forms.Panel();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // cashDeskLabel
        // 
        cashDeskLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        cashDeskLabel.Location = new System.Drawing.Point(12, 9);
        cashDeskLabel.Name = "cashDeskLabel";
        cashDeskLabel.Size = new System.Drawing.Size(100, 23);
        cashDeskLabel.TabIndex = 0;
        cashDeskLabel.Text = "Кошик";
        // 
        // parcelsFlowPanel
        // 
        parcelsFlowPanel.Location = new System.Drawing.Point(12, 35);
        parcelsFlowPanel.Name = "parcelsFlowPanel";
        parcelsFlowPanel.Size = new System.Drawing.Size(176, 443);
        parcelsFlowPanel.TabIndex = 1;
        // 
        // payBtn
        // 
        payBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
        payBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        payBtn.Font = new System.Drawing.Font("Arial", 9.75F);
        payBtn.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)104)), ((int)((byte)159)), ((int)((byte)56)));
        payBtn.Location = new System.Drawing.Point(0, 27);
        payBtn.Margin = new System.Windows.Forms.Padding(0);
        payBtn.Name = "payBtn";
        payBtn.Size = new System.Drawing.Size(176, 25);
        payBtn.TabIndex = 2;
        payBtn.Text = "Оплатити та видати";
        payBtn.UseVisualStyleBackColor = true;
        payBtn.Click += payBtn_Click;
        // 
        // clearBtn
        // 
        clearBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
        clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        clearBtn.Font = new System.Drawing.Font("Arial", 9.75F);
        clearBtn.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)87)), ((int)((byte)34)));
        clearBtn.Location = new System.Drawing.Point(0, 62);
        clearBtn.Name = "clearBtn";
        clearBtn.Size = new System.Drawing.Size(176, 25);
        clearBtn.TabIndex = 3;
        clearBtn.Text = "Очистити кошик";
        clearBtn.UseVisualStyleBackColor = true;
        clearBtn.Click += clearBtn_Click;
        // 
        // panel1
        // 
        panel1.Controls.Add(toPayLabel);
        panel1.Controls.Add(payBtn);
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(clearBtn);
        panel1.Location = new System.Drawing.Point(12, 494);
        panel1.Margin = new System.Windows.Forms.Padding(0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(176, 87);
        panel1.TabIndex = 4;
        // 
        // toPayLabel
        // 
        toPayLabel.Dock = System.Windows.Forms.DockStyle.Top;
        toPayLabel.Font = new System.Drawing.Font("Arial", 9.75F);
        toPayLabel.Location = new System.Drawing.Point(0, 0);
        toPayLabel.Name = "toPayLabel";
        toPayLabel.Size = new System.Drawing.Size(176, 23);
        toPayLabel.TabIndex = 5;
        toPayLabel.Text = "До сплати:  0";
        // 
        // panel2
        // 
        panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
        panel2.Location = new System.Drawing.Point(0, 52);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(176, 10);
        panel2.TabIndex = 4;
        // 
        // FCashDesk
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.White;
        ClientSize = new System.Drawing.Size(200, 590);
        Controls.Add(parcelsFlowPanel);
        Controls.Add(cashDeskLabel);
        Controls.Add(panel1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Text = "FCashDesk";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label toPayLabel;

    private System.Windows.Forms.Panel panel2;

    private System.Windows.Forms.Panel panel1;

    private System.Windows.Forms.Button clearBtn;

    private System.Windows.Forms.Button payBtn;

    private System.Windows.Forms.FlowLayoutPanel parcelsFlowPanel;

    private System.Windows.Forms.Label cashDeskLabel;
    
    #endregion
}