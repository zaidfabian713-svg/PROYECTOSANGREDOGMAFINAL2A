using System;
using System.Drawing;
using System.IO;

namespace PROYECTO_SANGRE_Y_DOGMA
{
	// CLASE DE LOGICA MATEMATICA: CalculadoraCombate


	public static class CalculadoraCombate
	{
		// 1. Calcula el dano usando la formula de Calculo (Limites) y el Triangulo Elemental
		public static int CalcularAtaque(Personaje atacante, Personaje defensor)
		{
			// FORMULA DE CALCULO (Limites Matematicos): Dano = (ATK * ATK) / (ATK + DEF)

			double danoCalculo = (double)(atacante.ATK * atacante.ATK) / (double)(atacante.ATK + defensor.DEF);

			// Triangulo de Superioridad Elemental usando condicionales clasicos (if / else if):
		
			if (atacante.Elemento == "Viento" && defensor.Elemento == "Fuego")
			{
				danoCalculo = danoCalculo * 1.5;
			}
			else if (atacante.Elemento == "Fuego" && defensor.Elemento == "Tierra")
			{
				danoCalculo = danoCalculo * 1.5;
			}
			else if (atacante.Elemento == "Tierra" && defensor.Elemento == "Agua")
			{
				danoCalculo = danoCalculo * 1.5;
			}
			else if (atacante.Elemento == "Agua" && defensor.Elemento == "Viento")
			{
				danoCalculo = danoCalculo * 1.5;
			}

			int danoFinal = (int)Math.Round(danoCalculo);
			if (danoFinal < 1) danoFinal = 1;

			// Restamos la vida (HP) al defensor directamente
			defensor.HP = defensor.HP - danoFinal;
			if (defensor.HP < 0) defensor.HP = 0;

			return danoFinal;
		}

		// 2. Ejecuta la Habilidad Especial segun si el objeto es Protagonista o Duende
		public static string EjecutarHabilidadEspecial(Personaje atacante, Personaje defensor)
		{
			if (atacante is Protagonista)
			{
				Protagonista prota = (Protagonista)atacante;

				if (prota.Rol == "Guerrero")
				{
					defensor.HP = defensor.HP - 25;
					if (defensor.HP < 0) defensor.HP = 0;
					return atacante.Nombre + " [Golpe de Hierro: -25 HP]";
				}
				else if (prota.Rol == "Maga")
				{
					int dano = (atacante.ATK * 2) - defensor.DEF;
					if (dano < 12) dano = 12;
					defensor.HP = defensor.HP - dano;
					if (defensor.HP < 0) defensor.HP = 0;

					atacante.HP = atacante.HP + 8;
					if (atacante.HP > atacante.HPMax) atacante.HP = atacante.HPMax;
					return atacante.Nombre + " [Llamarada: -" + dano + " HP, +8 HP]";
				}
				else if (prota.Rol == "Sanador")
				{
					atacante.HP = atacante.HP + 15;
					if (atacante.HP > atacante.HPMax) atacante.HP = atacante.HPMax;
					atacante.DEF = atacante.DEF + 4;

					defensor.HP = defensor.HP - 12;
					if (defensor.HP < 0) defensor.HP = 0;
					return atacante.Nombre + " [Viento Curativo: +15 HP, +4 DEF, -12 HP]";
				}
				else if (prota.Rol == "Alquimista")
				{
					defensor.DEF = defensor.DEF - 6;
					if (defensor.DEF < 1) defensor.DEF = 1;
					atacante.ATK = atacante.ATK + 4;

					defensor.HP = defensor.HP - 16;
					if (defensor.HP < 0) defensor.HP = 0;
					return atacante.Nombre + " [Ácido: -6 DEF rival, +4 ATK, -16 HP]";
				}
				else
				{
					if (defensor.ENER > 0)
					{
						defensor.ENER = defensor.ENER - 1;
						atacante.ENER = atacante.ENER + 1;
					}
					defensor.HP = defensor.HP - 20;
					if (defensor.HP < 0) defensor.HP = 0;
					return atacante.Nombre + " [Fuego Oscuro: roba 1 ENER, -20 HP]";
				}
			}
			else if (atacante is Duende)
			{
				Duende duen = (Duende)atacante;
				int dano = 10 + (duen.Nivel * 3);
				defensor.HP = defensor.HP - dano;
				if (defensor.HP < 0) defensor.HP = 0;
				return atacante.Nombre + " [Ataque Duende Nv." + duen.Nivel + ": -" + dano + " HP]";
			}
			else
			{
				int dano = 15;
				defensor.HP = defensor.HP - dano;
				if (defensor.HP < 0) defensor.HP = 0;
				return atacante.Nombre + " [Especial: -" + dano + " HP]";
			}
		}

