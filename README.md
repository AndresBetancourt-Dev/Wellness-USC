# Wellness-USC

### Integrantes
    - Andrés David Betancourt
    - Kevin Ivan Ulloa Nepas
    - Carolina Narvaéz

Este proyecto consistes en un sistema que nos permita aportar un beneficio para los estudiantes de la Universidad Santiago de Cali y así agilizar rápido sus tramite de matriculación de las Clases de  cultura y deporte en la Universidad Santiago de Cali. En la universidad existen diversos cursos ya sea de deporte o de cultura de acceso libre para los estudiantes, sin embargos los procesos para la inscripción de ellos no están del todo optimizados, por lo general la inscripción hacia estos recursos es en base a llenar un formulario o que toque hacer largas filas y así evitar aglomeración de personas. 

A traves de eso realizaremos un sistema que permita el registro hacia estos cursos de manera virtual, También que se pueda visualizar el catálogo de los cursos disponibles y asi poder registrarse en la clase de estos cursos ya sean de deporte o cultura y elegir el docente y los horarios que desee. 

También contiene información con respecto a tarifas de Gimnasio, Salud USC y el Centro Recreacional.

Nuestra Aplicación Permite
  - Crear Cursos, Manipular, Visualizar, Eliminar
  - Crear Clases en Base a estos Cursos, Manipular, Visualizar, Eliminar
  - Crear Profesores, Manipular, Visualizar, Eliminar
  - Registrarse en las Clases deseadas
  - Registro y Login y Interfaz de Usuario gracias al Scaffolding de .NET
  - Vistas Estáticas Gimnasio, Salud, Centro Recreacional
  - Manejar Roles
  - Manejar Registros en Clases
  - Error 404
  - Errores de Autorización

## Arquitectura

La Arquitectura de la Aplicación es MVC, por sus Siglas Model View Controller, es un patrón de arquitectura que en este caso nos permitió separar la lógica de negocio con la de la interfaz.
En este Caso Nuestros Controladores que manejan que información le entregamos a las vistas están localizados en las carpetas Controllers, estos permiten la interacción entre el Modelo de Datos y La Vista también es el que se encarga de contactar con nuestra base de datos SQLite, Mapear los Datos en Base al Modelo y Retornar esto a nuestra vista, Nuestros Modelos están en las carpetas Models y ViewModels nos permiten definir que Modelos de datos se utilizarán y sus atributos, y Nuestras Vistas están localizadas en la Carpeta Views, Como su nombre lo indica son las vistas de nuestra aplicación y reciben información del Controlador en Base a un Modelo.
Nuestro proyecto utilizando esta arquitectura permitió que podamos desarrollar una aplicación de forma rápida, sencilla y segura, además de las buenas prácticas del uso de este Patrón de Arquitectura Monolítica.

![Arquitectura](https://raw.githubusercontent.com/AndresBetancourt-Dev/Wellness-USC/master/docs/mvc-arquitectura.png)


## Tutoriales Utilizados
https://www.youtube.com/watch?v=TuJd2Ez9i3I&ab_channel=kudvenkat - Para los Roles
https://www.youtube.com/watch?v=QpJvqiHl1Fo&ab_channel=CodAffection - Subida de imagenes

# Muchas Gracias
