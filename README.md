# Proyecto-T3-SO-2024
Proyecto del grupo T3

Version v5-dev generado por Wenjie C.

----
# Protocolo de la aplicación.
```mermaid
sequenceDiagram;
participant Cliente B;
participant Cliente A;

participant Servidor (Shiva);
participant Base de datos (Shiva 2);

Servidor (Shiva) ->> Base de datos (Shiva 2): MYSQL * mysql_real_connect();
alt Conexión con éxito
Base de datos (Shiva 2) ->> Servidor (Shiva): MYSQL * cnx;
else Coneión fallida
Base de datos (Shiva 2) ->> Servidor (Shiva): cnx = NULL;
Note right of Servidor (Shiva): Server.c: return -1;
end;
Note right of Servidor (Shiva): Servidor listo para recibir las peticiones de los clientes.
Note right of Cliente A: Proceso de login;
Cliente A ->> Servidor (Shiva): 2/nombre/contraseña
Servidor (Shiva) ->> Base de datos (Shiva 2): mysql_query();
alt Login con éxito
Base de datos (Shiva 2) ->> Servidor (Shiva): MYSQL_RES * resultado = mysql_store_result();
Servidor (Shiva) -->> Cliente A: 2/id_jugador;
Note right of Cliente A: Todos los mensajes recibidos desde el servidor es asíncrono y es recibido desde otro hilo.
else Login fallido
Servidor (Shiva) -->> Cliente A: 2/-1;
end

Note right of Cliente A: Listar partidas del jugador.
Cliente A ->> Servidor (Shiva): 3/;
Servidor (Shiva) ->> Base de datos (Shiva 2): mysql_query();
Base de datos (Shiva 2) ->> Servidor (Shiva): MYSQL_RES * resultado = mysql_store_result();
Servidor (Shiva) -->> Cliente A: 3/2/7/19;
Note right of Cliente B: El mismo proceso que ha realizado cliente A.
Note right of Cliente A: Proceso de creacion de nueva partida.;
Cliente A ->> Servidor (Shiva): 4/0;
Servidor (Shiva) ->> Base de datos (Shiva 2): mysql_query();
Servidor (Shiva) -->> Cliente A: 4/id_partida;

Note right of Cliente B: Proceso de invitacion de partida;

Cliente B ->> Servidor (Shiva): 7/id_partida
Servidor (Shiva) -->> Cliente A: 8/id_jugador
alt Ha aceptado
Cliente A -->> Servidor (Shiva): 8/0;
else No ha aceptado
Cliente A -->> Servidor (Shiva): 8/-1
end;
Servidor (Shiva) -->> Cliente B: 7/decision tomado por el otro cliente.
Note right of Cliente B: Suponemos que cliente A ha aceptado.

Note right of Cliente B: Se inicia el formulario de Juego en los dos clientes.

par Por cada ciclo de juego
loop Hasta cerrar el formulario de juego.
Cliente A ->> Servidor (Shiva): 9/id:coordenada x coordenada y/mapa en formato JSON;
Servidor (Shiva) -->> Cliente A: 9/id2:coordenada x coordenada y/mapa en formato JSON;
end;
and Por cada ciclo de juego
loop Hasta cerrar el formulario de juego.
Cliente B ->> Servidor (Shiva): 9/id2:coordenada x coordenada y/mapa en formato JSON;
Servidor (Shiva) -->> Cliente B: 9/id:coordenada x coordenada y/mapa en formato JSON;
end;
and Cada vez que se envia un mensaje
loop Hasta cerrar el formulario de juego.
Cliente A ->> Servidor (Shiva): 5/Usuario:Hola!;
Servidor (Shiva) -->> Cliente B : 5/Usuario:Hola;
end;
end;

Note right of Cliente B: Se va de la partida;
Cliente B ->> Servidor (Shiva): 0/;

```

Dependiendo si estamos en el cliente o en el servidor los parámetros de cada comando van a variar.

## Registrar

Empieza con el comando 1.

## Login

Empieza con el comando 2:

### En el cliente

`2/Usuario/Contraseña`

Enviado después de dar click al botón de login.

+ El primer parámetro es el nombre del usuario.
+ El segundo parámetro es la contraseña vinculada al usuario.

### En el server

`2/9`

Respuesta a la petición de login del cliente.

+ El único parámetro indica el id del jugador. Si no existe el usuario o la contraseña es incorrecta entonces devuelve -1.

## Listar partidas

Empieza con el comando 3:

### En el cliente

`3/`

Enviado después de dar click en el botón de Play.

### En el server

`3/2/7/12`

Respuesta a la petición de listar partidas del cliente.

+ El primer parámetro indica la cantidad de partidas que tiene el jugador (o disponible para todos?).
+ Los siguientes parámetro depende del primer parámetro ya que son los ids de las partidas.

## Nueva partida

Empieza con el comando 4:

### En el cliente

`4/0`

Enviado después de dar click en el botón de nueva partida.

+ El primer parámetro indica la configuración de partida, pero no se me ocurre nada de momento.

### En el server

`4/30`

Respuesta a la petición de crear nueva partida.

+ El único parámetro es el id de la partida.

## Enviar mensaje

Empieza con el comando 5:

### En el cliente

`5/Usuario:Hola!`

Enviado después de que el usuario tecle ENTER.

+ El único parámetro es el nombre del jugador seguido por el mensaje y separado por dos puntos.

### En el server

`5/Uusario2:Hola!`

Cuando otro usuario envía un mensaje.

## Unirse en la partida.

Empieza con el comando 6:

### En el cliente

`6/id_partida`

Después de dar click en el botón al seleccionar una partida de la lista.

+ El único parámetro es el id de la partida.

### En el server

`6/0`

+ El primer campo indica si se ha unido a la partida con éxito o no.

## Unirse en una partida creada por otro jugador.

Empieza con el comando 7:

### En el cliente

`7/id_partida`

Después de dar click en aceptar.

+ El primer campo indica a la partida que quieres unirte

### En el server

`7/0`

Después de que el otro usuario haya aceptado.

+ El primer campo indica si el otro usuario ha aceptado o no.

## Notificar que hay un jugador que se quiere unir a tu partida.

Empieza con el comando 8:

### En el server

`8/id_jugador`

Después de recibir la petición de ser invitado de otro jugador.

+ El único campo corresponde al id del otro jugador.

### En el cliente

`8/0`

Después de aceptar o denegar al dialogo.

+ El único campo indica si el jugador ha aceptado (0) o no (-1) 

## Actualización de jugadores en la partida.

Empieza con el comando 9 :

### En el cliente

`9/<id_del_jugador>:<coordenada_X>;<coordenada_Y>/map:<mapa en formato json>`

Por cada ciclo del juego.

+ El parámetro principal es el nombre del jugador, dos puntos y sus coordenadas.
+ El segundo parámetro corresponde a una copia del mapa local en formato json. Aún no esta implementado.

### En el server

`9/<id_del_jugador>:<coordenada_X>;<coordenada_Y>/map:<mapa en formato json>`

Redireccion de las peticiones de los jugadores.

# Configuracion de Shiva

Se utiliza el puerto 50070.