		// 3. Calcula el Buff (+4 ATK) o Nerf (-2 DEF) de la Carta de Escenario
		public static string AplicarEscenario(CartaEscenario escenario, Personaje p)
		{
			if (p.Elemento == escenario.Elemento)
			{
				p.ATK = p.ATK + 4;
				return p.Nombre + " (+4 ATK - " + escenario.Nombre + ")";
			}
			else
			{
				p.DEF = p.DEF - 2;
				if (p.DEF < 1) p.DEF = 1;
				return p.Nombre + " (-2 DEF - " + escenario.Nombre + ")";
			}
		}

		// 4. Evalua el evento Terremoto comparando la Empatia (EMP) con el umbral del escenario
		public static string DispararTerremoto(CartaEscenario escenario, Personaje p)
		{
			if (p.EMP >= escenario.UmbralEMP)
			{
				p.HP = p.HP + 10;
				if (p.HP > p.HPMax) p.HP = p.HPMax;
				p.DEF = p.DEF + 4;
				return "Terremoto: " + p.Nombre + " (EMP " + p.EMP + " >= " + escenario.UmbralEMP + ") -> +10 HP, +4 DEF";
			}
			else
			{
				p.HP = p.HP - 8;
				if (p.HP < 0) p.HP = 0;
				return "Terremoto: " + p.Nombre + " (EMP " + p.EMP + " < " + escenario.UmbralEMP + ") -> -8 HP";
			}
		}

		// 5. Busca rutas relativas o absolutas para cargar imagenes sin importar desde donde se ejecute el programa
		public static string BuscarRutaArchivo(string nombreArchivo)
		{
			if (File.Exists(nombreArchivo)) return nombreArchivo;

			string baseDir = AppDomain.CurrentDomain.BaseDirectory;
			string ruta1 = Path.Combine(baseDir, nombreArchivo);
			if (File.Exists(ruta1)) return ruta1;

			string ruta2 = Path.Combine(baseDir, "..\\..\\", nombreArchivo);
			if (File.Exists(ruta2)) return ruta2;

			string ruta3 = Path.Combine(baseDir, "PROYECTO SANGRE Y DOGMA", nombreArchivo);
			if (File.Exists(ruta3)) return ruta3;

			string ruta4 = Path.Combine(Environment.CurrentDirectory, nombreArchivo);
			if (File.Exists(ruta4)) return ruta4;

			string ruta5 = Path.Combine(Environment.CurrentDirectory, "PROYECTO SANGRE Y DOGMA", nombreArchivo);
			if (File.Exists(ruta5)) return ruta5;

			return null;
		}

