#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <mysql.h>
#include <pthread.h>


MYSQL * db_cnx; // Mejor declararlo como variable global.ç
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
char consulta [80];

int partidas_ganadas;

typedef struct{
	int socket;
	int id;
	char nombre[20];
	pthread_t thread;
} Conectado;
Conectado lista[100];

typedef struct{
	int id_partida;
	int id_chat;
	Conectado conectados[100];
	int num_jugador;
} partida;

partida partidas[100];

static const partida PARTIDA_VACIA; // Esta variable es util para destruir (vaciar) una partida, segun lo que he mirado en una pregunta de StackOverflow (https://stackoverflow.com/questions/6891720/initialize-reset-struct-to-zero-null) es la mejor manera.



typedef struct{
	int ids[64];
	int num;
} listas; // Lista de partidas de un jugador

// --- Funciones de base de datos ---

int unirse_en_la_partida(int id_partida,Conectado *self,partida *mi_partida){
	int err;
	MYSQL_RES * resultado;
	MYSQL_ROW row;
	//Antes de unirnos en la partida tenemos que verificar si es que pertenecemos en la prtida o no.
	// La comprobacion se implementara más tarde.
	mi_partida->id_partida = id_partida;
	mi_partida->id_chat = 0; // De momento no lo utilizamos.
	//printf("Antes de unirse en la partida\n");
	mi_partida->conectados[0] = *self;
	
	//memcpy((void *)mi_partida->conectados[0],(void*)self,sizeof(partida));
	mi_partida->num_jugador = 1;
	//printf("Despues de unirse en la partida\n");

	return 0;

}

int crear_partida(int id_jugador){
	int err;
	MYSQL_RES * resultado;
	MYSQL_ROW ultimo_id;
	
	pthread_mutex_lock(&mutex);
	// No se si crear quiero añadir el chat teniendo en cuenta de que en futuro lo podriamos quitarlo como base de datos.
	//Creamos una entrada para la tabla de Partida
	err = mysql_query(db_cnx, "INSERT INTO Partida (chat_id) VALUES (NULL);");
	if(err!= 0){
		printf("Error al crear nueva partida: %u &s\n", mysql_errno(db_cnx),mysql_error(db_cnx));
		return -1;
	}
	//Como no nos devuelve el id, el server tiene que buscarlo
	err = mysql_query(db_cnx,"SELECT MAX(id) FROM Partida;");
	if(err!= 0){
		printf("Error al seleccionar el ultimo id: %u &s\n", mysql_errno(db_cnx),mysql_error(db_cnx));
		exit(1);
	}
	resultado = mysql_store_result(db_cnx);
	ultimo_id = mysql_fetch_row(resultado); // Obtenemos la única fila
	
	int id_partida_creada = atoi(ultimo_id[0]);
	char comando[300];
	sprintf(comando,"INSERT INTO Nucleo (id_j,id_p) VALUES (%d,%d);",id_jugador,id_partida_creada); // Hay que crear una relacion entre la partida y el jugador.
	//Creamos una entrada para la tabla de Nucleo
	err = mysql_query(db_cnx, comando);
	if(err!= 0){
		printf("Error al crear el Core: %u &s\n", mysql_errno(db_cnx),mysql_error(db_cnx));
		return -1;
	}
	pthread_mutex_unlock(&mutex);
	printf("Nueva partida creada con id: %d\n",id_partida_creada);
	
	return id_partida_creada;
}

