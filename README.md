## MOVIE MANAGEMENT SYSTEM



Aplicação de consola desenvolvida em C# para a gestão e catálogo de filmes, realizadores e categorias. O projeto foi construído seguindo uma Arquitetura em Camadas (Layers) para garantir a separação de responsabilidades, modularidade e facilidade de manutenção.





##### ESTRUTURA DO PROJETO



A solução está dividida em 4 camadas principais:





* MovieManagement.Domain: Contém as entidades de negócio (Filme, Categoria, Realizador) e as interfaces (contratos de repositório). Esta camada é totalmente isolada e independente de tecnologias de persistência ou interfaces.
* MovieManagement.Data: Responsável pela persistência dos dados. Implementa o armazenamento temporário em memória (`List<T>`) e a integração com a base de dados SQLite.
* MovieManagement.Business: Centraliza todas as regras de negócio, validações (ex: impedir títulos duplicados, validar classificações) e serve de ponte entre a UI e os dados.
* MovieManagementUI: Camada de apresentação (Interface de Consola) que gere a interação com o utilizador, menus e captura de dados.





#### Tecnologias Utilizadas



* C# / .NET 10.0 (Console Application / Class Libraries)
* Microsoft.Data.Sqlite (ADO.NET Provider para SQLite)
* Git \& GitHub para controlo de versões



* Prazos e Entregas
* Data Limite: 8 de Junho de 2026



