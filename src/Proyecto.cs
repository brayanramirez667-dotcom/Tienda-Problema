# Tienda-Problema
Una tienda en línea necesita un programa que, dados los datos de un pedido, determine su categoría de despacho y el costo de envío.
using System;

class Program
{
    static void Main()
    {
        // VARIABLES
        decimal montoPedido;
        string ciudadDestino;
        string tipoCliente;
        int cantidadItems;

        string categoriaDespacho = "";
        decimal costoEnvio = 0;

        // ENTRADAS
        Console.WriteLine("Ingrese el monto del pedido:");
        montoPedido = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Ingrese la ciudad destino:");
        ciudadDestino = Console.ReadLine().ToLower();

        Console.WriteLine("Tipo de cliente (nuevo / recurrente):");
        tipoCliente = Console.ReadLine().ToLower();

        Console.WriteLine("Cantidad de items:");
        cantidadItems = int.Parse(Console.ReadLine());

        // REGLAS DE DESPACHO

        // 1️⃣ Envío gratis
        if (montoPedido >= 150000 && tipoCliente == "recurrente")
        {
            categoriaDespacho = "Envío Gratis";
            costoEnvio = 0;
        }
        // 2️⃣ Envío express
        else if (cantidadItems >= 5 || montoPedido >= 300000)
        {
            categoriaDespacho = "Envío Express";
            costoEnvio = 20000;
        }
        // 3️⃣ Envío estándar
        else
        {
            categoriaDespacho = "Envío Estándar";
            costoEnvio = 10000;
        }

        // Costo adicional si es exterior
        if (ciudadDestino == "exterior")
        {
            costoEnvio += 15000;
        }

        // SALIDA
        Console.WriteLine("\n----- RESULTADO -----");
        Console.WriteLine("Categoría: " + categoriaDespacho);
        Console.WriteLine("Costo de envío: $" + costoEnvio);
        Console.WriteLine("Gracias por su compra!");

        Console.ReadKey();
    }
}
