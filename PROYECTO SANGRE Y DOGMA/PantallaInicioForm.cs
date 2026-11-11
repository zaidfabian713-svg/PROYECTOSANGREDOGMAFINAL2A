using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana de inicio con la imagen de portada
	public partial class PantallaInicioForm : Form
	{
		public PantallaInicioForm()
		{
			InitializeComponent();
			CargarImagenPortada();
			AplicarIdioma();
		}

		// Cambia los botones de inicio segun el idioma con if/else
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Sangre y Dogma: Arena Elemental";
				btnIniciarJuego.Text = "Iniciar Juego";
				btnSalir.Text = "Salir";
			}
			else
			{
				this.Text = "Blood and Dogma: Elemental Arena";
				btnIniciarJuego.Text = "Start Game";
				btnSalir.Text = "Exit";
			}
		}

		// Busca la imagen portada.jpg y la pone en el PictureBox
		private void CargarImagenPortada()
		{
			try
			{
				string ruta1 = Path.Combine(Application.StartupPath, "portada.jpg");
				string ruta2 = Path.Combine(Application.StartupPath, "..\\..\\portada.jpg");

				if (File.Exists(ruta1))
					picPortada.Image = Image.FromFile(ruta1);
				else if (File.Exists("portada.jpg"))
					picPortada.Image = Image.FromFile("portada.jpg");
				else if (File.Exists(ruta2))
					picPortada.Image = Image.FromFile(ruta2);
			}
			catch
			{
			}
		}

		// Abre el menu principal
		private void BtnIniciarJuegoClick(object sender, EventArgs e)
		{
			this.Hide();
			MainForm menu = new MainForm();
			menu.ShowDialog();
			AplicarIdioma();
			this.Show();
		}

		private void BtnSalirClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
