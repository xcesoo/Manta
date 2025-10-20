using System.ComponentModel;

namespace Manta.Presentation.Controls;

partial class StatusHistory
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
        status = new System.Windows.Forms.Label();
        changedById = new System.Windows.Forms.Label();
        changedByName = new System.Windows.Forms.Label();
        changedByEmail = new System.Windows.Forms.Label();
        changedByRole = new System.Windows.Forms.Label();
        changedAt = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // status
        // 
        status.Dock = System.Windows.Forms.DockStyle.Left;
        status.Font = new System.Drawing.Font("Arial", 9.75F);
        status.Location = new System.Drawing.Point(0, 0);
        status.Name = "status";
        status.Size = new System.Drawing.Size(150, 25);
        status.TabIndex = 0;
        status.Text = "Статус";
        status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // changedById
        // 
        changedById.Dock = System.Windows.Forms.DockStyle.Left;
        changedById.Font = new System.Drawing.Font("Arial", 9.75F);
        changedById.Location = new System.Drawing.Point(150, 0);
        changedById.Name = "changedById";
        changedById.Size = new System.Drawing.Size(25, 25);
        changedById.TabIndex = 1;
        changedById.Text = "№";
        changedById.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // changedByName
        // 
        changedByName.Dock = System.Windows.Forms.DockStyle.Left;
        changedByName.Font = new System.Drawing.Font("Arial", 9.75F);
        changedByName.Location = new System.Drawing.Point(175, 0);
        changedByName.Name = "changedByName";
        changedByName.Size = new System.Drawing.Size(150, 25);
        changedByName.TabIndex = 2;
        changedByName.Text = "ПІБ";
        changedByName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // changedByEmail
        // 
        changedByEmail.Dock = System.Windows.Forms.DockStyle.Left;
        changedByEmail.Font = new System.Drawing.Font("Arial", 9.75F);
        changedByEmail.Location = new System.Drawing.Point(325, 0);
        changedByEmail.Name = "changedByEmail";
        changedByEmail.Size = new System.Drawing.Size(150, 25);
        changedByEmail.TabIndex = 3;
        changedByEmail.Text = "Email";
        changedByEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // changedByRole
        // 
        changedByRole.Dock = System.Windows.Forms.DockStyle.Left;
        changedByRole.Font = new System.Drawing.Font("Arial", 9.75F);
        changedByRole.Location = new System.Drawing.Point(475, 0);
        changedByRole.Name = "changedByRole";
        changedByRole.Size = new System.Drawing.Size(100, 25);
        changedByRole.TabIndex = 4;
        changedByRole.Text = "Роль";
        changedByRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // changedAt
        // 
        changedAt.Dock = System.Windows.Forms.DockStyle.Left;
        changedAt.Font = new System.Drawing.Font("Arial", 9.75F);
        changedAt.Location = new System.Drawing.Point(575, 0);
        changedAt.Name = "changedAt";
        changedAt.Size = new System.Drawing.Size(165, 25);
        changedAt.TabIndex = 5;
        changedAt.Text = "Змінено";
        changedAt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // StatusHistory
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(changedAt);
        Controls.Add(changedByRole);
        Controls.Add(changedByEmail);
        Controls.Add(changedByName);
        Controls.Add(changedById);
        Controls.Add(status);
        Size = new System.Drawing.Size(740, 25);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label changedById;
    private System.Windows.Forms.Label changedByName;
    private System.Windows.Forms.Label changedByEmail;
    private System.Windows.Forms.Label changedByRole;
    private System.Windows.Forms.Label changedAt;

    private System.Windows.Forms.Label status;

    #endregion
}