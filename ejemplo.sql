use juego;
insert into Jugador (Nombre, pass) values ('Juan','1234');
insert into Chat (id, mensajes, fecha) values (7, "Hola", NOW());
insert into Partida (id, chat_id) values (23, 7);
insert into Nucleo (id_j, id_p) values (1,23);
insert into Jugador (Nombre, pass) values ('Pepe','qwerty');
insert into Nucleo (id_j, id_p) values (2,23);