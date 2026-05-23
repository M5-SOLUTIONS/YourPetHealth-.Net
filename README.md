# YourPetHealth - Sistema de Gestão para Clínicas Veterinárias

## Descrição do Projeto

O **YourPetHealth** é um sistema web completo desenvolvido em **ASP.NET MVC (.NET 8)** para gerenciamento de uma clínica veterinária. 

O sistema permite o cadastro e gerenciamento de:
- Responsáveis ("tutores" dos pets)
- Veterinários
- Pets
- Consultas
- Histórico Clínico

O projeto segue o padrão **MVC (Model-View-Controller)**, utiliza **Entity Framework Core** com banco de dados **Oracle** e implementa operações **CRUD** completas.

### Funcionalidades Principais

- **Cadastro de Responsáveis**: Gerenciamento dos tutores dos animais.
- **Cadastro de Veterinários**: Registro dos profissionais da clínica.
- **Cadastro de Pets**: Inclusão de animais com informações como nome, raça, idade, peso, sexo e vínculo com responsável.
- **Agendamento de Consultas**: Criação de consultas vinculadas a um pet e um veterinário.
- **Histórico Clínico**: Registro de procedimentos e acompanhamentos dos pets.
- **Interface amigável** com validações de dados e feedback ao usuário.

---

## Documentação das Funcionalidades (Create)

Como o projeto utiliza o padrão MVC com rotas padrão do ASP.NET, as principais ações de **cadastro (Create)** estão disponíveis nos seguintes controllers:

### 1. PetsController
- **Create (GET)**: Exibe o formulário de cadastro de um novo pet.
- **Create (POST)**: Recebe os dados e realiza o cadastro no banco.
  - Validações: Nome e Sexo obrigatórios.
  - Converte sexo para maiúsculo.
  - Vincula o pet a um responsável.

### 2. ConsultasController
- **Create (GET)**: Exibe formulário com dropdowns de Pets e Veterinários.
- **Create (POST)**: Cadastra nova consulta.
  - Validação: Status deve ser "AGENDADA".
  - Vincula pet e veterinário.

### 3. ResponsaveisController / VeterinariosController
- Possuem ações de Create para cadastro de tutores e veterinários.

Todas as operações de criação utilizam **TempData** para exibir mensagens de sucesso após o redirecionamento.

---

## Instruções de Instalação e Execução

### Pré-requisitos

- **.NET 8 SDK** instalado
- **Oracle Database** (ou Oracle Docker)
- Visual Studio

### Passo a passo

1. **Clone o repositório**
   ```bash
   git clone https://github.com/M5-SOLUTIONS/YourPetHealth-.Net.git
   cd YourPetHealth-.Net

Configure a Connection String
Abra o arquivo appsettings.json e configure a string de conexão com o Oracle:JSON{
 {
  "ConnectionStrings": {
    "OracleConnection": "User Id=Seu_Usuario;Password=Sua_Senha;Data Source=Sua_Conexao;"
  }
}
Atualize o banco de dados
 ```
Bash
dotnet ef database update
 ```
Execute o projeto
 ```
Bash
dotnet run
 ```
Ou pressione F5 no Visual Studio.
Acesse o sistema
Abra o navegador em: https://localhost:xxxx (ou a porta que aparecer no terminal)


Tecnologias Utilizadas

ASP.NET MVC 8
Entity Framework Core
Oracle Database
Razor Views
Bootstrap (frontend básico)
LINQ


Estrutura do Projeto
 ```
YourPetHealth-.Net
├── Controllers/         
├── Models/               
├── Views/               
├── Data/                 
├── wwwroot/              
└── Program.cs            

 ```
