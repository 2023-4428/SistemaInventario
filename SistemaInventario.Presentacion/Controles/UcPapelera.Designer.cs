#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class UcPapelera
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.listaCategorias = new UcPapeleraCategorias();
            this.paginaCategorias = new System.Windows.Forms.TabPage();
            this.listaSuplidores = new UcPapeleraSuplidores();
            this.paginaSuplidores = new System.Windows.Forms.TabPage();
            this.listaProductos = new UcPapeleraProductos();
            this.paginaProductos = new System.Windows.Forms.TabPage();
            this.listaFacturas = new UcPapeleraFacturas();
            this.paginaFacturas = new System.Windows.Forms.TabPage();
            this.pestanas = new System.Windows.Forms.TabControl();
            this.panelEncabezado.SuspendLayout();
            this.paginaCategorias.SuspendLayout();
            this.paginaSuplidores.SuspendLayout();
            this.paginaProductos.SuspendLayout();
            this.paginaFacturas.SuspendLayout();
            this.pestanas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(10, 17);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Papelera";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtitulo.Location = new System.Drawing.Point(2, 40);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(10, 17);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Registros eliminados lógicamente. Restáurelos cuando los necesite o elimínelos definitivamente (esto último no se puede deshacer).";
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.Controls.Add(this.lblSubtitulo);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(964, 70);
            this.panelEncabezado.TabIndex = 0;
            // 
            // listaCategorias
            // 
            this.listaCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listaCategorias.Location = new System.Drawing.Point(0, 0);
            this.listaCategorias.Name = "listaCategorias";
            this.listaCategorias.Size = new System.Drawing.Size(944, 400);
            this.listaCategorias.TabIndex = 0;
            // 
            // paginaCategorias
            // 
            this.paginaCategorias.Controls.Add(this.listaCategorias);
            this.paginaCategorias.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.paginaCategorias.Location = new System.Drawing.Point(4, 32);
            this.paginaCategorias.Name = "paginaCategorias";
            this.paginaCategorias.Size = new System.Drawing.Size(952, 430);
            this.paginaCategorias.TabIndex = 0;
            this.paginaCategorias.Text = "Categorías";
            this.paginaCategorias.UseVisualStyleBackColor = false;
            // 
            // listaSuplidores
            // 
            this.listaSuplidores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listaSuplidores.Location = new System.Drawing.Point(0, 0);
            this.listaSuplidores.Name = "listaSuplidores";
            this.listaSuplidores.Size = new System.Drawing.Size(944, 400);
            this.listaSuplidores.TabIndex = 0;
            // 
            // paginaSuplidores
            // 
            this.paginaSuplidores.Controls.Add(this.listaSuplidores);
            this.paginaSuplidores.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.paginaSuplidores.Location = new System.Drawing.Point(4, 32);
            this.paginaSuplidores.Name = "paginaSuplidores";
            this.paginaSuplidores.Size = new System.Drawing.Size(952, 430);
            this.paginaSuplidores.TabIndex = 1;
            this.paginaSuplidores.Text = "Suplidores";
            this.paginaSuplidores.UseVisualStyleBackColor = false;
            // 
            // listaProductos
            // 
            this.listaProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listaProductos.Location = new System.Drawing.Point(0, 0);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(944, 400);
            this.listaProductos.TabIndex = 0;
            // 
            // paginaProductos
            // 
            this.paginaProductos.Controls.Add(this.listaProductos);
            this.paginaProductos.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.paginaProductos.Location = new System.Drawing.Point(4, 32);
            this.paginaProductos.Name = "paginaProductos";
            this.paginaProductos.Size = new System.Drawing.Size(952, 430);
            this.paginaProductos.TabIndex = 2;
            this.paginaProductos.Text = "Productos";
            this.paginaProductos.UseVisualStyleBackColor = false;
            // 
            // listaFacturas
            // 
            this.listaFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listaFacturas.Location = new System.Drawing.Point(0, 0);
            this.listaFacturas.Name = "listaFacturas";
            this.listaFacturas.Size = new System.Drawing.Size(944, 400);
            this.listaFacturas.TabIndex = 0;
            // 
            // paginaFacturas
            // 
            this.paginaFacturas.Controls.Add(this.listaFacturas);
            this.paginaFacturas.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.paginaFacturas.Location = new System.Drawing.Point(4, 32);
            this.paginaFacturas.Name = "paginaFacturas";
            this.paginaFacturas.Size = new System.Drawing.Size(952, 430);
            this.paginaFacturas.TabIndex = 3;
            this.paginaFacturas.Text = "Facturas";
            this.paginaFacturas.UseVisualStyleBackColor = false;
            // 
            // pestanas
            // 
            this.pestanas.Controls.Add(this.paginaCategorias);
            this.pestanas.Controls.Add(this.paginaSuplidores);
            this.pestanas.Controls.Add(this.paginaProductos);
            this.pestanas.Controls.Add(this.paginaFacturas);
            this.pestanas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pestanas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pestanas.Location = new System.Drawing.Point(28, 92);
            this.pestanas.Name = "pestanas";
            this.pestanas.Padding = new System.Drawing.Point(18, 6);
            this.pestanas.SelectedIndex = 0;
            this.pestanas.Size = new System.Drawing.Size(960, 460);
            this.pestanas.TabIndex = 1;
            // 
            // UcPapelera
            // 
            this.Controls.Add(this.pestanas);
            this.Controls.Add(this.panelEncabezado);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcPapelera";
            this.Padding = new System.Windows.Forms.Padding(28, 22, 28, 14);
            this.Size = new System.Drawing.Size(1020, 760);
            this.panelEncabezado.ResumeLayout(false);
            this.paginaCategorias.ResumeLayout(false);
            this.paginaSuplidores.ResumeLayout(false);
            this.paginaProductos.ResumeLayout(false);
            this.paginaFacturas.ResumeLayout(false);
            this.pestanas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelEncabezado;
        private UcPapeleraCategorias listaCategorias;
        private System.Windows.Forms.TabPage paginaCategorias;
        private UcPapeleraSuplidores listaSuplidores;
        private System.Windows.Forms.TabPage paginaSuplidores;
        private UcPapeleraProductos listaProductos;
        private System.Windows.Forms.TabPage paginaProductos;
        private UcPapeleraFacturas listaFacturas;
        private System.Windows.Forms.TabPage paginaFacturas;
        private System.Windows.Forms.TabControl pestanas;
    }
}
