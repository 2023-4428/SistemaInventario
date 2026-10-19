#nullable disable

namespace SistemaInventario.Presentacion.Controles
{
    partial class TarjetaFoto
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
            this.components = new System.ComponentModel.Container();
            this.ayuda = new System.Windows.Forms.ToolTip(this.components);
            this.visor = new System.Windows.Forms.PictureBox();
            this.lblNombreArchivo = new System.Windows.Forms.Label();
            this.btnQuitar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.visor)).BeginInit();
            this.SuspendLayout();
            // 
            // ayuda
            // 
            // 
            // visor
            // 
            this.visor.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.visor.Location = new System.Drawing.Point(4, 4);
            this.visor.Name = "visor";
            this.visor.Size = new System.Drawing.Size(108, 84);
            this.visor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.visor.TabIndex = 0;
            this.visor.TabStop = false;
            // 
            // lblNombreArchivo
            // 
            this.lblNombreArchivo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreArchivo.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblNombreArchivo.Location = new System.Drawing.Point(4, 91);
            this.lblNombreArchivo.Name = "lblNombreArchivo";
            this.lblNombreArchivo.Size = new System.Drawing.Size(108, 18);
            this.lblNombreArchivo.TabIndex = 1;
            this.lblNombreArchivo.Text = "";
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.White;
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.btnQuitar.FlatAppearance.BorderSize = 1;
            this.btnQuitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.btnQuitar.Location = new System.Drawing.Point(4, 111);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(108, 24);
            this.btnQuitar.TabIndex = 2;
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // TarjetaFoto
            // 
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblNombreArchivo);
            this.Controls.Add(this.visor);
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TarjetaFoto";
            this.Size = new System.Drawing.Size(118, 140);
            ((System.ComponentModel.ISupportInitialize)(this.visor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ayuda;
        private System.Windows.Forms.PictureBox visor;
        private System.Windows.Forms.Label lblNombreArchivo;
        private System.Windows.Forms.Button btnQuitar;
    }
}
