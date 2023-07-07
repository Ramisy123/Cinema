﻿SELECT Ticket.Place
FROM (Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day]) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]
WHERE (((Films.[Name]) = 'Терминатор') AND ((Cinemas.[Name]) = 'Jazz_Cinema') AND ((Time_Index.[Time]) = '00:00:00'))