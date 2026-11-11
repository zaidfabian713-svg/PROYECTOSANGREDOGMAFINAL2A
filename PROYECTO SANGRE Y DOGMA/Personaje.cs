using System;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// CLASE PADRE: Personaje (Modelo de Datos con Encapsulamiento)
	// Nota para el profesor: Siguiendo la correccion de arquitectura, esta clase solo contiene
	// los atributos privados, sus propiedades encapsuladas (get y set), constructores y clonacion.
	// Toda la logica matematica de dano y limites se separo en la clase CalculadoraCombate.cs.
	public class Personaje
	{
		private int id;
		private string nombre;
		private string elemento; // Agua, Tierra, Viento, Fuego
		private int hp;
		private int hpMax;
		private int def;
		private int atk;
		private int ener;
		private int esp;
		private int emp;
		private string historia;
		private string historiaEN;

		// Propiedades publicas (get y set) para encapsular los atributos privados
		public int Id { get { return id; } set { id = value; } }
		public string Nombre { get { return nombre; } set { nombre = value; } }
		public string Elemento { get { return elemento; } set { elemento = value; } }
		public int HP { get { return hp; } set { hp = value; } }
		public int HPMax { get { return hpMax; } set { hpMax = value; } }
		public int DEF { get { return def; } set { def = value; } }
		public int ATK { get { return atk; } set { atk = value; } }
		public int ENER { get { return ener; } set { ener = value; } }
		public int ESP { get { return esp; } set { esp = value; } }
		public int EMP { get { return emp; } set { emp = value; } }
		public string Historia { get { return historia; } set { historia = value; } }
		public string HistoriaEN { get { return historiaEN; } set { historiaEN = value; } }

		// Constructor: todos los personajes del catalogo cumplen que HP + DEF + ATK = 100
		public Personaje(int _id, string _nombre, string _elemento, int _hp, int _def, int _atk, int _emp)
		{
			Id = _id;
			Nombre = _nombre;
			Elemento = _elemento;
			HP = _hp;
			HPMax = _hp;
			DEF = _def;
			ATK = _atk;
			ENER = 2; // Inicia con 2 puntos de energia
			ESP = 0;  // Inicia en 0 y se llena en 2 turnos
			EMP = _emp;
			Historia = "Contexto: Combatiente registrado en la Arena Elemental.\r\nMotivo: Defender a su facción.";
			HistoriaEN = "Context: Fighter registered in the Elemental Arena.\r\nMotive: Defend their faction.";
		}

		// Metodo polimorfico para identificar el tipo de carta en las clases hijas
		public virtual string ObtenerTipoCarta()
		{
			return "Personaje Base";
		}

		public virtual Personaje Copiar()
		{
			Personaje copia = new Personaje(Id, Nombre, Elemento, HPMax, DEF, ATK, EMP);
			copia.Historia = Historia;
			copia.HistoriaEN = HistoriaEN;
			return copia;
		}
	}

	// CLASE HIJA 1: Protagonista hereda de Personaje
	public class Protagonista : Personaje
	{
		private string rol;
		public string Rol { get { return rol; } set { rol = value; } }

		public Protagonista(int id, string nombre, string elemento, int hp, int def, int atk, int emp, string _rol)
			: base(id, nombre, elemento, hp, def, atk, emp)
		{
			Rol = _rol;
		}

		public override string ObtenerTipoCarta()
		{
			return "Protagonista (" + Rol + ")";
		}

		public override Personaje Copiar()
		{
			Protagonista copia = new Protagonista(Id, Nombre, Elemento, HPMax, DEF, ATK, EMP, Rol);
			copia.Historia = Historia;
			copia.HistoriaEN = HistoriaEN;
			return copia;
		}
	}

	// CLASE HIJA 2: Duende hereda de Personaje
	public class Duende : Personaje
	{
		private int nivel;
		public int Nivel { get { return nivel; } set { nivel = value; } }

		public Duende(int id, string nombre, string elemento, int hp, int def, int atk, int emp, int _nivel)
			: base(id, nombre, elemento, hp, def, atk, emp)
		{
			Nivel = _nivel;
		}

		public override string ObtenerTipoCarta()
		{
			return "Duende (Nv. " + Nivel + ")";
		}

		public override Personaje Copiar()
		{
			Duende copia = new Duende(Id, Nombre, Elemento, HPMax, DEF, ATK, EMP, Nivel);
			copia.Historia = Historia;
			copia.HistoriaEN = HistoriaEN;
			return copia;
		}
	}

	// Modelo de datos para las 4 Cartas de Escenario
	public class CartaEscenario
	{
		private int id;
		private string nombre;
		private string elemento;
		private int umbralEMP;
		private string historia;
		private string historiaEN;

		public int Id { get { return id; } set { id = value; } }
		public string Nombre { get { return nombre; } set { nombre = value; } }
		public string Elemento { get { return elemento; } set { elemento = value; } }
		public int UmbralEMP { get { return umbralEMP; } set { umbralEMP = value; } }
		public string Historia { get { return historia; } set { historia = value; } }
		public string HistoriaEN { get { return historiaEN; } set { historiaEN = value; } }

		public CartaEscenario(int _id, string _nombre, string _elemento, int _umbralEmp)
		{
			Id = _id;
			Nombre = _nombre;
			Elemento = _elemento;
			UmbralEMP = _umbralEmp;
			Historia = "Campo de batalla elemental de la facción " + _elemento + ".";
			HistoriaEN = "Elemental battlefield of the " + _elemento + " faction.";
		}
	}
}
