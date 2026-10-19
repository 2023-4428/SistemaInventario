#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class UcFacturas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.panelAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.panelBarra = new System.Windows.Forms.Panel();
            this.colNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClienteNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClienteDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidadLineas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItbis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelMarco = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelBarra.SuspendLayout();
            this.panelMarco.SuspendLayout();
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
            // dataGridViewCellStyle4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            // 
            // dataGridViewCellStyle5
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            // 
            // dataGridViewCellStyle6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            // 
            // dataGridViewCellStyle7
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            // 
            // dataGridViewCellStyle8
            // 
            dataGridViewCellStyle8.Format = "dd/MM/yyyy HH:mm";
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
            this.lblTitulo.Text = "Facturación";
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
            this.lblSubtitulo.Text = "Cree facturas con ITBIS (18 %), consulte las emitidas y anule las que sean necesarias.";
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
            // btnNueva
            // 
            this.btnNueva.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnNueva.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNueva.FlatAppearance.BorderSize = 0;
            this.btnNueva.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnNueva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNueva.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNueva.ForeColor = System.Drawing.Color.White;
            this.btnNueva.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(150, 34);
            this.btnNueva.TabIndex = 0;
            this.btnNueva.Text = "+ Nueva factura";
            this.btnNueva.UseVisualStyleBackColor = false;
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            // 
            // btnVer
            // 
            this.btnVer.BackColor = System.Drawing.Color.White;
            this.btnVer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnVer.FlatAppearance.BorderSize = 1;
            this.btnVer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVer.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnVer.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(110, 34);
            this.btnVer.TabIndex = 1;
            this.btnVer.Text = "Ver detalle";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnAnular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnular.FlatAppearance.BorderSize = 0;
            this.btnAnular.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnular.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnular.ForeColor = System.Drawing.Color.White;
            this.btnAnular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(90, 34);
            this.btnAnular.TabIndex = 2;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnActualizar.FlatAppearance.BorderSize = 1;
            this.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 34);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.btnNueva);
            this.panelAcciones.Controls.Add(this.btnVer);
            this.panelAcciones.Controls.Add(this.btnAnular);
            this.panelAcciones.Controls.Add(this.btnActualizar);
            this.panelAcciones.AutoSize = true;
            this.panelAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelAcciones.WrapContents = false;
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.panelAcciones.Size = new System.Drawing.Size(474, 52);
            this.panelAcciones.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBuscar.Location = new System.Drawing.Point(0, 12);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Buscar por número, cliente, cédula/RNC o total...";
            this.txtBuscar.Size = new System.Drawing.Size(380, 27);
            this.txtBuscar.TabIndex = 0;
            // 
            // panelBarra
            // 
            this.panelBarra.Controls.Add(this.panelAcciones);
            this.panelBarra.Controls.Add(this.txtBuscar);
            this.panelBarra.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarra.Name = "panelBarra";
            this.panelBarra.Size = new System.Drawing.Size(964, 52);
            this.panelBarra.TabIndex = 1;
            // 
            // colNumero
            // 
            this.colNumero.DataPropertyName = "Numero";
            this.colNumero.FillWeight = 12F;
            this.colNumero.HeaderText = "Número";
            this.colNumero.MinimumWidth = 6;
            this.colNumero.Name = "colNumero";
            this.colNumero.ReadOnly = true;
            this.colNumero.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colClienteNombre
            // 
            this.colClienteNombre.DataPropertyName = "ClienteNombre";
            this.colClienteNombre.FillWeight = 26F;
            this.colClienteNombre.HeaderText = "Cliente";
            this.colClienteNombre.MinimumWidth = 6;
            this.colClienteNombre.Name = "colClienteNombre";
            this.colClienteNombre.ReadOnly = true;
            this.colClienteNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colClienteDocumento
            // 
            this.colClienteDocumento.DataPropertyName = "ClienteDocumento";
            this.colClienteDocumento.FillWeight = 14F;
            this.colClienteDocumento.HeaderText = "Cédula / RNC";
            this.colClienteDocumento.MinimumWidth = 6;
            this.colClienteDocumento.Name = "colClienteDocumento";
            this.colClienteDocumento.ReadOnly = true;
            this.colClienteDocumento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colCantidadLineas
            // 
            this.colCantidadLineas.DataPropertyName = "CantidadLineas";
            this.colCantidadLineas.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCantidadLineas.FillWeight = 7F;
            this.colCantidadLineas.HeaderText = "Líneas";
            this.colCantidadLineas.MinimumWidth = 6;
            this.colCantidadLineas.Name = "colCantidadLineas";
            this.colCantidadLineas.ReadOnly = true;
            this.colCantidadLineas.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSubtotal
            // 
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSubtotal.FillWeight = 12F;
            this.colSubtotal.HeaderText = "Subtotal (RD$)";
            this.colSubtotal.MinimumWidth = 6;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            this.colSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colItbis
            // 
            this.colItbis.DataPropertyName = "Itbis";
            this.colItbis.DefaultCellStyle = dataGridViewCellStyle6;
            this.colItbis.FillWeight = 11F;
            this.colItbis.HeaderText = "ITBIS (RD$)";
            this.colItbis.MinimumWidth = 6;
            this.colItbis.Name = "colItbis";
            this.colItbis.ReadOnly = true;
            this.colItbis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "Total";
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTotal.FillWeight = 12F;
            this.colTotal.HeaderText = "Total (RD$)";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.DataPropertyName = "FechaCreacion";
            this.colFechaCreacion.DefaultCellStyle = dataGridViewCellStyle8;
            this.colFechaCreacion.FillWeight = 14F;
            this.colFechaCreacion.HeaderText = "Fecha";
            this.colFechaCreacion.MinimumWidth = 6;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            this.colFechaCreacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumero,
            this.colClienteNombre,
            this.colClienteDocumento,
            this.colCantidadLineas,
            this.colSubtotal,
            this.colItbis,
            this.colTotal,
            this.colFechaCreacion});
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
            // panelMarco
            // 
            this.panelMarco.Controls.Add(this.grid);
            this.panelMarco.BackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.panelMarco.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMarco.Name = "panelMarco";
            this.panelMarco.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panelMarco.TabIndex = 3;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(964, 30);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcFacturas
            // 
            this.Controls.Add(this.panelMarco);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.panelBarra);
            this.Controls.Add(this.panelEncabezado);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcFacturas";
            this.Padding = new System.Windows.Forms.Padding(28, 22, 28, 14);
            this.Size = new System.Drawing.Size(1020, 760);
            this.panelEncabezado.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.panelBarra.ResumeLayout(false);
            this.panelMarco.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.FlowLayoutPanel panelAcciones;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel panelBarra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClienteNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClienteDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidadLineas;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItbis;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelMarco;
        private System.Windows.Forms.Label lblEstado;
    }
}
