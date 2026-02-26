# Tienda-Problema
Una tienda en línea necesita un programa que, dados los datos de un pedido, determine su categoría de despacho y el costo de envío.
using System;

class Program
{
    enum TipoCliente
    {
        Nuevo,
        Recurrente
    }

    enum CategoriaDespacho
    {
        EnvioGratis,
        EnvioExpress,
        EnvioEstandar
    }

    static void Main()
    {
        Console.Title = "Sistema de Despachos - Tienda Online";

        decimal montoPedido = LeerDecimal("Ingrese el monto del pedido:");
        string ciudadDestino = LeerTexto("Ingrese la ciudad destino (interior/exterior):");
        TipoCliente tipoCliente = LeerTipoCliente();
        int cantidadItems = LeerEntero("Cantidad de items:");

        CategoriaDespacho categoria = CalcularCategoria(montoPedido, tipoCliente, cantidadItems);
        decimal costoEnvio = CalcularCosto(categoria, ciudadDestino);

        MostrarResultado(categoria, costoEnvio);

        Console.ReadKey();
    }

    static decimal LeerDecimal(string mensaje)
    {
        decimal valor;
        while (true)
        {
            Console.WriteLine(mensaje);
            if (decimal.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                return valor;

            Console.WriteLine("⚠️ Ingrese un monto válido.");
        }
    }

    static int LeerEntero(string mensaje)
    {
        int valor;
        while (true)
        {
            Console.WriteLine(mensaje);
            if (int.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                return valor;

            Console.WriteLine("⚠️ Ingrese un número válido.");
        }
    }

    static string LeerTexto(string mensaje)
    {
        Console.WriteLine(mensaje);
        return Console.ReadLine().Trim().ToLower();
    }

    static TipoCliente LeerTipoCliente()
    {
        while (true)
        {
            Console.WriteLine("Tipo de cliente (nuevo / recurrente):");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "nuevo")
                return TipoCliente.Nuevo;

            if (input == "recurrente")
                return TipoCliente.Recurrente;

            Console.WriteLine("⚠️ Ingrese 'nuevo' o 'recurrente'.");
        }
    }

    static CategoriaDespacho CalcularCategoria(decimal monto, TipoCliente cliente, int items)
    {
        if (monto >= 150000 && cliente == TipoCliente.Recurrente)
            return CategoriaDespacho.EnvioGratis;

        if (items >= 5 || monto >= 300000)
            return CategoriaDespacho.EnvioExpress;

        return CategoriaDespacho.EnvioEstandar;
    }

    static decimal CalcularCosto(CategoriaDespacho categoria, string ciudad)
    {
        decimal costoBase = categoria switch
        {
            CategoriaDespacho.EnvioGratis => 0,
            CategoriaDespacho.EnvioExpress => 20000,
            _ => 10000
        };

        if (ciudad == "exterior")
            costoBase += 15000;

        return costoBase;
    }

    static void MostrarResultado(CategoriaDespacho categoria, decimal costo)
    {
        Console.WriteLine("\n----- RESULTADO -----");
        Console.WriteLine($"Categoría: {FormatearCategoria(categoria)}");
        Console.WriteLine($"Costo de envío: ${costo:N0}");
        Console.WriteLine("Gracias por su compra!");
    }

    static string FormatearCategoria(CategoriaDespacho categoria)
    {
        return categoria switch
        {
            CategoriaDespacho.EnvioGratis => "Envío Gratis",
            CategoriaDespacho.EnvioExpress => "Envío Express",
            _ => "Envío Estándar"
        };
    }
}
