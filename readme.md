# Sistema de Clasificación de Pedidos

## Integrantes del Desarrollo de la Tienda

| Nombre |
|--------|
| Brayan Stic Ramirez Posada |
| Luis Miguel Cortés Sena |

---

## Descripción

Sistema desarrollado en C# que permite registrar, clasificar y gestionar pedidos de una tienda en línea mediante una aplicación de consola.

El programa evalúa cada pedido según diferentes criterios y determina automáticamente el tipo de envío y su costo.

---

## Características del Sistema

| Funcionalidad | Descripción |
|--------------|------------|
| Registro de pedidos | Permite ingresar nuevos pedidos desde consola |
| Clasificación automática | Determina el tipo de envío según reglas |
| Cálculo de costos | Calcula el costo final del envío |
| Historial | Guarda los pedidos realizados en memoria |
| Validación | Controla errores en la entrada de datos |
| Menú interactivo | Navegación mediante opciones |

---

## Menú del Sistema

| Opción | Acción |
|------|--------|
| 1 | Registrar nuevo pedido |
| 2 | Ver historial de pedidos |
| 3 | Salir |

---

## Entradas del Sistema

| Variable | Tipo | Descripción |
|---------|------|------------|
| montoPedido | decimal | Valor total del pedido |
| ciudadDestino | numérico | 0 = interior, 1 = exterior |
| tipoCliente | cadena | Nuevo o recurrente/frecuente |
| cantidadItems | entero | Número de productos |

---

## Lógica del Proceso

| Condición | Resultado |
|----------|----------|
| monto >= 150000 y cliente recurrente | Envío Gratis |
| artículos >= 5 o monto >= 300000 | Envío Express |
| Otros casos | Envío Estándar |
| destino exterior | Se suma costo adicional |

---

## Costos de Envío

| Tipo de Envío | Costo |
|--------------|------|
| Envío Gratis | $0 |
| Envío Express | $20.000 |
| Envío Estándar | $10.000 |
| Recargo exterior | + $15.000 |

---

## Salidas del Sistema

| Resultado | Descripción |
|----------|------------|
| Categoría de despacho | Tipo de envío asignado |
| Costo de envío | Valor total a pagar |
| Mensaje final | Información para el cliente |

---

## Historial de Pedidos

El sistema almacena cada pedido en memoria mostrando:

| Campo | Descripción |
|------|------------|
| Tipo de envío | Categoría asignada |
| Costo | Valor final del envío |
| Hora | Momento del registro |

---

## Estructura del Programa

| Componente | Descripción |
|-----------|------------|
| Enum | Tipo de cliente y zona |
| Clase Pedido | Almacena datos del pedido |
| Métodos | Procesos de cálculo y validación |
| List<Pedido> | Almacena historial |

---

## Validaciones Implementadas

| Tipo de Validación | Descripción |
|------------------|------------|
| Decimal | Solo valores positivos |
| Entero | Solo números positivos |
| Zona | Solo 0 o 1 |
| Cliente | Solo valores válidos |

---

## Casos de Prueba

### Caso 1 – Normal

| Entrada | Valor |
|--------|------|
| Monto | 200000 |
| Cliente | recurrente |
| Artículos | 2 |
| Ciudad | Medellín |

| Resultado | Valor |
|----------|------|
| Categoría | Envío Gratis |
| Costo | $0 |

**Explicación:**  
Cumple la condición de monto >= 150000 y cliente recurrente.

---

### Caso 2 – Reglas cruzadas

| Entrada | Valor |
|--------|------|
| Monto | 320000 |
| Cliente | nuevo |
| Artículos | 6 |
| Ciudad | exterior |

| Resultado | Valor |
|----------|------|
| Categoría | Envío Express |
| Costo | $35.000 |

**Explicación:**  
Cumple condición de envío express y aplica recargo por destino exterior.

---

## Consideraciones

- El historial se guarda únicamente en memoria  
- Los datos se pierden al cerrar el programa  
- Aplicación de consola  
- Uso de estructuras como List<T>  

---

## Tecnologías Utilizadas

| Tecnología | Uso |
|-----------|-----|
| C# | Lenguaje principal |
| .NET | Plataforma de ejecución |
| Consola | Interfaz del sistema |

---
