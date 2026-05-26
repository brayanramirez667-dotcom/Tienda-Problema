using System;
using System.Collections.Generic;

namespace SistemaEnviosMejorado
{
    // Tipos de clientes aca hacemos que se ejucte mejor que utilizando el string, evita errores y hace el codigo mas seguro.
    enum ClienteTipo
    {
        Nuevo,
        Frecuente
    }

    // Zonas de envío, Definimos las zonas de envio y despues explicamos cual es o donde queda cada zona
    enum Zona
    {
        Interior,
        Exterior
    }

    // Modelo que representa un pedido, aca no imprimimos, ni guardamos solo almacena.
    class Pedido
    {
        public string TipoEnvio { get; set; }

        public decimal Precio { get; set; }

        public DateTime Momento { get; set; }
    }

    // Maneja toda la lógica de envíos como quien envio, cuanto cuesta.
    class ServicioEnvios
    {
        private const decimal LIMITE_GRATIS = 150000;
        private const decimal LIMITE_EXPRESS = 300000;
	// aca aplicamos la constante para que guarde valores fijos, es decir podemos cambiar un valor de 15000 a 20000 sin problemas.
        private const decimal TARIFA_ESTANDAR = 10000;
        private const decimal TARIFA_EXPRESS = 20000;
        private const decimal EXTRA_EXTERIOR = 15000;

        /// <summary>
        /// Calcula el tipo de envío y lo evalua para determinar sus beneficios. 
        /// </summary>
        public string DeterminarTipoEnvio(
            decimal monto,
            ClienteTipo cliente,
            int cantidadProductos
        )
        {
            if (
                monto >= LIMITE_GRATIS &&
                cliente == ClienteTipo.Frecuente
            )
            {
                return "Gratis";
            }

            if (
                cantidadProductos >= 5 ||
                monto >= LIMITE_EXPRESS
            )
            {
                return "Express";
            }

            return "Estandar";
        }

        /// <summary>
        /// Calcula el costo del envío, tomando como valores la zona de envio y el tipo de envio.
        /// </summary>
        public decimal CalcularCostoEnvio(
            string tipoEnvio,
            Zona zona
        )
        {
            decimal costoBase = tipoEnvio switch
            {
                "Gratis" => 0,
                "Express" => TARIFA_EXPRESS,
                _ => TARIFA_ESTANDAR
            };

            if (zona == Zona.Exterior)
            {
                costoBase += EXTRA_EXTERIOR;
            }

            return costoBase;
        }

        /// <summary>
        /// Crea un nuevo pedido y ejecuta de nuevo la misma funcion inicial.
        /// </summary>
        public Pedido CrearPedido(
            string tipoEnvio,
            decimal precio
        )
        {
            return new Pedido
            {
                TipoEnvio = tipoEnvio,
                Precio = precio,
                // Se cambia DateTime.Now por DateTime.UtcNow.AddHours(-5) para corregir la hora del servidor a hora Colombia
                Momento = DateTime.UtcNow.AddHours(-5)
            };
        }
    }

