using System;
using System.Collections.Generic;
using System.IO;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// CLASE GESTORA DE ESTRUCTURAS DE DATOS (Contrato de Aprendizaje + Enunciado)
	// Nota para el profesor:
	// 1. Usamos Arreglo fijo (Personaje[]) y Listas (List<Personaje>) con .Add(), .Remove() y foreach
	//    para cumplir el Contrato de Aprendizaje en el manejo del catalogo de 12 personajes y los mazos.
	// 2. Usamos Cola (Queue<Personaje>) UNICAMENTE para el orden de relevos en combate (FIFO: el primero
	//    en la fila es el primero en entrar a pelear).
	// 3. Usamos Pila (Stack<string>) UNICAMENTE para el historial de ataques (LIFO: el ultimo ataque
	//    ocurrido queda en el tope del registro).
	public class GestorEstructuras
	{
		// Arreglo estatico de 12 cartas de personaje (3 por cada una de las 4 facciones elementales)
		public static Personaje[] Catalogo12Personajes = new Personaje[12];

		// Listas dinamicas (List<T>) para el catalogo y los mazos de 5 personajes
		public List<Personaje> ListaPersonajes;
		public List<CartaEscenario> ListaEscenarios;
		public List<Personaje> MazoGuardadoJ1;
		public List<Personaje> MazoGuardadoJ2;

		// Cola (Queue) usada exclusivamente para la mecanica de relevos en combate
		public Queue<Personaje> ColaJugador1;
		public Queue<Personaje> ColaJugador2;

		// Pila (Stack) usada exclusivamente para el historial de ataques
		public Stack<string> PilaHistorial;

		public GestorEstructuras()
		{
			ListaPersonajes = new List<Personaje>();
			ListaEscenarios = new List<CartaEscenario>();
			MazoGuardadoJ1 = new List<Personaje>();
			MazoGuardadoJ2 = new List<Personaje>();
			ColaJugador1 = new Queue<Personaje>();
			ColaJugador2 = new Queue<Personaje>();
			PilaHistorial = new Stack<string>();

			CargarCatalogo16Cartas();
			CargarMazosIniciales();
		}

		// Instancia los 12 personajes (3 Agua, 3 Tierra, 3 Viento, 3 Fuego; HP+DEF+ATK=100) y los 4 escenarios
		public void CargarCatalogo16Cartas()
		{
			ListaPersonajes.Clear();
			ListaEscenarios.Clear();

			// 5 Protagonistas (Clases hijas Protagonista) con una pequena historia sencilla (Contexto y Motivo)
			Catalogo12Personajes[0] = new Protagonista(1, "Fabián - El Titán de Hierro",  "Tierra", 45, 30, 25, 65, "Guerrero");
			Catalogo12Personajes[0].Historia = "Contexto: Antiguo herrero de las montañas cuya aldea fue destruida por un terremoto.\r\nMotivo: Entró a la Arena Elemental con su armadura de hierro para proteger a su pueblo.";
			Catalogo12Personajes[0].HistoriaEN = "Context: Former blacksmith from the northern mountains whose village was destroyed by an earthquake.\r\nMotive: Entered the Elemental Arena in his iron armor to protect his people.";

			Catalogo12Personajes[1] = new Protagonista(2, "Deibislin - La Maga de Fuego", "Fuego",  42, 20, 38, 80, "Maga");
			Catalogo12Personajes[1].Historia = "Contexto: Joven aprendiz de la Academia Real que descubrió el poder del fuego en un libro antiguo.\r\nMotivo: Lucha en la Arena para dominar sus llamas y recuperar el honor de su familia.";
			Catalogo12Personajes[1].HistoriaEN = "Context: Young apprentice of the Royal Academy who discovered fire magic in an ancient book.\r\nMotive: Fights in the Arena to master her flames and restore her family's honor.";

			Catalogo12Personajes[2] = new Protagonista(3, "Jesús - El Curandero Blanco",  "Viento", 44, 32, 24, 75, "Sanador");
			Catalogo12Personajes[2].Historia = "Contexto: Monje viajero del Templo del Viento dedicado a curar a los heridos.\r\nMotivo: Participa en el torneo buscando restaurar la paz entre las cuatro facciones.";
			Catalogo12Personajes[2].HistoriaEN = "Context: Traveling monk from the Wind Temple dedicated to healing the wounded.\r\nMotive: Joins the tournament seeking to restore peace among the four factions.";

			Catalogo12Personajes[3] = new Protagonista(4, "Yeferson - El Alquimista",     "Agua",   46, 26, 28, 70, "Alquimista");
			Catalogo12Personajes[3].Historia = "Contexto: Estudiante e investigador de los pantanos del sur que prepara pociones de agua y ácidos.\r\nMotivo: Combate para reunir ingredientes y crear una medicina para su aldea.";
			Catalogo12Personajes[3].HistoriaEN = "Context: Student and researcher from the southern swamps who brews water potions and acids.\r\nMotive: Fights to gather ingredients and create a cure for his village.";

			Catalogo12Personajes[4] = new Protagonista(5, "Ereu - El Brujo de Sombras",   "Fuego",  40, 22, 38, 35, "Brujo");
			Catalogo12Personajes[4].Historia = "Contexto: Hechicero solitario que entrenó en las ruinas volcánicas dominando el fuego oscuro.\r\nMotivo: Entró a la Arena Elemental para absorber la energía de los rivales más fuertes.";
			Catalogo12Personajes[4].HistoriaEN = "Context: Lone sorcerer who trained in volcanic ruins mastering dark fire.\r\nMotive: Entered the Elemental Arena to absorb energy from the strongest rivals.";

			// 7 Duendes de niveles 1 al 7 (Clases hijas Duende) con su contexto y motivo
			Catalogo12Personajes[5]  = new Duende(6,  "Duende Explorador Nv. 1", "Tierra", 52, 28, 20, 30, 1);
			Catalogo12Personajes[5].Historia = "Contexto: Pequeño vigía de las cuevas subterráneas encargado de vigilar los caminos de piedra.\r\nMotivo: Pelea por órdenes del Rey Duende para defender la entrada de su territorio.";
			Catalogo12Personajes[5].HistoriaEN = "Context: Small lookout of the underground caves in charge of watching the stone paths.\r\nMotive: Fights by order of the Goblin King to defend their territory's entrance.";

			Catalogo12Personajes[6]  = new Duende(7,  "Duende Saqueador Nv. 2",  "Viento", 50, 26, 24, 35, 2);
			Catalogo12Personajes[6].Historia = "Contexto: Habitante veloz de los cañones ventosos que recolecta objetos perdidos.\r\nMotivo: Entra al combate motivado por quedarse con el botín de los rivales vencidos.";
			Catalogo12Personajes[6].HistoriaEN = "Context: Fast dweller of the windy canyons who collects lost items.\r\nMotive: Enters combat motivated by keeping the loot of defeated rivals.";

			Catalogo12Personajes[7]  = new Duende(8,  "Duende Chamán Nv. 3",     "Agua",   48, 26, 26, 55, 3);
			Catalogo12Personajes[7].Historia = "Contexto: Aprendiz de magia de la tribu del río que aprendió a controlar la lluvia.\r\nMotivo: Su motivo es proteger los manantiales de agua limpia donde vive su clan.";
			Catalogo12Personajes[7].HistoriaEN = "Context: Magic apprentice of the river tribe who learned to control the rain.\r\nMotive: His motive is to protect the clean water springs where his clan lives.";

			Catalogo12Personajes[8]  = new Duende(9,  "Duende Cazador Nv. 4",    "Agua",   46, 24, 30, 45, 4);
			Catalogo12Personajes[8].Historia = "Contexto: Rastreador experto de las cascadas que entrena todos los días con su lanza.\r\nMotivo: Lucha en la Arena para alimentar a su aldea y subir de rango.";
			Catalogo12Personajes[8].HistoriaEN = "Context: Expert tracker of the waterfalls who trains every day with his spear.\r\nMotive: Fights in the Arena to feed his village and rise in rank.";

			Catalogo12Personajes[9]  = new Duende(10, "Duende Berserker Nv. 5",  "Tierra", 42, 26, 32, 25, 5);
			Catalogo12Personajes[9].Historia = "Contexto: Guerrero criado en las minas profundas que destaca por su resistencia física.\r\nMotivo: Su objetivo es enfrentarse a guerreros grandes como Fabián para medir su fuerza.";
			Catalogo12Personajes[9].HistoriaEN = "Context: Warrior raised in the deep mines known for his physical endurance.\r\nMotive: His goal is to face big warriors like Fabian to test his strength.";

			Catalogo12Personajes[10] = new Duende(11, "Duende Capitán Nv. 6",    "Viento", 44, 22, 34, 40, 6);
			Catalogo12Personajes[10].Historia = "Contexto: Comandante veterano de la guardia real que dirige a las tropas desde las alturas.\r\nMotivo: Combate por lealtad a su rey y para mantener unidas a las tribus.";
			Catalogo12Personajes[10].HistoriaEN = "Context: Veteran commander of the royal guard who leads troops from the heights.\r\nMotive: Fights out of loyalty to his king and to keep the tribes united.";

			Catalogo12Personajes[11] = new Duende(12, "Jefe Duende Nv. 7",        "Fuego",  45, 20, 35, 60, 7);
			Catalogo12Personajes[11].Historia = "Contexto: Monarca del Volcán de Fuego que unió a los clanes duendes bajo su mando.\r\nMotivo: Lucha en la Arena Elemental para defender su reino y exigir respeto para su pueblo.";
			Catalogo12Personajes[11].HistoriaEN = "Context: Monarch of the Fire Volcano who united the goblin clans under his command.\r\nMotive: Fights in the Elemental Arena to defend his kingdom and demand respect for his people.";

			// Recorremos el arreglo con foreach y cargamos la Lista usando .Add()
			foreach (Personaje p in Catalogo12Personajes)
			{
				ListaPersonajes.Add(p.Copiar());
			}

			// 4 Cartas de Escenario
			CartaEscenario esc1 = new CartaEscenario(13, "Santuario de Agua",   "Agua",   50);
			esc1.Historia = "Contexto: Antiguo templo rodeado de cascadas.\r\nEfecto: Da +4 ATK a facción Agua y -2 DEF al resto.";
			esc1.HistoriaEN = "Context: Ancient temple surrounded by waterfalls.\r\nEffect: Gives +4 ATK to Water faction and -2 DEF to others.";
			ListaEscenarios.Add(esc1);

			CartaEscenario esc2 = new CartaEscenario(14, "Fortaleza de Tierra", "Tierra", 50);
			esc2.Historia = "Contexto: Muralla de piedra en las montañas del norte.\r\nEfecto: Da +4 ATK a facción Tierra y -2 DEF al resto.";
			esc2.HistoriaEN = "Context: Stone fortress in the northern mountains.\r\nEffect: Gives +4 ATK to Earth faction and -2 DEF to others.";
			ListaEscenarios.Add(esc2);

			CartaEscenario esc3 = new CartaEscenario(15, "Templo del Viento",   "Viento", 50);
			esc3.Historia = "Contexto: Ruinas altas donde soplan fuertes corrientes de aire.\r\nEfecto: Da +4 ATK a facción Viento y -2 DEF al resto.";
			esc3.HistoriaEN = "Context: High ruins where strong wind currents blow.\r\nEffect: Gives +4 ATK to Wind faction and -2 DEF to others.";
			ListaEscenarios.Add(esc3);

			CartaEscenario esc4 = new CartaEscenario(16, "Volcán de Fuego",     "Fuego",  50);
			esc4.Historia = "Contexto: Cráter ardiente gobernado por el Rey Duende.\r\nEfecto: Da +4 ATK a facción Fuego y -2 DEF al resto.";
			esc4.HistoriaEN = "Context: Burning crater ruled by the Goblin King.\r\nEffect: Gives +4 ATK to Fire faction and -2 DEF to others.";
			ListaEscenarios.Add(esc4);
		}

		// Carga 5 personajes por defecto en las listas de cada jugador usando .Add()
		public void CargarMazosIniciales()
		{
			MazoGuardadoJ1.Clear();
			MazoGuardadoJ2.Clear();

			for (int i = 0; i < 5; i++)
			{
				MazoGuardadoJ1.Add(Catalogo12Personajes[i].Copiar());
				MazoGuardadoJ2.Add(Catalogo12Personajes[i].Copiar());
			}
		}

		// Demuestra el uso explicito de .Remove() sobre una Lista (List<Personaje>)
		public bool QuitarPersonajeDeMazo(List<Personaje> mazo, int indice)
		{
			if (indice >= 0 && indice < mazo.Count)
			{
				Personaje elegido = mazo[indice];
				return mazo.Remove(elegido);
			}
			return false;
		}

		// Pasa los 5 personajes de las Listas a la Cola (Queue) de relevos usando foreach y Enqueue()
		public void PrepararColasCombate(bool contraDuendes)
		{
			ColaJugador1.Clear();
			ColaJugador2.Clear();

			foreach (Personaje p in MazoGuardadoJ1)
			{
				ColaJugador1.Enqueue(p.Copiar());
			}

			if (contraDuendes)
			{
				// Usamos los 5 duendes con sus cartas ilustradas (Nv. 3, 4, 5, 6 y 7)
				ColaJugador2.Enqueue(Catalogo12Personajes[7].Copiar());  // Duende Chamán Nv. 3
				ColaJugador2.Enqueue(Catalogo12Personajes[8].Copiar());  // Duende Cazador Nv. 4
				ColaJugador2.Enqueue(Catalogo12Personajes[9].Copiar());  // Duende Berserker Nv. 5
				ColaJugador2.Enqueue(Catalogo12Personajes[10].Copiar()); // Duende Capitán Nv. 6
				ColaJugador2.Enqueue(Catalogo12Personajes[11].Copiar()); // Jefe Duende Nv. 7
			}
			else
			{
				foreach (Personaje p2 in MazoGuardadoJ2)
				{
					ColaJugador2.Enqueue(p2.Copiar());
				}
			}
		}

		// Guarda el historial de la Pila (Stack) en HistorialBatalla.txt e inserta el cierre destacado del combate
		public void GuardarTxt(string nombreArchivo, string ganador)
		{
			try
			{
				string[] lineas = PilaHistorial.ToArray();
				StreamWriter escritor = new StreamWriter(nombreArchivo, false);
				escritor.WriteLine("=== HISTORIAL DE BATALLA - SANGRE Y DOGMA ===");
				escritor.WriteLine("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
				escritor.WriteLine("---------------------------------------------");

				for (int i = lineas.Length - 1; i >= 0; i--)
				{
					escritor.WriteLine(lineas[i]);
				}

				// Salto de linea y mensaje destacado indicando cuando se acabo el combate y quien gano
				escritor.WriteLine();
				escritor.WriteLine("==================================================");
				escritor.WriteLine(">>> SE ACABÓ EL COMBATE <<<");
				escritor.WriteLine("Fecha y Hora de Fin: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
				escritor.WriteLine("RESULTADO FINAL - GANADOR DE LA PARTIDA: " + ganador);
				escritor.WriteLine("==================================================");

				escritor.Close();
			}
			catch
			{
			}
		}

		// Registra al ganador en PuntajesMaximos.txt usando StreamWriter
		public void RegistrarPuntajeMaximoTxt(string ganador, int puntaje)
		{
			try
			{
				StreamWriter escritor = new StreamWriter("PuntajesMaximos.txt", true);
				escritor.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " | " + ganador + " | Puntaje: " + puntaje + " pts");
				escritor.Close();
			}
			catch
			{
			}
		}

		// Lee el archivo PuntajesMaximos.txt usando StreamReader
		public string LeerPuntajesMaximosTxt()
		{
			if (!File.Exists("PuntajesMaximos.txt"))
			{
				if (MainForm.Idioma == "ES")
					return "Aún no hay puntajes guardados en PuntajesMaximos.txt.";
				else
					return "No scores saved yet in PuntajesMaximos.txt.";
			}

			StreamReader lector = new StreamReader("PuntajesMaximos.txt");
			string texto = lector.ReadToEnd();
			lector.Close();
			return texto;
		}
	}
}
