using System;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana para consultar la pequena historia (contexto y motivo) y los atributos numericos de las 16 cartas
	public partial class FormLore : Form
	{
		public GestorEstructuras Gestor;

		public FormLore(GestorEstructuras gestor)
		{
			InitializeComponent();
			Gestor = gestor;

			AplicarIdioma();

			for (int i = 0; i < Gestor.ListaPersonajes.Count; i++)
			{
				Personaje p = Gestor.ListaPersonajes[i];
				lstPersonajes.Items.Add("[" + p.Elemento + "] " + p.Nombre);
			}
			for (int j = 0; j < Gestor.ListaEscenarios.Count; j++)
			{
				CartaEscenario esc = Gestor.ListaEscenarios[j];
				lstPersonajes.Items.Add("[Escenario] " + esc.Nombre);
			}

			if (lstPersonajes.Items.Count > 0)
				lstPersonajes.SelectedIndex = 0;
		}

		// Cambia los textos de los controles usando if/else basico
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Historia y Catálogo de Personajes";
				grpLista.Text = "16 Cartas del Catálogo";
				grpHistoria.Text = "Historia (Contexto y Motivo) y Atributos";
				btnMostrarHistoria.Text = "MOSTRAR HISTORIA";
				btnCerrar.Text = "Volver";
			}
			else
			{
				this.Text = "Character Story and Catalog";
				grpLista.Text = "16 Catalog Cards";
				grpHistoria.Text = "Story (Context & Motive) and Stats";
				btnMostrarHistoria.Text = "MOSTRAR HISTORIA / SHOW STORY";
				btnCerrar.Text = "Back";
			}
		}

		private void LstPersonajesSelectedIndexChanged(object sender, EventArgs e)
		{
			int i = lstPersonajes.SelectedIndex;
			if (i < 0) return;

			if (i < Gestor.ListaPersonajes.Count)
			{
				Personaje p = Gestor.ListaPersonajes[i];
				int suma = p.HPMax + p.DEF + p.ATK;

				// Mostramos la carta ilustrada en el PictureBox
				picCarta.Image = CalculadoraCombate.ObtenerImagenCartaGrande(p);
				lblInfoCarta.Text = p.Nombre + "\r\n[" + p.Elemento + "] HP:" + p.HPMax + " ATK:" + p.ATK + " DEF:" + p.DEF;

				if (MainForm.Idioma == "ES")
				{
					string tipoClase = "Personaje Base";
					if (p is Protagonista)
					{
						Protagonista prota = (Protagonista)p;
						tipoClase = "Clase Hija: Protagonista (Rol: " + prota.Rol + ")";
					}
					else if (p is Duende)
					{
						Duende duen = (Duende)p;
						tipoClase = "Clase Hija: Duende (Nivel: " + duen.Nivel + ")";
					}

					txtHistoria.Text =
						"HISTORIA DEL PERSONAJE (CONTEXTO Y MOTIVO)\r\n" +
						"--------------------------------------------\r\n" +
						p.Historia + "\r\n\r\n" +
						"FICHA TÉCNICA DE COMBATE\r\n" +
						"--------------------------------------------\r\n" +
						"Nombre: " + p.Nombre + "\r\n" +
						"Jerarquía POO: " + tipoClase + "\r\n" +
						"Facción Elemental: " + p.Elemento + "\r\n\r\n" +
						"ATRIBUTOS NUMÉRICOS (SUMA = 100):\r\n" +
						"- HP: " + p.HPMax + " | DEF: " + p.DEF + " | ATK: " + p.ATK + " (Total: " + suma + "/100)\r\n" +
						"- ENER: " + p.ENER + " | ESP: " + p.ESP + "/2 | EMP: " + p.EMP;
				}
				else
				{
					string tipoClase = "Base Character";
					if (p is Protagonista)
					{
						Protagonista prota = (Protagonista)p;
						tipoClase = "Child Class: Protagonista (Role: " + prota.Rol + ")";
					}
					else if (p is Duende)
					{
						Duende duen = (Duende)p;
						tipoClase = "Child Class: Duende (Level: " + duen.Nivel + ")";
					}

					txtHistoria.Text =
						"CHARACTER STORY (CONTEXT AND MOTIVE)\r\n" +
						"--------------------------------------------\r\n" +
						p.HistoriaEN + "\r\n\r\n" +
						"COMBAT TECHNICAL SHEET\r\n" +
						"--------------------------------------------\r\n" +
						"Name: " + p.Nombre + "\r\n" +
						"OOP Hierarchy: " + tipoClase + "\r\n" +
						"Elemental Faction: " + p.Elemento + "\r\n\r\n" +
						"NUMERICAL ATTRIBUTES (SUM = 100):\r\n" +
						"- HP: " + p.HPMax + " | DEF: " + p.DEF + " | ATK: " + p.ATK + " (Total: " + suma + "/100)\r\n" +
						"- ENER: " + p.ENER + " | ESP: " + p.ESP + "/2 | EMP: " + p.EMP;
				}
			}
			else
			{
				CartaEscenario esc = Gestor.ListaEscenarios[i - Gestor.ListaPersonajes.Count];
				picCarta.Image = CalculadoraCombate.ObtenerImagenEscenario(esc);
				lblInfoCarta.Text = esc.Nombre + "\r\n[Escenario - " + esc.Elemento + "]";

				if (MainForm.Idioma == "ES")
				{
					txtHistoria.Text =
						"HISTORIA Y CONTEXTO DEL ESCENARIO\r\n" +
						"--------------------------------------------\r\n" +
						esc.Historia + "\r\n\r\n" +
						"FICHA DE CARTA DE ESCENARIO\r\n" +
						"--------------------------------------------\r\n" +
						"Nombre: " + esc.Nombre + "\r\n" +
						"Elemento Asociado: " + esc.Elemento + "\r\n" +
						"Umbral de Empatía (Terremoto): " + esc.UmbralEMP;
				}
				else
				{
					txtHistoria.Text =
						"STAGE STORY AND CONTEXT\r\n" +
						"--------------------------------------------\r\n" +
						esc.HistoriaEN + "\r\n\r\n" +
						"STAGE CARD SHEET\r\n" +
						"--------------------------------------------\r\n" +
						"Name: " + esc.Nombre + "\r\n" +
						"Element: " + esc.Elemento + "\r\n" +
						"Empathy Threshold (Earthquake): " + esc.UmbralEMP;
				}
			}
		}

		// Boton MOSTRAR HISTORIA para desplegar el contexto y motivo en un MessageBox
		private void BtnMostrarHistoriaClick(object sender, EventArgs e)
		{
			int i = lstPersonajes.SelectedIndex;
			if (i < 0) return;

			if (i < Gestor.ListaPersonajes.Count)
			{
				Personaje p = Gestor.ListaPersonajes[i];
				if (MainForm.Idioma == "ES")
					MessageBox.Show(p.Nombre + " (" + p.Elemento + ")\r\n\r\n" + p.Historia, "MOSTRAR HISTORIA");
				else
					MessageBox.Show(p.Nombre + " (" + p.Elemento + ")\r\n\r\n" + p.HistoriaEN, "SHOW STORY");
			}
			else
			{
				CartaEscenario esc = Gestor.ListaEscenarios[i - Gestor.ListaPersonajes.Count];
				if (MainForm.Idioma == "ES")
					MessageBox.Show(esc.Nombre + "\r\n\r\n" + esc.Historia, "MOSTRAR HISTORIA");
				else
					MessageBox.Show(esc.Nombre + "\r\n\r\n" + esc.HistoriaEN, "SHOW STORY");
			}
		}

		private void BtnCerrarClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