    // Maneja toda la interacción con consola
    class ConsolaUI
    {
        /// <summary>
        /// Muestra el menú principal.
        /// </summary>
        public string MostrarMenu()
        {
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");

            Console.WriteLine("1. Registrar envío");

            Console.WriteLine("2. Ver registros");

            Console.WriteLine("3. Salir");

            Console.Write("Seleccione una opción: ");

            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Solicita el monto del pedido, valida que sea un numero y que no sea negativo.
        /// </summary>
        public decimal SolicitarMonto()
        {
            decimal montoIngresado;

            bool entradaValida;

            do
            {
                Console.Write(
                    "Ingrese el monto del pedido: "
                );

                entradaValida = decimal.TryParse(
                    Console.ReadLine().Trim(),
                    out montoIngresado
                ) && montoIngresado >= 0;

                if (!entradaValida)
                {
                    Console.WriteLine(
                        "Monto inválido."
                    );
                }

            } while (!entradaValida); //Esta funcion obliga al usuraio a ingresar una informacion valida y repetir si no. do-while
            return montoIngresado;
        }

        /// <summary>
        /// Solicita la cantidad de productos.
        /// </summary>
        public int SolicitarCantidadProductos()
        {
            int cantidad;

            bool entradaValida;

            do
            {
                Console.Write(
                    "Cantidad de productos: "
                );

                entradaValida = int.TryParse(
                    Console.ReadLine().Trim(),
                    out cantidad
                ) && cantidad >= 0;

                if (!entradaValida)
                {
                    Console.WriteLine(
                        "Cantidad inválida."
                    );
                }

            } while (!entradaValida);

            return cantidad;
        }

        /// <summary>
        /// Solicita la zona del pedido y pedimos al usuario que ingrese lo que se pide o el programa retorna denuevo la pregunta.
        /// </summary>
        public Zona SolicitarZona()
        {
            while (true)
            {
                Console.WriteLine(
                    "\n--- ZONAS DE ENVÍO ---"
                );

                Console.WriteLine(
                    "0: Interior " +
                    "(Dentro de la ciudad o área metropolitana)"
                );

                Console.WriteLine(
                    "1: Exterior " +
                    "(Otras ciudades, regiones o zonas rurales)"
                );

                Console.WriteLine(
                    "----------------------"
                );

                Console.Write(
                    "Seleccione zona: "
                );

                string entrada =
                    Console.ReadLine().Trim();

                if (entrada == "0")
                {
                    return Zona.Interior;
                }

                if (entrada == "1")
                {
                    return Zona.Exterior;
                }

                Console.WriteLine(
                    "Ingrese solo 0 o 1."
                );
            }
        }

        /// <summary>
        /// Solicita el tipo de cliente.
        /// </summary>
        public ClienteTipo SolicitarTipoCliente()
        {
            while (true)
            {
                Console.WriteLine(
                    "\n--- TIPOS DE CLIENTE ---"
                );

                Console.WriteLine(
                    "0: Nuevo " +
                    "(Cliente que compra por primera vez)"
                );

                Console.WriteLine(
                    "1: Frecuente " +
                    "(Cliente recurrente)"
                );

                Console.WriteLine(
                    "-------------------------"
                );

                Console.Write(
                    "Seleccione tipo de cliente: "
                );

                string entrada =
                    Console.ReadLine().Trim();

                if (entrada == "0")
                {
                    return ClienteTipo.Nuevo;
                }

                if (entrada == "1")
                {
                    return ClienteTipo.Frecuente;
                }

                Console.WriteLine(
                    "Ingrese solo 0 o 1."
                );
            }
        }

        /// <summary>
        /// Muestra el resumen del pedido.
        /// </summary>
        public void MostrarResumenPedido(
            string tipoEnvio,
            decimal costo
        )
        {
            Console.WriteLine(
                "\n===== RESUMEN ====="
            );

            Console.WriteLine(
                $"Tipo de envío: {tipoEnvio}"
            );

            Console.WriteLine(
                $"Costo total: ${costo:N0}"
            );

            if (costo == 0)
            {
                Console.WriteLine(
                    "¡Envío gratis aplicado!"
                );
            }
        }

        /// <summary>
        /// Muestra todos los registros, suma todos los pedidos que se registraron y se muestran.
        /// </summary>
        public void MostrarRegistros(
            List<Pedido> registros
        )
        {
            Console.WriteLine(
                "\n===== REGISTROS ====="
            );

            if (registros.Count == 0)
            {
                Console.WriteLine(
                    "No existen registros."
                );

                return;
            }

            decimal totalRecaudado = 0;

            foreach (Pedido pedido in registros)
            {
                Console.WriteLine(
                    $"{pedido.Momento:HH:mm} | " +
                    $"{pedido.TipoEnvio} | " +
                    $"${pedido.Precio:N0}"
                );

                totalRecaudado += pedido.Precio;
            }

            Console.WriteLine(
                "---------------------"
            );

            Console.WriteLine(
                $"Total pedidos: {registros.Count}"
            );

            Console.WriteLine(
                $"Total recaudado: ${totalRecaudado:N0}"
            );
        }

        /// <summary>
        /// Muestra mensaje de despedida.
        /// </summary>
        public void MostrarDespedida()
        {
            Console.WriteLine(
                "Programa finalizado."
            );
        }

        /// <summary>
        /// Muestra error de opción inválida.
        /// </summary>
        public void MostrarOpcionInvalida()
        {
            Console.WriteLine(
                "Opción inválida."
            );
        }
    }

    // Esta es la clave principal que controla todo el sistema
    class ProgramaPrincipal
    {
        static void Main()
        {
            EjecutarSistema();
        }

        /// <summary>
        /// Controla el flujo principal del sistema, crea objetos, muestra el menu y procesa opciones
        /// </summary>
        static void EjecutarSistema()
        {
            List<Pedido> registrosPedidos =
                new List<Pedido>();

            ConsolaUI interfaz =
                new ConsolaUI();

            ServicioEnvios servicio =
                new ServicioEnvios();

            string opcionSeleccionada;

            do
            {
                opcionSeleccionada =
                    interfaz.MostrarMenu();

                ProcesarOpcion(
                    opcionSeleccionada,
                    registrosPedidos,
                    interfaz,
                    servicio
                );

            } while (opcionSeleccionada != "3");
        }

        /// <summary>
        /// Procesar la opción seleccionada y decide que hace segun lo opcion.
        /// </summary>
        static void ProcesarOpcion(
            string opcion,
            List<Pedido> registros,
            ConsolaUI interfaz,
            ServicioEnvios servicio
        )
        {
            switch (opcion)
            {
                case "1":

                    RegistrarPedido(
                        registros,
                        interfaz,
                        servicio
                    );

                    break;

                case "2":

                    interfaz.MostrarRegistros(
                        registros
                    );

                    break;

                case "3":

                    interfaz.MostrarDespedida();

                    break;

                default:

                    interfaz.MostrarOpcionInvalida();

                    break;
            }
        }

        /// <summary>
        /// Registra un nuevo pedido, pedimos todo lo relacionado con la informacion que se requiere y los calucla, almacena. Todo junto
        /// </summary>
        static void RegistrarPedido(
            List<Pedido> registros,
            ConsolaUI interfaz,
            ServicioEnvios servicio
        )
        {
            decimal monto =
                interfaz.SolicitarMonto();

            Zona zona =
                interfaz.SolicitarZona();

            ClienteTipo cliente =
                interfaz.SolicitarTipoCliente();

            int cantidadProductos =
                interfaz.SolicitarCantidadProductos();

            string tipoEnvio =
                servicio.DeterminarTipoEnvio(
                    monto,
                    cliente,
                    cantidadProductos
                );

            decimal costo =
                servicio.CalcularCostoEnvio(
                    tipoEnvio,
                    zona
                );

            Pedido nuevoPedido =
                servicio.CrearPedido(
                    tipoEnvio,
                    costo
                );

            registros.Add(nuevoPedido);

            interfaz.MostrarResumenPedido(
                tipoEnvio,
                costo
            );
        }
    }
}
