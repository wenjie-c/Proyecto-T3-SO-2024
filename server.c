#include <stdio.h>
#include <string.h>
#include <netinet/in.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <unistd.h>

int main(){
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
       
}