/*
 * Created by SharpDevelop.
 * User: WINDOWS
 * Date: 7/11/2026
 */
namespace PROYECTO_SANGRE_Y_DOGMA
{
	partial class FormLore
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.GroupBox grpLista;
		private System.Windows.Forms.ListBox lstPersonajes;
		private System.Windows.Forms.GroupBox grpHistoria;
		private System.Windows.Forms.PictureBox picCarta;
		private System.Windows.Forms.Label lblInfoCarta;
		private System.Windows.Forms.TextBox txtHistoria;
		private System.Windows.Forms.Button btnMostrarHistoria;
		private System.Windows.Forms.Button btnCerrar;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.grpLista = new System.Windows.Forms.GroupBox();
			this.lstPersonajes = new System.Windows.Forms.ListBox();
			this.grpHistoria = new System.Windows.Forms.GroupBox();
			this.picCarta = new System.Windows.Forms.PictureBox();
			this.lblInfoCarta = new System.Windows.Forms.Label();
			this.txtHistoria = new System.Windows.Forms.TextBox();
			this.btnMostrarHistoria = new System.Windows.Forms.Button();
			this.btnCerrar = new System.Windows.Forms.Button();
			this.grpLista.SuspendLayout();
			this.grpHistoria.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picCarta)).BeginInit();
			this.SuspendLayout();
			// 
			// grpLista
			// 
			this.grpLista.Controls.Add(this.lstPersonajes);
			this.grpLista.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.grpLista.Location = new System.Drawing.Point(12, 12);
			this.grpLista.Name = "grpLista";
			this.grpLista.Size = new System.Drawing.Size(230, 330);
			this.grpLista.TabIndex = 0;
			this.grpLista.TabStop = false;
			this.grpLista.Text = "16 Cartas del Catálogo";
			// 
			// lstPersonajes
			// 
			this.lstPersonajes.Font = new System.Drawing.Font("Tahoma", 8.5F);
			this.lstPersonajes.FormattingEnabled = true;
			this.lstPersonajes.Location = new System.Drawing.Point(12, 22);
			this.lstPersonajes.Name = "lstPersonajes";
			this.lstPersonajes.Size = new System.Drawing.Size(206, 290);
			this.lstPersonajes.TabIndex = 0;
			this.lstPersonajes.SelectedIndexChanged += new System.EventHandler(this.LstPersonajesSelectedIndexChanged);
			// 
			// grpHistoria
			// 
			this.grpHistoria.Controls.Add(this.picCarta);
			this.grpHistoria.Controls.Add(this.lblInfoCarta);
			this.grpHistoria.Controls.Add(this.txtHistoria);
			this.grpHistoria.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.grpHistoria.Location = new System.Drawing.Point(252, 12);
			this.grpHistoria.Name = "grpHistoria";
			this.grpHistoria.Size = new System.Drawing.Size(585, 330);
			this.grpHistoria.TabIndex = 1;
			this.grpHistoria.TabStop = false;
			this.grpHistoria.Text = "Carta Ilustrada, Historia y Atributos";
			// 
			// picCarta
			// 
			this.picCarta.BackColor = System.Drawing.Color.FromArgb(242, 244, 248);
			this.picCarta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picCarta.Location = new System.Drawing.Point(14, 22);
			this.picCarta.Name = "picCarta";
			this.picCarta.Size = new System.Drawing.Size(185, 250);
			this.picCarta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picCarta.TabIndex = 1;
			this.picCarta.TabStop = false;
			// 
			// lblInfoCarta
			// 
			this.lblInfoCarta.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblInfoCarta.Location = new System.Drawing.Point(14, 276);
			this.lblInfoCarta.Name = "lblInfoCarta";
			this.lblInfoCarta.Size = new System.Drawing.Size(185, 45);
			this.lblInfoCarta.TabIndex = 2;
			this.lblInfoCarta.Text = "Carta seleccionada";
			this.lblInfoCarta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtHistoria
			// 
			this.txtHistoria.BackColor = System.Drawing.Color.White;
			this.txtHistoria.Font = new System.Drawing.Font("Tahoma", 8.5F);
			this.txtHistoria.Location = new System.Drawing.Point(210, 22);
			this.txtHistoria.Multiline = true;
			this.txtHistoria.Name = "txtHistoria";
			this.txtHistoria.ReadOnly = true;
			this.txtHistoria.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtHistoria.Size = new System.Drawing.Size(360, 290);
			this.txtHistoria.TabIndex = 0;
			// 
			// btnMostrarHistoria
			// 
			this.btnMostrarHistoria.BackColor = System.Drawing.Color.FromArgb(250, 238, 210);
			this.btnMostrarHistoria.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
			this.btnMostrarHistoria.Location = new System.Drawing.Point(252, 348);
			this.btnMostrarHistoria.Name = "btnMostrarHistoria";
			this.btnMostrarHistoria.Size = new System.Drawing.Size(180, 32);
			this.btnMostrarHistoria.TabIndex = 2;
			this.btnMostrarHistoria.Text = "MOSTRAR HISTORIA";
			this.btnMostrarHistoria.UseVisualStyleBackColor = false;
			this.btnMostrarHistoria.Click += new System.EventHandler(this.BtnMostrarHistoriaClick);
			// 
			// btnCerrar
			// 
			this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(235, 238, 245);
			this.btnCerrar.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
			this.btnCerrar.Location = new System.Drawing.Point(657, 348);
			this.btnCerrar.Name = "btnCerrar";
			this.btnCerrar.Size = new System.Drawing.Size(180, 32);
			this.btnCerrar.TabIndex = 3;
			this.btnCerrar.Text = "Volver";
			this.btnCerrar.UseVisualStyleBackColor = false;
			this.btnCerrar.Click += new System.EventHandler(this.BtnCerrarClick);
			// 
			// FormLore
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(232, 238, 247);
			this.ClientSize = new System.Drawing.Size(850, 390);
			this.Controls.Add(this.btnMostrarHistoria);
			this.Controls.Add(this.btnCerrar);
			this.Controls.Add(this.grpHistoria);
			this.Controls.Add(this.grpLista);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "FormLore";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Historia de Personajes y Catálogo";
			this.grpLista.ResumeLayout(false);
			this.grpHistoria.ResumeLayout(false);
			this.grpHistoria.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picCarta)).EndInit();
			this.ResumeLayout(false);
		}
	}
}
