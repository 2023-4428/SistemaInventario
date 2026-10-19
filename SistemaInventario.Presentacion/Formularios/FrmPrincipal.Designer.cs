#nullable disable

namespace SistemaInventario.Presentacion.Formularios
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.flujoNavegacion = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPie = new System.Windows.Forms.Panel();
            this.btnErrores = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.panelLateral.SuspendLayout();
            this.panelPie.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(240, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1020, 760);
            this.panelContenido.TabIndex = 0;
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.panelLateral.Controls.Add(this.flujoNavegacion);
            this.panelLateral.Controls.Add(this.panelPie);
            this.panelLateral.Controls.Add(this.lblMarca);
            this.panelLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.Size = new System.Drawing.Size(240, 760);
            this.panelLateral.TabIndex = 1;
            // 
            // flujoNavegacion
            // 
            this.flujoNavegacion.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.flujoNavegacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flujoNavegacion.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flujoNavegacion.Location = new System.Drawing.Point(0, 104);
            this.flujoNavegacion.Name = "flujoNavegacion";
            this.flujoNavegacion.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.flujoNavegacion.Size = new System.Drawing.Size(240, 560);
            this.flujoNavegacion.TabIndex = 1;
            this.flujoNavegacion.WrapContents = false;
            // 
            // panelPie
            // 
            this.panelPie.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.panelPie.Controls.Add(this.btnErrores);
            this.panelPie.Controls.Add(this.lblVersion);
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Location = new System.Drawing.Point(0, 664);
            this.panelPie.Name = "panelPie";
            this.panelPie.Size = new System.Drawing.Size(240, 96);
            this.panelPie.TabIndex = 2;
            // 
            // btnErrores
            // 
            this.btnErrores.BackColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnErrores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnErrores.FlatAppearance.BorderSize = 0;
            this.btnErrores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.btnErrores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnErrores.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnErrores.ForeColor = System.Drawing.Color.White;
            this.btnErrores.Location = new System.Drawing.Point(0, 8);
            this.btnErrores.Margin = new System.Windows.Forms.Padding(0);
            this.btnErrores.Name = "btnErrores";
            this.btnErrores.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnErrores.Size = new System.Drawing.Size(240, 40);
            this.btnErrores.TabIndex = 0;
            this.btnErrores.Text = "Ver registro de errores";
            this.btnErrores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnErrores.UseVisualStyleBackColor = false;
            this.btnErrores.Click += new System.EventHandler(this.btnErrores_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblVersion.Location = new System.Drawing.Point(22, 60);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(176, 15);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "ISW-124 · Programación Avanzada";
            // 
            // lblMarca
            // 
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(0, 0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.lblMarca.Size = new System.Drawing.Size(240, 104);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "Sistema de\r\nInventario";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.ClientSize = new System.Drawing.Size(1260, 760);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelLateral);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1060, 660);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Inventario";
            this.panelLateral.ResumeLayout(false);
            this.panelPie.ResumeLayout(false);
            this.panelPie.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.FlowLayoutPanel flujoNavegacion;
        private System.Windows.Forms.Panel panelPie;
        private System.Windows.Forms.Button btnErrores;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblMarca;
    }
}