		// Carga imagenes con FileStream para no bloquear los archivos .jpg en disco
		public static Bitmap CargarImagenSinBloqueo(string ruta)
		{
			try
			{
				if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta)) return null;
				using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
				{
					using (Image temp = Image.FromStream(fs))
					{
						return new Bitmap(temp);
					}
				}
			}
			catch
			{
				return null;
			}
		}

		// Obtiene la imagen de la carta en alta resolucion para mostrarla en el PictureBox
		public static Image ObtenerImagenCartaGrande(Personaje p)
		{
			// Si es uno de los 5 protagonistas, buscamos su carta individual (prota_1.jpg a prota_5.jpg)
			if (p.Id >= 1 && p.Id <= 5)
			{
				string rutaProta = BuscarRutaArchivo("prota_" + p.Id + ".jpg");
				Bitmap bmpProta = CargarImagenSinBloqueo(rutaProta);
				if (bmpProta != null) return bmpProta;
			}

			if (p is Duende)
			{
				Duende duen = (Duende)p;
				string ruta = BuscarRutaArchivo("duende_nv" + duen.Nivel + ".jpg");
				Bitmap bmpDuende = CargarImagenSinBloqueo(ruta);
				if (bmpDuende != null) return bmpDuende;
			}
			return GenerarImagenCarta(p);
		}

		// Obtiene la ilustracion de una Carta de Escenario segun su elemento (escenario_agua.jpg, escenario_tierra.jpg, etc.)
		public static Image ObtenerImagenEscenario(CartaEscenario esc)
		{
			if (esc == null) return null;

			string archivo = null;
			if (esc.Elemento == "Agua") archivo = "escenario_agua.jpg";
			else if (esc.Elemento == "Tierra") archivo = "escenario_tierra.jpg";
			else if (esc.Elemento == "Viento") archivo = "escenario_viento.jpg";
			else if (esc.Elemento == "Fuego") archivo = "escenario_fuego.jpg";

			if (archivo == null) return null;
			return CargarImagenSinBloqueo(BuscarRutaArchivo(archivo));
		}

		// 6. Dibuja visualmente la carta del personaje para mostrarla en el PictureBox durante el combate
		public static Bitmap GenerarImagenCarta(Personaje p)
		{
			Bitmap bmp = new Bitmap(130, 170);
			Graphics g = Graphics.FromImage(bmp);

			// Elegimos el color del marco segun la faccion elemental
			Color colorMarco = Color.FromArgb(180, 60, 50); // Fuego por defecto
			Color colorFondo = Color.FromArgb(255, 240, 235);
			if (p.Elemento == "Agua")
			{
				colorMarco = Color.FromArgb(45, 110, 180);
				colorFondo = Color.FromArgb(235, 245, 255);
			}
			else if (p.Elemento == "Tierra")
			{
				colorMarco = Color.FromArgb(130, 90, 50);
				colorFondo = Color.FromArgb(248, 242, 232);
			}
			else if (p.Elemento == "Viento")
			{
				colorMarco = Color.FromArgb(55, 145, 120);
				colorFondo = Color.FromArgb(235, 250, 245);
			}

			g.Clear(colorFondo);

			// Si es uno de los 5 protagonistas (Fabián, Deibislin, Jesús, Yeferson, Ereu), cargamos su carta completa
			if (p.Id >= 1 && p.Id <= 5)
			{
				string rutaProta = BuscarRutaArchivo("prota_" + p.Id + ".jpg");
				Bitmap imgProta = CargarImagenSinBloqueo(rutaProta);
				if (imgProta != null)
				{
					// Dibujamos la carta completa del protagonista en el PictureBox
					g.DrawImage(imgProta, new Rectangle(0, 0, 130, 170));
					imgProta.Dispose();

					// Franja semitransparente abajo con los atributos en tiempo real
					using (SolidBrush brochaFondoStats = new SolidBrush(Color.FromArgb(210, 20, 20, 20)))
					{
						g.FillRectangle(brochaFondoStats, 0, 148, 130, 22);
					}
					using (Font fuenteStats = new Font("Tahoma", 7.5F, FontStyle.Bold))
					{
						g.DrawString("HP:" + p.HP + "  ATK:" + p.ATK + "  DEF:" + p.DEF, fuenteStats, Brushes.Yellow, 6, 152);
					}
					using (Pen lapizBorde = new Pen(colorMarco, 2))
					{
						g.DrawRectangle(lapizBorde, 0, 0, 129, 169);
					}

					g.Dispose();
					return bmp;
				}
			}

			// Si es un Duende, cargamos su imagen de carta completa (duende_nv1.jpg a duende_nv7.jpg)
			if (p is Duende)
			{
				Duende duen = (Duende)p;
				string rutaDuende = BuscarRutaArchivo("duende_nv" + duen.Nivel + ".jpg");
				Bitmap imgDuende = CargarImagenSinBloqueo(rutaDuende);
				if (imgDuende != null)
				{
					// Dibujamos la carta completa del duende en el PictureBox
					g.DrawImage(imgDuende, new Rectangle(0, 0, 130, 170));
					imgDuende.Dispose();

					// Franja semitransparente abajo con los atributos en tiempo real
					using (SolidBrush brochaFondoStats = new SolidBrush(Color.FromArgb(210, 20, 20, 20)))
					{
						g.FillRectangle(brochaFondoStats, 0, 148, 130, 22);
					}
					using (Font fuenteStats = new Font("Tahoma", 7.5F, FontStyle.Bold))
					{
						g.DrawString("HP:" + p.HP + "  ATK:" + p.ATK + "  DEF:" + p.DEF, fuenteStats, Brushes.Yellow, 6, 152);
					}
					using (Pen lapizBorde = new Pen(colorMarco, 2))
					{
						g.DrawRectangle(lapizBorde, 0, 0, 129, 169);
					}

					g.Dispose();
					return bmp;
				}
			}

			// Intentamos recortar el retrato del personaje desde portada.jpg si es uno de los 5 protagonistas (respaldo)
			bool retratoDibujado = false;
			try
			{
				if (p.Id >= 1 && p.Id <= 5)
				{
					string rutaPortada = BuscarRutaArchivo("portada.jpg");
					Bitmap portada = CargarImagenSinBloqueo(rutaPortada);
					if (portada != null)
					{
						int w = portada.Width;
						int h = portada.Height;

						// En portada.jpg el orden de izquierda a derecha es:
						// 1: Deibislin (Id 2), 2: Yeferson (Id 4), 3: Jesus (Id 3), 4: Ereu (Id 5), 5: Fabian (Id 1)
						int col = 0;
						if (p.Id == 2) col = 0;
						else if (p.Id == 4) col = 1;
						else if (p.Id == 3) col = 2;
						else if (p.Id == 5) col = 3;
						else if (p.Id == 1) col = 4;

						int xOrigen = (int)(w * (0.03 + col * 0.19));
						int yOrigen = (int)(h * 0.14);
						int anchoOrigen = (int)(w * 0.17);
						int altoOrigen = (int)(h * 0.50);

						Rectangle rectDestino = new Rectangle(10, 26, 110, 98);
						Rectangle rectOrigen = new Rectangle(xOrigen, yOrigen, anchoOrigen, altoOrigen);
						g.DrawImage(portada, rectDestino, rectOrigen, GraphicsUnit.Pixel);
						portada.Dispose();
						retratoDibujado = true;
					}
				}
			}
			catch
			{
			}

			// Si es una carta sin imagen propia, dibujamos un marco elemental sencillo
			if (!retratoDibujado)
			{
				using (SolidBrush brochaCaja = new SolidBrush(Color.FromArgb(220, 225, 230)))
				{
					g.FillRectangle(brochaCaja, 10, 26, 110, 98);
				}
				using (SolidBrush brochaCirculo = new SolidBrush(colorMarco))
				{
					g.FillEllipse(brochaCirculo, 38, 38, 54, 54);
				}
				using (Font fuenteIcono = new Font("Tahoma", 11F, FontStyle.Bold))
				{
					string etiqueta = (p is Duende) ? ("NV." + ((Duende)p).Nivel) : p.Elemento.Substring(0, 3).ToUpper();
					g.DrawString(etiqueta, fuenteIcono, Brushes.White, 45, 56);
				}
			}

			// Cabecera con la faccion elemental
			using (SolidBrush brochaMarco = new SolidBrush(colorMarco))
			{
				g.FillRectangle(brochaMarco, 0, 0, 130, 22);
			}
			using (Font fuenteCabecera = new Font("Tahoma", 8.5F, FontStyle.Bold))
			{
				g.DrawString("CARTA: " + p.Elemento.ToUpper(), fuenteCabecera, Brushes.White, 8, 4);
			}

			// Pie de la carta con nombre corto y estadisticas
			using (SolidBrush brochaMarco = new SolidBrush(colorMarco))
			{
				g.FillRectangle(brochaMarco, 0, 128, 130, 42);
			}
			string nombreCorto = p.Nombre;
			if (nombreCorto.Length > 18) nombreCorto = nombreCorto.Substring(0, 18);

			using (Font fuenteNombre = new Font("Tahoma", 7.5F, FontStyle.Bold))
			using (Font fuenteStats = new Font("Tahoma", 7.5F, FontStyle.Regular))
			{
				g.DrawString(nombreCorto, fuenteNombre, Brushes.White, 5, 131);
				g.DrawString("HP:" + p.HP + " ATK:" + p.ATK + " DEF:" + p.DEF, fuenteStats, Brushes.Yellow, 5, 148);
			}

			// Borde exterior de la carta
			using (Pen lapizBorde = new Pen(colorMarco, 3))
			{
				g.DrawRectangle(lapizBorde, 1, 1, 127, 167);
			}
			g.DrawRectangle(Pens.Black, 10, 26, 110, 98);

			g.Dispose();
			return bmp;
		}
	}
}
