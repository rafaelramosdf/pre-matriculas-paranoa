# Sistema de Gestão de Pré Matrículas (SGPM)

O **Sistema de Gestão de Pré Matrículas (SGPM)** é uma aplicação web desenvolvida para auxiliar secretarias regionais de ensino público na organização, controle e gestão de vagas escolares. O sistema permite um monitoramento preciso das vagas ocupadas e disponíveis, proporcionando uma visão geral e eficiente do fluxo de matrículas e transferências entre escolas.

## 📋 Visão Geral

O sistema SGPM foi projetado para:
- Facilitar o controle das matrículas realizadas em cada escola.
- Proporcionar uma gestão integrada das vagas para o planejamento do ano letivo.
- Suportar o gerenciamento de transferências entre escolas e séries, adequando a oferta de vagas de acordo com o fluxo de alunos.

### Tecnologias Utilizadas
- **Backend**: .NET 8 (C#)
- **Frontend**: .NET Blazor WebAssembly
- **Banco de Dados**: SQL Server
- **Implantação na Nuvem**: Azure

## 🛠 Funcionalidades Principais

### Tela de Planejamento do Ano Letivo
A tela de planejamento do ano letivo permite aos usuários:
- Cadastrar e manter registros do planejamento escolar por ano letivo.
- Preencher um formulário para cada escola da regional de ensino, incluindo:
  - Quantidade de matrículas novas (entrando).
  - Quantidade de matrículas saindo (saindo).

Esses dados facilitam o monitoramento das vagas ocupadas e das vagas disponíveis em cada escola, assegurando uma gestão eficaz da oferta e demanda de matrículas.

### Tela de Matrículas Sequenciais
A tela de matrículas sequenciais permite gerenciar o fluxo de alunos entre diferentes escolas, baseado na progressão do aluno para séries que não são atendidas pela escola atual. Esse fluxo de alunos entre escolas de diferentes níveis garante que os alunos possam ser alocados nas escolas adequadas para a continuidade de seus estudos.

### Diagrama de Entidades e Relacionamentos (ERD)
Aqui está um espaço reservado para o diagrama de entidades e relacionamentos, que ilustra a estrutura do banco de dados, incluindo as tabelas principais e as relações entre elas.

![diagrama-entidades-relacionamentos-sgpm](https://github.com/user-attachments/assets/3a3d8362-4f42-4426-851c-dfdae26763a8)


### Casos de Uso
O SGPM inclui casos de uso principais para gerenciamento de matrículas e planejamento de vagas:
1. **Cadastrar e Manter Planejamento do Ano Letivo** - Para registro e controle de vagas disponíveis por escola.
2. **Gerenciar Matrículas Sequenciais** - Para controle do fluxo de alunos entre escolas em razão de mudança de série.

## 🚀 Implantação e Execução

Para rodar o SGPM localmente ou em um ambiente de testes, siga estas instruções:

1. Clone o repositório:
    ```bash
    git clone https://github.com/rafaelramosdf/pre-matriculas-paranoa.git
    ```
2. Configure a conexão com o SQL Server no arquivo `appsettings.json`, utilizando a sua string de conexão.
3. Execute as migrações para inicializar o banco de dados:
    ```bash
    dotnet ef database update
    ```
4. Execute o projeto:
    ```bash
    dotnet run
    ```
5. Acesse o sistema no navegador em `http://localhost:5000`.

## 💻 Estrutura do Projeto

- `PreMatriculasParanoa.Api` - Contém as APIs e lógica de negócios do sistema.
- `PreMatriculasParanoa.Web.Admin` - Interface de usuário desenvolvida em Blazor WebAssembly.
- `PreMatriculasParanoa.Infra` - Estrutura e migrações para o banco de dados SQL Server.

## 🧩 Documentação Adicional

- [API do sistema de gestao de pre matriculas](https://pre-matriculas-paranoa-api.azurewebsites.net/index.html)

## 📝 Contribuições

Contribuições para o desenvolvimento do SGPM são bem-vindas. Sinta-se à vontade para abrir uma *issue* ou enviar uma *pull request*.
