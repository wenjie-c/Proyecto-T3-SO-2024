drop database if exists T3;
create database T3;
use T3;
create table Chat (id integer, mensajes text, fecha datetime, primary key(id));
create table Partida (id integer AUTO_INCREMENT, chat_id integer, FOREIGN KEY(chat_id) REFERENCES Chat(id),primary key(id));
CREATE TABLE Jugador(id INT AUTO_INCREMENT,Nombre VARCHAR(20),pass VARCHAR(20),PRIMARY KEY(id));
create table Nucleo (id_j integer, id_p integer,FOREIGN KEY(id_j) REFERENCES Jugador(id), FOREIGN KEY(id_p) REFERENCES Partida(id));
