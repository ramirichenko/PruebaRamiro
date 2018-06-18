    Instrucciones para ejecutar la aplicación

	Para el correcto funcionamiento de la aplicación, es necesario:
		-Servidor Windows o Máquina virtual windows o PC con windows.
		-SQL server configurado en el puerto de serie
		-Crendenciales SQL:
			-User: gs
			-Password:test
		-Ejecutar el script que se encuentra en la raiz del proyecto para crear la base de datos y la tabla necesaria          		
		-IIS corriendo con 2 aplicaciones. Una será el Front, corriendo preferiblemente en el puerto 80, aunque no es importante, y la API, corriendo en el puerto 1414
		-Los proyectos se desplegarán cada uno en su carpeta (API y FRONT)
		-Podremos utilizar el Front con la IP del servidor o DNS si está configurado en cualquier navegador, o utilizar directamente la API utilizando el puerto 1414 para interactuar con los datos desde un programa como Postman, o para integrar con otras aplicaciones.


    Diseño

        La aplicación consta de 2 proyectos diferenciados:

            Front, que se encargará de gestionar las Vistas que verá el usuario y de gestionar las llamadas a la API.
            API, encargada de gestionar las peticiones del Front, y de cualquier aplicación que queramos integrar y gestionar la base de datos del inventario.

            Como se pide obligatoriamente una API, y opcionalmente una interfaz web, me pareció correcto utilizar esta estructura y diferenciar la parte de base de datos y API, de la gestión de las vistas.



    Estructura de código 
    
    Api:
        En la carpeta Controllers irán los controllers de la api, en este caso solo tenemos el InventoryItemController
        En la carpeta Jobs irán las tareas programadas que he utilizado para resolver el problema de la expiración de los artículos, que explicaré más adelante.
        En la carpeta Models irán los modelos de nuestra base de datos
        En la carpeta Security irán las clases necesarias para gestionar la seguridad
        En la carpeta Utils irán clases con funciones genéricas, como un Logger personalizado, gestor de notificaciones, etc..
    
    Front (Además de las carpetas que son iguales):
        Content: CSS(personalizados y materialize) y recursos como imágenes.
        Fonts: Fuentes
        Scripts(personalizados , materialize y jquery)
        View: Vistas
    
    Mantenimiento
        Para el tema de mantenimiento he añadido unos pequeños logs precarios en consola en algunos puntos, pero si tuviese más tiempo añadiría Log4Net y utilizaría ficheros o GoogleCloud, además de mejorar el formato de los mismos estructurando la información en JSON para que sea más fácil tratarla.
    
    Seguridad
        Como no he montado ningún sistema de seguridad en Apis, pero he utilizado autenticación por token, he tirado por este camino y he implementado una versión muy precaria en varios sentidos que podría solucionar pero por falta de tiempo no he podido:
            -Principal, tabla (modelos,vistas,funcionalidades), para gestionar usuarios, desde el alta hasta el cambio de contraseña,guardar las contraseñas encriptadas, etc… y como esto no era viable, he optado por generar un token siempre que se use una contraseña por defecto, en este caso “test”. 
            -El Front no tiene la misma seguridad, solo es un simple control de la sesión para simular un login y un logout (junto a la “trampa” mencionada anteriormente), guardando el token en la sesión del usuario para utilizar la api.
            -Crear y gestionar roles, para que aparte de simplemente autorizar o no el acceso a un servicio, podamos filtrar por roles.
            -Información contenida en webconfig como credenciales deberían estar encriptadas.
            -Configurar la aplicación para que funcione con protocolo HTTPS, y el servidor IIS para gestionar las peticiones y los certificados.


    Como no se especifica si varios artículos puedan tener el mismo nombre y luego se pide que se pueda sacar un artículo del inventario, he hecho que si hay varios, saque el primero que esté disponible.


    Problemas:
	   El mayor problema de los 4 apartados, fue el tema de la expiración, y lo mejor que se me ocurrió fue utilizar Quartz .NET, un gestor de tareas programadas que ya he utilizado mucho para que todas las noches a las 12, se inicie un proceso que notifique de todos los elementos que caducan ese día. 
       Di por hecho que un producto caduca un dia en concreto sin importar la hora y el minuto, por lo que la idea de notificar a las 12 no me pareció tan mala.
    
    
    
    Cosas que mejoraría con tiempo:
    
    En el apartado de test, solo he añadido los pocos que me dio tiempo. Hay 2 proyectos de test, uno para cada proyecto web.    
    Mensajes en el Front, ser preciso en el tipo de error (el elemento no existe, el usuario no existe, contraseña incorrecta ...etc), también requiere una mejora en las respuestas de la Api.    
    El control del Login, sesión y seguridad, mencionado anteriormente    
    Mejorar notificaciones, como mínimo poner un servidor smtp si se quiere Email, y yo añadiría cualquier aplicación que el cliente ya utilice (si quiere) como Telegram, Slack, Jira… etc.
    Cambiar las rutas http del código (como los localhost:1414) a web.config por ejemplo    
    Mover todo el css y js que quede en las vistas los ficheros css y js correspondientes.
    Añadir ajax para una mejor experiencia del usuario.