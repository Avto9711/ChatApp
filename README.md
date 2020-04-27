

# ChatApp

This application  allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

##### Features

- [x] Allow registered users to log in and talk with other users in a chatroom.

- [x] Allow users to post messages as commands into the chatroom with the following format /stock=stock_code


- [x] Create a decoupled bot that will call an API using the stock_code as a parameter.

- [x] The bot should parse the received CSV file and then it should send a message back into the chatroom using a message broker like RabbitMQ. The message will be a stock quote using the following format: “APPL.US quote is $93.42 per share”. The post owner will be the bot.


- [x] Have the chat messages ordered by their timestamps and show only the last 50 messages.

- [x] Unit test the functionality you prefer.


- [x] Use .NET identity for users authentication (and authorizations)

- [x] Handle messages that are not understood or any exceptions raised within the bot.

##### Technologies
* :radio_button: ASP .Net Core
* :radio_button: SignalR
* :radio_button: ASP .Net Core Identity
* :radio_button: AutoMapper
* :radio_button: Serilog
* :radio_button: NService Bus (Like Rabbit MQ :rabbit:)


## Installation

* Install Nuget Packages.
* Update ChatApp.Api and ChatApp.bus projects 'DefaultConnection' and 'NServiceBusConnectionString' connection strings in appsetting.json to your local instance of SQL server. Note: Use the same Database in both connection string.

* Set up ChatApp.Api as startup project and open 'Package Manager Console'. Put ChatApp.Model as Default Project within 'Package Manager Console'. 
* Run 
``` bash
Update-Database 
``` 
Command to create the database.

* Once database is created,  set ChatApp.Api and ChatApp.Bus as startup projects

* In case you need, change the Clients URL in the ChatApp.Api appsetting.json if the client app has a different url (http://localhost:8080)
* Run the App :smile:


