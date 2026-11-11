namespace PROYECTO_SANGRE_Y_DOGMA
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label lblTitulo;
		private System.Windows.Forms.Label lblSubtitulo;
		private System.Windows.Forms.GroupBox grpMenu;
		private System.Windows.Forms.Button btnModoHistoria;
		private System.Windows.Forms.Button btnModo2Jugadores;
		private System.Windows.Forms.Button btnLore;
		private System.Windows.Forms.Button btnGeneradorMazos;
		private System.Windows.Forms.Button btnGestionMazos;
		private System.Windows.Forms.Button btnCombateRed;
		private System.Windows.Forms.Button btnLeerTxt;
		private System.Windows.Forms.Button btnCambiarIdioma;

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
			this.lblTitulo = new System.Windows.Forms.Label();
			this.lblSubtitulo = new System.Windows.Forms.Label();
			this.grpMenu = new System.Windows.Forms.GroupBox();
			this.btnModoHistoria = new System.Windows.Forms.Button();
			this.btnModo2Jugadores = new System.Windows.Forms.Button();
			this.btnLore = new System.Windows.Forms.Button();
			this.btnGeneradorMazos = new System.Windows.Forms.Button();
			this.btnGestionMazos = new System.Windows.Forms.Button();
			this.btnCombateRed = new System.Windows.Forms.Button();
			this.btnLeerTxt = new System.Windows.Forms.Button();
			this.btnCambiarIdioma = new System.Windows.Forms.Button();
			this.grpMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblTitulo
			// 
			this.lblTitulo.BackColor = System.Drawing.Color.FromArgb(120, 15, 25);
			this.lblTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTitulo.Font = new System.Drawing.Font("Georgia", 16F, System.Drawing.FontStyle.Bold);
			this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(240, 200, 90);
			this.lblTitulo.Location = new System.Drawing.Point(15, 15);
			this.lblTitulo.Name = "lblTitulo";
			this.lblTitulo.Size = new System.Drawing.Size(390, 60);
			this.lblTitulo.TabIndex = 0;
			this.lblTitulo.Text = "Sangre y Dogma: Arena Elemental";
			this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSubtitulo
			// 
			this.lblSubtitulo.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Italic);
			this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(200, 180, 140);
			this.lblSubtitulo.Location = new System.Drawing.Point(15, 80);
			this.lblSubtitulo.Name = "lblSubtitulo";
			this.lblSubtitulo.Size = new System.Drawing.Size(390, 20);
			this.lblSubtitulo.TabIndex = 2;
			this.lblSubtitulo.Text = "Elige tu destino...";
			this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpMenu
			// 
			this.grpMenu.BackColor = System.Drawing.Color.FromArgb(40, 30, 45);
			this.grpMenu.Controls.Add(this.btnModoHistoria);
			this.grpMenu.Controls.Add(this.btnModo2Jugadores);
			this.grpMenu.Controls.Add(this.btnLore);
			this.grpMenu.Controls.Add(this.btnGeneradorMazos);
			this.grpMenu.Controls.Add(this.btnGestionMazos);
			this.grpMenu.Controls.Add(this.btnCombateRed);
			this.grpMenu.Controls.Add(this.btnLeerTxt);
			this.grpMenu.Controls.Add(this.btnCambiarIdioma);
			this.grpMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.grpMenu.ForeColor = System.Drawing.Color.FromArgb(240, 200, 90);
			this.grpMenu.Location = new System.Drawing.Point(15, 105);
			this.grpMenu.Name = "grpMenu";
			this.grpMenu.Size = new System.Drawing.Size(390, 390);
			this.grpMenu.TabIndex = 1;
			this.grpMenu.TabStop = false;
			this.grpMenu.Text = "Menu Principal";
			// 
			// btnModoHistoria
			// 
			this.btnModoHistoria.BackColor = System.Drawing.Color.FromArgb(170,30,40);
			this.btnModoHistoria.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnModoHistoria.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnModoHistoria.FlatAppearance.BorderSize = 2;
			this.btnModoHistoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnModoHistoria.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnModoHistoria.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnModoHistoria.Location = new System.Drawing.Point(25, 25);
			this.btnModoHistoria.Name = "btnModoHistoria";
			this.btnModoHistoria.Size = new System.Drawing.Size(310, 38);
			this.btnModoHistoria.TabIndex = 0;
			this.btnModoHistoria.Text = "1 Jugador vs Sistema (Duendes)";
			this.btnModoHistoria.UseVisualStyleBackColor = false;
			this.btnModoHistoria.Click += new System.EventHandler(this.BtnModoHistoriaClick);
			// 
			// btnModo2Jugadores
			// 
			this.btnModo2Jugadores.BackColor = System.Drawing.Color.FromArgb(170,30,40);
			this.btnModo2Jugadores.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnModo2Jugadores.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnModo2Jugadores.FlatAppearance.BorderSize = 2;
			this.btnModo2Jugadores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnModo2Jugadores.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnModo2Jugadores.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnModo2Jugadores.Location = new System.Drawing.Point(25, 70);
			this.btnModo2Jugadores.Name = "btnModo2Jugadores";
			this.btnModo2Jugadores.Size = new System.Drawing.Size(310, 38);
			this.btnModo2Jugadores.TabIndex = 1;
			this.btnModo2Jugadores.Text = "2 Jugadores (Local)";
			this.btnModo2Jugadores.UseVisualStyleBackColor = false;
			this.btnModo2Jugadores.Click += new System.EventHandler(this.BtnModo2JugadoresClick);
			// 
			// btnLore
			// 
			this.btnLore.BackColor = System.Drawing.Color.FromArgb(200,150,30);
			this.btnLore.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLore.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnLore.FlatAppearance.BorderSize = 2;
			this.btnLore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLore.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnLore.ForeColor = System.Drawing.Color.FromArgb(30, 20, 10);
			this.btnLore.Location = new System.Drawing.Point(25, 115);
			this.btnLore.Name = "btnLore";
			this.btnLore.Size = new System.Drawing.Size(310, 38);
			this.btnLore.TabIndex = 2;
			this.btnLore.Text = "MOSTRAR HISTORIA";
			this.btnLore.UseVisualStyleBackColor = false;
			this.btnLore.Click += new System.EventHandler(this.BtnLoreClick);
			// 
			// btnGeneradorMazos
			// 
			this.btnGeneradorMazos.BackColor = System.Drawing.Color.FromArgb(40,120,70);
			this.btnGeneradorMazos.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnGeneradorMazos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnGeneradorMazos.FlatAppearance.BorderSize = 2;
			this.btnGeneradorMazos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGeneradorMazos.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnGeneradorMazos.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnGeneradorMazos.Location = new System.Drawing.Point(25, 160);
			this.btnGeneradorMazos.Name = "btnGeneradorMazos";
			this.btnGeneradorMazos.Size = new System.Drawing.Size(310, 38);
			this.btnGeneradorMazos.TabIndex = 3;
			this.btnGeneradorMazos.Text = "Armar Mazos y Crear Cartas";
			this.btnGeneradorMazos.UseVisualStyleBackColor = false;
			this.btnGeneradorMazos.Click += new System.EventHandler(this.BtnGeneradorMazosClick);
			// 
			// btnGestionMazos
			// 
			this.btnGestionMazos.BackColor = System.Drawing.Color.FromArgb(50,70,110);
			this.btnGestionMazos.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnGestionMazos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnGestionMazos.FlatAppearance.BorderSize = 2;
			this.btnGestionMazos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnGestionMazos.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnGestionMazos.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnGestionMazos.Location = new System.Drawing.Point(25, 205);
			this.btnGestionMazos.Name = "btnGestionMazos";
			this.btnGestionMazos.Size = new System.Drawing.Size(310, 38);
			this.btnGestionMazos.TabIndex = 4;
			this.btnGestionMazos.Text = "Base de Datos (SQL)";
			this.btnGestionMazos.UseVisualStyleBackColor = false;
			this.btnGestionMazos.Click += new System.EventHandler(this.BtnGestionMazosClick);
			// 
			// btnCombateRed
			// 
			this.btnCombateRed.BackColor = System.Drawing.Color.FromArgb(50,70,110);
			this.btnCombateRed.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCombateRed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnCombateRed.FlatAppearance.BorderSize = 2;
			this.btnCombateRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCombateRed.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnCombateRed.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnCombateRed.Location = new System.Drawing.Point(25, 250);
			this.btnCombateRed.Name = "btnCombateRed";
			this.btnCombateRed.Size = new System.Drawing.Size(310, 38);
			this.btnCombateRed.TabIndex = 5;
			this.btnCombateRed.Text = "Combate en Red (LAN)";
			this.btnCombateRed.UseVisualStyleBackColor = false;
			this.btnCombateRed.Click += new System.EventHandler(this.BtnCombateRedClick);
			// 
			// btnLeerTxt
			// 
			this.btnLeerTxt.BackColor = System.Drawing.Color.FromArgb(50,70,110);
			this.btnLeerTxt.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLeerTxt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnLeerTxt.FlatAppearance.BorderSize = 2;
			this.btnLeerTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLeerTxt.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnLeerTxt.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnLeerTxt.Location = new System.Drawing.Point(25, 295);
			this.btnLeerTxt.Name = "btnLeerTxt";
			this.btnLeerTxt.Size = new System.Drawing.Size(310, 38);
			this.btnLeerTxt.TabIndex = 6;
			this.btnLeerTxt.Text = "Leer Puntajes (.TXT)";
			this.btnLeerTxt.UseVisualStyleBackColor = false;
			this.btnLeerTxt.Click += new System.EventHandler(this.BtnLeerTxtClick);
			// 
			// btnCambiarIdioma
			// 
			this.btnCambiarIdioma.BackColor = System.Drawing.Color.FromArgb(80,80,90);
			this.btnCambiarIdioma.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCambiarIdioma.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(230, 190, 80);
			this.btnCambiarIdioma.FlatAppearance.BorderSize = 2;
			this.btnCambiarIdioma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCambiarIdioma.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnCambiarIdioma.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
			this.btnCambiarIdioma.Location = new System.Drawing.Point(25, 340);
			this.btnCambiarIdioma.Name = "btnCambiarIdioma";
			this.btnCambiarIdioma.Size = new System.Drawing.Size(310, 38);
			this.btnCambiarIdioma.TabIndex = 7;
			this.btnCambiarIdioma.Text = "Cambiar Idioma: ES / EN";
			this.btnCambiarIdioma.UseVisualStyleBackColor = false;
			this.btnCambiarIdioma.Click += new System.EventHandler(this.BtnCambiarIdiomaClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(20, 15, 25);
			this.ClientSize = new System.Drawing.Size(420, 510);
			this.Controls.Add(this.grpMenu);
			this.Controls.Add(this.lblSubtitulo);
			this.Controls.Add(this.lblTitulo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Menu Principal - Sangre y Dogma";
			this.grpMenu.ResumeLayout(false);
			this.ResumeLayout(false);
		}
	}
}