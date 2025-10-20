using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class ShipmentInfo
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
        panel1 = new System.Windows.Forms.Panel();
        storage = new System.Windows.Forms.Label();
        currentStatus = new System.Windows.Forms.Label();
        weight = new System.Windows.Forms.Label();
        panel2 = new System.Windows.Forms.Panel();
        id = new System.Windows.Forms.Label();
        panel3 = new System.Windows.Forms.Panel();
        createdAt = new System.Windows.Forms.Label();
        panel4 = new System.Windows.Forms.Panel();
        recipientName = new System.Windows.Forms.Label();
        recipientPhoneNumber = new System.Windows.Forms.Label();
        statusHistoryFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
        label1 = new System.Windows.Forms.Label();
        panel5 = new System.Windows.Forms.Panel();
        paid = new System.Windows.Forms.Label();
        amoutDue = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        panel6 = new System.Windows.Forms.Panel();
        senderEmail = new System.Windows.Forms.Label();
        senderName = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        cashDeskBtn = new System.Windows.Forms.Button();
        shipmentCancel = new System.Windows.Forms.Button();
        panel1.SuspendLayout();
        panel3.SuspendLayout();
        panel4.SuspendLayout();
        panel5.SuspendLayout();
        panel6.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = System.Drawing.Color.FromArgb(((int)((byte)221)), ((int)((byte)238)), ((int)((byte)200)));
        panel1.Controls.Add(storage);
        panel1.Controls.Add(currentStatus);
        panel1.Controls.Add(weight);
        panel1.Controls.Add(panel2);
        panel1.Controls.Add(id);
        panel1.Dock = System.Windows.Forms.DockStyle.Top;
        panel1.Location = new System.Drawing.Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new System.Drawing.Size(772, 50);
        panel1.TabIndex = 0;
        // 
        // storage
        // 
        storage.Dock = System.Windows.Forms.DockStyle.Right;
        storage.Font = new System.Drawing.Font("Arial", 9.75F);
        storage.Location = new System.Drawing.Point(372, 0);
        storage.Name = "storage";
        storage.Size = new System.Drawing.Size(200, 50);
        storage.TabIndex = 4;
        storage.Text = "Зберігання до";
        storage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // currentStatus
        // 
        currentStatus.Dock = System.Windows.Forms.DockStyle.Right;
        currentStatus.Font = new System.Drawing.Font("Arial", 9.75F);
        currentStatus.Location = new System.Drawing.Point(572, 0);
        currentStatus.Name = "currentStatus";
        currentStatus.Size = new System.Drawing.Size(200, 50);
        currentStatus.TabIndex = 3;
        currentStatus.Text = "Статус";
        currentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // weight
        // 
        weight.Dock = System.Windows.Forms.DockStyle.Left;
        weight.Font = new System.Drawing.Font("Arial", 9.75F);
        weight.Location = new System.Drawing.Point(225, 0);
        weight.Name = "weight";
        weight.Size = new System.Drawing.Size(125, 50);
        weight.TabIndex = 2;
        weight.Text = "Вага";
        weight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel2
        // 
        panel2.BackColor = System.Drawing.Color.White;
        panel2.Dock = System.Windows.Forms.DockStyle.Left;
        panel2.Location = new System.Drawing.Point(200, 0);
        panel2.Name = "panel2";
        panel2.Size = new System.Drawing.Size(25, 50);
        panel2.TabIndex = 1;
        // 
        // id
        // 
        id.Dock = System.Windows.Forms.DockStyle.Left;
        id.Font = new System.Drawing.Font("Arial", 9.75F);
        id.Location = new System.Drawing.Point(0, 0);
        id.Name = "id";
        id.Size = new System.Drawing.Size(200, 50);
        id.TabIndex = 0;
        id.Text = "Номер відправлення";
        id.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // panel3
        // 
        panel3.Controls.Add(createdAt);
        panel3.Dock = System.Windows.Forms.DockStyle.Top;
        panel3.Location = new System.Drawing.Point(0, 50);
        panel3.Name = "panel3";
        panel3.Size = new System.Drawing.Size(772, 25);
        panel3.TabIndex = 1;
        // 
        // createdAt
        // 
        createdAt.Dock = System.Windows.Forms.DockStyle.Left;
        createdAt.Font = new System.Drawing.Font("Arial", 9.75F);
        createdAt.ForeColor = System.Drawing.Color.DimGray;
        createdAt.Location = new System.Drawing.Point(0, 0);
        createdAt.Name = "createdAt";
        createdAt.Size = new System.Drawing.Size(235, 25);
        createdAt.TabIndex = 5;
        createdAt.Text = "Створений - ";
        createdAt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // panel4
        // 
        panel4.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)250)), ((int)((byte)250)));
        panel4.Controls.Add(recipientName);
        panel4.Controls.Add(recipientPhoneNumber);
        panel4.Dock = System.Windows.Forms.DockStyle.Top;
        panel4.Location = new System.Drawing.Point(0, 75);
        panel4.Name = "panel4";
        panel4.Size = new System.Drawing.Size(772, 100);
        panel4.TabIndex = 6;
        // 
        // recipientName
        // 
        recipientName.Dock = System.Windows.Forms.DockStyle.Top;
        recipientName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        recipientName.Location = new System.Drawing.Point(0, 50);
        recipientName.Name = "recipientName";
        recipientName.Size = new System.Drawing.Size(772, 50);
        recipientName.TabIndex = 2;
        recipientName.Text = "Отримувач";
        recipientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // recipientPhoneNumber
        // 
        recipientPhoneNumber.Dock = System.Windows.Forms.DockStyle.Top;
        recipientPhoneNumber.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)204));
        recipientPhoneNumber.Location = new System.Drawing.Point(0, 0);
        recipientPhoneNumber.Name = "recipientPhoneNumber";
        recipientPhoneNumber.Size = new System.Drawing.Size(772, 50);
        recipientPhoneNumber.TabIndex = 1;
        recipientPhoneNumber.Text = "Номер телефона";
        recipientPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // statusHistoryFlowPanel
        // 
        statusHistoryFlowPanel.AutoScroll = true;
        statusHistoryFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        statusHistoryFlowPanel.WrapContents = false;
        statusHistoryFlowPanel.Dock = System.Windows.Forms.DockStyle.Top;
        statusHistoryFlowPanel.Location = new System.Drawing.Point(0, 175);
        statusHistoryFlowPanel.Name = "statusHistoryFlowPanel";
        statusHistoryFlowPanel.Size = new System.Drawing.Size(772, 100);
        statusHistoryFlowPanel.TabIndex = 7;
        // 
        // label1
        // 
        label1.Dock = System.Windows.Forms.DockStyle.Top;
        label1.Font = new System.Drawing.Font("Arial", 9.75F);
        label1.Location = new System.Drawing.Point(0, 275);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(772, 25);
        label1.TabIndex = 8;
        label1.Text = "Деталі замовлення";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // panel5
        // 
        panel5.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)250)), ((int)((byte)250)));
        panel5.Controls.Add(paid);
        panel5.Controls.Add(amoutDue);
        panel5.Controls.Add(label2);
        panel5.Dock = System.Windows.Forms.DockStyle.Top;
        panel5.Location = new System.Drawing.Point(0, 300);
        panel5.Name = "panel5";
        panel5.Size = new System.Drawing.Size(772, 75);
        panel5.TabIndex = 9;
        // 
        // paid
        // 
        paid.Dock = System.Windows.Forms.DockStyle.Top;
        paid.Font = new System.Drawing.Font("Arial", 9.75F);
        paid.ForeColor = System.Drawing.Color.DimGray;
        paid.Location = new System.Drawing.Point(0, 50);
        paid.Name = "paid";
        paid.Size = new System.Drawing.Size(772, 25);
        paid.TabIndex = 4;
        paid.Text = "Оплата";
        paid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // amoutDue
        // 
        amoutDue.Dock = System.Windows.Forms.DockStyle.Top;
        amoutDue.Font = new System.Drawing.Font("Arial", 9.75F);
        amoutDue.ForeColor = System.Drawing.Color.DimGray;
        amoutDue.Location = new System.Drawing.Point(0, 25);
        amoutDue.Name = "amoutDue";
        amoutDue.Size = new System.Drawing.Size(772, 25);
        amoutDue.TabIndex = 3;
        amoutDue.Text = "Сума оплати";
        amoutDue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label2
        // 
        label2.Dock = System.Windows.Forms.DockStyle.Top;
        label2.Font = new System.Drawing.Font("Arial", 9.75F);
        label2.Location = new System.Drawing.Point(0, 0);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(772, 25);
        label2.TabIndex = 2;
        label2.Text = "Інформація про оплату:";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // panel6
        // 
        panel6.BackColor = System.Drawing.Color.FromArgb(((int)((byte)250)), ((int)((byte)250)), ((int)((byte)250)));
        panel6.Controls.Add(senderEmail);
        panel6.Controls.Add(senderName);
        panel6.Controls.Add(label3);
        panel6.Dock = System.Windows.Forms.DockStyle.Top;
        panel6.Location = new System.Drawing.Point(0, 375);
        panel6.Name = "panel6";
        panel6.Size = new System.Drawing.Size(772, 75);
        panel6.TabIndex = 10;
        // 
        // senderEmail
        // 
        senderEmail.Dock = System.Windows.Forms.DockStyle.Top;
        senderEmail.Font = new System.Drawing.Font("Arial", 9.75F);
        senderEmail.ForeColor = System.Drawing.Color.DimGray;
        senderEmail.Location = new System.Drawing.Point(0, 50);
        senderEmail.Name = "senderEmail";
        senderEmail.Size = new System.Drawing.Size(772, 25);
        senderEmail.TabIndex = 4;
        senderEmail.Text = "Email відправника";
        senderEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // senderName
        // 
        senderName.Dock = System.Windows.Forms.DockStyle.Top;
        senderName.Font = new System.Drawing.Font("Arial", 9.75F);
        senderName.ForeColor = System.Drawing.Color.DimGray;
        senderName.Location = new System.Drawing.Point(0, 25);
        senderName.Name = "senderName";
        senderName.Size = new System.Drawing.Size(772, 25);
        senderName.TabIndex = 3;
        senderName.Text = "Імʼя відправника";
        senderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label3
        // 
        label3.Dock = System.Windows.Forms.DockStyle.Top;
        label3.Font = new System.Drawing.Font("Arial", 9.75F);
        label3.Location = new System.Drawing.Point(0, 0);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(772, 25);
        label3.TabIndex = 2;
        label3.Text = "Відправник:";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // cashDeskBtn
        // 
        cashDeskBtn.BackColor = System.Drawing.Color.FromArgb(((int)((byte)104)), ((int)((byte)159)), ((int)((byte)56)));
        cashDeskBtn.FlatAppearance.BorderSize = 0;
        cashDeskBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        cashDeskBtn.Font = new System.Drawing.Font("Arial", 9.75F);
        cashDeskBtn.ForeColor = System.Drawing.Color.White;
        cashDeskBtn.Location = new System.Drawing.Point(3, 469);
        cashDeskBtn.Name = "cashDeskBtn";
        cashDeskBtn.Size = new System.Drawing.Size(150, 50);
        cashDeskBtn.TabIndex = 11;
        cashDeskBtn.Text = "Додати до кошика";
        cashDeskBtn.UseVisualStyleBackColor = false;
        cashDeskBtn.Click += cashDeskBtn_Click;
        // 
        // shipmentCancel
        // 
        shipmentCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        shipmentCancel.Font = new System.Drawing.Font("Arial", 9.75F);
        shipmentCancel.ForeColor = System.Drawing.Color.FromArgb(((int)((byte)255)), ((int)((byte)87)), ((int)((byte)34)));
        shipmentCancel.Location = new System.Drawing.Point(159, 469);
        shipmentCancel.Name = "shipmentCancel";
        shipmentCancel.Size = new System.Drawing.Size(125, 50);
        shipmentCancel.TabIndex = 12;
        shipmentCancel.Text = "Відмовляється";
        shipmentCancel.UseVisualStyleBackColor = true;
        shipmentCancel.Click += shipmentCancel_Click;
        // 
        // ShipmentInfo
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(shipmentCancel);
        Controls.Add(cashDeskBtn);
        Controls.Add(panel6);
        Controls.Add(panel5);
        Controls.Add(label1);
        Controls.Add(statusHistoryFlowPanel);
        Controls.Add(panel4);
        Controls.Add(panel3);
        Controls.Add(panel1);
        Size = new System.Drawing.Size(772, 522);
        panel1.ResumeLayout(false);
        panel3.ResumeLayout(false);
        panel4.ResumeLayout(false);
        panel5.ResumeLayout(false);
        panel6.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button shipmentCancel;

    private System.Windows.Forms.Button cashDeskBtn;

    private System.Windows.Forms.Label amoutDue;
    private System.Windows.Forms.Label paid;
    private System.Windows.Forms.Label senderName;
    private System.Windows.Forms.Label senderEmail;

    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Panel panel5;

    private System.Windows.Forms.Label id;

    private System.Windows.Forms.Label weight;

    private System.Windows.Forms.Label currentStatus;

    private System.Windows.Forms.Label recipientName;
    private System.Windows.Forms.FlowLayoutPanel statusHistoryFlowPanel;

    private System.Windows.Forms.Label recipientPhoneNumber;

    private System.Windows.Forms.Panel panel4;

    private System.Windows.Forms.Label createdAt;

    private System.Windows.Forms.Panel panel3;

    private System.Windows.Forms.Label storage;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Panel panel2;

    private System.Windows.Forms.Label parcelId;

    private System.Windows.Forms.Panel panel1;

    #endregion
}