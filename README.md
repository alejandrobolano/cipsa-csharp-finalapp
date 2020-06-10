# Proyecto final del Máster .Net de CIPSA

## Video Club - Version Original

- Práctica final del Máster C#.Net, relacionado a la gestión básica de un video club.
- Es una aplicación **WPF** donde se gestiona Películas y Video Juegos, además se tiene el control de los clientes y la actualización de los mismos convirtiendolos a VIP

## Pantallas

- Clientes :  Se muestra una pantalla con acceso para agregar, modificar y eliminar clientes. Además se podrá filtrar por el estado actual de los clientes.
- Películas :  Se muestra una pantalla con acceso para agregar, modificar y eliminar películas. Además se podrá filtrar por el estado actual de los películas.
- Video juegos :  Se muestra una pantalla con acceso para agregar, modificar y eliminar video juegos. Además se podrá filtrar por el estado actual de los video juegos.
- Rentas :  Se muestra una pantalla con acceso para realizar una renta y devolver un producto. Además se podrá filtrar por el estado actual de las rentas.
- Configuraciones :  Se muestra una pantalla para la selección de los distintos procesos automáticos, pudiendo desconectar y hacer dichos procesos de manera manual dentro de la pantalla principal.
- Pantalla principal : Tendrá acceso para realizar una renta y devolver un producto de manera rápida. Además que cuenta con unos acceso manuales de distintos procesos que se deben realizar respecto a los clientes. Por otra parte, contiene un botón con el resumen actual de las rentas, obteniendo las beneficios que se han ahorrado los clientes con los descuentos

## Tecnología de desarrollo

### Arquitectura

* La programación por capas es una arquitectura cliente-servidor en el que el objetivo primordial es la separación de la lógica de negocios de la lógica de diseño; un ejemplo básico de esto consiste en separar la capa de datos de la capa de presentación al usuario. Esta aplicación tiene una arquitectura *N Capas*.
* El proyecto cuenta con tres capas lógicas: 
    - Presentación
    - Lógica de negocio
    - Repositorio de datos
* A estas capas, se le suma una capa transversal Común, con datos comunes 

### SQLServer

Microsoft SQL Server es un sistema de gestión de base de datos relacional, desarrollado por la empresa Microsoft.

#### EntityFramework

La Entity Framework es un conjunto de tecnologías de ADO.NET que admiten el desarrollo de aplicaciones de software orientadas a datos. Los arquitectos y programadores de aplicaciones orientadas a datos se han enfrentado a la necesidad de lograr dos objetivos muy diferentes.

[EntityFramework Code First](https://www.entityframeworktutorial.net/code-first/what-is-code-first.aspx) usado en el proyecto

#### Patrón repositorio - Unit Of Work

El uso de un repositorio separado para una sola transacción podría generar actualizaciones parciales. 

### MahApps

Un framework que permite a los desarrolladores crear una mejor interfaz de usuario para sus propias aplicaciones WPF con un mínimo esfuerzo. 

[Enlace oficial de MahApps](https://mahapps.com/)

### Log4Net

Log4net es un framework de registro(logs) para la plataforma .NET. Definitivamente no es el único, pero es uno de los marcos más populares que existen. Un framework de logs es una herramienta que puede reducir drásticamente la carga de lidiar con los registros.

[Enlace oficial de log4Net](https://logging.apache.org/log4net/)


### AutoMapper

AutoMapper es un mapeador objeto-objeto. El mapeo objeto-objeto funciona transformando un objeto de entrada de un tipo en un objeto de salida de un tipo diferente. Lo que hace que AutoMapper sea interesante es que proporciona algunas convenciones interesantes para evitar el trabajo sucio de descubrir cómo mapear el tipo A al tipo B. Siempre y cuando el tipo B siga la convención establecida de AutoMapper, se necesita una configuración casi nula para mapear dos tipos.

[Enlace oficial de AutoMapper](https://automapper.org/)
[Documentación oficial de log4Net](https://docs.automapper.org/en/stable/Getting-started.html)


⌨️ con ❤️ por [Alejandro Bolaño](https://github.com/alejandrobolano/) 😊
