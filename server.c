#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <mysql.h>
#include <pthread.h>


MYSQL * db_cnx; // Mejor declararlo como variable global.ç

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
	int id_jugadores[2];
} partida;

typedef struct{
	int ids[64];
	int num;
} listas; // Lista de partidas de un jugador

// --- Funciones de base de datos ---

listas listar_partidas(MYSQL * cnx, int id_j){ // Devuelve la lista de ids de partidas.
	MYSQL_RES * resultados;
	MYSQL_ROW row;
	pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
	listas res;
	char comando[300];
	sprintf(comando, "SELECT Nucleo.id_p FROM Jugador,Nucleo WHERE Nucleo.id_j=%d ", id_j); //Obtener todas las partidas del jugador
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

int login(MYSQL * cnx,char * Nombre, char * password){ // Devuelve el id del primer usuario con ese nombre y esa contraseña
	MYSQL_RES * resultados;
	MYSQL_ROW row;
	pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
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

void AtenderCliente(void * temporal){
	int sock_cnx;
	int *s;
	int * index = (int *)temporal;
	Conectado * conectado = &lista[*index];
	//args * argumentos = (args *)arguments;
	sock_cnx = conectado->socket;	
	int err;	
	MYSQL_RES *resultado;
	MYSQL_ROW row;	
	char peticion[512];
	char respuesta[512];
	char str_query[512];	
	int terminar = 0;
	int id=0;
	
	while(terminar == 0){
		int ret = read(sock_cnx,peticion,sizeof(peticion));
		
		peticion[ret] = '\0';
		printf(" %s\n",peticion);
		
		int tipo;
		char * token;
		token = strtok(peticion,"/");
		tipo = atoi(token);
		char nombre[20];
		switch (tipo)
		{
		case 0: 
			terminar = 1;
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
			sprintf(respuesta,"%d",id);
			write(sock_cnx,respuesta,strlen(respuesta));
			break;
		case 3: //Listar partidas
			printf("Peticion de listar partidas.\n");
			//token = strtok(NULL,"/");
			//int id_j = atoi(token);
			int id_j = conectado->id;
			listas buffer = listar_partidas(db_cnx,id_j);
			char respuesta[128] = {0};
			char buffer2[5] = {0};
			sprintf(respuesta,"%d",buffer.num);
			for(int i = 0; i < buffer.num; i++){
				
				sprintf(buffer2, "/%d",buffer.ids[i]);
				strcat(respuesta,buffer2);
			}
			write(sock_cnx,respuesta,strlen(respuesta));
			break;
		case 4: // Nueva partida
			break;
		case 5: //Enviar mensajes
			break;
		case 6: // Consultar mensajes
			break;
		default:
			break;
		}
	}
	close(sock_cnx);
	
}
// --- Fin de funciones del servidor ---

int main(){
	
	
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
	
	//int sockets[100];
	//pthread_t thread[100];
	//int sockets[100];
	
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
		
		//pthread_create(&lista[i].thread, NULL, AtenderCliente,&i);
		AtenderCliente(&i);
		
		
	}
	
	for(int i = 0; i < 5 ; i++){
		pthread_join(lista[i].thread,NULL); // Esperamos a que los 5 hilos completen la conexión para apagarse.
	}
	
	close(sock_listen);
	mysql_close(db_cnx);
	return 0;
}