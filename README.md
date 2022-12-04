# Modelación de sistemas multiagentes con gráficas computacionales

## Integrantes del equipo:

- Alejandro Martinez Luna - A01276785
- Monserrat Karime Moreno Casas - A01276775
- Halim Abraham Hamanoiel Galindo - A01769137

## Instrucciones para la ejecución del proyecto

Debemos de tener tanto Python como Flask instalado, para ello podemos ejecutar el siguiente comando:

```
pip i flask
```

Deberemos de movernos a la carpeta “Server” con el comando
`cd .\Server\`

Dentro de la carpeta deberemos de ejecutar el script de nombre 'server.py' con el comando
` flask --app server.py run`
Una vez que hayamos inicializado el servidor, deberemos de tener un mensaje como el siguiente

```* Serving Flask app 'server.py'
 * Debug mode: off
WARNING: This is a development server. Do not use it in a production deployment. Use a production WSGI server instead.
 * Running on http://127.0.0.1:5000
Press CTRL+C to quit
```

Al dirigirnos a la dirección [http://127.0.0.1:5000](http://127.0.0.1:5000). nos deberá desplegar un mensaje de que el servidor se encuentra arriba y listo

## Unity

Una vez que hayamos inicializado el servidor, deberemos de dirigirnos a nuestro proyecto en Unity
Nos aseguraremos de estar en la escena con el Nombre "Proyecto"
![Unity1](/Server/images/Unity1.png)

En el apartado Hierarchy está un objeto llamado “Managers” donde estarán los dos scripts, uno con la solución Óptima y otra con la solución No óptima.

![Unity2](/Server/images/Unity2.png)

Si queremos ejecutar una, basta con seleccionarla y activarla en la sección del inspector

![Unity3](/Server/images/Unity3.png)

Y finalmente para correrla en Unity basta con darle en el botón Play

![Unity4](/Server/images/Unity4.png)
