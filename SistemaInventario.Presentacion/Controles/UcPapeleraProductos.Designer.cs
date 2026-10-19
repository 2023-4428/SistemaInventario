#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class UcPapeleraProductos
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
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnEliminarDefinitivo = new System.Windows.Forms.Button();
            this.panelAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.panelBarra = new System.Windows.Forms.Panel();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoriaNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSuplidorNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaModificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelMarco = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
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
            dataGridViewCellStyle4.Format = "N2";
            // 
            // dataGridViewCellStyle5
            // 
            dataGridViewCellStyle5.Format = "dd/MM/yyyy HH:mm";
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.FlatAppearance.BorderSize = 0;
            this.btnRestaurar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.ForeColor = System.Drawing.Color.White;
            this.btnRestaurar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(180, 34);
            this.btnRestaurar.TabIndex = 0;
            this.btnRestaurar.Text = "Restaurar seleccionado";
            this.btnRestaurar.UseVisualStyleBackColor = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnEliminarDefinitivo
            // 
            this.btnEliminarDefinitivo.BackColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.btnEliminarDefinitivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarDefinitivo.FlatAppearance.BorderSize = 0;
            this.btnEliminarDefinitivo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnEliminarDefinitivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarDefinitivo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarDefinitivo.ForeColor = System.Drawing.Color.White;
            this.btnEliminarDefinitivo.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.btnEliminarDefinitivo.Name = "btnEliminarDefinitivo";
            this.btnEliminarDefinitivo.Size = new System.Drawing.Size(190, 34);
            this.btnEliminarDefinitivo.TabIndex = 1;
            this.btnEliminarDefinitivo.Text = "Eliminar definitivamente";
            this.btnEliminarDefinitivo.UseVisualStyleBackColor = false;
            this.btnEliminarDefinitivo.Click += new System.EventHandler(this.btnEliminarDefinitivo_Click);
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.btnRestaurar);
            this.panelAcciones.Controls.Add(this.btnEliminarDefinitivo);
            this.panelAcciones.AutoSize = true;
            this.panelAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelAcciones.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelAcciones.WrapContents = false;
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.panelAcciones.Size = new System.Drawing.Size(386, 48);
            this.panelAcciones.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBuscar.Location = new System.Drawing.Point(0, 10);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PlaceholderText = "Buscar en la papelera...";
            this.txtBuscar.Size = new System.Drawing.Size(380, 27);
            this.txtBuscar.TabIndex = 0;
            // 
            // panelBarra
            // 
            this.panelBarra.Controls.Add(this.panelAcciones);
            this.panelBarra.Controls.Add(this.txtBuscar);
            this.panelBarra.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarra.Name = "panelBarra";
            this.panelBarra.Size = new System.Drawing.Size(1000, 48);
            this.panelBarra.TabIndex = 1;
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
            // colFechaModificacion
            // 
            this.colFechaModificacion.DataPropertyName = "FechaModificacion";
            this.colFechaModificacion.DefaultCellStyle = dataGridViewCellStyle5;
            this.colFechaModificacion.FillWeight = 14F;
            this.colFechaModificacion.HeaderText = "Eliminado el";
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
            this.panelMarco.TabIndex = 2;
            // 
            // lblEstado
            // 
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(1000, 30);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UcPapeleraProductos
            // 
            this.Controls.Add(this.panelMarco);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.panelBarra);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcPapeleraProductos";
            this.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.Size = new System.Drawing.Size(1000, 500);
            this.panelAcciones.ResumeLayout(false);
            this.panelAcciones.PerformLayout();
            this.panelBarra.ResumeLayout(false);
            this.panelMarco.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnEliminarDefinitivo;
        private System.Windows.Forms.FlowLayoutPanel panelAcciones;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Panel panelBarra;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoriaNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSuplidorNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaModificacion;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelMarco;
        private System.Windows.Forms.Label lblEstado;
    }
}
