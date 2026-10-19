#nullable disable

namespace SistemaInventario.Presentacion.Formularios
{
    partial class FrmFacturaEditar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.components = new System.ComponentModel.Container();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.colProductoNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridLineas = new System.Windows.Forms.DataGridView();
            this.panelMarcoLineas = new System.Windows.Forms.Panel();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblEstadoFactura = new System.Windows.Forms.Label();
            this.lblSubtotalEtiqueta = new System.Windows.Forms.Label();
            this.lblSubtotalValor = new System.Windows.Forms.Label();
            this.lblItbisEtiqueta = new System.Windows.Forms.Label();
            this.lblItbisValor = new System.Windows.Forms.Label();
            this.lblTotalEtiqueta = new System.Windows.Forms.Label();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.panelMarcoLineas.SuspendLayout();
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
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblCliente.Location = new System.Drawing.Point(24, 20);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(10, 17);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente *";
            // 
            // txtCliente
            // 
            this.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCliente.Location = new System.Drawing.Point(24, 42);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(500, 27);
            this.txtCliente.TabIndex = 1;
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblDocumento.Location = new System.Drawing.Point(548, 20);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(10, 17);
            this.lblDocumento.TabIndex = 2;
            this.lblDocumento.Text = "Cédula o RNC (opcional)";
            // 
            // txtDocumento
            // 
            this.txtDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocumento.Location = new System.Drawing.Point(548, 42);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.PlaceholderText = "001-1234567-8  o  131123456";
            this.txtDocumento.Size = new System.Drawing.Size(288, 27);
            this.txtDocumento.TabIndex = 3;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblProducto.Location = new System.Drawing.Point(24, 88);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(10, 17);
            this.lblProducto.TabIndex = 4;
            this.lblProducto.Text = "Producto";
            // 
            // cboProducto
            // 
            this.cboProducto.BackColor = System.Drawing.Color.White;
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboProducto.FormattingEnabled = true;
            this.cboProducto.Location = new System.Drawing.Point(24, 110);
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Size = new System.Drawing.Size(420, 27);
            this.cboProducto.TabIndex = 5;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblCantidad.Location = new System.Drawing.Point(460, 88);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(10, 17);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "Cantidad";
            // 
            // numCantidad
            // 
            this.numCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numCantidad.Location = new System.Drawing.Point(460, 110);
            this.numCantidad.Maximum = new decimal(new int[] {9999, 0, 0, 0});
            this.numCantidad.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(90, 27);
            this.numCantidad.TabIndex = 7;
            this.numCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCantidad.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(566, 106);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(130, 34);
            this.btnAgregar.TabIndex = 8;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // colProductoNombre
            // 
            this.colProductoNombre.DataPropertyName = "ProductoNombre";
            this.colProductoNombre.FillWeight = 46F;
            this.colProductoNombre.HeaderText = "Producto";
            this.colProductoNombre.MinimumWidth = 6;
            this.colProductoNombre.Name = "colProductoNombre";
            this.colProductoNombre.ReadOnly = true;
            this.colProductoNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colCantidad
            // 
            this.colCantidad.DataPropertyName = "Cantidad";
            this.colCantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.colCantidad.FillWeight = 12F;
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.MinimumWidth = 6;
            this.colCantidad.Name = "colCantidad";
            this.colCantidad.ReadOnly = true;
            this.colCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colPrecioUnitario
            // 
            this.colPrecioUnitario.DataPropertyName = "PrecioUnitario";
            this.colPrecioUnitario.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPrecioUnitario.FillWeight = 20F;
            this.colPrecioUnitario.HeaderText = "Precio (RD$)";
            this.colPrecioUnitario.MinimumWidth = 6;
            this.colPrecioUnitario.Name = "colPrecioUnitario";
            this.colPrecioUnitario.ReadOnly = true;
            this.colPrecioUnitario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSubtotal
            // 
            this.colSubtotal.DataPropertyName = "Subtotal";
            this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.colSubtotal.FillWeight = 22F;
            this.colSubtotal.HeaderText = "Importe (RD$)";
            this.colSubtotal.MinimumWidth = 6;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.ReadOnly = true;
            this.colSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gridLineas
            // 
            this.gridLineas.AllowUserToAddRows = false;
            this.gridLineas.AllowUserToDeleteRows = false;
            this.gridLineas.AllowUserToResizeRows = false;
            this.gridLineas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLineas.AutoGenerateColumns = false;
            this.gridLineas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLineas.BackgroundColor = System.Drawing.Color.White;
            this.gridLineas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridLineas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridLineas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridLineas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLineas.ColumnHeadersHeight = 38;
            this.gridLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridLineas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductoNombre,
            this.colCantidad,
            this.colPrecioUnitario,
            this.colSubtotal});
            this.gridLineas.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridLineas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLineas.EnableHeadersVisualStyles = false;
            this.gridLineas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLineas.GridColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.gridLineas.MultiSelect = false;
            this.gridLineas.Name = "gridLineas";
            this.gridLineas.ReadOnly = true;
            this.gridLineas.RowHeadersVisible = false;
            this.gridLineas.RowHeadersWidth = 51;
            this.gridLineas.RowTemplate.Height = 32;
            this.gridLineas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLineas.TabIndex = 0;
            // 
            // panelMarcoLineas
            // 
            this.panelMarcoLineas.Controls.Add(this.gridLineas);
            this.panelMarcoLineas.BackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.panelMarcoLineas.Location = new System.Drawing.Point(24, 152);
            this.panelMarcoLineas.Name = "panelMarcoLineas";
            this.panelMarcoLineas.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panelMarcoLineas.Size = new System.Drawing.Size(812, 300);
            this.panelMarcoLineas.TabIndex = 9;
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.White;
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnQuitar.FlatAppearance.BorderSize = 1;
            this.btnQuitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnQuitar.Location = new System.Drawing.Point(24, 460);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(130, 34);
            this.btnQuitar.TabIndex = 10;
            this.btnQuitar.Text = "Quitar línea";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // lblEstadoFactura
            // 
            this.lblEstadoFactura.AutoSize = true;
            this.lblEstadoFactura.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoFactura.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblEstadoFactura.Location = new System.Drawing.Point(24, 468);
            this.lblEstadoFactura.Name = "lblEstadoFactura";
            this.lblEstadoFactura.Size = new System.Drawing.Size(10, 17);
            this.lblEstadoFactura.TabIndex = 11;
            this.lblEstadoFactura.Text = "FACTURA ANULADA";
            this.lblEstadoFactura.Visible = false;
            // 
            // lblSubtotalEtiqueta
            // 
            this.lblSubtotalEtiqueta.AutoSize = true;
            this.lblSubtotalEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalEtiqueta.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblSubtotalEtiqueta.Location = new System.Drawing.Point(560, 464);
            this.lblSubtotalEtiqueta.Name = "lblSubtotalEtiqueta";
            this.lblSubtotalEtiqueta.Size = new System.Drawing.Size(10, 17);
            this.lblSubtotalEtiqueta.TabIndex = 12;
            this.lblSubtotalEtiqueta.Text = "Subtotal";
            // 
            // lblSubtotalValor
            // 
            this.lblSubtotalValor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtotalValor.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblSubtotalValor.Location = new System.Drawing.Point(680, 464);
            this.lblSubtotalValor.Name = "lblSubtotalValor";
            this.lblSubtotalValor.Size = new System.Drawing.Size(156, 20);
            this.lblSubtotalValor.TabIndex = 13;
            this.lblSubtotalValor.Text = "RD$ 0.00";
            this.lblSubtotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItbisEtiqueta
            // 
            this.lblItbisEtiqueta.AutoSize = true;
            this.lblItbisEtiqueta.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItbisEtiqueta.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblItbisEtiqueta.Location = new System.Drawing.Point(560, 492);
            this.lblItbisEtiqueta.Name = "lblItbisEtiqueta";
            this.lblItbisEtiqueta.Size = new System.Drawing.Size(10, 17);
            this.lblItbisEtiqueta.TabIndex = 14;
            this.lblItbisEtiqueta.Text = "ITBIS (18 %)";
            // 
            // lblItbisValor
            // 
            this.lblItbisValor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItbisValor.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblItbisValor.Location = new System.Drawing.Point(680, 492);
            this.lblItbisValor.Name = "lblItbisValor";
            this.lblItbisValor.Size = new System.Drawing.Size(156, 20);
            this.lblItbisValor.TabIndex = 15;
            this.lblItbisValor.Text = "RD$ 0.00";
            this.lblItbisValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalEtiqueta
            // 
            this.lblTotalEtiqueta.AutoSize = true;
            this.lblTotalEtiqueta.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEtiqueta.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTotalEtiqueta.Location = new System.Drawing.Point(560, 526);
            this.lblTotalEtiqueta.Name = "lblTotalEtiqueta";
            this.lblTotalEtiqueta.Size = new System.Drawing.Size(10, 17);
            this.lblTotalEtiqueta.TabIndex = 16;
            this.lblTotalEtiqueta.Text = "Total";
            // 
            // lblTotalValor
            // 
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblTotalValor.Location = new System.Drawing.Point(636, 520);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(200, 30);
            this.lblTotalValor.TabIndex = 17;
            this.lblTotalValor.Text = "RD$ 0.00";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(600, 590);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(118, 34);
            this.btnGuardar.TabIndex = 18;
            this.btnGuardar.Text = "Facturar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnCancelar.FlatAppearance.BorderSize = 1;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnCancelar.Location = new System.Drawing.Point(726, 590);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 34);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // errores
            // 
            this.errores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errores.ContainerControl = this;
            // 
            // FrmFacturaEditar
            // 
            this.Controls.Add(this.lblEstadoFactura);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.panelMarcoLineas);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.cboProducto);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblSubtotalEtiqueta);
            this.Controls.Add(this.lblSubtotalValor);
            this.Controls.Add(this.lblItbisEtiqueta);
            this.Controls.Add(this.lblItbisValor);
            this.Controls.Add(this.lblTotalEtiqueta);
            this.Controls.Add(this.lblTotalValor);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(860, 646);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFacturaEditar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva factura";
            this.panelMarcoLineas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductoNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.DataGridView gridLineas;
        private System.Windows.Forms.Panel panelMarcoLineas;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblEstadoFactura;
        private System.Windows.Forms.Label lblSubtotalEtiqueta;
        private System.Windows.Forms.Label lblSubtotalValor;
        private System.Windows.Forms.Label lblItbisEtiqueta;
        private System.Windows.Forms.Label lblItbisValor;
        private System.Windows.Forms.Label lblTotalEtiqueta;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errores;
    }
}
