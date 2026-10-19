#nullable disable

namespace SistemaInventario.Presentacion.Formularios
{
    partial class FrmCategoriaEditar
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
            this.components = new System.ComponentModel.Container();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblContador = new System.Windows.Forms.Label();
            this.lblFotos = new System.Windows.Forms.Label();
            this.lblAyudaFotos = new System.Windows.Forms.Label();
            this.panelFotos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAgregarFoto = new System.Windows.Forms.Button();
            this.lblInfoFotos = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblNombre.Location = new System.Drawing.Point(24, 20);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(10, 17);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Location = new System.Drawing.Point(24, 42);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(466, 27);
            this.txtNombre.TabIndex = 1;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblDescripcion.Location = new System.Drawing.Point(24, 83);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(10, 17);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción *";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Location = new System.Drawing.Point(24, 105);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.AcceptsReturn = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(466, 90);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblContador
            // 
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblContador.Location = new System.Drawing.Point(410, 83);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(80, 20);
            this.lblContador.TabIndex = 4;
            this.lblContador.Text = "0/500";
            this.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFotos
            // 
            this.lblFotos.AutoSize = true;
            this.lblFotos.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblFotos.Location = new System.Drawing.Point(24, 209);
            this.lblFotos.Name = "lblFotos";
            this.lblFotos.Size = new System.Drawing.Size(10, 17);
            this.lblFotos.TabIndex = 5;
            this.lblFotos.Text = "Fotos (opcional)";
            // 
            // lblAyudaFotos
            // 
            this.lblAyudaFotos.AutoSize = true;
            this.lblAyudaFotos.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaFotos.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblAyudaFotos.Location = new System.Drawing.Point(142, 211);
            this.lblAyudaFotos.Name = "lblAyudaFotos";
            this.lblAyudaFotos.Size = new System.Drawing.Size(10, 17);
            this.lblAyudaFotos.TabIndex = 6;
            this.lblAyudaFotos.Text = "Hasta 5 · .jpg, .jpeg o .png · máx. 2 MB cada una";
            // 
            // panelFotos
            // 
            this.panelFotos.AutoScroll = true;
            this.panelFotos.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.panelFotos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFotos.Location = new System.Drawing.Point(24, 233);
            this.panelFotos.Name = "panelFotos";
            this.panelFotos.Padding = new System.Windows.Forms.Padding(2);
            this.panelFotos.Size = new System.Drawing.Size(492, 172);
            this.panelFotos.TabIndex = 7;
            this.panelFotos.WrapContents = false;
            // 
            // btnAgregarFoto
            // 
            this.btnAgregarFoto.BackColor = System.Drawing.Color.White;
            this.btnAgregarFoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarFoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnAgregarFoto.FlatAppearance.BorderSize = 1;
            this.btnAgregarFoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnAgregarFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarFoto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarFoto.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnAgregarFoto.Location = new System.Drawing.Point(24, 413);
            this.btnAgregarFoto.Name = "btnAgregarFoto";
            this.btnAgregarFoto.Size = new System.Drawing.Size(150, 34);
            this.btnAgregarFoto.TabIndex = 8;
            this.btnAgregarFoto.Text = "+ Agregar fotos…";
            this.btnAgregarFoto.UseVisualStyleBackColor = false;
            this.btnAgregarFoto.Click += new System.EventHandler(this.btnAgregarFoto_Click);
            // 
            // lblInfoFotos
            // 
            this.lblInfoFotos.AutoSize = true;
            this.lblInfoFotos.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblInfoFotos.Location = new System.Drawing.Point(188, 420);
            this.lblInfoFotos.Name = "lblInfoFotos";
            this.lblInfoFotos.Size = new System.Drawing.Size(10, 17);
            this.lblInfoFotos.TabIndex = 9;
            this.lblInfoFotos.Text = "0 de 5 fotos";
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
            this.btnGuardar.Location = new System.Drawing.Point(288, 465);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(110, 34);
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
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
            this.btnCancelar.Location = new System.Drawing.Point(406, 465);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 34);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // errores
            // 
            this.errores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errores.ContainerControl = this;
            // 
            // FrmCategoriaEditar
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(540, 521);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblInfoFotos);
            this.Controls.Add(this.btnAgregarFoto);
            this.Controls.Add(this.panelFotos);
            this.Controls.Add(this.lblAyudaFotos);
            this.Controls.Add(this.lblFotos);
            this.Controls.Add(this.lblContador);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCategoriaEditar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nueva categoría";
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.Label lblFotos;
        private System.Windows.Forms.Label lblAyudaFotos;
        private System.Windows.Forms.FlowLayoutPanel panelFotos;
        private System.Windows.Forms.Button btnAgregarFoto;
        private System.Windows.Forms.Label lblInfoFotos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider errores;
    }
}
