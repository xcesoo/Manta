namespace Manta.Presentation.Forms;

partial class FMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        sideMenuPanel = new System.Windows.Forms.Panel();
        shipmentsBtn = new System.Windows.Forms.Button();
        returnRequestBtn = new System.Windows.Forms.Button();
        shipmentDeliveryBtn = new System.Windows.Forms.Button();
        borderPanel = new System.Windows.Forms.Panel();
        cashDeskPanel = new System.Windows.Forms.Panel();
        mainPanel = new System.Windows.Forms.Panel();
        sideMenuPanel.SuspendLayout();
        SuspendLayout();
        // 
        // sideMenuPanel
        // 
        sideMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)59)), ((int)((byte)77)), ((int)((byte)86)));
        sideMenuPanel.Controls.Add(shipmentsBtn);
        sideMenuPanel.Controls.Add(returnRequestBtn);
        sideMenuPanel.Controls.Add(shipmentDeliveryBtn);
        sideMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
        sideMenuPanel.Location = new System.Drawing.Point(0, 50);
        sideMenuPanel.Name = "sideMenuPanel";
        sideMenuPanel.Size = new System.Drawing.Size(180, 590);
        sideMenuPanel.TabIndex = 0;
        // 
        // shipmentsBtn
        // 
        shipmentsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        shipmentsBtn.Dock = System.Windows.Forms.DockStyle.Top;
        shipmentsBtn.FlatAppearance.BorderSize = 0;
        shipmentsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        shipmentsBtn.ForeColor = System.Drawing.Color.White;
        shipmentsBtn.Location = new System.Drawing.Point(0, 88);
        shipmentsBtn.Name = "shipmentsBtn";
        shipmentsBtn.Size = new System.Drawing.Size(180, 44);
        shipmentsBtn.TabIndex = 5;
        shipmentsBtn.Text = "Відправлення";
        shipmentsBtn.UseVisualStyleBackColor = true;
        shipmentsBtn.Click += shipmentsBtn_Click;
        // 
        // returnRequestBtn
        // 
        returnRequestBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        returnRequestBtn.Dock = System.Windows.Forms.DockStyle.Top;
        returnRequestBtn.FlatAppearance.BorderSize = 0;
        returnRequestBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        returnRequestBtn.ForeColor = System.Drawing.Color.White;
        returnRequestBtn.Location = new System.Drawing.Point(0, 44);
        returnRequestBtn.Name = "returnRequestBtn";
        returnRequestBtn.Size = new System.Drawing.Size(180, 44);
        returnRequestBtn.TabIndex = 4;
        returnRequestBtn.Text = "Потрібно відправити";
        returnRequestBtn.UseVisualStyleBackColor = true;
        // 
        // shipmentDeliveryBtn
        // 
        shipmentDeliveryBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        shipmentDeliveryBtn.Dock = System.Windows.Forms.DockStyle.Top;
        shipmentDeliveryBtn.FlatAppearance.BorderSize = 0;
        shipmentDeliveryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        shipmentDeliveryBtn.ForeColor = System.Drawing.Color.White;
        shipmentDeliveryBtn.Location = new System.Drawing.Point(0, 0);
        shipmentDeliveryBtn.Name = "shipmentDeliveryBtn";
        shipmentDeliveryBtn.Size = new System.Drawing.Size(180, 44);
        shipmentDeliveryBtn.TabIndex = 3;
        shipmentDeliveryBtn.Text = "Видача відправлень";
        shipmentDeliveryBtn.UseVisualStyleBackColor = true;
        // 
        // borderPanel
        // 
        borderPanel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)54)), ((int)((byte)71)), ((int)((byte)79)));
        borderPanel.Dock = System.Windows.Forms.DockStyle.Top;
        borderPanel.Location = new System.Drawing.Point(0, 0);
        borderPanel.Name = "borderPanel";
        borderPanel.Size = new System.Drawing.Size(1152, 50);
        borderPanel.TabIndex = 1;
        borderPanel.MouseDown += borderPanel_MouseDown;
        borderPanel.MouseMove += borderPanel_MouseMove;
        borderPanel.MouseUp += borderPanel_MouseUp;
        // 
        // cashDeskPanel
        // 
        cashDeskPanel.Dock = System.Windows.Forms.DockStyle.Right;
        cashDeskPanel.Location = new System.Drawing.Point(952, 50);
        cashDeskPanel.Name = "cashDeskPanel";
        cashDeskPanel.Size = new System.Drawing.Size(200, 590);
        cashDeskPanel.TabIndex = 2;
        // 
        // mainPanel
        // 
        mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        mainPanel.Location = new System.Drawing.Point(180, 50);
        mainPanel.Name = "mainPanel";
        mainPanel.Size = new System.Drawing.Size(772, 590);
        mainPanel.TabIndex = 3;
        // 
        // FMain
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.White;
        ClientSize = new System.Drawing.Size(1152, 640);
        Controls.Add(mainPanel);
        Controls.Add(cashDeskPanel);
        Controls.Add(sideMenuPanel);
        Controls.Add(borderPanel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        sideMenuPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Panel mainPanel;

    private System.Windows.Forms.Button shipmentsBtn;

    private System.Windows.Forms.Button returnRequestBtn;

    private System.Windows.Forms.Button shipmentDeliveryBtn;


    private System.Windows.Forms.Panel cashDeskPanel;

    private System.Windows.Forms.Panel borderPanel;

    private System.Windows.Forms.Panel sideMenuPanel;

    private System.Windows.Forms.DataGridView parcelDataGrid;

    #endregion
}