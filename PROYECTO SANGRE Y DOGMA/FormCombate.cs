using System;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana de Combate: muestra las cartas en PictureBox, llama a CalculadoraCombate y verifica fin de partida
	public partial class FormCombate : Form
	{
		public GestorEstructuras Gestor;
		public ConexionBD BaseDatos;
		public bool EsModoHistoria;

		public Personaje CartaJ1;
		public Personaje CartaJ2;

		public int Ronda = 1;
		public int Turno = 1;
		public int Segundos = 15;
		public bool FinJuego = false;

		public FormCombate(GestorEstructuras gestor, ConexionBD bd, bool modoHistoria)
		{
			InitializeComponent();
			Gestor = gestor;
			BaseDatos = bd;
			EsModoHistoria = modoHistoria;

			Gestor.PrepararColasCombate(EsModoHistoria);
			Gestor.PilaHistorial.Clear();

			// Sacamos el primer personaje de cada Cola (Queue) con Dequeue()
			CartaJ1 = Gestor.ColaJugador1.Dequeue();
			CartaJ2 = Gestor.ColaJugador2.Dequeue();

			cmbEscenario.Items.Clear();
			for (int i = 0; i < Gestor.ListaEscenarios.Count; i++)
			{
				CartaEscenario esc = Gestor.ListaEscenarios[i];
				cmbEscenario.Items.Add(esc.Nombre + " (" + esc.Elemento + ")");
			}
			cmbEscenario.SelectedIndex = 0;

			AplicarIdioma();

			// Sorteo inicial aleatorio (50% de probabilidad para cada jugador)
			Random aleatorio = new Random();
			Turno = aleatorio.Next(1, 3);

			if (MainForm.Idioma == "ES")
				GuardarLog("Inicia el combate. Sorteo aleatorio: empieza Jugador " + Turno + ".");
			else
				GuardarLog("Battle starts. Random draw: Player " + Turno + " starts.");

			ActualizarPantalla();
			timerTurno.Start();
		}

		// Cambia los textos de los botones y etiquetas usando if/else basico
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Sangre y Dogma: Arena Elemental";
				lblEscenario.Text = "Carta de Escenario:";
				btnAplicarEscenario.Text = "Aplicar Escenario";
				btnTerremoto.Text = "Terremoto";
				grpJugador1.Text = "Jugador 1";
				if (EsModoHistoria)
					grpJugador2.Text = "Sistema (Duendes)";
				else
					grpJugador2.Text = "Jugador 2";

				btnAtacar.Text = "Atacar";
				btnHabilidadEspecial.Text = "Habilidad Especial";
				btnRelevo.Text = "Relevo";
				btnPasarTurno.Text = "Pasar Turno";
				btnMostrarHistoria.Text = "MOSTRAR HISTORIA";
				lblBitacora.Text = "Registro de Combate (Pila / Stack):";
			}
			else
			{
				this.Text = "Blood and Dogma: Elemental Arena";
				lblEscenario.Text = "Stage Card:";
				btnAplicarEscenario.Text = "Apply Stage";
				btnTerremoto.Text = "Earthquake";
				grpJugador1.Text = "Player 1";
				if (EsModoHistoria)
					grpJugador2.Text = "System (Goblins)";
				else
					grpJugador2.Text = "Player 2";

				btnAtacar.Text = "Attack";
				btnHabilidadEspecial.Text = "Special Ability";
				btnRelevo.Text = "Relay";
				btnPasarTurno.Text = "Pass Turn";
				btnMostrarHistoria.Text = "MOSTRAR HISTORIA";
				lblBitacora.Text = "Battle Log (Stack):";
			}
		}

		private void GuardarLog(string mensaje)
		{
			string linea = "[R" + Ronda + "] " + mensaje;
			// Guardamos la accion en la Pila (Stack) con Push()
			Gestor.PilaHistorial.Push(linea);
			lstBitacora.Items.Insert(0, linea);
		}

		private void ActualizarPantalla()
		{
			if (MainForm.Idioma == "ES")
			{
				lblInfoTurno.Text = "  Ronda " + Ronda + " | Turno: Jugador " + Turno;
				lblReloj.Text = "Tiempo: " + Segundos + " seg";
				lblVidaJ1.Text = "Vida (HP): " + CartaJ1.HP + " / " + CartaJ1.HPMax;
				lblEspecialJ1.Text = "Habilidad Especial (ESP): " + (CartaJ1.ESP * 50) + "%";
				lblColaJ1.Text = "Relevos en Cola (Queue): " + Gestor.ColaJugador1.Count;

				lblVidaJ2.Text = "Vida (HP): " + CartaJ2.HP + " / " + CartaJ2.HPMax;
				lblEspecialJ2.Text = "Habilidad Especial (ESP): " + (CartaJ2.ESP * 50) + "%";
				lblColaJ2.Text = "Relevos en Cola (Queue): " + Gestor.ColaJugador2.Count;
			}
			else
			{
				lblInfoTurno.Text = "  Round " + Ronda + " | Turn: Player " + Turno;
				lblReloj.Text = "Time: " + Segundos + " sec";
				lblVidaJ1.Text = "Health (HP): " + CartaJ1.HP + " / " + CartaJ1.HPMax;
				lblEspecialJ1.Text = "Special Charge (ESP): " + (CartaJ1.ESP * 50) + "%";
				lblColaJ1.Text = "Relays in Queue: " + Gestor.ColaJugador1.Count;

				lblVidaJ2.Text = "Health (HP): " + CartaJ2.HP + " / " + CartaJ2.HPMax;
				lblEspecialJ2.Text = "Special Charge (ESP): " + (CartaJ2.ESP * 50) + "%";
				lblColaJ2.Text = "Relays in Queue: " + Gestor.ColaJugador2.Count;
			}

			lblDatosJ1.Text = CartaJ1.Nombre + " [" + CartaJ1.Elemento + "]\r\n" +
				"ATK: " + CartaJ1.ATK + " | DEF: " + CartaJ1.DEF + "\r\n" +
				"ENER: " + CartaJ1.ENER + " | EMP: " + CartaJ1.EMP;
			pbVidaJ1.Maximum = CartaJ1.HPMax;
			pbVidaJ1.Value = CartaJ1.HP;
			pbEspecialJ1.Maximum = 2;
			if (CartaJ1.ESP <= 2) pbEspecialJ1.Value = CartaJ1.ESP;

			lblDatosJ2.Text = CartaJ2.Nombre + " [" + CartaJ2.Elemento + "]\r\n" +
				"ATK: " + CartaJ2.ATK + " | DEF: " + CartaJ2.DEF + "\r\n" +
				"ENER: " + CartaJ2.ENER + " | EMP: " + CartaJ2.EMP;
			pbVidaJ2.Maximum = CartaJ2.HPMax;
			pbVidaJ2.Value = CartaJ2.HP;
			pbEspecialJ2.Maximum = 2;
			if (CartaJ2.ESP <= 2) pbEspecialJ2.Value = CartaJ2.ESP;

			// Actualizamos las imagenes de las cartas en los PictureBox (cambian en cada relevo o ataque)
			picCartaJ1.Image = CalculadoraCombate.GenerarImagenCarta(CartaJ1);
			picCartaJ2.Image = CalculadoraCombate.GenerarImagenCarta(CartaJ2);

			// Si termino la partida, mantenemos todos los botones de accion deshabilitados
			if (FinJuego)
			{
				btnAtacar.Enabled = false;
				btnHabilidadEspecial.Enabled = false;
				btnRelevo.Enabled = false;
				btnPasarTurno.Enabled = false;
				btnAplicarEscenario.Enabled = false;
				btnTerremoto.Enabled = false;
				return;
			}

			if (EsModoHistoria && Turno == 2)
			{
				btnAtacar.Enabled = false;
				btnHabilidadEspecial.Enabled = false;
				btnRelevo.Enabled = false;
				btnPasarTurno.Enabled = false;
				return;
			}

			Personaje actual = (Turno == 1) ? CartaJ1 : CartaJ2;
			int relevos = (Turno == 1) ? Gestor.ColaJugador1.Count : Gestor.ColaJugador2.Count;

			btnAtacar.Enabled = (actual.ENER >= 1);
			btnRelevo.Enabled = (relevos > 0);
			btnPasarTurno.Enabled = true;

			if (Ronda > 1 && actual.ESP >= 2 && actual.ENER >= 2)
				btnHabilidadEspecial.Enabled = true;
			else
				btnHabilidadEspecial.Enabled = false;
		}

		// Boton 1: Atacar llamando a CalculadoraCombate.CalcularAtaque()
		private void BtnAtacarClick(object sender, EventArgs e)
		{
			if (FinJuego) return;

			Personaje atacante = (Turno == 1) ? CartaJ1 : CartaJ2;
			Personaje defensor = (Turno == 1) ? CartaJ2 : CartaJ1;

			if (atacante.ENER < 1)
			{
				CambiarTurno();
				return;
			}

			atacante.ENER = atacante.ENER - 1;
			int dano = CalculadoraCombate.CalcularAtaque(atacante, defensor);

			if (MainForm.Idioma == "ES")
				GuardarLog(atacante.Nombre + " ataca a " + defensor.Nombre + " (-" + dano + " HP).");
			else
				GuardarLog(atacante.Nombre + " attacks " + defensor.Nombre + " (-" + dano + " HP).");

			VerificarFinDePartida();
			if (FinJuego) return;

			if (atacante.ENER > 0 && !(EsModoHistoria && Turno == 2))
				ActualizarPantalla();
			else
				CambiarTurno();
		}

		// Boton 2: Habilidad Especial llamando a CalculadoraCombate.EjecutarHabilidadEspecial()
		private void BtnHabilidadEspecialClick(object sender, EventArgs e)
		{
			if (FinJuego || Ronda == 1) return;

			Personaje atacante = (Turno == 1) ? CartaJ1 : CartaJ2;
			Personaje defensor = (Turno == 1) ? CartaJ2 : CartaJ1;

			if (atacante.ESP < 2 || atacante.ENER < 2) return;

			atacante.ENER = atacante.ENER - 2;
			atacante.ESP = 0;

			string resultado = CalculadoraCombate.EjecutarHabilidadEspecial(atacante, defensor);
			GuardarLog(resultado);

			VerificarFinDePartida();
			if (!FinJuego) CambiarTurno();
		}

		// Boton 3: Relevo usando la Cola (Queue): mete el actual al final con Enqueue y saca el siguiente con Dequeue
		private void BtnRelevoClick(object sender, EventArgs e)
		{
			if (FinJuego) return;

			if (Turno == 1 && Gestor.ColaJugador1.Count > 0)
			{
				Gestor.ColaJugador1.Enqueue(CartaJ1);
				CartaJ1 = Gestor.ColaJugador1.Dequeue();
				GuardarLog("Relevo J1: -> " + CartaJ1.Nombre);
				CambiarTurno();
			}
			else if (Turno == 2 && Gestor.ColaJugador2.Count > 0)
			{
				Gestor.ColaJugador2.Enqueue(CartaJ2);
				CartaJ2 = Gestor.ColaJugador2.Dequeue();
				GuardarLog("Relevo J2: -> " + CartaJ2.Nombre);
				CambiarTurno();
			}
		}

		// Boton 4: Pasar Turno
		private void BtnPasarTurnoClick(object sender, EventArgs e)
		{
			if (FinJuego) return;
			GuardarLog("Jugador " + Turno + " pasa turno (ENER = " + ((Turno == 1) ? CartaJ1.ENER : CartaJ2.ENER) + ").");
			CambiarTurno();
		}

		// Metodo exigido por el profesor: si la vida llega a 0 y la Cola de relevos esta vacia,
		// declara al ganador y deshabilita (Enabled = false) todos los botones de accion
		private void VerificarFinDePartida()
		{
			if (CartaJ1.HP <= 0)
			{
				if (Gestor.ColaJugador1.Count > 0)
				{
					CartaJ1 = Gestor.ColaJugador1.Dequeue();
					GuardarLog("Relevo J1: -> " + CartaJ1.Nombre);
				}
				else
				{
					DeclararGanador(2);
					return;
				}
			}

			if (CartaJ2.HP <= 0)
			{
				if (Gestor.ColaJugador2.Count > 0)
				{
					CartaJ2 = Gestor.ColaJugador2.Dequeue();
					GuardarLog("Relevo J2: -> " + CartaJ2.Nombre);
				}
				else
				{
					DeclararGanador(1);
					return;
				}
			}

			ActualizarPantalla();
		}

		private void DeclararGanador(int ganador)
		{
			FinJuego = true;
			timerTurno.Stop();

			// Deshabilitamos explicitamente todos los botones de accion una vez que alguien gana
			btnAtacar.Enabled = false;
			btnHabilidadEspecial.Enabled = false;
			btnRelevo.Enabled = false;
			btnPasarTurno.Enabled = false;
			btnAplicarEscenario.Enabled = false;
			btnTerremoto.Enabled = false;

			string nombreGanador = (ganador == 1) ? "Jugador 1" : (EsModoHistoria ? "Sistema (Duendes)" : "Jugador 2");
			int puntaje = 100 + (Ronda * 10);

			GuardarLog("SE ACABÓ EL COMBATE - GANADOR: " + nombreGanador);

			// Exportamos al archivo .txt insertando el salto de linea y el cierre destacado con el ganador
			Gestor.GuardarTxt("HistorialBatalla.txt", nombreGanador);
			Gestor.RegistrarPuntajeMaximoTxt(nombreGanador, puntaje);

			if (BaseDatos != null && BaseDatos.ListaLocal.Count > 0)
			{
				BaseDatos.SumarResultado(BaseDatos.ListaLocal[0].IdJugador, ganador == 1);
			}

			ActualizarPantalla();

			// Mostramos al ganador en un cuadro de dialogo
			if (MainForm.Idioma == "ES")
			{
				MessageBox.Show(
					"¡SE ACABÓ EL COMBATE!\n\nGanador: " + nombreGanador +
					"\n\nEl resultado final fue guardado en HistorialBatalla.txt." +
					"\nAl presionar Aceptar volverás al Menú Principal.",
					"Fin de la Partida", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show(
					"THE BATTLE IS OVER!\n\nWinner: " + nombreGanador +
					"\n\nFinal result saved in HistorialBatalla.txt." +
					"\nPress OK to return to the Main Menu.",
					"End of Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			// Cerramos la ventana de combate. Como el Menu Principal la abrio con ShowDialog(),
			// al cerrarse el programa regresa automaticamente al Menu Principal.
			this.Close();
		}

		private void CambiarTurno()
		{
			if (FinJuego) return;

			if (Turno == 1)
			{
				Turno = 2;
				CartaJ2.ENER = CartaJ2.ENER + 1;
				if (CartaJ2.ESP < 2) CartaJ2.ESP = CartaJ2.ESP + 1;
			}
			else
			{
				Turno = 1;
				Ronda = Ronda + 1;
				CartaJ1.ENER = CartaJ1.ENER + 1;
				if (CartaJ1.ESP < 2) CartaJ1.ESP = CartaJ1.ESP + 1;
			}

			Segundos = 15;
			ActualizarPantalla();
		}

		private void TimerTurnoTick(object sender, EventArgs e)
		{
			if (FinJuego) return;

			if (EsModoHistoria && Turno == 2 && Segundos <= 13)
			{
				if (Ronda > 1 && CartaJ2.ESP >= 2 && CartaJ2.ENER >= 2)
					BtnHabilidadEspecialClick(null, null);
				else
					BtnAtacarClick(null, null);
				return;
			}

			Segundos = Segundos - 1;
			ActualizarPantalla();

			if (Segundos <= 0)
			{
				GuardarLog("0 seg -> Pasa turno.");
				CambiarTurno();
			}
		}

		// Aplica Buff (+4 ATK) o Nerf (-2 DEF) llamando a CalculadoraCombate.AplicarEscenario()
		private void BtnAplicarEscenarioClick(object sender, EventArgs e)
		{
			if (FinJuego) return;
			CartaEscenario esc = Gestor.ListaEscenarios[cmbEscenario.SelectedIndex];
			string b1 = CalculadoraCombate.AplicarEscenario(esc, CartaJ1);
			string b2 = CalculadoraCombate.AplicarEscenario(esc, CartaJ2);
			GuardarLog(b1);
			GuardarLog(b2);
			ActualizarPantalla();
		}

		// Dispara el evento algoritmico "Terremoto" llamando a CalculadoraCombate.DispararTerremoto()
		private void BtnTerremotoClick(object sender, EventArgs e)
		{
			if (FinJuego) return;
			CartaEscenario esc = Gestor.ListaEscenarios[cmbEscenario.SelectedIndex];
			string r1 = CalculadoraCombate.DispararTerremoto(esc, CartaJ1);
			string r2 = CalculadoraCombate.DispararTerremoto(esc, CartaJ2);
			GuardarLog(r1);
			GuardarLog(r2);
			VerificarFinDePartida();
			// Solo mostramos el resultado del terremoto si la partida sigue (si termino, la ventana ya se cerro)
			if (!FinJuego)
				MessageBox.Show(r1 + "\n" + r2, "Terremoto / Earthquake");
		}

		// Boton MOSTRAR HISTORIA: muestra el contexto y motivo de los dos personajes en combate
		private void BtnMostrarHistoriaClick(object sender, EventArgs e)
		{
			if (MainForm.Idioma == "ES")
			{
				string texto =
					"=== JUGADOR 1: " + CartaJ1.Nombre + " (" + CartaJ1.Elemento + ") ===\r\n" +
					CartaJ1.Historia + "\r\n\r\n" +
					"=== RIVAL: " + CartaJ2.Nombre + " (" + CartaJ2.Elemento + ") ===\r\n" +
					CartaJ2.Historia;
				MessageBox.Show(texto, "MOSTRAR HISTORIA DE LOS COMBATIENTES");
			}
			else
			{
				string texto =
					"=== PLAYER 1: " + CartaJ1.Nombre + " (" + CartaJ1.Elemento + ") ===\r\n" +
					CartaJ1.HistoriaEN + "\r\n\r\n" +
					"=== RIVAL: " + CartaJ2.Nombre + " (" + CartaJ2.Elemento + ") ===\r\n" +
					CartaJ2.HistoriaEN;
				MessageBox.Show(texto, "SHOW FIGHTERS STORY");
			}
		}

		private void FormCombateFormClosing(object sender, FormClosingEventArgs e)
		{
			timerTurno.Stop();
		}
	}
}
