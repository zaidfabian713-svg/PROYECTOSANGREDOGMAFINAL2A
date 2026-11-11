using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana para crear cartas (suma = 100) y armar los mazos de 5 personajes usando Listas (.Add, .Remove, foreach)
	public partial class FormGeneradorMazos : Form
	{
		public GestorEstructuras Gestor;
		public List<Personaje> TemporalJ1;
		public List<Personaje> TemporalJ2;

		public FormGeneradorMazos(GestorEstructuras gestor)
		{
			InitializeComponent();
			Gestor = gestor;
			TemporalJ1 = new List<Personaje>();
			TemporalJ2 = new List<Personaje>();

			// Recorremos con foreach las listas guardadas y agregamos con .Add()
			foreach (Personaje p1 in Gestor.MazoGuardadoJ1)
			{
				TemporalJ1.Add(p1.Copiar());
			}
			foreach (Personaje p2 in Gestor.MazoGuardadoJ2)
			{
				TemporalJ2.Add(p2.Copiar());
			}

			cmbElemento.SelectedIndex = 0;
			AplicarIdioma();
			MostrarCatalogo();
			MostrarMazos();

			if (lstCatalogo.Items.Count > 0)
				lstCatalogo.SelectedIndex = 0;
		}

		// Cambia los textos del formulario usando if/else basico
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Armar Mazos y Crear Cartas";
				grpCatalogo.Text = "Catálogo de 16 Cartas (List<T>)";
				grpEditor.Text = "Crear Nueva Carta (HP + DEF + ATK = 100)";
				lblNombre.Text = "Nombre:";
				lblElemento.Text = "Elemento:";
				lblNota.Text = "Regla: HP + DEF + ATK = 100 exactos";
				btnCrearCarta.Text = "Crear y Agregar Carta";
				grpMazos.Text = "Selección de Mazos (5 Personajes por Jugador)";
				btnAgregarJ1.Text = "+ Agregar a Mazo 1";
				btnLimpiarJ1.Text = "Quitar (.Remove)";
				btnAgregarJ2.Text = "+ Agregar a Mazo 2";
				btnLimpiarJ2.Text = "Quitar (.Remove)";
				btnGuardar.Text = "Guardar Ambos Mazos (5 vs 5) y Volver";
			}
			else
			{
				this.Text = "Build Decks and Create Cards";
				grpCatalogo.Text = "16-Card Catalog (List<T>)";
				grpEditor.Text = "Create New Card (HP + DEF + ATK = 100)";
				lblNombre.Text = "Name:";
				lblElemento.Text = "Element:";
				lblNota.Text = "Rule: HP + DEF + ATK = 100 exact";
				btnCrearCarta.Text = "Create and Add Card";
				grpMazos.Text = "Deck Selection (5 Characters per Player)";
				btnAgregarJ1.Text = "+ Add to Deck 1";
				btnLimpiarJ1.Text = "Remove (.Remove)";
				btnAgregarJ2.Text = "+ Add to Deck 2";
				btnLimpiarJ2.Text = "Remove (.Remove)";
				btnGuardar.Text = "Save Both Decks (5 vs 5) and Return";
			}
		}

		private void MostrarCatalogo()
		{
			lstCatalogo.Items.Clear();
			for (int i = 0; i < Gestor.ListaPersonajes.Count; i++)
			{
				Personaje p = Gestor.ListaPersonajes[i];
				lstCatalogo.Items.Add("[" + p.Elemento + "] " + p.Nombre);
			}
			for (int j = 0; j < Gestor.ListaEscenarios.Count; j++)
			{
				CartaEscenario esc = Gestor.ListaEscenarios[j];
				lstCatalogo.Items.Add("[Escenario] " + esc.Nombre);
			}
		}

		private void MostrarMazos()
		{
			lstMazoJ1.Items.Clear();
			for (int i = 0; i < TemporalJ1.Count; i++)
				lstMazoJ1.Items.Add((i + 1) + ". " + TemporalJ1[i].Nombre);

			lstMazoJ2.Items.Clear();
			for (int j = 0; j < TemporalJ2.Count; j++)
				lstMazoJ2.Items.Add((j + 1) + ". " + TemporalJ2[j].Nombre);

			if (MainForm.Idioma == "ES")
			{
				lblJ1.Text = "Mazo Jugador 1 (" + TemporalJ1.Count + "/5 cartas):";
				lblJ2.Text = "Mazo Jugador 2 (" + TemporalJ2.Count + "/5 cartas):";
			}
			else
			{
				lblJ1.Text = "Player 1 Deck (" + TemporalJ1.Count + "/5 cards):";
				lblJ2.Text = "Player 2 Deck (" + TemporalJ2.Count + "/5 cards):";
			}
		}

		private void LstCatalogoSelectedIndexChanged(object sender, EventArgs e)
		{
			int i = lstCatalogo.SelectedIndex;
			if (i < 0) return;

			if (i < Gestor.ListaPersonajes.Count)
			{
				Personaje p = Gestor.ListaPersonajes[i];
				int suma = p.HPMax + p.DEF + p.ATK;
				lblInfoCarta.Text = p.Nombre + "\r\nHP=" + p.HPMax + " DEF=" + p.DEF + " ATK=" + p.ATK + " (Suma=" + suma + ")\r\nEMP=" + p.EMP;
			}
			else
			{
				CartaEscenario esc = Gestor.ListaEscenarios[i - Gestor.ListaPersonajes.Count];
				lblInfoCarta.Text = "Escenario: " + esc.Nombre + "\r\nFacción: " + esc.Elemento + "\r\nUmbral EMP: " + esc.UmbralEMP;
			}
		}

		// Valida con int.TryParse y comprueba que HP + DEF + ATK sumen exactamente 100
		private void BtnCrearCartaClick(object sender, EventArgs e)
		{
			if (txtNombre.Text.Trim() == "")
			{
				MessageBox.Show(MainForm.Idioma == "ES" ? "Escribe el nombre del personaje." : "Enter character name.");
				return;
			}

			int hp = 0;
			int def = 0;
			int atk = 0;
			int emp = 50;

			if (!int.TryParse(txtHP.Text, out hp) || !int.TryParse(txtDEF.Text, out def) ||
			    !int.TryParse(txtATK.Text, out atk) || !int.TryParse(txtEMP.Text, out emp))
			{
				MessageBox.Show(MainForm.Idioma == "ES" ? "Ingresa solo números enteros válidos." : "Enter valid integer numbers only.");
				return;
			}

			int suma = hp + def + atk;
			if (suma != 100)
			{
				if (MainForm.Idioma == "ES")
					MessageBox.Show("Regla de balance: HP + DEF + ATK debe sumar 100 exactos.\nTu suma actual da: " + suma);
				else
					MessageBox.Show("Balance rule: HP + DEF + ATK must equal 100.\nCurrent sum: " + suma);
				return;
			}

			int nuevoId = Gestor.ListaPersonajes.Count + 1;
			Personaje nueva = new Personaje(nuevoId, txtNombre.Text.Trim(), cmbElemento.Text, hp, def, atk, emp);
			Gestor.ListaPersonajes.Add(nueva);
			MostrarCatalogo();
			MessageBox.Show(MainForm.Idioma == "ES" ? "¡Carta creada y agregada con .Add()!" : "Card created and added with .Add()!");
		}

		private void BtnAgregarJ1Click(object sender, EventArgs e)
		{
			int i = lstCatalogo.SelectedIndex;
			if (i < 0 || i >= Gestor.ListaPersonajes.Count) return;

			if (TemporalJ1.Count < 5)
			{
				TemporalJ1.Add(Gestor.ListaPersonajes[i].Copiar());
				MostrarMazos();
			}
			else
			{
				MessageBox.Show(MainForm.Idioma == "ES" ? "El Mazo 1 ya tiene 5 cartas." : "Deck 1 already has 5 cards.");
			}
		}

		// Demuestra el uso de .Remove() si selecciono una carta, o .Clear() si no selecciono ninguna
		private void BtnLimpiarJ1Click(object sender, EventArgs e)
		{
			if (lstMazoJ1.SelectedIndex >= 0)
			{
				Gestor.QuitarPersonajeDeMazo(TemporalJ1, lstMazoJ1.SelectedIndex);
			}
			else
			{
				TemporalJ1.Clear();
			}
			MostrarMazos();
		}

		private void BtnAgregarJ2Click(object sender, EventArgs e)
		{
			int i = lstCatalogo.SelectedIndex;
			if (i < 0 || i >= Gestor.ListaPersonajes.Count) return;

			if (TemporalJ2.Count < 5)
			{
				TemporalJ2.Add(Gestor.ListaPersonajes[i].Copiar());
				MostrarMazos();
			}
			else
			{
				MessageBox.Show(MainForm.Idioma == "ES" ? "El Mazo 2 ya tiene 5 cartas." : "Deck 2 already has 5 cards.");
			}
		}

		private void BtnLimpiarJ2Click(object sender, EventArgs e)
		{
			if (lstMazoJ2.SelectedIndex >= 0)
			{
				Gestor.QuitarPersonajeDeMazo(TemporalJ2, lstMazoJ2.SelectedIndex);
			}
			else
			{
				TemporalJ2.Clear();
			}
			MostrarMazos();
		}

		private void BtnGuardarClick(object sender, EventArgs e)
		{
			if (TemporalJ1.Count != 5 || TemporalJ2.Count != 5)
			{
				MessageBox.Show(MainForm.Idioma == "ES"
					? "Cada jugador debe seleccionar exactamente 5 personajes."
					: "Each player must select exactly 5 characters.");
				return;
			}

			Gestor.MazoGuardadoJ1.Clear();
			Gestor.MazoGuardadoJ2.Clear();

			foreach (Personaje p1 in TemporalJ1)
			{
				Gestor.MazoGuardadoJ1.Add(p1.Copiar());
			}
			foreach (Personaje p2 in TemporalJ2)
			{
				Gestor.MazoGuardadoJ2.Add(p2.Copiar());
			}

			Gestor.PrepararColasCombate(false);
			MessageBox.Show(MainForm.Idioma == "ES"
				? "¡Mazos de 5 personajes guardados correctamente!"
				: "5-character decks saved successfully!");
			this.Close();
		}
	}
}
