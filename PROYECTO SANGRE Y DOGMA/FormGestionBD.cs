using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana para guardar jugadores en la Base de Datos SQL y ver sus estadisticas
	public partial class FormGestionBD : Form
	{
		public GestorEstructuras Gestor;
		public ConexionBD BaseDatos;
		public List<RegistroJugadorBD> ListaMostrada;

		public FormGestionBD(GestorEstructuras gestor, ConexionBD bd)
		{
			InitializeComponent();
			Gestor = gestor;
			BaseDatos = bd;
			ListaMostrada = new List<RegistroJugadorBD>();

			AplicarIdioma();
			MostrarMazoActual();
			CargarTablaJugadores();
		}

		// Cambia los textos del formulario usando if/else basico
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Base de Datos SQL (Jugadores, Personajes y Estadísticas)";
				lblCadena.Text = "Cadena SQL:";
				grpNuevo.Text = "1. Registrar Jugador y Mazo (PK / FK)";
				lblNombre.Text = "Nombre del Jugador:";
				lblMazo.Text = "Mazo actual de 5 cartas (Tabla Personajes):";
				btnGuardarBD.Text = "Guardar en BD (SqlParameter)";
				grpTabla.Text = "2. Jugadores y Estadísticas (Partidas, V, D y %)";
				btnSumarVictoria.Text = "+1 Victoria";
				btnSumarDerrota.Text = "+1 Derrota";
				btnVolver.Text = "Volver";
			}
			else
			{
				this.Text = "SQL Database (Players, Characters and Statistics)";
				lblCadena.Text = "SQL String:";
				grpNuevo.Text = "1. Register Player and Deck (PK / FK)";
				lblNombre.Text = "Player Name:";
				lblMazo.Text = "Current 5-Card Deck (Personajes Table):";
				btnGuardarBD.Text = "Save to DB (SqlParameter)";
				grpTabla.Text = "2. Players and Statistics (Matches, W, L and %)";
				btnSumarVictoria.Text = "+1 Win";
				btnSumarDerrota.Text = "+1 Loss";
				btnVolver.Text = "Back";
			}
		}

		private void MostrarMazoActual()
		{
			lstMazoGuardar.Items.Clear();
			for (int i = 0; i < Gestor.MazoGuardadoJ1.Count; i++)
			{
				lstMazoGuardar.Items.Add((i + 1) + ". " + Gestor.MazoGuardadoJ1[i].Nombre);
			}
		}

		private void CargarTablaJugadores()
		{
			BaseDatos.CadenaConexion = txtCadena.Text;
			ListaMostrada = BaseDatos.ConsultarTodos();

			lstRegistros.Items.Clear();
			for (int i = 0; i < ListaMostrada.Count; i++)
			{
				RegistroJugadorBD r = ListaMostrada[i];
				int partidas = r.Victorias + r.Derrotas;
				lstRegistros.Items.Add(
					"PK:" + r.IdJugador + " | " + r.Nombre +
					" (P:" + partidas + " | V:" + r.Victorias + " | D:" + r.Derrotas + ")");
			}

			if (lstRegistros.Items.Count > 0)
				lstRegistros.SelectedIndex = 0;
		}

		private void BtnGuardarBDClick(object sender, EventArgs e)
		{
			string nombre = txtNombre.Text.Trim();
			if (nombre == "")
			{
				MessageBox.Show(MainForm.Idioma == "ES" ? "Escribe el nombre del jugador." : "Enter player name.");
				return;
			}

			BaseDatos.CadenaConexion = txtCadena.Text;
			string respuesta = BaseDatos.GuardarJugadorYMazo(nombre, Gestor.MazoGuardadoJ1);

			txtNombre.Text = "";
			CargarTablaJugadores();
			MessageBox.Show(respuesta);
		}

		// Calcula Partidas Jugadas y el Porcentaje de Exito (%) del jugador seleccionado
		private void LstRegistrosSelectedIndexChanged(object sender, EventArgs e)
		{
			int i = lstRegistros.SelectedIndex;
			if (i < 0 || i >= ListaMostrada.Count) return;

			RegistroJugadorBD r = ListaMostrada[i];
			int partidasJugadas = r.Victorias + r.Derrotas;
			double porcentajeExito = 0;
			if (partidasJugadas > 0)
			{
				porcentajeExito = ((double)r.Victorias / (double)partidasJugadas) * 100.0;
			}

			if (MainForm.Idioma == "ES")
			{
				lblDetalleRegistro.Text =
					"Jugador (PK " + r.IdJugador + "): " + r.Nombre + "\r\n" +
					"Partidas: " + partidasJugadas + " | Victorias: " + r.Victorias + " | Derrotas: " + r.Derrotas +
					" | Éxito: " + Math.Round(porcentajeExito, 1) + "%\r\n" +
					"Mazo (FK): " + r.ResumenMazo;
			}
			else
			{
				lblDetalleRegistro.Text =
					"Player (PK " + r.IdJugador + "): " + r.Nombre + "\r\n" +
					"Matches: " + partidasJugadas + " | Wins: " + r.Victorias + " | Losses: " + r.Derrotas +
					" | Win Rate: " + Math.Round(porcentajeExito, 1) + "%\r\n" +
					"Deck (FK): " + r.ResumenMazo;
			}
		}

		private void BtnSumarVictoriaClick(object sender, EventArgs e)
		{
			int i = lstRegistros.SelectedIndex;
			if (i < 0 || i >= ListaMostrada.Count) return;

			BaseDatos.SumarResultado(ListaMostrada[i].IdJugador, true);
			CargarTablaJugadores();
		}

		private void BtnSumarDerrotaClick(object sender, EventArgs e)
		{
			int i = lstRegistros.SelectedIndex;
			if (i < 0 || i >= ListaMostrada.Count) return;

			BaseDatos.SumarResultado(ListaMostrada[i].IdJugador, false);
			CargarTablaJugadores();
		}

		private void BtnVolverClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
