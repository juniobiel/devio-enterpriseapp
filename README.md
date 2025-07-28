# Enterprise App - Treinamento Desenvolvedor.io

Este repositório é destinado ao projeto trabalhado durante o curso Enterprise Applications da plataforma Desenvolvedor.io - Professor Eduardo Pires.

Abordagem de microsserviços e aplicações corporativas.

✅APIs REST
✅GraphQL
✅Microsserviços
✅Gateway
✅Back-end For Front-end (BFF)
✅RabbitMQ
✅Middleware (MediatoR)

## Setup do SQL Server via Docker

1. Baixar a imagem do Sql Server

```docker pull mcr.microsoft.com/mssql/server:2022-latest ```

2. Iniciar o container

```docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Admin@123456" -p 1433:1433 --name mssqlserver --hostname mssqlserver -d mcr.microsoft.com/mssql/server:2022-latest```

Para conectar ao SQL Server, precisa utilizar o endereço de localhost: `127.0.0.1` e a senha definida na imagem do container: `Admin@123456`

## Setup do RabbitMQ

1. Baixar e rodar a imagem do RabbitMQ

```docker run -d --hostname rabbit-host --name rabbit-nerdstore -p 15672:15672 -p 5672:5672 rabbitmq:management```

Para conectar ao dashboard do rabbit é necessário utilizar o usuário e senha: `guest`

### Stacks e Tools

1. EasyQ -> operações de filas
