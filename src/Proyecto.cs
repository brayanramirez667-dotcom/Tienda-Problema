using System;
using System.Collections.Generic;

namespace SistemaEnviosMejorado
{
    enum ClienteTipo { Nuevo, Frecuente }
    enum Zona { Interior, Exterior }

    class Pedido
    {
        public string TipoEnvio { get; set; }
        public decimal Precio { get; set; }
        public DateTime Momento { get; set; }
    }

    class ProgramaPrincipal
    {
        const decimal LIMITE_GRATIS = 150000;
        const decimal LIMITE_EXPRESS = 300000;

        const decimal TARIFA_ESTANDAR = 10000;
        const decimal TARIFA_EXPRESS = 20000;
        const decimal EXTRA_EXTERIOR = 15000;

        static void Main()
        {
            List<Pedido> registros = new List<Pedido>();
            string opcionMenu;

            do
            {
                Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Nuevo envío");
                Console.WriteLine("2. Ver registros");
                Console.WriteLine("3. Salir");
                Console.Write("Opción: ");
                opcionMenu = Console.ReadLine().Trim();

                switch (opcionMenu)
                {
                    case "1":
                        RegistrarPedido(registros);
                        break;

                    case "2":
                        VerRegistros(registros);
                        break;

                    case "3":
                        Console.WriteLine("Programa finalizado.");
                        break;

                    default:
                        Console.WriteLine("Opción incorrecta.");
                        break;
                }

            } while (opcionMenu != "3");
        }

        static void RegistrarPedido(List<Pedido> lista)
        {
            decimal monto = PedirDecimal("Ingrese el monto del pedido:");
            Zona zona = PedirZona();
            ClienteTipo cliente = PedirCliente();
            int items = PedirEntero("Cantidad de productos:");

            string tipoEnvio = DeterminarEnvio(monto, cliente, items);
            decimal costo = CalcularValor(tipoEnvio, zona);

            lista.Add(new Pedido
            {
                TipoEnvio = tipoEnvio,
                Precio = costo,
                Momento = DateTime.Now
            });

            MostrarInfo(tipoEnvio, costo);
        }

        static string DeterminarEnvio(decimal monto, ClienteTipo cliente, int items)
        {
            if (monto >= LIMITE_GRATIS && cliente == ClienteTipo.Frecuente)
                return "Gratis";

            if (items >= 5 || monto >= LIMITE_EXPRESS)
                return "Express";

            return "Estandar";
        }

        static decimal CalcularValor(string tipo, Zona zona)
        {
            decimal valor = tipo switch
            {
                "Gratis" => 0,
                "Express" => TARIFA_EXPRESS,
                _ => TARIFA_ESTANDAR
            };

            if (zona == Zona.Exterior)
                valor += EXTRA_EXTERIOR;

            return valor;
        }

        static decimal PedirDecimal(string msg)
        {
            decimal num;
            bool ok;

            do
            {
                Console.WriteLine(msg);
                ok = decimal.TryParse(Console.ReadLine().Trim(), out num) && num >= 0;
                if (!ok) Console.WriteLine("Valor inválido.");
            } while (!ok);

            return num;
        }

        static int PedirEntero(string msg)
        {
            int num;
            bool ok;

            do
            {
                Console.WriteLine(msg);
                ok = int.TryParse(Console.ReadLine().Trim(), out num) && num >= 0;
                if (!ok) Console.WriteLine("Número inválido.");
            } while (!ok);

            return num;
        }

        static Zona PedirZona()
        {
            while (true)
            {
                Console.WriteLine("Seleccione el destino (0: Interior, 1: Exterior):");
                string entrada = Console.ReadLine().Trim();

                if (entrada == "0")
                    return Zona.Interior;

                if (entrada == "1")
                    return Zona.Exterior;

                Console.WriteLine("Ingrese 0 o 1.");
            }
        }

        static ClienteTipo PedirCliente()
        {
            while (true)
            {
                Console.WriteLine("Cliente (nuevo / frecuente):");
                string entrada = Console.ReadLine().Trim().ToLower();

                if (entrada == "nuevo")
                    return ClienteTipo.Nuevo;

                if (entrada == "frecuente" || entrada == "recurrente")
                    return ClienteTipo.Frecuente;

                Console.WriteLine("Escriba 'nuevo' o 'frecuente'.");
            }
        }

        static void MostrarInfo(string tipo, decimal costo)
        {
            Console.WriteLine("\n--- DETALLE ---");
            Console.WriteLine($"Tipo de envío: {tipo}");
            Console.WriteLine($"Total a pagar: ${costo:N0}");

            if (costo == 0)
                Console.WriteLine("Envio gratis aplicado.");
        }

        static void VerRegistros(List<Pedido> lista)
        {
            Console.WriteLine("\n--- REGISTROS ---");

            if (lista.Count == 0)
            {
                Console.WriteLine("No hay pedidos aún.");
                return;
            }

            foreach (var p in lista)
            {
                Console.WriteLine($"{p.Momento:HH:mm} - {p.TipoEnvio} - ${p.Precio:N0}");
            }
        }
    }
}