listas listar_partidas(MYSQL * cnx, int id_j){ // Devuelve la lista de ids de partidas.
	MYSQL_RES * resultados;
	MYSQL_ROW row;
	listas res;
	char comando[300];
	sprintf(comando, "SELECT id_p FROM Nucleo WHERE id_j=%d ", id_j); //Obtener todas las partidas del jugador
	pthread_mutex_lock(&mutex);
	int err = mysql_query(cnx, comando);
	
	resultados = mysql_store_result(db_cnx);
	pthread_mutex_unlock(&mutex);
	row = mysql_fetch_row(resultados);
	int num = 0;
	if(row == NULL){
		res.num = 0;
		return res;
	}else{
		while(row != NULL){
			res.ids[num] = atoi(row[0]);
			num++;
			row = mysql_fetch_row(resultados);
		}
	}
	res.num = num;
	return res;
	
}
void Registrarse(char *Nombre[20],char *password[20],MYSQL *db_cnx,int *sock_conn,char respuesta[500];){ 
	char str_query[500];
	int err;
	char respuesta;
	char nombre;
	int contrasenya;
	int sock_cnx;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	strcpy(respuesta,"1/\n");
	int id=0;
	strcpy(str_query,"SELECT MAX(id) FROM juego.Jugador;");
	err=mysql_query (db_cnx, str_query);
	if (err!=0)
	{
		printf ("Error al consultar datos de la base para la id: %u %s \n", mysql_errno(db_cnx), mysql_error(db_cnx));
	}
	
	resultado = mysql_store_result (db_cnx);
	row = mysql_fetch_row (resultado);
	if(row!=NULL){
		id = atoi(row[0]);
		id = id+1;
	}
	
	sprintf(str_query, "INSERT INTO Jugador VALUES ('%d','%s', '%s');",id ,nombre ,contrasenya);
	err=mysql_query (db_cnx, str_query);
	
	if (err!=0)
	{
		printf ("Error al consultar datos de la base %u %s \n",
				mysql_errno(db_cnx), mysql_error(db_cnx));
		
	}
	write (sock_cnx,respuesta, strlen(respuesta));
	printf("salgo del register\n");
}
int login(MYSQL * cnx,char * Nombre, char * password){ // Devuelve el id del primer usuario con ese nombre y esa contraseña
	MYSQL_RES * resultados;
	MYSQL_ROW row;
	char comando[300];
	sprintf(comando, "SELECT id FROM Jugador WHERE Nombre='%s' AND pass='%s'", Nombre, password);
	pthread_mutex_lock(&mutex);
	int err = mysql_query(cnx, comando);
	if(err!= 0){
		printf("Error al logear: %u &s\n", mysql_errno(cnx),mysql_error(cnx));
		exit(1);
	}
	
	resultados = mysql_store_result(cnx);
	pthread_mutex_unlock(&mutex);
	row = mysql_fetch_row(resultados);
	if(row == NULL){
		return -1;
	}
	else{
		return atoi(row[0]);
	}
	
	
	
}

MYSQL * init()
{
	MYSQL * cnx = mysql_init(NULL);
	int err;
	
	if(cnx == NULL){
		printf("Error al crear la conexion: %u &s\n", mysql_errno(cnx),mysql_error(cnx));
		exit(1);
	}
	
	cnx = mysql_real_connect (cnx, "localhost", "root", "mysql", NULL, 0, NULL, 0);
	
	if(cnx == NULL){
		printf("Error al crear la conexion: %u &s\n", mysql_errno(cnx),mysql_error(cnx));
		exit(1);
	}
	
	//mysql_query(cnx, "CREATE DATABASE IF NO EXISTS juego");
	err=mysql_query(cnx, "use juego");
	if(err!= 0){
		printf("Error al crear la tabla: %u &s\n", mysql_errno(cnx),mysql_error(cnx));
		exit(1);
	}
	
	return cnx;
	
}

// --- Fin de funciones de base de datos ---

// --- Funciones del servidor ---

/*
typedef struct{
int * sock_cnx;
MYSQL * db_cnx;
} args;
*/

void Broadcast(Conectado * yo, int id_partida, char * mensaje){ // Reenviar a todas los sockets de la partida excepto la nuestra.
	for(int x = 0;  x < partidas[id_partida].num_jugador; x++){
		if(partidas[id_partida].conectados[x].socket != yo->socket){
			printf("Enviando: %s al socket %d\n", mensaje, partidas[id_partida].conectados[x].socket );
			write(partidas[id_partida].conectados[x].socket, mensaje, strlen(mensaje));
		}
	}
}


