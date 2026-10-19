#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class UcProductos
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
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.panelAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.panelBarra = new System.Windows.Forms.Panel();
            this.lblFiltroCategoria = new System.Windows.Forms.Label();
            this.cboCategoria = new System.Windows.Forms.ComboBox();
            this.lblFiltroSuplidor = new System.Windows.Forms.Label();
            this.cboSuplidor = new System.Windows.Forms.ComboBox();
            this.lblFiltroPrecio = new System.Windows.Forms.Label();
            this.txtPrecioMinimo = new System.Windows.Forms.TextBox();
            this.lblFiltroY = new System.Windows.Forms.Label();
            this.txtPrecioMaximo = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panelFiltros = new System.Windows.Forms.FlowLayoutPanel();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoriaNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSuplidorNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelMarco = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.panelBarra.SuspendLayout();
            this.panelFiltros.SuspendLayout();
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
            dataGridViewCellStyle4.Format = "N2";
            // 
            // dataGridViewCellStyle5
            // 
            dataGridViewCellStyle5.Format = "dd/MM/yyyy HH:mm";
            // 
            // dataGridViewCellStyle6
            // 
            dataGridViewCellStyle6.Format = "dd/MM/yyyy HH:mm";
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
            this.lblTitulo.Text = "Productos";
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
            this.lblSubtitulo.Text = "Administre el inventario y filtre por categoría, suplidor o rango de precios.";
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
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(100, 34);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "+ Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.White;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnEditar.FlatAppearance.BorderSize = 1;
            this.btnEditar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(90, 34);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(100, 34);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            this.panelAcciones.Controls.Add(this.btnNuevo);
            this.panelAcciones.Controls.Add(this.btnEditar);
            this.panelAcciones.Controls.Add(this.btnEliminar);
            this.panelAcciones.Controls.Add(this.btnActualizar);
            this.panelAcciones.AutoSize = true;
            this.panelAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelAcciones.WrapContents = false;
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.panelAcciones.Size = new System.Drawing.Size(414, 52);
            this.panelAcciones.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBuscar.Location = new System.Drawing.Point(0, 12);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Buscar... (filtra mientras escribe)";
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
            // lblFiltroCategoria
            // 
            this.lblFiltroCategoria.AutoSize = true;
            this.lblFiltroCategoria.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroCategoria.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFiltroCategoria.Name = "lblFiltroCategoria";
            this.lblFiltroCategoria.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblFiltroCategoria.Size = new System.Drawing.Size(10, 17);
            this.lblFiltroCategoria.TabIndex = 0;
            this.lblFiltroCategoria.Text = "Categoría";
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
            this.cboCategoria.Size = new System.Drawing.Size(190, 27);
            this.cboCategoria.TabIndex = 1;
            this.cboCategoria.SelectedIndexChanged += new System.EventHandler(this.cboCategoria_SelectedIndexChanged);
            // 
            // lblFiltroSuplidor
            // 
            this.lblFiltroSuplidor.AutoSize = true;
            this.lblFiltroSuplidor.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroSuplidor.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFiltroSuplidor.Name = "lblFiltroSuplidor";
            this.lblFiltroSuplidor.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblFiltroSuplidor.Size = new System.Drawing.Size(10, 17);
            this.lblFiltroSuplidor.TabIndex = 2;
            this.lblFiltroSuplidor.Text = "Suplidor";
            // 
            // cboSuplidor
            // 
            this.cboSuplidor.BackColor = System.Drawing.Color.White;
            this.cboSuplidor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSuplidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSuplidor.FormattingEnabled = true;
            this.cboSuplidor.Location = new System.Drawing.Point(0, 3);
            this.cboSuplidor.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.cboSuplidor.Name = "cboSuplidor";
            this.cboSuplidor.Size = new System.Drawing.Size(220, 27);
            this.cboSuplidor.TabIndex = 3;
            this.cboSuplidor.SelectedIndexChanged += new System.EventHandler(this.cboSuplidor_SelectedIndexChanged);
            // 
            // lblFiltroPrecio
            // 
            this.lblFiltroPrecio.AutoSize = true;
            this.lblFiltroPrecio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroPrecio.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFiltroPrecio.Name = "lblFiltroPrecio";
            this.lblFiltroPrecio.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblFiltroPrecio.Size = new System.Drawing.Size(10, 17);
            this.lblFiltroPrecio.TabIndex = 4;
            this.lblFiltroPrecio.Text = "Precio entre";
            // 
            // txtPrecioMinimo
            // 
            this.txtPrecioMinimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioMinimo.MaxLength = 14;
            this.txtPrecioMinimo.Location = new System.Drawing.Point(0, 3);
            this.txtPrecioMinimo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.txtPrecioMinimo.Name = "txtPrecioMinimo";
            this.txtPrecioMinimo.PlaceholderText = "Mínimo";
            this.txtPrecioMinimo.Size = new System.Drawing.Size(90, 27);
            this.txtPrecioMinimo.TabIndex = 5;
            this.txtPrecioMinimo.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // lblFiltroY
            // 
            this.lblFiltroY.AutoSize = true;
            this.lblFiltroY.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroY.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblFiltroY.Name = "lblFiltroY";
            this.lblFiltroY.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblFiltroY.Size = new System.Drawing.Size(10, 17);
            this.lblFiltroY.TabIndex = 6;
            this.lblFiltroY.Text = "y";
            // 
            // txtPrecioMaximo
            // 
            this.txtPrecioMaximo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioMaximo.MaxLength = 14;
            this.txtPrecioMaximo.Location = new System.Drawing.Point(0, 3);
            this.txtPrecioMaximo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 0);
            this.txtPrecioMaximo.Name = "txtPrecioMaximo";
            this.txtPrecioMaximo.PlaceholderText = "Máximo";
            this.txtPrecioMaximo.Size = new System.Drawing.Size(90, 27);
            this.txtPrecioMaximo.TabIndex = 7;
            this.txtPrecioMaximo.TextChanged += new System.EventHandler(this.txtPrecio_TextChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.White;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnLimpiar.FlatAppearance.BorderSize = 1;
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 27);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar filtros";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // panelFiltros
            // 
            this.panelFiltros.Controls.Add(this.lblFiltroCategoria);
            this.panelFiltros.Controls.Add(this.cboCategoria);
            this.panelFiltros.Controls.Add(this.lblFiltroSuplidor);
            this.panelFiltros.Controls.Add(this.cboSuplidor);
            this.panelFiltros.Controls.Add(this.lblFiltroPrecio);
            this.panelFiltros.Controls.Add(this.txtPrecioMinimo);
            this.panelFiltros.Controls.Add(this.lblFiltroY);
            this.panelFiltros.Controls.Add(this.txtPrecioMaximo);
            this.panelFiltros.Controls.Add(this.btnLimpiar);
            this.panelFiltros.AutoSize = true;
            this.panelFiltros.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelFiltros.Size = new System.Drawing.Size(964, 47);
            this.panelFiltros.TabIndex = 2;
            // 
            // errores
            // 
            this.errores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errores.ContainerControl = this;
            // 
            // colNombre
            // 
            this.colNombre.DataPropertyName = "Nombre";
            this.colNombre.FillWeight = 28F;
            this.colNombre.HeaderText = "Producto";
            this.colNombre.MinimumWidth = 6;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colCategoriaNombre
            // 
            this.colCategoriaNombre.DataPropertyName = "CategoriaNombre";
            this.colCategoriaNombre.FillWeight = 17F;
            this.colCategoriaNombre.HeaderText = "Categoría";
            this.colCategoriaNombre.MinimumWidth = 6;
            this.colCategoriaNombre.Name = "colCategoriaNombre";
            this.colCategoriaNombre.ReadOnly = true;
            this.colCategoriaNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSuplidorNombre
            // 
            this.colSuplidorNombre.DataPropertyName = "SuplidorNombre";
            this.colSuplidorNombre.FillWeight = 20F;
            this.colSuplidorNombre.HeaderText = "Suplidor";
            this.colSuplidorNombre.MinimumWidth = 6;
            this.colSuplidorNombre.Name = "colSuplidorNombre";
            this.colSuplidorNombre.ReadOnly = true;
            this.colSuplidorNombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colPrecioUnitario
            // 
            this.colPrecioUnitario.DataPropertyName = "PrecioUnitario";
            this.colPrecioUnitario.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPrecioUnitario.FillWeight = 11F;
            this.colPrecioUnitario.HeaderText = "Precio (RD$)";
            this.colPrecioUnitario.MinimumWidth = 6;
            this.colPrecioUnitario.Name = "colPrecioUnitario";
            this.colPrecioUnitario.ReadOnly = true;
            this.colPrecioUnitario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.DataPropertyName = "FechaCreacion";
            this.colFechaCreacion.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFechaCreacion.FillWeight = 12F;
            this.colFechaCreacion.HeaderText = "Creación";
            this.colFechaCreacion.MinimumWidth = 6;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            this.colFechaCreacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colFechaModificacion
            // 
            this.colFechaModificacion.DataPropertyName = "FechaModificacion";
            this.colFechaModificacion.DefaultCellStyle = dataGridViewCellStyle6;
            this.colFechaModificacion.FillWeight = 12F;
            this.colFechaModificacion.HeaderText = "Modificación";
            this.colFechaModificacion.MinimumWidth = 6;
            this.colFechaModificacion.Name = "colFechaModificacion";
            this.colFechaModificacion.ReadOnly = true;
            this.colFechaModificacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            this.colNombre,
            this.colCategoriaNombre,
            this.colSuplidorNombre,
            this.colPrecioUnitario,
            this.colFechaCreacion,
            this.colFechaModificacion});
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
            // UcProductos
            // 
            this.Controls.Add(this.panelMarco);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelBarra);
            this.Controls.Add(this.panelEncabezado);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcProductos";
            this.Padding = new System.Windows.Forms.Padding(28, 22, 28, 14);
            this.Size = new System.Drawing.Size(1020, 760);
            this.panelEncabezado.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.panelBarra.ResumeLayout(false);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.panelMarco.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.FlowLayoutPanel panelAcciones;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel panelBarra;
        private System.Windows.Forms.Label lblFiltroCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Label lblFiltroSuplidor;
        private System.Windows.Forms.ComboBox cboSuplidor;
        private System.Windows.Forms.Label lblFiltroPrecio;
        private System.Windows.Forms.TextBox txtPrecioMinimo;
        private System.Windows.Forms.Label lblFiltroY;
        private System.Windows.Forms.TextBox txtPrecioMaximo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.FlowLayoutPanel panelFiltros;
        private System.Windows.Forms.ErrorProvider errores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoriaNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuplidorNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaModificacion;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelMarco;
        private System.Windows.Forms.Label lblEstado;
    }
}
