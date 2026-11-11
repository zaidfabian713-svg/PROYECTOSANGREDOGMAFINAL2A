using System;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Menu principal (con idioma ES/EN)
	public partial class MainForm : Form
	{
		// Idioma actual: ES o EN
		public static string Idioma = "ES";

		public GestorEstructuras Gestor;
		public ConexionBD BaseDatos;

		public MainForm()
		{
			InitializeComponent();
			Gestor = new GestorEstructuras();
			BaseDatos = new ConexionBD();
			// Efecto al pasar el mouse por los botones
			foreach (Control c in grpMenu.Controls)
			{
				if (c is Button)
				{
					c.MouseEnter += BotonEntra;
					c.MouseLeave += BotonSale;
				}
			}
			AplicarIdioma();
		}

		// Al entrar el mouse guardo el color y lo aclaro
		private void BotonEntra(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			b.Tag = b.BackColor;
			b.BackColor = ControlPaint.Light(b.BackColor);
		}

		// Al salir regreso el color de antes
		private void BotonSale(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			if (b.Tag != null)
			{
				b.BackColor = (System.Drawing.Color)b.Tag;
			}
		}

		// Cambia los textos segun el idioma
		private void AplicarIdioma()
		{
			if (Idioma == "ES")
			{
				this.Text = "Menú Principal - Sangre y Dogma";
				lblTitulo.Text = "Sangre y Dogma: Arena Elemental";
				lblSubtitulo.Text = "Elige tu destino...";
				grpMenu.Text = "Menú Principal";
				btnModoHistoria.Text = "1 Jugador vs Sistema (Duendes)";
				btnModo2Jugadores.Text = "2 Jugadores (Local)";
				btnLore.Text = "MOSTRAR HISTORIA";
				btnGeneradorMazos.Text = "Armar Mazos y Crear Cartas";
				btnGestionMazos.Text = "Base de Datos (SQL)";
				btnCombateRed.Text = "Combate en Red (LAN)";
				btnLeerTxt.Text = "Leer Puntajes (.TXT)";
				btnCambiarIdioma.Text = "Cambiar Idioma: Español -> Inglés";
			}
			else
			{
				this.Text = "Main Menu - Blood and Dogma";
				lblTitulo.Text = "Blood and Dogma: Elemental Arena";
			lblSubtitulo.Text = "Choose your fate...";
				grpMenu.Text = "Main Menu";
				btnModoHistoria.Text = "1 Player vs System (Goblins)";
				btnModo2Jugadores.Text = "2 Players (Local)";
				btnLore.Text = "MOSTRAR HISTORIA / SHOW STORY";
				btnGeneradorMazos.Text = "Build Decks and Create Cards";
				btnGestionMazos.Text = "Database (SQL)";
				btnCombateRed.Text = "Network Battle (LAN)";
				btnLeerTxt.Text = "Read Scores (.TXT)";
				btnCambiarIdioma.Text = "Change Language: English -> Spanish";
			}
		}

		// Cambia de ES a EN y viceversa
		private void BtnCambiarIdiomaClick(object sender, EventArgs e)
		{
			if (Idioma == "ES")
			{
				Idioma = "EN";
			}
			else
			{
				Idioma = "ES";
			}
			AplicarIdioma();
		}

		// 1 jugador vs duendes
		private void BtnModoHistoriaClick(object sender, EventArgs e)
		{
			FormCombate ventana = new FormCombate(Gestor, BaseDatos, true);
			ventana.ShowDialog();
		}

		// 2 jugadores en la misma PC
		private void BtnModo2JugadoresClick(object sender, EventArgs e)
		{
			FormCombate ventana = new FormCombate(Gestor, BaseDatos, false);
			ventana.ShowDialog();
		}

		// Historia y cartas
		private void BtnLoreClick(object sender, EventArgs e)
		{
			FormLore ventana = new FormLore(Gestor);
			ventana.ShowDialog();
		}

		// Crear cartas y armar mazos
		private void BtnGeneradorMazosClick(object sender, EventArgs e)
		{
			FormGeneradorMazos ventana = new FormGeneradorMazos(Gestor);
			ventana.ShowDialog();
		}

		// Base de datos SQL
		private void BtnGestionMazosClick(object sender, EventArgs e)
		{
			FormGestionBD ventana = new FormGestionBD(Gestor, BaseDatos);
			ventana.ShowDialog();
		}

		// Combate en red
		private void BtnCombateRedClick(object sender, EventArgs e)
		{
			FormCombateRed ventana = new FormCombateRed(Gestor);
			ventana.ShowDialog();
		}

		// Leer el .txt de puntajes
		private void BtnLeerTxtClick(object sender, EventArgs e)
		{
			string datosTxt = Gestor.LeerPuntajesMaximosTxt();
			if (Idioma == "ES")
				MessageBox.Show(datosTxt, "Puntajes Guardados (.TXT)");
			else
				MessageBox.Show(datosTxt, "Saved Scores (.TXT)");
		}
	}
}
