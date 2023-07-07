SELECT Cinemas.[Name], Sum(Ticket.Price) as [Profit]
FROM (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on	Hall.Id_hall = [Session].Hall) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]) INNER JOIN Time_Table on [Session].Id_session = Time_Table.[Session]
GROUP BY Cinemas.[Name]