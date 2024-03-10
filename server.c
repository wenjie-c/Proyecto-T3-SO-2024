#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>
#include <mysql.h>

// --- Funciones de base de datos ---

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

int main(){

    MYSQL * db_cnx = init();

    // --- Inicialización ---
    int sock_cnx , sock_listen, ret;
    struct sockaddr_in serv_adr;
    char peticion[512];
    char respuesta[512];
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
    
    /* Proceso de recoger y  generar las respuestas:
        para ello se toma el modelo II.
        */
    int tipo = 0;
    char * token;
    for(int i = 0; i < 5; i++){

        printf("Escuchando ...\n");
        sock_cnx = accept(sock_listen, NULL, NULL);
        printf("Conexion establecida!\nProcesando peticion numero %d:",i+1);
        ret = read(sock_cnx,peticion,sizeof(peticion));
        peticion[ret] = '\0';
        printf(" %s\n",peticion);

        token = strtok(peticion,"/");
        int tipo = atoi(token);
        
        /* // Eliminar en el Release y si el switch funciona bien.
        if(tipo < 1 || tipo > 6){
            printf("Tipo erroneo: %d",tipo);
            return 1;
        }*/

        switch (tipo)
        {
        case 1: // Registro

            break;
        case 2: //Login
            break;
        case 3: //Listar partidas
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

        close(sock_cnx);

    }

    close(sock_listen);
    return 0;
}