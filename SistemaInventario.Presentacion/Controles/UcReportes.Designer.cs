#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class UcReportes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lista = new System.Windows.Forms.ListBox();
            this.panelMarcoLista = new System.Windows.Forms.Panel();
            this.lblReporteTitulo = new System.Windows.Forms.Label();
            this.lblReporteDescripcion = new System.Windows.Forms.Label();
            this.lblReporteTecnica = new System.Windows.Forms.Label();
            this.panelCabecera = new System.Windows.Forms.Panel();
            this.lblParamCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.lblParamPrecio = new System.Windows.Forms.Label();
            this.txtMinimo = new System.Windows.Forms.TextBox();
            this.lblParamY = new System.Windows.Forms.Label();
            this.txtMaximo = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.panelParametros = new System.Windows.Forms.FlowLayoutPanel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelMarcoGrid = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelMarcoLista.SuspendLayout();
            this.panelCabecera.SuspendLayout();
            this.panelParametros.SuspendLayout();
            this.panelMarcoGrid.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewCellStyle1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewCellStyle2
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            // 
            // dataGridViewCellStyle3
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
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
            this.lblTitulo.Text = "Reportes (LINQ)";
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
            this.lblSubtitulo.Text = "Consultas LINQ de la capa de negocio sobre los datos activos del inventario.";
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
            // lista
            // 
            this.lista.BackColor = System.Drawing.Color.White;
            this.lista.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lista.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lista.FormattingEnabled = false;
            this.lista.IntegralHeight = false;
            this.lista.ItemHeight = 44;
            this.lista.Location = new System.Drawing.Point(1, 1);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(278, 400);
            this.lista.TabIndex = 0;
            this.lista.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lista_DrawItem);
            this.lista.SelectedIndexChanged += new System.EventHandler(this.lista_SelectedIndexChanged);
            // 
            // panelMarcoLista
            // 
            this.panelMarcoLista.Controls.Add(this.lista);
            this.panelMarcoLista.BackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.panelMarcoLista.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMarcoLista.Name = "panelMarcoLista";
            this.panelMarcoLista.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panelMarcoLista.Size = new System.Drawing.Size(280, 400);
            this.panelMarcoLista.TabIndex = 1;
            // 
            // lblReporteTitulo
            // 
            this.lblReporteTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReporteTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporteTitulo.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblReporteTitulo.Name = "lblReporteTitulo";
            this.lblReporteTitulo.Size = new System.Drawing.Size(660, 34);
            this.lblReporteTitulo.TabIndex = 0;
            this.lblReporteTitulo.Text = "";
            // 
            // lblReporteDescripcion
            // 
            this.lblReporteDescripcion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReporteDescripcion.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporteDescripcion.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblReporteDescripcion.Name = "lblReporteDescripcion";
            this.lblReporteDescripcion.Size = new System.Drawing.Size(660, 44);
            this.lblReporteDescripcion.TabIndex = 1;
            this.lblReporteDescripcion.Text = "";
            // 
            // lblReporteTecnica
            // 
            this.lblReporteTecnica.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblReporteTecnica.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporteTecnica.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblReporteTecnica.Name = "lblReporteTecnica";
            this.lblReporteTecnica.Size = new System.Drawing.Size(660, 28);
            this.lblReporteTecnica.TabIndex = 2;
            this.lblReporteTecnica.Text = "";
            // 
            // panelCabecera
            // 
            this.panelCabecera.Controls.Add(this.lblReporteTecnica);
            this.panelCabecera.Controls.Add(this.lblReporteDescripcion);
            this.panelCabecera.Controls.Add(this.lblReporteTitulo);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(660, 108);
            this.panelCabecera.TabIndex = 0;
            // 
            // lblParamCategoria
            // 
            this.lblParamCategoria.AutoSize = true;
            this.lblParamCategoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParamCategoria.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblParamCategoria.Name = "lblParamCategoria";
            this.lblParamCategoria.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblParamCategoria.Size = new System.Drawing.Size(10, 17);
            this.lblParamCategoria.TabIndex = 0;
            this.lblParamCategoria.Text = "Categoría";
            // 
            // cboCategoria
            // 
            this.cboCategoria.BackColor = System.Drawing.Color.White;
            this.cboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategoria.FormattingEnabled = true;
            this.cboCategoria.Location = new System.Drawing.Point(0, 3);
            this.cboCategoria.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.cboCategoria.Name = "cboCategoria";
            this.cboCategoria.Size = new System.Drawing.Size(200, 27);
            this.cboCategoria.TabIndex = 1;
            this.cboCategoria.SelectedIndexChanged += new System.EventHandler(this.cboCategoria_SelectedIndexChanged);
            // 
            // lblParamPrecio
            // 
            this.lblParamPrecio.AutoSize = true;
            this.lblParamPrecio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParamPrecio.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblParamPrecio.Name = "lblParamPrecio";
            this.lblParamPrecio.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblParamPrecio.Size = new System.Drawing.Size(10, 17);
            this.lblParamPrecio.TabIndex = 2;
            this.lblParamPrecio.Text = "Precio entre";
            // 
            // txtMinimo
            // 
            this.txtMinimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinimo.MaxLength = 14;
            this.txtMinimo.Location = new System.Drawing.Point(0, 3);
            this.txtMinimo.Margin = new System.Windows.Forms.Padding(0, 3, 6, 0);
            this.txtMinimo.Name = "txtMinimo";
            this.txtMinimo.PlaceholderText = "Mínimo";
            this.txtMinimo.Size = new System.Drawing.Size(90, 27);
            this.txtMinimo.TabIndex = 3;
            // 
            // lblParamY
            // 
            this.lblParamY.AutoSize = true;
            this.lblParamY.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParamY.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblParamY.Name = "lblParamY";
            this.lblParamY.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblParamY.Size = new System.Drawing.Size(10, 17);
            this.lblParamY.TabIndex = 4;
            this.lblParamY.Text = "y";
            // 
            // txtMaximo
            // 
            this.txtMaximo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaximo.MaxLength = 14;
            this.txtMaximo.Location = new System.Drawing.Point(0, 3);
            this.txtMaximo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.txtMaximo.Name = "txtMaximo";
            this.txtMaximo.PlaceholderText = "Máximo";
            this.txtMaximo.Size = new System.Drawing.Size(90, 27);
            this.txtMaximo.TabIndex = 5;
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.FlatAppearance.BorderSize = 0;
            this.btnConsultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.ForeColor = System.Drawing.Color.White;
            this.btnConsultar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 27);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // panelParametros
            // 
            this.panelParametros.Controls.Add(this.lblParamCategoria);
            this.panelParametros.Controls.Add(this.cboCategoria);
            this.panelParametros.Controls.Add(this.lblParamPrecio);
            this.panelParametros.Controls.Add(this.txtMinimo);
            this.panelParametros.Controls.Add(this.lblParamY);
            this.panelParametros.Controls.Add(this.txtMaximo);
            this.panelParametros.Controls.Add(this.btnConsultar);
            this.panelParametros.AutoSize = true;
            this.panelParametros.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelParametros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelParametros.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelParametros.Name = "panelParametros";
            this.panelParametros.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelParametros.Size = new System.Drawing.Size(660, 47);
            this.panelParametros.TabIndex = 1;
            this.panelParametros.Visible = false;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grid.AutoGenerateColumns = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 38;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grid.GridColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 51;
            this.grid.RowTemplate.Height = 32;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.TabIndex = 0;
            // 
            // panelMarcoGrid
            // 
            this.panelMarcoGrid.Controls.Add(this.grid);
            this.panelMarcoGrid.BackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.panelMarcoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMarcoGrid.Name = "panelMarcoGrid";
            this.panelMarcoGrid.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panelMarcoGrid.TabIndex = 2;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(660, 28);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDetalle
            // 
            this.panelDetalle.Controls.Add(this.panelMarcoGrid);
            this.panelDetalle.Controls.Add(this.lblEstado);
            this.panelDetalle.Controls.Add(this.panelParametros);
            this.panelDetalle.Controls.Add(this.panelCabecera);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(18, 0, 0, 0);
            this.panelDetalle.Size = new System.Drawing.Size(678, 400);
            this.panelDetalle.TabIndex = 2;
            // 
            // errores
            // 
            this.errores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errores.ContainerControl = this;
            // 
            // UcReportes
            // 
            this.Controls.Add(this.panelDetalle);
            this.Controls.Add(this.panelMarcoLista);
            this.Controls.Add(this.panelEncabezado);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcReportes";
            this.Padding = new System.Windows.Forms.Padding(28, 22, 28, 14);
            this.Size = new System.Drawing.Size(1020, 760);
            this.panelEncabezado.ResumeLayout(false);
            this.panelMarcoLista.ResumeLayout(false);
            this.panelCabecera.ResumeLayout(false);
            this.panelParametros.ResumeLayout(false);
            this.panelParametros.PerformLayout();
            this.panelMarcoGrid.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.ListBox lista;
        private System.Windows.Forms.Panel panelMarcoLista;
        private System.Windows.Forms.Label lblReporteTitulo;
        private System.Windows.Forms.Label lblReporteDescripcion;
        private System.Windows.Forms.Label lblReporteTecnica;
        private System.Windows.Forms.Panel panelCabecera;
        private System.Windows.Forms.Label lblParamCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Label lblParamPrecio;
        private System.Windows.Forms.TextBox txtMinimo;
        private System.Windows.Forms.Label lblParamY;
        private System.Windows.Forms.TextBox txtMaximo;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.FlowLayoutPanel panelParametros;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelMarcoGrid;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Panel panelDetalle;
        private System.Windows.Forms.ErrorProvider errores;
    }
}
