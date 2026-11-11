using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// Ventana para pelear en red local usando TcpListener y TcpClient con textos simples separados por comas
	public partial class FormCombateRed : Form
	{
		public GestorEstructuras Gestor;
		public TcpListener Servidor;
		public TcpClient Cliente;
		public NetworkStream FlujoRed;

		public Queue<Personaje> MiCola;
		public Personaje MiCarta;
		public Personaje CartaRemota;

		public bool SoyServidor = false;
		public bool Conectado = false;
		public bool EsMiTurno = false;
		public int RondaRed = 1;

		public FormCombateRed(GestorEstructuras gestor)
		{
			InitializeComponent();
			Gestor = gestor;
			MiCola = new Queue<Personaje>();

			for (int i = 0; i < Gestor.MazoGuardadoJ1.Count; i++)
			{
				MiCola.Enqueue(Gestor.MazoGuardadoJ1[i].Copiar());
			}
			MiCarta = MiCola.Dequeue();
			CartaRemota = new Personaje(0, "Rival", "Fuego", 45, 25, 30, 50);

			AplicarIdioma();
			ActualizarVista();
		}

		// Cambia los textos del formulario usando if/else basico
		private void AplicarIdioma()
		{
			if (MainForm.Idioma == "ES")
			{
				this.Text = "Combate en Red Local (LAN - Sockets TCP/IP)";
				grpConexion.Text = "1. Conexión TCP/IP (TcpListener / TcpClient)";
				btnServidor.Text = "Crear Partida (Servidor)";
				btnCliente.Text = "Unirse (Cliente)";
				lblEstadoRed.Text = "Estado: Desconectado (Usa 127.0.0.1 para probar con 2 ventanas)";
				grpLocal.Text = "Mi Personaje Activo (Local)";
				grpRemoto.Text = "Personaje Rival (En Red)";
				btnAtacarRed.Text = "Atacar";
				btnEspecialRed.Text = "Habilidad Especial";
				btnRelevoRed.Text = "Relevo";
				btnPasarRed.Text = "Pasar Turno";
				btnEnviarChat.Text = "Enviar";
			}
			else
			{
				this.Text = "Local Network Battle (LAN - TCP/IP Sockets)";
				grpConexion.Text = "1. TCP/IP Connection (TcpListener / TcpClient)";
				btnServidor.Text = "Host Match (Server)";
				btnCliente.Text = "Join (Client)";
				lblEstadoRed.Text = "Status: Disconnected (Use 127.0.0.1 to test with 2 windows)";
				grpLocal.Text = "My Active Character (Local)";
				grpRemoto.Text = "Rival Character (Network)";
				btnAtacarRed.Text = "Attack";
				btnEspecialRed.Text = "Special Ability";
				btnRelevoRed.Text = "Relay";
				btnPasarRed.Text = "Pass Turn";
				btnEnviarChat.Text = "Send";
			}
		}

		private void BtnServidorClick(object sender, EventArgs e)
		{
			try
			{
				int puerto = Convert.ToInt32(txtPuerto.Text);
				Servidor = new TcpListener(IPAddress.Any, puerto);
				Servidor.Start();

				SoyServidor = true;
				btnServidor.Enabled = false;
				btnCliente.Enabled = false;
				lblEstadoRed.Text = (MainForm.Idioma == "ES")
					? "Servidor esperando en puerto " + puerto + "..."
					: "Server waiting on port " + puerto + "...";
				timerRed.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}

		private void BtnClienteClick(object sender, EventArgs e)
		{
			try
			{
				int puerto = Convert.ToInt32(txtPuerto.Text);
				Cliente = new TcpClient(txtIP.Text.Trim(), puerto);
				FlujoRed = Cliente.GetStream();

				SoyServidor = false;
				Conectado = true;
				EsMiTurno = false;
				btnServidor.Enabled = false;
				btnCliente.Enabled = false;

				lblEstadoRed.Text = (MainForm.Idioma == "ES")
					? "¡Conectado al Servidor! Esperando turno del Jugador 1..."
					: "Connected to Server! Waiting for Player 1...";
				EnviarTextoPorRed("CARTA," + MiCarta.Nombre + "," + MiCarta.Elemento + "," + MiCarta.HP + "," + MiCarta.HPMax + "," + MiCarta.ATK + "," + MiCarta.DEF);
				ActualizarVista();
				timerRed.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}

		// Envia un string simple separado por comas a traves del NetworkStream
		private void EnviarTextoPorRed(string mensajeConComas)
		{
			if (FlujoRed == null) return;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(mensajeConComas + "\n");
				FlujoRed.Write(bytes, 0, bytes.Length);
			}
			catch
			{
				lblEstadoRed.Text = "Conexión cerrada / Connection closed.";
			}
		}

		private void TimerRedTick(object sender, EventArgs e)
		{
			if (SoyServidor && !Conectado && Servidor != null)
			{
				if (Servidor.Pending())
				{
					Cliente = Servidor.AcceptTcpClient();
					FlujoRed = Cliente.GetStream();
					Conectado = true;
					EsMiTurno = true;

					lblEstadoRed.Text = (MainForm.Idioma == "ES")
						? "¡Cliente conectado! Es tu turno (Jugador 1)."
						: "Client connected! It is your turn (Player 1).";
					EnviarTextoPorRed("CARTA," + MiCarta.Nombre + "," + MiCarta.Elemento + "," + MiCarta.HP + "," + MiCarta.HPMax + "," + MiCarta.ATK + "," + MiCarta.DEF);
					ActualizarVista();
				}
				return;
			}

			if (Conectado && FlujoRed != null && FlujoRed.DataAvailable)
			{
				byte[] buffer = new byte[1024];
				int leidos = FlujoRed.Read(buffer, 0, buffer.Length);
				string recibido = Encoding.UTF8.GetString(buffer, 0, leidos);

				string[] lineas = recibido.Split('\n');
				for (int i = 0; i < lineas.Length; i++)
				{
					// Si la ventana ya se cerro por fin de partida, dejamos de procesar mensajes
					if (this.IsDisposed) break;
					string lin = lineas[i].Trim();
					if (lin != "") ProcesarMensajeConComas(lin);
				}
			}
		}

		// Procesa los comandos separados por comas (CARTA, ATAQUE, ESPECIAL, TURNO, VICTORIA, CHAT)
		private void ProcesarMensajeConComas(string paquete)
		{
			string[] datos = paquete.Split(',');
			string tipo = datos[0];

			if (tipo == "CARTA" && datos.Length >= 7)
			{
				CartaRemota.Nombre = datos[1];
				CartaRemota.Elemento = datos[2];
				CartaRemota.HP = Convert.ToInt32(datos[3]);
				CartaRemota.HPMax = Convert.ToInt32(datos[4]);
				CartaRemota.ATK = Convert.ToInt32(datos[5]);
				CartaRemota.DEF = Convert.ToInt32(datos[6]);
				ActualizarVista();
			}
			else if (tipo == "ATAQUE" || tipo == "ESPECIAL")
			{
				int dano = Convert.ToInt32(datos[1]);
				string resumen = datos[2];

				MiCarta.HP = MiCarta.HP - dano;
				if (MiCarta.HP < 0) MiCarta.HP = 0;
				AgregarLog("[RED] " + resumen);

				if (MiCarta.HP <= 0)
				{
					if (MiCola.Count > 0)
					{
						MiCarta = MiCola.Dequeue();
						AgregarLog("Relevo: -> " + MiCarta.Nombre);
					}
					else
					{
						EnviarTextoPorRed("VICTORIA,5/5");
						Conectado = false;
						ActualizarVista();
						MessageBox.Show(MainForm.Idioma == "ES" ? "Fin del combate en red: Has perdido.\nVolverás al Menú Principal." : "Network battle ended: You lost.\nReturning to Main Menu.");
						this.Close();
						return;
					}
				}

				EnviarTextoPorRed("CARTA," + MiCarta.Nombre + "," + MiCarta.Elemento + "," + MiCarta.HP + "," + MiCarta.HPMax + "," + MiCarta.ATK + "," + MiCarta.DEF);
				ActivarMiTurno();
			}
			else if (tipo == "TURNO")
			{
				AgregarLog("[RED] " + datos[1]);
				ActivarMiTurno();
			}
			else if (tipo == "VICTORIA")
			{
				Conectado = false;
				EsMiTurno = false;
				ActualizarVista();
				MessageBox.Show(MainForm.Idioma == "ES" ? "¡GANASTE EN RED!\nVolverás al Menú Principal." : "YOU WON ON NETWORK!\nReturning to Main Menu.");
				this.Close();
			}
			else if (tipo == "CHAT")
			{
				AgregarLog("[Rival]: " + datos[1]);
			}
		}

		private void ActivarMiTurno()
		{
			EsMiTurno = true;
			RondaRed = RondaRed + 1;
			MiCarta.ENER = MiCarta.ENER + 1;
			if (MiCarta.ESP < 2) MiCarta.ESP = MiCarta.ESP + 1;

			lblEstadoRed.Text = (MainForm.Idioma == "ES")
				? "¡ES TU TURNO! (Ronda " + RondaRed + ")"
				: "YOUR TURN! (Round " + RondaRed + ")";
			ActualizarVista();
		}

		private void BtnAtacarRedClick(object sender, EventArgs e)
		{
			if (!Conectado || !EsMiTurno) return;

			int dano = CalculadoraCombate.CalcularAtaque(MiCarta, CartaRemota);
			string log = MiCarta.Nombre + " -> -" + dano + " HP";
			AgregarLog(log);

			EnviarTextoPorRed("ATAQUE," + dano + "," + log);
			TerminarMiTurno();
		}

		private void BtnEspecialRedClick(object sender, EventArgs e)
		{
			if (!Conectado || !EsMiTurno) return;
			if (RondaRed == 1 || MiCarta.ESP < 2 || MiCarta.ENER < 2) return;

			MiCarta.ENER = MiCarta.ENER - 2;
			MiCarta.ESP = 0;

			int vidaAntes = CartaRemota.HP;
			string log = CalculadoraCombate.EjecutarHabilidadEspecial(MiCarta, CartaRemota).Replace(",", ";");
			int dano = vidaAntes - CartaRemota.HP;
			if (dano < 0) dano = 0;

			AgregarLog(log);
			EnviarTextoPorRed("ESPECIAL," + dano + "," + log);
			EnviarTextoPorRed("CARTA," + MiCarta.Nombre + "," + MiCarta.Elemento + "," + MiCarta.HP + "," + MiCarta.HPMax + "," + MiCarta.ATK + "," + MiCarta.DEF);
			TerminarMiTurno();
		}

		private void BtnRelevoRedClick(object sender, EventArgs e)
		{
			if (!Conectado || !EsMiTurno || MiCola.Count == 0) return;

			MiCola.Enqueue(MiCarta);
			MiCarta = MiCola.Dequeue();

			string log = "Relevo: -> " + MiCarta.Nombre;
			AgregarLog(log);

			EnviarTextoPorRed("CARTA," + MiCarta.Nombre + "," + MiCarta.Elemento + "," + MiCarta.HP + "," + MiCarta.HPMax + "," + MiCarta.ATK + "," + MiCarta.DEF);
			EnviarTextoPorRed("TURNO," + log);
			TerminarMiTurno();
		}

		private void BtnPasarRedClick(object sender, EventArgs e)
		{
			if (!Conectado || !EsMiTurno) return;
			EnviarTextoPorRed("TURNO,Paso turno / Pass turn");
			TerminarMiTurno();
		}

		private void TerminarMiTurno()
		{
			EsMiTurno = false;
			lblEstadoRed.Text = (MainForm.Idioma == "ES")
				? "Turno enviado. Esperando al rival..."
				: "Turn sent. Waiting for rival...";
			ActualizarVista();
		}

		private void BtnEnviarChatClick(object sender, EventArgs e)
		{
			if (!Conectado || txtChat.Text.Trim() == "") return;

			string msg = txtChat.Text.Trim().Replace(",", " ");
			EnviarTextoPorRed("CHAT," + msg);
			AgregarLog("[Yo]: " + msg);
			txtChat.Text = "";
		}

		private void AgregarLog(string texto)
		{
			Gestor.PilaHistorial.Push(texto);
			lstRed.Items.Insert(0, texto);
		}

		private void ActualizarVista()
		{
			lblDatosLocal.Text = MiCarta.Nombre + " (" + MiCarta.Elemento + ")\n" +
				"HP:" + MiCarta.HP + "/" + MiCarta.HPMax + " | ATK:" + MiCarta.ATK +
				" | DEF:" + MiCarta.DEF + " | ENER:" + MiCarta.ENER + " | ESP:" + MiCarta.ESP + "/2";
			pbVidaLocal.Maximum = MiCarta.HPMax;
			pbVidaLocal.Value = MiCarta.HP;
			lblColaLocal.Text = (MainForm.Idioma == "ES")
				? "Mis Relevos en Cola (Queue): " + MiCola.Count
				: "My Relays in Queue: " + MiCola.Count;

			lblDatosRemoto.Text = CartaRemota.Nombre + " (" + CartaRemota.Elemento + ")\n" +
				"HP:" + CartaRemota.HP + "/" + CartaRemota.HPMax + " | ATK:" + CartaRemota.ATK + " | DEF:" + CartaRemota.DEF;
			pbVidaRemoto.Maximum = CartaRemota.HPMax;
			if (CartaRemota.HP <= pbVidaRemoto.Maximum)
				pbVidaRemoto.Value = CartaRemota.HP;

			btnAtacarRed.Enabled = (Conectado && EsMiTurno);
			btnRelevoRed.Enabled = (Conectado && EsMiTurno && MiCola.Count > 0);
			btnPasarRed.Enabled = (Conectado && EsMiTurno);

			if (Conectado && EsMiTurno && RondaRed > 1 && MiCarta.ESP >= 2 && MiCarta.ENER >= 2)
				btnEspecialRed.Enabled = true;
			else
				btnEspecialRed.Enabled = false;
		}

		private void FormCombateRedFormClosing(object sender, FormClosingEventArgs e)
		{
			timerRed.Stop();
			try
			{
				if (FlujoRed != null) FlujoRed.Close();
				if (Cliente != null) Cliente.Close();
				if (Servidor != null) Servidor.Stop();
			}
			catch
			{
			}
		}
	}
}
