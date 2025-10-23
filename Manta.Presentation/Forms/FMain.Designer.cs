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
        acceptParcels = new System.Windows.Forms.Button();
        adminToolsBtn = new System.Windows.Forms.Button();
        optionsBtn = new System.Windows.Forms.Button();
        shipmentsBtn = new System.Windows.Forms.Button();
        returnRequestBtn = new System.Windows.Forms.Button();
        shipmentDeliveryBtn = new System.Windows.Forms.Button();
        borderPanel = new System.Windows.Forms.Panel();
        minimizeBtn = new System.Windows.Forms.Button();
        exitBtn = new System.Windows.Forms.Button();
        cashDeskPanel = new System.Windows.Forms.Panel();
        mainPanel = new System.Windows.Forms.Panel();
        sideMenuPanel.SuspendLayout();
        borderPanel.SuspendLayout();
        SuspendLayout();
        // 
        // sideMenuPanel
        // 
        sideMenuPanel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)59)), ((int)((byte)77)), ((int)((byte)86)));
        sideMenuPanel.Controls.Add(acceptParcels);
        sideMenuPanel.Controls.Add(adminToolsBtn);
        sideMenuPanel.Controls.Add(optionsBtn);
        sideMenuPanel.Controls.Add(shipmentsBtn);
        sideMenuPanel.Controls.Add(returnRequestBtn);
        sideMenuPanel.Controls.Add(shipmentDeliveryBtn);
        sideMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
        sideMenuPanel.Location = new System.Drawing.Point(0, 50);
        sideMenuPanel.Name = "sideMenuPanel";
        sideMenuPanel.Size = new System.Drawing.Size(180, 590);
        sideMenuPanel.TabIndex = 0;
        // 
        // acceptParcels
        // 
        acceptParcels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        acceptParcels.Dock = System.Windows.Forms.DockStyle.Top;
        acceptParcels.FlatAppearance.BorderSize = 0;
        acceptParcels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        acceptParcels.Font = new System.Drawing.Font("Arial", 9.75F);
        acceptParcels.ForeColor = System.Drawing.Color.White;
        acceptParcels.Location = new System.Drawing.Point(0, 132);
        acceptParcels.Name = "acceptParcels";
        acceptParcels.Size = new System.Drawing.Size(180, 44);
        acceptParcels.TabIndex = 8;
        acceptParcels.Text = "Прийом відправлень";
        acceptParcels.UseVisualStyleBackColor = true;
        acceptParcels.Click += acceptParcels_Click;
        // 
        // adminToolsBtn
        // 
        adminToolsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        adminToolsBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
        adminToolsBtn.FlatAppearance.BorderSize = 0;
        adminToolsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        adminToolsBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        adminToolsBtn.ForeColor = System.Drawing.Color.Silver;
        adminToolsBtn.Location = new System.Drawing.Point(0, 502);
        adminToolsBtn.Name = "adminToolsBtn";
        adminToolsBtn.Size = new System.Drawing.Size(180, 44);
        adminToolsBtn.TabIndex = 7;
        adminToolsBtn.Text = "AdminTools";
        adminToolsBtn.UseVisualStyleBackColor = true;
        // 
        // optionsBtn
        // 
        optionsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        optionsBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
        optionsBtn.FlatAppearance.BorderSize = 0;
        optionsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        optionsBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        optionsBtn.ForeColor = System.Drawing.Color.Silver;
        optionsBtn.Location = new System.Drawing.Point(0, 546);
        optionsBtn.Name = "optionsBtn";
        optionsBtn.Size = new System.Drawing.Size(180, 44);
        optionsBtn.TabIndex = 6;
        optionsBtn.Text = "Налаштування";
        optionsBtn.UseVisualStyleBackColor = true;
        optionsBtn.Click += optionsBtn_Click;
        // 
        // shipmentsBtn
        // 
        shipmentsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        shipmentsBtn.Dock = System.Windows.Forms.DockStyle.Top;
        shipmentsBtn.FlatAppearance.BorderSize = 0;
        shipmentsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        shipmentsBtn.Font = new System.Drawing.Font("Arial", 9.75F);
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
        returnRequestBtn.Font = new System.Drawing.Font("Arial", 9.75F);
        returnRequestBtn.ForeColor = System.Drawing.Color.White;
        returnRequestBtn.Location = new System.Drawing.Point(0, 44);
        returnRequestBtn.Name = "returnRequestBtn";
        returnRequestBtn.Size = new System.Drawing.Size(180, 44);
        returnRequestBtn.TabIndex = 4;
        returnRequestBtn.Text = "Потрібно відправити";
        returnRequestBtn.UseVisualStyleBackColor = true;
        returnRequestBtn.Click += returnRequestBtn_Click;
        // 
        // shipmentDeliveryBtn
        // 
        shipmentDeliveryBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        shipmentDeliveryBtn.Dock = System.Windows.Forms.DockStyle.Top;
        shipmentDeliveryBtn.FlatAppearance.BorderSize = 0;
        shipmentDeliveryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        shipmentDeliveryBtn.Font = new System.Drawing.Font("Arial", 9.75F);
        shipmentDeliveryBtn.ForeColor = System.Drawing.Color.White;
        shipmentDeliveryBtn.Location = new System.Drawing.Point(0, 0);
        shipmentDeliveryBtn.Name = "shipmentDeliveryBtn";
        shipmentDeliveryBtn.Size = new System.Drawing.Size(180, 44);
        shipmentDeliveryBtn.TabIndex = 3;
        shipmentDeliveryBtn.Text = "Видача відправлень";
        shipmentDeliveryBtn.UseVisualStyleBackColor = true;
        shipmentDeliveryBtn.Click += shipmentDelivery_Click;
        // 
        // borderPanel
        // 
        borderPanel.BackColor = System.Drawing.Color.FromArgb(((int)((byte)54)), ((int)((byte)71)), ((int)((byte)79)));
        borderPanel.Controls.Add(minimizeBtn);
        borderPanel.Controls.Add(exitBtn);
        borderPanel.Dock = System.Windows.Forms.DockStyle.Top;
        borderPanel.Location = new System.Drawing.Point(0, 0);
        borderPanel.Name = "borderPanel";
        borderPanel.Size = new System.Drawing.Size(1152, 50);
        borderPanel.TabIndex = 1;
        borderPanel.MouseDown += borderPanel_MouseDown;
        borderPanel.MouseMove += borderPanel_MouseMove;
        borderPanel.MouseUp += borderPanel_MouseUp;
        // 
        // minimizeBtn
        // 
        minimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        minimizeBtn.Dock = System.Windows.Forms.DockStyle.Right;
        minimizeBtn.FlatAppearance.BorderSize = 0;
        minimizeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        minimizeBtn.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        minimizeBtn.ForeColor = System.Drawing.Color.White;
        minimizeBtn.Location = new System.Drawing.Point(1052, 0);
        minimizeBtn.Name = "minimizeBtn";
        minimizeBtn.Size = new System.Drawing.Size(50, 50);
        minimizeBtn.TabIndex = 9;
        minimizeBtn.Text = "-";
        minimizeBtn.UseVisualStyleBackColor = true;
        minimizeBtn.Click += minimizeBtn_Click;
        // 
        // exitBtn
        // 
        exitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
        exitBtn.Dock = System.Windows.Forms.DockStyle.Right;
        exitBtn.FlatAppearance.BorderSize = 0;
        exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        exitBtn.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)204));
        exitBtn.ForeColor = System.Drawing.Color.White;
        exitBtn.Location = new System.Drawing.Point(1102, 0);
        exitBtn.Name = "exitBtn";
        exitBtn.Size = new System.Drawing.Size(50, 50);
        exitBtn.TabIndex = 8;
        exitBtn.Text = "×";
        exitBtn.UseVisualStyleBackColor = true;
        exitBtn.Click += exitBtn_Click;
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
        borderPanel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button acceptParcels;

    private System.Windows.Forms.Button minimizeBtn;

    private System.Windows.Forms.Button exitBtn;

    private System.Windows.Forms.Button adminToolsBtn;

    private System.Windows.Forms.Button optionsBtn;

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