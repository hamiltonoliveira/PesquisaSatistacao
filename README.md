**Projeto Pesquisa de Satisfação - README**

Bem-vindo ao projeto Pesquisa de Satisfação! Este é um sistema desenvolvido utilizando a plataforma .NET 7 e a arquitetura em camadas DDD (Domain-Driven Design). O projeto visa oferecer uma estrutura robusta e escalável para a criação de um sistema completo, com foco na organização e manutenção do código.

## Requisitos

Certifique-se de que você possui as seguintes ferramentas instaladas em seu ambiente de desenvolvimento:

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/get-started)

## Configuração do Banco de Dados

Para criar o banco de dados no SQL Server, siga os passos abaixo:

1. Navegue até a camada `Infra` do projeto.
2. Abra o terminal e execute o comando: `update-database`.

Isso executará as migrações necessárias e criará as tabelas no banco de dados configurado.

## Autenticação e Refresh Token

O sistema utiliza autenticação para garantir a segurança dos usuários. O mecanismo de autenticação é baseado em tokens JWT (JSON Web Tokens) e também inclui um sistema de refresh token para garantir que os usuários permaneçam autenticados.

## Limite Diário de Enquetes

Na camada de serviço, foi implementado um limite de até 20 enquetes diárias por usuário. Isso visa evitar abusos e manter o sistema em um estado equilibrado.

## Interação com o Frontend

Todo o processo de interação entre o frontend e o backend foi projetado de forma a retornar mensagens claras e coerentes. Isso garante uma experiência de usuário mais amigável e eficiente.

## Executando o Backend com Docker Compose

O backend pode ser facilmente executado usando o Docker Compose para gerenciar todos os serviços relacionados. Siga os passos abaixo para executar o sistema:

1. Certifique-se de ter o Docker e o Docker Compose instalados e em execução.
2. Navegue até o diretório raiz do projeto.
3. Abra o terminal e execute o seguinte comando: `docker-compose up --build`

Isso criará e inicializará todos os contêineres necessários para o sistema, incluindo o backend.

Agora o backend está em execução e pode ser acessado em `http://localhost:8080`.

## Conclusão

Este README forneceu uma visão geral do projeto Pesquisa de Satisfação, incluindo a versão do .NET utilizada, a arquitetura em camadas DDD, o processo de criação do banco de dados, os recursos de autenticação e refresh token, o limite diário de enquetes e a disponibilidade do backend em contêineres Docker gerenciados pelo Docker Compose. Sinta-se à vontade para explorar o projeto e contribuir para seu desenvolvimento contínuo.

Caso tenha alguma dúvida ou feedback, não hesite em entrar em contato com nossa equipe.

Divirta-se codificando!

Atenciosamente,
Equipe Hamilton Vale