void * AtenderCliente(void * temporal){
	int sock_cnx;
	int *s;
	//int  index = *(int *)temporal;
	Conectado * conectado = (Conectado *)temporal;
	//args * argumentos = (args *)arguments;
	sock_cnx = conectado->socket;
	printf("\nSocket: %d", sock_cnx)	;
	int err;	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	partida partida_actual;
	char peticion[512];
	char respuesta[512];
	char str_query[512];	
	int terminar = 0;
	int id=0;
	
	while(terminar == 0){
		int ret = read(sock_cnx,peticion,sizeof(peticion));
		
		peticion[ret] = '\0';
		printf(" %s\n",peticion);
		strcpy(respuesta,peticion); // Copio la peticion para evitar errores cuando llamo la funcion Broadcast y solamente quiero reenviar la peticion de un cliente al resto de clientes.
		
		int tipo;
		char * token;
		token = strtok(peticion,"/");
		tipo = atoi(token);
		char nombre[20];
		switch (tipo)
		{
		case 0: 
			terminar = 1;
			if(partida_actual.id_partida != NULL){ // Comprobar si ya estabamos en una partida.
				if(partidas[partida_actual.id_partida].num_jugador == 1){ // Comprobar si solo quedamos nosotros
					partidas[partida_actual.id_partida] = PARTIDA_VACIA;
				}
				else{
					partidas[partida_actual.id_partida].num_jugador -=1; // Le restamos un jugador.
				}
			}
			break;
		case 1: // Registro
			
			
			strcpy(str_query,"SELECT MAX(id) FROM juego.Jugador;");
			err=mysql_query (db_cnx, str_query);					
			if (err!=0)
			{
				
				printf ("Error al consultar datos de la base para la id: %u %s \n",
						mysql_errno(db_cnx), mysql_error(db_cnx));
				
				
			}
			
			resultado = mysql_store_result (db_cnx);
			row = mysql_fetch_row (resultado);
			//id = mysql_fetch_row (resultado);
			if(row == NULL)
				printf("Vacio");
			else{

				id = atoi(row[0]);
				id = id+1;
			}					
			token = strtok( NULL, "/");
			strcpy (nombre,token);
			token=strtok(NULL,"/");
			char contrasenya[20];
			strcpy(contrasenya,token);			
			printf ("Tipo: %d, Nombre: %s\n", tipo, nombre);			
			printf("Nombre: %s, contrasenya: %s \n ",nombre, contrasenya);
			sprintf(str_query, "INSERT INTO Jugador VALUES ('%d','%s', '%s');",id ,nombre ,contrasenya);
			err=mysql_query (db_cnx, str_query);
			
			
			if (err!=0)
			{
				printf ("Error al consultar datos de la base %u %s \n",
						mysql_errno(db_cnx), mysql_error(db_cnx));
				
			}
			else{
			sprintf(respuesta,"Bienvenido: %s \n",nombre);
			write (sock_cnx,respuesta, strlen(respuesta));
			}
			
			break;
			
		case 2: //Login
			printf("Peticion de login.\n");
			token = strtok(NULL,"/");
			char Nombre[20] = {0};
			strcpy(Nombre,token);
			strcpy(conectado->nombre,token);
			token = strtok(NULL,"/");
			char password[30] = {0};
			strcpy(password,token);
			int id = login(db_cnx,Nombre,password);
			conectado->id = id;
			sprintf(respuesta,"2/%d",id);
			write(sock_cnx,respuesta,strlen(respuesta));
			break;
		case 3: //Listar partidas
			printf("Peticion de listar partidas.\n");
			//token = strtok(NULL,"/");
			//int id_j = atoi(token);
			int id_j = conectado->id;
			listas buffer = listar_partidas(db_cnx,id_j);
			char buffer2[5] = {0};
			sprintf(respuesta,"3/%d",buffer.num);
			for(int i = 0; i < buffer.num; i++){
				
				sprintf(buffer2, "/%d",buffer.ids[i]);
				strcat(respuesta,buffer2);
			}
			printf("%s\n",respuesta);
			write(sock_cnx,respuesta,strlen(respuesta));
			break;
		case 4: // Nueva partida
			printf("Peticion de crear una nueva partida.\n");
			
			token = strtok(peticion,"/");
			int config = atoi(token); //Numero de configuracion.
			
			int id_partida_creada = crear_partida(conectado->id);
			sprintf(respuesta,"4/%d",id_partida_creada);
			printf("Enviando: %s\n",respuesta);
			write(sock_cnx,respuesta,strlen(respuesta));
			break;
		case 5: //Enviar mensajes
			printf("Renvio de mensajes de chat.\n");
			Broadcast(conectado,partida_actual.id_partida, respuesta);
			break;
		case 6: // Unirse en la partida
			printf("Peticion de unir en la partida.\n");
			token = strtok(NULL,"/");

			int res = unirse_en_la_partida(atoi(token),conectado,&partida_actual);

			sprintf(respuesta,"6/%d",res);
			if(res == 0){ // Comprobamos si se ha unido con exito
				printf("El jugado %s con id %d se ha unido en la partida %d\n",conectado->nombre,conectado->id,partida_actual.id_partida);
				partidas[partida_actual.id_partida] = partida_actual; // Creo que es mejor utilizar el id de la partida como indice del array.
			}
			else
			{
				printf("No ha sido posible unirse en la partida.\n");
			}
			sprintf(respuesta,"6/%d",res);
			write(sock_cnx,respuesta,strlen(respuesta));

			break;

			case 7:
			printf("Peticion de unirse en una partida ajena.\n");
			token = strtok(NULL,"/");
			int id_partida = atoi(token);
			pthread_mutex_lock(&mutex);
			if(partidas[id_partida].num_jugador == 0){ // No hay jugador, no te puedes unirte en la partida
			sprintf(respuesta,"7/-1");
			printf("%s",respuesta);
			write(sock_cnx,respuesta,strlen(respuesta));
			pthread_mutex_unlock(&mutex);
			}else{
				// Pedir al jugador principal se invitado.
				
				sprintf(respuesta,"8/%d",conectado->id);
				printf("Preguntado: %s a %d\n", respuesta,partidas[id_partida].conectados[0].id);
				write(partidas[id_partida].conectados[0].socket,respuesta,strlen(respuesta));
				/*
				ret = read(sock_cnx,peticion,sizeof(peticion));
				peticion[ret] = '\0';
				printf("Nos ha llegado la respuesta: %s\n",peticion);
				int decision;
				sscanf(peticion,"8/%d",&decision);
				*/
				/*
				token = strtok(peticion,'/');
				token = strtok(NULL,'/');
				decision = atoi(token);*/
				
				while(consulta[0] == '\0'); // Esperamos, esto literalmente es programacion asincrona.
				int decision;
				sscanf(consulta,"8/%d",&decision);
				consulta[0] = '\0';

				printf(" Ha decidido: %d\n",decision);
				if(decision == 0){ // Aceptado
					printf("El jugador %d ha aceptado que el jugador %d se una a la partida %d\n",respuesta,partidas[id_partida].conectados[0].id,conectado->id,id_partida);
					partidas[id_partida].conectados[partidas[id_partida].num_jugador] = *conectado;
					partidas[id_partida].num_jugador += 1;
					partida_actual = partidas[id_partida];
				}
				pthread_mutex_unlock(&mutex);
				sprintf(respuesta,"7/%d",decision);
				printf("%s\n",respuesta);
				write(sock_cnx,respuesta,strlen(respuesta));
				
				
			}
			break;
			case 8:
			sprintf(consulta,"%s",peticion); //Redirigir la peticion al otro hilo
			break;
			case 9:
			//sprintf(respuesta,"%s",peticion);
			Broadcast(conectado,partida_actual.id_partida, respuesta);
		default:
			break;
		}
	}
	close(sock_cnx);
	
}
// --- Fin de funciones del servidor ---

