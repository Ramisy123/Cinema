SELECT Ticket.Place
FROM Cinemas INNER JOIN ((Hall INNER JOIN (Films INNER JOIN ((Time_Index INNER JOIN [Session] on Time_Index.Id = [Session].[Time]) INNER JOIN (Day_Index INNER JOIN Time_Table on Day_Index.Id = Time_Table.[Day]) on [Session].Id_session = Time_Table.[Session]) on Films.Id_films = Time_Table.Film) on Hall.Id_hall = [Session].Hall) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]) on Cinemas.Id_cinema = Hall.Cinema
WHERE (((Ticket.[Date]) = '1/8/2021') AND ((Films.[Name]) = 'Terminator') AND ((Hall.Id_hall) = 1) AND ((Cinemas.[Name]) = 'Jazz_Cinema'))
