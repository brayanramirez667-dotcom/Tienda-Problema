  # Sistema de Clasificación de Pedidos

## Integrantes del Desarrollo de la Tienda
- Brayan Stic Ramirez Posada
- Luis Miguel Cortes Sena

## 📌 Descripción
Programa en C# que clasifica un pedido según:
- Monto del pedido
- Tipo de cliente
- Cantidad de ítems
- Ciudad destino

Determina:
- Categoría de despacho
- Costo de envío
- Mensaje final al cliente

---

# 📥 IPO

## ENTRADAS
- montoPedido (decimal)
- ciudadDestino (string)
- tipoCliente (string)
- cantidadItems (int)

## PROCESO
1. Si monto >= 150000 y cliente recurrente → Envío Gratis
2. Si items >= 5 o monto >= 300000 → Envío Express
3. En los demás casos → Envío Estándar
4. Si ciudad es "exterior" → costo adicional

## SALIDAS
- Categoría de despacho
- Costo de envío
- Mensaje final

---

# 📊 Tabla de Variables

| Variable           | Tipo     | Descripción |
|--------------------|----------|------------|
| MontoPedido        | decimal  | Valor total del pedido |
| CiudadDestino      | string   | Ciudad de envío |
| TipoCliente        | string   | Nuevo o recurrente |
| CantidadItems      | int      | Número de productos |
| CategoriaDespacho  | string   | Tipo de envío |
| CostoEnvio         | decimal  | Valor final del envío |

---

# 🧪 Casos de Prueba

## ✅ Caso 1 – Normal

Entrada:
- Monto: 200000
- Cliente: recurrente
- Items: 2
- Ciudad: Medellín

Resultado esperado:
- Categoría: Envío Gratis
- Costo: $0

Explicación:
Cumple la condición de monto >= 150000 y cliente recurrente.

---

## ⚠️ Caso 2 – Caso diferente (reglas cruzadas)

Entrada:
- Monto: 320000
- Cliente: nuevo
- Items: 6
- Ciudad: exterior

Resultado esperado:
- Categoría: Envío Express
- Costo: 20000 + 15000 = $35000

Explicación:
Cumple condición de express (items >= 5 o monto >= 300000)
Además tiene recargo por ser exterior.