int main(){
	
	consulta[0] = '\0';
	//MYSQL * 
	db_cnx = init();
	
	// --- Inicialización ---
	int sock_cnx , sock_listen;
	struct sockaddr_in serv_adr;
	
	// --- Fin de inicializacion ---
	
	// --- Abrir Socket ---
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error al crear socket.");
	
	// --- Hacer bind al puerto ---
	memset(&serv_adr, 0 , sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	
	// --- Asociar el socket a cualquier ip de la maquina ---
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// --- Escuchamos en el puerto 9050 ---
	serv_adr.sin_port = htons(9050);
	if(bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf("Error en el bind");
	
	if (listen(sock_listen, 3) < 0) // Establecer la cola de espera de peticiones en tres.
		printf("Error en el listen.");
	
	
	for(int i = 0; i < 100; i++){ // Initializar las lista de partidas
		partidas[i].num_jugador = 0;
	}

	/* Proceso de recoger y  generar las respuestas:
	para ello se toma el modelo II.
	*/
	
	for(int i = 0; i < 5; i++){ // Atiende a 5 clientes a la vez.
		
		printf("Escuchando ...\n");
		sock_cnx = accept(sock_listen, NULL, NULL);
		printf("Conexion establecida!\nProcesando peticion numero %d:",i+1);
		
		//sockets[i] = sock_cnx;
		Conectado temp = {.socket = sock_cnx};
		lista[i] = temp;
		
		pthread_create(&lista[i].thread, NULL, AtenderCliente,&lista[i]);
		
		
	}
	
	for(int i = 0; i < 5 ; i++){
		pthread_join(lista[i].thread,NULL); // Esperamos a que los 5 hilos completen la conexión para apagarse.
	}
	
	close(sock_listen);
	mysql_close(db_cnx);
	return 0;
}
